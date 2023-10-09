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
using Preschool.Models.ViewModels;
using Preschool.Services;
using Preschool.Services.EntitiesServices;
using SQLitePCL;

namespace Preschool.Controllers
{
    [Authorize(Roles = ("Admin"))]
    public class SubscriptionsController : Controller
    {
       

        private readonly IStorgeSubscriptionService _storgeSubscriptionService;

        private readonly IStorgeChildService _storgeChildService;

        private readonly IStorgeSubscriptionTypeService _storgeSubscriptionTypeService;

        public SubscriptionsController( IStorgeSubscriptionService storgeSubscriptionService,
                                        IStorgeChildService storgeChildService,
                                        IStorgeSubscriptionTypeService storgeSubscriptionTypeService
                                        )
        {
            _storgeSubscriptionService = storgeSubscriptionService;
            _storgeChildService = storgeChildService;
            _storgeSubscriptionTypeService = storgeSubscriptionTypeService;
        }

        // GET: Subscriptions
        public async Task<IActionResult> Index()
        {
            CheckSubscriptionsExpireDateToExpire();
            return View( await Task.Run(()=>Conversions.ToSubscriptions(_storgeSubscriptionService.GetSubscriptionEntities())));
        }


        public void CheckSubscriptionsExpireDateToExpire()
        {
            var subs = Conversions.ToSubscriptions(_storgeSubscriptionService.GetSubscriptionEntities());
            foreach (var sub in subs)
            {
                if (sub.ExpireAt.Date < DateTime.Now.Date)
                {
                    sub.IsActive = false;
                    _storgeSubscriptionService.UpdateSubscriptionEntity(Conversions.ToSubscriptionEntity(sub));
                }
            }
        }

        // GET: Subscriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           
            //var subscription = await _subscriptionService.GetSubscriptionById(id);
            var subscription = await Task.Run(()=> Conversions.ToSubscription(_storgeSubscriptionService.GetSubscriptionEntityById(id)));
            if (subscription == null)
            {
                return NotFound();
            }

            return View(subscription);
        }

        // GET: Subscriptions/Create
        public IActionResult Create()
        {

            List<SubscriptionType> subtypelist = Conversions.ToSubscriptionTypes(_storgeSubscriptionTypeService.GetSubscriptionTypeEntities());

            List<Child> childrenlist = Conversions.ToChildren(_storgeChildService.GetChildEntities()).ToList();
            ViewData["SubscriptionTypeId"] = new SelectList(Conversions
                                                            .ToSubscriptionTypes(_storgeSubscriptionTypeService
                                                            .GetSubscriptionTypeEntities().ToList()), "Id", "Name");

            ViewData["ChildId"] = new SelectList(Conversions.ToChildren(_storgeChildService.GetChildEntities().ToList()).ToList(), "Id", "FullName");

            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Subscription subscription)
        {
            if (IsValidChildId(subscription.ChildId))
            {
                ModelState.AddModelError("ChildId", "This child has already an active subscription.");
            }
            if (ModelState.IsValid)
            {
                subscription.CreatedAt = DateTime.Now.Date;
                
                subscription.ExpireAt = subscription.CreatedAt.AddMonths(
                Conversions.ToSubscriptionType(_storgeSubscriptionTypeService
                                                .GetSubscriptionTypeEntityById(subscription.SubscriptionTypeId))
                                                .DurationMonth);

                subscription.IsActive = true;
                //await Task.Run(() => _subscriptionService.AddSubscription(subscription));
                await Task.Run(() => _storgeSubscriptionService.AddSubscriptionToTable(Conversions.ToSubscriptionEntity(subscription)));
                return RedirectToAction(nameof(Index));
            }
            //ViewData["SubscriptionTypeId"] = new SelectList(await Task.Run(()=>_subscriptionTypeService.GetSubscriptionTypes().Result), "Id", "Name", subscription.SubscriptionTypeId);
            ViewData["SubscriptionTypeId"] = new SelectList(Conversions.ToSubscriptionTypes(_storgeSubscriptionTypeService.GetSubscriptionTypeEntities()), "Id", "Name", subscription.SubscriptionTypeId);

            ViewData["ChildId"] = new SelectList(Conversions.ToChildren(_storgeChildService.GetChildEntities()), "Id", "FullName", subscription.ChildId);
            return View(subscription);
        }

