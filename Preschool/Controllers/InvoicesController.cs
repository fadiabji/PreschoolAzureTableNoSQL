using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;
using Preschool.Data;
using Preschool.Models;
using Preschool.Models.ViewModels;
using Preschool.Services;

namespace Preschool.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IChildService _childService;
        private readonly ISubscriptionTypeService _subscriptionTypeService;
        private readonly ApplicationDbContext _db;


        public InvoicesController(ApplicationDbContext db ,IInvoiceService invoiceService, IChildService childService, ISubscriptionTypeService subscriptionTypeService)
        {
            _db = db;
            _invoiceService = invoiceService;
            _childService = childService;
            _subscriptionTypeService = subscriptionTypeService;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = await _invoiceService.GetInvoices();
            foreach(var invoice in applicationDbContext)
            {
                invoice.CalculateTotal = CalculateTotal(invoice);
                invoice.CalculatePayments = CalculatePayments(invoice);
                invoice.CalculateBalance = CalculateBalance(invoice);
            }
            return View(applicationDbContext);
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _invoiceService.GetInvoiceById(id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            ViewData["ChildId"] = new SelectList(_childService.GetChildren().Result, "Id", "FullName");
            ViewData["InvoiceItems"] = new SelectList(_subscriptionTypeService.GetSubscriptionTypes().Result, "Id", "Name");
            return View();
        } 

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InvoiceVM invoiceVm)
        {
            var invoice = new Invoice()
            {
                Id = invoiceVm.Id,
                ChildId = invoiceVm.ChildId,
                Discount = invoiceVm.Discount,
                InvoiceNumber = invoiceVm.InvoiceNumber,
                QRCodeFileAddress = invoiceVm.QRCodeFileAddress,
                LogoFileAddress = invoiceVm.LogoFileAddress,
                InvoiceDate = invoiceVm.InvoiceDate,
            };

            AddInvoiceSubscriptonTypeToInvoice(invoiceVm, invoice);
            if (ModelState.IsValid)
            {
                _invoiceService.AddInvoice(invoice);
                AddPayment(invoiceVm.Payment, invoice);
                //_invoiceService.UpdateInvoice(invoice);
                return RedirectToAction(nameof(Index));
             }

            return View(invoice);
        }

        public  void AddInvoiceSubscriptonTypeToInvoice(InvoiceVM invoiceVm, Invoice invoice)
        {
            var test = invoiceVm.InvoiceItems;
            var test1 = test[1];
            string[] parts = test1.Split(',');
            invoice.InvoiceSubscriptionType = new List<InvoiceSubscriptionType>();
            foreach (string part in parts)
            {
                if (part != "")
                {
                    int subscritpiontypeId = int.Parse(part);
                    
                    var newItem = new InvoiceSubscriptionType()
                    {
                        SubscriptionTypeId = subscritpiontypeId,
                        InvoiceId = invoiceVm.Id
                    };
                    invoice.InvoiceSubscriptionType.Add(newItem);
                }
                
            }
            _db.SaveChanges();
        }
        public async Task<IActionResult>  AddOtherPayment(int id)
        {
            var invoice = await _invoiceService.GetInvoiceById(id);
            invoice.CalculateTotal = CalculateTotal(invoice);
            invoice.CalculatePayments = CalculatePayments(invoice);
            invoice.CalculateBalance = CalculateBalance(invoice);
            return View(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> AddOtherPayment(AddOtherPaymentVM addOtherPaymentVM)
        {
            var invoice = await _invoiceService.GetInvoiceById(addOtherPaymentVM.InvoiceId);
            AddPayment(addOtherPaymentVM.Amount, invoice);
            return RedirectToAction(nameof(Index));
        }

        public void AddPayment(decimal payment, Invoice invoice)
        {
            var addedPayment = new Payment()
            {
                Amount = payment,
                PaymentDate = DateTime.Now,
                InvoiceId = invoice.Id
            };
            _db.Payments.Add(addedPayment);
            invoice.Payments.Add(addedPayment);
            _db.SaveChanges();
        }

        public decimal CalculateSubtotal(Invoice invoice)
        {
            decimal subtotal = 0;
            foreach (var item in invoice.InvoiceSubscriptionType)
            {
                //subtotal += item.Price;
                subtotal += item.SubscriptionType.Price;
            }
            return subtotal;
        }

        public decimal CalculateTotal(Invoice invoice)
        {
            decimal subtotal = CalculateSubtotal(invoice);

            // Apply discount if it exists
            if (invoice.Discount.HasValue && invoice.Discount.Value > 0)
            {
                subtotal -= subtotal * invoice.Discount.Value;
            }

            return subtotal;
        }



        public decimal CalculatePayments(Invoice invoice)
        {
            decimal totalPayments = 0;
            if (invoice.Payments != null)
            {
                foreach (var payment in invoice.Payments)
                {
                    totalPayments += payment.Amount;
                }
            }
            return totalPayments;
        }

        public decimal CalculateBalance(Invoice invoice)
        {
            decimal total = CalculateTotal(invoice);

            // Calculate the sum of payments made towards this invoice
            decimal totalPayments = CalculatePayments(invoice);

            // Calculate the remaining balance
            return total - totalPayments;
        }



        [HttpPost]
        public IActionResult AddInvoiceItem(int invoiceItemId)
        {
            if(invoiceItemId == 0)
            {
                return BadRequest();
            }
            // Create a new InvoiceItem and add it to the database or in-memory collection
            var newInvoiceItem = new SubscriptionType();
            var addedItemName =  _subscriptionTypeService.GetSubscriptionTypeById(invoiceItemId).Result.Name;
            var addedItemPrice = _subscriptionTypeService.GetSubscriptionTypeById(invoiceItemId).Result.Price;
            var addedItemDescription = _subscriptionTypeService.GetSubscriptionTypeById(invoiceItemId).Result.Description;
            // Simulate adding an InvoiceItem and returning it as JSON
            var addedItem = new { Id = invoiceItemId, Name = addedItemName, Price = addedItemPrice, Description = addedItemDescription };
            // You can return the added item's name or a partial view with additional details
            return Json(addedItem);
        }

        public async Task<IActionResult> PrintInvoice(int id)
        {
            var invoice = await _invoiceService.GetInvoiceById(id);
            invoice.CalculateTotal = CalculateTotal(invoice);
            invoice.CalculatePayments = CalculatePayments(invoice);
            invoice.CalculateBalance = CalculateBalance(invoice);
            return View(invoice);
        }

        [HttpPost]
        public IActionResult PrintInvoice(AddOtherPaymentVM addOtherPaymentVM)
        {
            return RedirectToAction(nameof(Index));
        }




        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _invoiceService.GetInvoiceById(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["ChildId"] = new SelectList(_childService.GetChildren().Result, "Id", "FullName", invoice.ChildId);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(()=>_invoiceService.UpdateInvoice(invoice));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChildId"] = new SelectList(_childService.GetChildren().Result, "Id", "FullName", invoice.ChildId);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var invoice = await _invoiceService.GetInvoiceById(id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoices = await Task.Run(()=>_invoiceService.GetInvoices());
            if (invoices == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Invoices'  is null.");
            }
            var invoice =  invoices.ToList().Find(i =>i.Id == id);
            if (invoice != null)
            {
                _invoiceService.RemoveInvoice(invoice);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
          return _invoiceService.IsExists(id);
        }
    }
}
