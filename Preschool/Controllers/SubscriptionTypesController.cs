using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Preschool.Data;
using Preschool.Extentions;
using Preschool.Models;
using Preschool.Services;
using Preschool.Services.EntitiesServices;

namespace Preschool.Controllers
{
    [Authorize(Roles = ("Admin"))]
    public class SubscriptionTypesController : Controller
    {
        private readonly ISubscriptionTypeService _subscriptionTypeService;

        private readonly IStorgeSubscriptionTypeService _storgeSubscriptionTypeService;

        public SubscriptionTypesController(ISubscriptionTypeService subscriptionTypeService,
                                            IStorgeSubscriptionTypeService storgeSubscriptionTypeService)
        {
            _subscriptionTypeService = subscriptionTypeService;
            _storgeSubscriptionTypeService = storgeSubscriptionTypeService;
        }

        // GET: SubscriptionTypes
        public async Task<IActionResult> Index()
        {
            return View( await Task.Run(()=>
            Conversions.ToSubscriptionTypes(_storgeSubscriptionTypeService.GetSubscriptionTypeEntities())));
        }

        // GET: SubscriptionTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            try
            {
                var subscriptionType =  await Task.Run(() =>
                Conversions.ToSubscriptionType(_storgeSubscriptionTypeService.GetSubscriptionTypeEntityById(id)));

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

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubscriptionType subscriptionType)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => 
                _storgeSubscriptionTypeService.AddSubscriptionTypeToTable(
                                                Conversions.ToSubscriptionTypeEntity(subscriptionType)));
                return RedirectToAction(nameof(Index));
            }
            return View(subscriptionType);
        }

        // GET: SubscriptionTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var subscriptionType = await  _subscriptionTypeService.GetSubscriptionTypeById(id);
            var subscriptionType = await Task.Run(() => 
            Conversions.ToSubscriptionType(_storgeSubscriptionTypeService.GetSubscriptionTypeEntityById(id)));

            if (subscriptionType == null)
            {
                return NotFound();
            }
            return View(subscriptionType);
        }

       
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
                   //await Task.Run(()=> _subscriptionTypeService.UpdateSubscriptionTypeRegistration(subscriptionType));
                   await Task.Run(()=> _storgeSubscriptionTypeService
                                        .UpdateSubscriptionTypeEntity(
                                        Conversions.ToSubscriptionTypeEntity(subscriptionType)));    

                        
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
                var subscriptionType = await Task.Run(() =>
                Conversions.ToSubscriptionType(_storgeSubscriptionTypeService.GetSubscriptionTypeEntityById(id)));
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
                var subscriptionType = await Task.Run(() =>
                Conversions.ToSubscriptionType(_storgeSubscriptionTypeService.GetSubscriptionTypeEntityById(id)));
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
            //return  _subscriptionTypeService.IsExists(id);
            return _storgeSubscriptionTypeService.IsSubscriptionTypeExists(id);
        }
    }
}