        private bool IsValidChildId(int childId)
        {
            //return _childService.GetChildById(childId).Result.Subscriptions.Any(s => s.IsActive == true);
            return Conversions.ToChild(_storgeChildService.GetChildEntityById(childId)).Subscriptions.Any(s => s.IsActive == true);
        }


        // GET: Subscriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var subscription = await _subscriptionService.GetSubscriptionById(id);
            var subscription = await Task.Run(()=>Conversions.ToSubscription(_storgeSubscriptionService.GetSubscriptionEntityById(id)));
            if (subscription == null)
            {
                return NotFound();
            }
            //ViewData["SubscriptionTypeId"] = new SelectList(_subscriptionTypeService.GetSubscriptionTypes().Result, "Id", "Name", subscription.SubscriptionTypeId);
            ViewData["SubscriptionTypeId"] = new SelectList(Conversions.ToSubscriptionTypes(_storgeSubscriptionTypeService.GetSubscriptionTypeEntities()), "Id", "Name", subscription.SubscriptionTypeId);

            return View(subscription);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Subscription subscription)
        {
            if (id != subscription.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try

                {
                    //subscription.ExpireAt = subscription.CreatedAt.AddMonths(_subscriptionTypeService.GetSubscriptionTypeById(subscription.SubscriptionTypeId).Result.DurationMonth);
                    subscription.ExpireAt = subscription.CreatedAt.AddMonths(Conversions.ToSubscriptionType(
                                                                            _storgeSubscriptionTypeService
                                                                            .GetSubscriptionTypeEntityById(subscription.SubscriptionTypeId))
                                                                            .DurationMonth);
                    //await Task.Run(()=>_subscriptionService.UpdateSubscriptionRegistration(subscription));  
                    await Task.Run(()=>_storgeSubscriptionService.UpdateSubscriptionEntity(Conversions.ToSubscriptionEntity(subscription)));    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubscriptionExists(subscription.Id))
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
            //ViewData["SubscriptionTypeId"] = new SelectList(_subscriptionTypeService.GetSubscriptionTypes().Result, "Id", "Name", subscription.SubscriptionTypeId);
            ViewData["SubscriptionTypeId"] = new SelectList(Conversions.ToSubscriptionTypes(
                                                _storgeSubscriptionTypeService
                                                .GetSubscriptionTypeEntities()), "Id", "Name", subscription.SubscriptionTypeId);

            ViewData["ChildId"] = new SelectList(Conversions.ToChildren(_storgeChildService.GetChildEntities()), "Id", "FullName");
            return View(subscription);
        }

        // GET: Subscriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                //var subscription = await _subscriptionService.GetSubscriptionById(id);
                var subscription = await Task.Run(() => Conversions.ToSubscription(_storgeSubscriptionService.GetSubscriptionEntityById(id)));
                if (subscription == null)
                {
                    return NotFound();
                }

                return View(subscription);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        // POST: Subscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var subscription = await _subscriptionService.GetSubscriptionById(id);
            var subscription = await Task.Run(() => Conversions.ToSubscription(_storgeSubscriptionService.GetSubscriptionEntityById(id)));
            if (subscription != null)
            {
                //await Task.Run(() => _subscriptionService.RemoveSubscription(subscription));
                await Task.Run(()=> _storgeSubscriptionService.DeleteSubscriptionEntity(Conversions.ToSubscriptionEntity(subscription)));
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool SubscriptionExists(int id)
        {
          //return _subscriptionService.IsExists(id);
          return _storgeSubscriptionService.IsSubscriptionExists(id);
        }
    }
}
