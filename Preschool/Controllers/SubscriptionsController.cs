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
using Preschool.Models.ViewModels;
using Preschool.Services;
using SQLitePCL;

namespace Preschool.Controllers
{
    [Authorize(Roles = ("Admin"))]
    public class SubscriptionsController : Controller
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly ISubscriptionTypeService _subscriptionTypeService;
        private readonly IChildService _childService;

        public SubscriptionsController(ISubscriptionService subscriptionService, ISubscriptionTypeService subscriptionTypeService, IChildService childService)
        {
            _subscriptionService = subscriptionService;
            _subscriptionTypeService = subscriptionTypeService;
            _childService = childService;
        }

        // GET: Subscriptions
        public async Task<IActionResult> Index()
        {
            CheckSubscriptionsExpireDateToExpire();
            return View(await _subscriptionService.GetSubscriptions());
        }


        public void CheckSubscriptionsExpireDateToExpire()
        {
            var subs = _subscriptionService.GetSubscriptions().Result.ToList();
            foreach (var sub in subs)
            {
                if (sub.ExpireAt.Date < DateTime.Now.Date)
                {
                    sub.IsActive = false;
                    _subscriptionService.UpdateSubscriptionRegistration(sub);
                }
            }
        }

        // GET: Subscriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           
            var subscription = await _subscriptionService.GetSubscriptionById(id);
            if (subscription == null)
            {
                return NotFound();
            }

            return View(subscription);
        }

        // GET: Subscriptions/Create
        public IActionResult Create()
        {
            ViewData["SubscriptionTypeId"] = new SelectList(_subscriptionTypeService.GetSubscriptionTypes().Result, "Id", "Name");
            ViewData["ChildId"] = new SelectList(_childService.GetChildren().Result, "Id", "FullName");

            return View();
        }

        // POST: Subscriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                subscription.ExpireAt = subscription.CreatedAt.AddMonths(await Task.Run(() => _subscriptionTypeService.GetSubscriptionTypeById(subscription.SubscriptionTypeId).Result.DurationMonth));
                subscription.IsActive = true;
                await Task.Run(() => _subscriptionService.AddSubscription(subscription));
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubscriptionTypeId"] = new SelectList(await Task.Run(()=>_subscriptionTypeService.GetSubscriptionTypes().Result), "Id", "Name", subscription.SubscriptionTypeId);
            ViewData["ChildId"] = new SelectList(_childService.GetChildren().Result, "Id", "FullName", subscription.ChildId);
            return View(subscription);
        }

        private bool IsValidChildId(int childId)
        {
            return _childService.GetChildById(childId).Result.Subscriptions.Any(s => s.IsActive == true);
        }


        // GET: Subscriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = await _subscriptionService.GetSubscriptionById(id);
            if (subscription == null)
            {
                return NotFound();
            }
            ViewData["SubscriptionTypeId"] = new SelectList(_subscriptionTypeService.GetSubscriptionTypes().Result, "Id", "Name", subscription.SubscriptionTypeId);
            return View(subscription);
        }

        // POST: Subscriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    subscription.ExpireAt = subscription.CreatedAt.AddMonths(_subscriptionTypeService.GetSubscriptionTypeById(subscription.SubscriptionTypeId).Result.DurationMonth);
                    await Task.Run(()=>_subscriptionService.UpdateSubscriptionRegistration(subscription));  
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
            ViewData["SubscriptionTypeId"] = new SelectList(_subscriptionTypeService.GetSubscriptionTypes().Result, "Id", "Name", subscription.SubscriptionTypeId);
            ViewData["ChildId"] = new SelectList(_childService.GetChildren().Result, "Id", "FullName");
            return View(subscription);
        }

        // GET: Subscriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var subscription = await _subscriptionService.GetSubscriptionById(id);
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
            var subscription = await _subscriptionService.GetSubscriptionById(id);
            if (subscription != null)
            {
                await Task.Run(() => _subscriptionService.RemoveSubscription(subscription));
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool SubscriptionExists(int id)
        {
          return _subscriptionService.IsExists(id);
        }
    }
}
