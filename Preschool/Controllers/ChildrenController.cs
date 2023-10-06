using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

namespace Preschool.Controllers
{
    //[Authorize(Roles = ("Admin"))]
    //[Authorize(Roles = ("Teacher"))]
    [Authorize(Roles = "Admin,Teacher")]
    public class ChildrenController : Controller
    {
        private readonly IChildService _childrenService;
        private readonly IClassroomService _classroomService;
        private readonly ISubscriptionTypeService _subscriptionTypeService;
        private readonly IStorgeChildService _storgeChildService;

        public ChildrenController( IChildService childService, 
                                   IClassroomService classroomService, 
                                   ISubscriptionTypeService subscriptionTypeService,
                                   IStorgeChildService storgeChildService)
        {
            _childrenService = childService;
            _classroomService = classroomService;
            _subscriptionTypeService = subscriptionTypeService;
            _storgeChildService = storgeChildService;   
        }

        public async Task<IActionResult> ChildPage(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                //Child child = await _childrenService.GetChildById(id);
                Child child = await Task.Run(()=>Conversions.ToChild(_storgeChildService.GetChildEntityById(id)));
                CheckSubscriptionsExpireDateToExpire(child);
                return View(child);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<IActionResult> AttendencePage()
        {
            var classroomsList = await _classroomService.GetClasses();
            if (classroomsList != null)
            {
                return View(classroomsList);
            }
            else
            {
                return View(new List<Classroom>()); 
            }
        }

        public async Task<IActionResult> GetChildByClassRoom(int id)
        {
            var childernlist = await _childrenService.GetChildByClassroomId(id);
            if (childernlist != null)
            {
                return View(childernlist);
            }
            else
            {
                return View(new List<Child>());
            }
        }




        public void CheckSubscriptionsExpireDateToExpire(Child child)
        {
            foreach (var sub in child.Subscriptions)
            {
                if (sub.ExpireAt.Date < DateTime.Now.Date)
                {
                    sub.IsActive = false;
                    _childrenService.UpdateChildEnrollment(child);
                }
            }
        }
        

    
        public async Task<IActionResult> Index()
        {
            //return View(await _childrenService.GetChildren());
            return View(await Task.Run(()=>Conversions.ToChildren(_storgeChildService.GetChildEntities())));
        }

        // GET: Children/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                //var child = await _childrenService.GetChildById(id);
                Child child = await Task.Run(() => Conversions.ToChild(_storgeChildService.GetChildEntityById(id)));

                if (child == null)
                {
                    return NotFound();
                }

                return View(child);
            }
            catch (Exception)
            {

                throw;
            }
        }

 
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_classroomService.GetClasses().Result, "Id", "Name");
            //ViewData["SubscriptionTypeId"] = new SelectList(_subscriptionTypeService.GetSubscriptionTypes().Result, "Id", "Name");
            ChildVM chlidVm = new();
            return View(chlidVm);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<IFormFile> DocumentCopies, ChildVM childVm)
        {
            Child child = Conversions.ToChild(childVm);

            if (ModelState.IsValid && DocumentCopies != null)
            {
                foreach (var img in DocumentCopies)
                {
                    string fileName = img.FileName;
                    string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\DocumentsCopies"));
                    using (var filestream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    { await img.CopyToAsync(filestream); }
                    if (child.DocumentsImage == null)
                    {
                        child.DocumentsImage = new List<DocumentsCopies>();
                    }
                    child.DocumentsImage.Add(new DocumentsCopies { ImageFile = img.FileName });
                }

                //await Task.Run(() => _childrenService.EnrollChild(child));

                // convert child to childEntitiy
                _storgeChildService.AddChildToTable(Conversions.ToChildEntity(child));

                return RedirectToAction(nameof(Index));
            }
            return View(child);
        }



        

        // GET: Children/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //var child = await _childrenService.GetChildById(id);
            Child child = await Task.Run(() => Conversions.ToChild(_storgeChildService.GetChildEntityById(id)));

            if (child == null)
            {
                return NotFound();
            }
            ChildVM childVm = Conversions.ToChildVM(child);
            
            childVm.SubscriptionTypeId = child.Subscriptions.Select(s => s.SubscriptionTypeId).FirstOrDefault();
            foreach ( var docuemnt in child.DocumentsImage)
            {
                childVm.DocumentCopies.Add(docuemnt.ImageFile);
            }
            ViewData["ClassId"] = new SelectList(_classroomService.GetClasses().Result, "Id", "Name");
            //ViewData["SubscriptionTypeId"] = new SelectList(_subscriptionTypeService.GetSubscriptionTypes().Result, "Id", "Name");

            return View(childVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  ChildVM childvm, List<IFormFile> DocumentCopies)
        {
            //var child = _childrenService.GetChildById(id).Result;
            Child child = await Task.Run(() => Conversions.ToChild(_storgeChildService.GetChildEntityById(id)));


            child = Conversions.ToChild(childvm);

            var oldChildDocuments = child.DocumentsImage.ToList();


            //child.Subscriptions.Where(s => s.ChildId == childvm.Id).ToList().ForEach(s => s.SubscriptionTypeId = childvm.SubscriptionTypeId);
            if (id != child.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid && DocumentCopies != null)
            {
                try
                {
                    foreach (var img in DocumentCopies)
                    {
                        string fileName = img.FileName;
                        string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\DocumentsCopies"));
                        using (var filestream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                        { await img.CopyToAsync(filestream); }
                        if (child.DocumentsImage == null)
                        {
                            child.DocumentsImage = new List<DocumentsCopies>();
                        }
                        if (!oldChildDocuments.Any(d => d.ImageFile == img.FileName))
                        {
                            child.DocumentsImage.Add(new DocumentsCopies { ImageFile = img.FileName });
                        }
                        
                    }
                    //await Task.Run(() => _childrenService.UpdateChildEnrollment(child));
                    _storgeChildService.UpdateChildEntity(Conversions.ToChildEntity(child));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChildExists(child.Id))
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
            return View(child);
        }

        // GET: Children/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                //var child = await _childrenService.GetChildById(id);
                Child child = await Task.Run(() => Conversions.ToChild(_storgeChildService.GetChildEntityById(id)));

                if (child == null)
                {
                    return NotFound();
                }

                return View(child);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: Children/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                //var child = await _childrenService.GetChildById(id);
                Child child = await Task.Run(() => Conversions.ToChild(_storgeChildService.GetChildEntityById(id)));

                if (ChildExists(id))
                {
                    //_childrenService.RemoveChild(child);
                    _storgeChildService.DeleteChildEntity(Conversions.ToChildEntity(child));
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private bool ChildExists(int id)
        {
            //return _childrenService.IsExists(id) != null;
            return _storgeChildService.IsChildExists(id);
        }





        //public async Task<IActionResult> ReNewSubscription(int id)
        //{
        //    try
        //    {
        //        var child = await _childrenService.GetChildById(id);
        //        CheckSubscriptionsExpireDateToExpire(child);
        //        if(child.Subscriptions.Any(s =>s.IsActive == true))
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            var renewsub = new ChildReNewSubscriptionVM { ChildId = id, ClassroomId = child.ClassroomId, SubscriptionTypId = child.Subscriptions.FirstOrDefault().SubscriptionTypeId };
        //            ViewData["ClassId"] = new SelectList(_classroomService.GetClasses().Result, "Id", "Name");
        //            ViewData["SubscriptionTypeId"] = new SelectList(_subscriptionTypeService.GetSubscriptionTypes().Result, "Id", "Name");
        //            return View(renewsub);
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpPost]
        //public async Task<IActionResult> ReNewSubscription(ChildReNewSubscriptionVM renewsub)
        //{
        //    try
        //    {
        //        var child = await _childrenService.GetChildById(renewsub.ChildId);
        //        child.ClassroomId = renewsub.ClassroomId;
        //        child.Subscriptions.Add(new Subscription
        //                                   {
        //                                       SubscriptionTypeId = renewsub.SubscriptionTypId,
        //                                       IsActive = true,
        //                                       CreatedAt = DateTime.Now,
        //                                       ExpireAt = DateTime.Now.AddMonths(_subscriptionTypeService.GetSubscriptionTypeById(renewsub.SubscriptionTypId).Result.DurationMonth),
        //                                       PaymentComplete = true
        //                                   });

        //        await Task.Run(() => _childrenService.UpdateChildEnrollment(child));
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
