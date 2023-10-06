using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preschool.Data;
using Preschool.Models;
using Preschool.Services;

namespace Preschool.Controllers
{
    [Authorize(Roles = ("Admin"))]
    public class SubscriptionTypesController : Controller
    {
        private readonly ISubscriptionTypeService _subscriptionTypeService;

        public SubscriptionTypesController(ISubscriptionTypeService subscriptionTypeService)
        {
            _subscriptionTypeService = subscriptionTypeService;
        }

        // GET: SubscriptionTypes
        public async Task<IActionResult> Index()
        {
              return View(await _subscriptionTypeService.GetSubscriptionTypes());
        }

        // GET: SubscriptionTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            try
            {
                var subscriptionType = await  _subscriptionTypeService.GetSubscriptionTypeById(id);
                if (subscriptionType == null)
                {
                    return NotFound();
                }

                return View(subscriptionType);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: SubscriptionTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SubscriptionTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubscriptionType subscriptionType)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(()=> _subscriptionTypeService.AddSubscriptionType(subscriptionType));
                return RedirectToAction(nameof(Index));
            }
            return View(subscriptionType);
        }

        // GET: SubscriptionTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null ||  _subscriptionTypeService.GetSubscriptionTypes() == null)
            {
                return NotFound();
            }

            var subscriptionType = await  _subscriptionTypeService.GetSubscriptionTypeById(id);
            if (subscriptionType == null)
            {
                return NotFound();
            }
            return View(subscriptionType);
        }

        // POST: SubscriptionTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,SubscriptionType subscriptionType)
        {
            if (id != subscriptionType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await Task.Run(()=> _subscriptionTypeService.UpdateSubscriptionTypeRegistration(subscriptionType));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubscriptionTypeExists(subscriptionType.Id))
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
            return View(subscriptionType);
        }

        // GET: SubscriptionTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var subscriptionType = await _subscriptionTypeService.GetSubscriptionTypeById(id);
                if (subscriptionType == null)
                {
                    return NotFound();
                }
                return View(subscriptionType);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: SubscriptionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var subscriptionType = await _subscriptionTypeService.GetSubscriptionTypeById(id);
                if (subscriptionType != null)
                {
                    _subscriptionTypeService.RemoveSubscriptionType(subscriptionType);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool SubscriptionTypeExists(int id)
        {
          return  _subscriptionTypeService.IsExists(id);
        }
    }
}
