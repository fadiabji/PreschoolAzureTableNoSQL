using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using Preschool.Data;
using Preschool.Extentions;
using Preschool.Models;
using Preschool.Services;
using Preschool.Services.EntitiesServices;

namespace Preschool.Controllers
{
    [Authorize(Roles = ("Admin"))]
    public class ClassroomController : Controller
    {
        private readonly IClassroomService _classroomService;
        private readonly IStorgeClassroomService _storgeClassroomService;

        public ClassroomController(IClassroomService classService, IStorgeClassroomService storgeClassroomService)
        {
            _classroomService = classService;
            _storgeClassroomService = storgeClassroomService;
        }

        // GET: Classes
        public async Task<IActionResult> Index()
        {
            //return View(await _classroomService.GetClasses());
            return View(await Task.Run(() => Conversions.ToClassrooms(_storgeClassroomService.GetClassroomEntities())));
        }

        // GET: Classes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var @class = await _classroomService.GetClassById(id);
            
            var @class = await Task.Run(() => Conversions.ToClassroom(_storgeClassroomService.GetClassroomEntityById(id)));
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // GET: Classes/Create
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Classroom @class)
        {
            if (ModelState.IsValid)
            {
                //await Task.Run(()=> _classroomService.CreateClass(@class));
                await Task.Run(() => _storgeClassroomService.AddClassroomToTable(Conversions.ToClassroomEntity(@class)));

                return RedirectToAction(nameof(Index));
            }
            return View(@class);
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }
            //var @class = await _classroomService.GetClassById(id);
            var @class = await Task.Run(() => Conversions.ToClassroom(_storgeClassroomService.GetClassroomEntityById(id)));
            if (@class == null)
            {
                return NotFound();
            }
            return View(@class);
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Classroom @class)
        {
            if (id != @class.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //await Task.Run(()=>_classroomService.UpdateClass(@class));
                    await Task.Run(() => _storgeClassroomService.UpdateClassroomEntity(Conversions.ToClassroomEntity(@class)));

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(@class.Id))
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
            return View(@class);
        }

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            //var @class = await _classroomService.GetClassById(id);
            var @class = await Task.Run(() => Conversions.ToClassroom(_storgeClassroomService.GetClassroomEntityById(id)));
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                //var @class = await _classroomService.GetClassById(id);
                var @class = await Task.Run(() => Conversions.ToClassroom(_storgeClassroomService.GetClassroomEntityById(id)));
                if (@class != null)
                {
                    //_classroomService.RemoveClass(@class);
                    _storgeClassroomService.DeleteClassroomEntity(Conversions.ToClassroomEntity(@class));
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private bool ClassExists(int id)
        {
            //return _classroomService.IsExists(id);
            return Conversions.ToClassrooms(_storgeClassroomService.GetClassroomEntities()).Any(a => a.Id == id);
        }
    }
}
