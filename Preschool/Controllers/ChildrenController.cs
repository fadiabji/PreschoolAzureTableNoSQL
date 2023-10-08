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
        private readonly IStorgeChildService _storgeChildService;

        private readonly IStorgeClassroomService _storgeClassroomService;
        

        public ChildrenController( IStorgeChildService storgeChildService,
                                   IStorgeClassroomService storgeClassroomService)
        {
            _storgeChildService = storgeChildService;
            _storgeClassroomService = storgeClassroomService;
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
            List<Classroom> classroomsList = await Task.Run(()=> Conversions.ToClassrooms(_storgeClassroomService.GetClassroomEntities()));
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
            //var childernlist = await _childrenService.GetChildByClassroomId(id);
            var childernlist = (await Task.Run(() => Conversions.ToChildren(_storgeChildService.GetChildEntities()))).Where(c => c.ClassroomId == id).ToList();
            if (childernlist != null)
            {
                foreach(var child in childernlist)
                {
                    if(child.Attendances == null) { child.Attendances = new List<Attendance>(); }
                }
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
                    /*_childrenService.UpdateChildEnrollment(child)*/;
                    _storgeChildService.UpdateChildEntity(Conversions.ToChildEntity(child));
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
            ViewData["ClassId"] = new SelectList(Conversions.ToClassrooms(_storgeClassroomService.GetClassroomEntities()), "Id", "Name");
            ChildVM chlidVm = new();
            return View(chlidVm);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<IFormFile> DocumentCopies, ChildVM childVm)
        {
            Child child = Conversions.ToChild(childVm);
            child.Classroom = Conversions.ToClassroom(_storgeClassroomService.GetClassroomEntityById(childVm.ClassroomId));

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
                    child.DocumentsImage.Add(new DocumentsCopies { Id = new Random().Next(0,100000), ImageFile = img.FileName });
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
            ViewData["ClassId"] = new SelectList(Conversions.ToClassrooms(_storgeClassroomService.GetClassroomEntities()), "Id", "Name");

            return View(childVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  ChildVM childvm, List<IFormFile> DocumentCopies)
        {
            var oldChildDocuments = (await Task.Run(() => 
                    Conversions.ToChild(_storgeChildService.GetChildEntityById(id)))).DocumentsImage.ToList();
            Child child = Conversions.ToChild(childvm);
            child.DocumentsImage = oldChildDocuments;
            child.Classroom = Conversions.ToClassroom(_storgeClassroomService.GetClassroomEntityById(childvm.ClassroomId));


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
                Child child = await Task.Run(() => Conversions.ToChild(_storgeChildService.GetChildEntityById(id)));

                if (ChildExists(id))
                {
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
            return _storgeChildService.IsChildExists(id);
        }



    }
}
