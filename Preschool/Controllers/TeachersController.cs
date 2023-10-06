using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Preschool.Data;
using Preschool.Models;
using Preschool.Models.ViewModels;
using Preschool.Services;
using Microsoft.AspNetCore.Identity;
using Preschool.Services.EntitiesServices;
using Preschool.Extentions;

namespace Preschool.Controllers
{
    
    [Authorize(Roles = "Admin,Teacher")]
    public class TeachersController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly IClassroomService _classroomService;
        private readonly IAttendanceService _attendanceService;
        private readonly IChildService _childService;
        private UserManager<IdentityUser> _userManager;

        private readonly IStorgeChildService _storgeChildService;

        public TeachersController(ITeacherService teacherService,
                                  IClassroomService classroomService,
                                  IAttendanceService attendanceService,
                                  IChildService childService,
                                  UserManager<IdentityUser> userManager,
                                   IStorgeChildService storgeChildService)
        {
            _teacherService = teacherService;
            _classroomService = classroomService;
            _attendanceService = attendanceService;
            _childService = childService;
            _userManager = userManager;
            _storgeChildService = storgeChildService;
        }

        // GET: Teacherren
        public async Task<IActionResult> Index()
        {
            return View(await _teacherService.GetTeachers());
        }


        public async Task<IActionResult> TeacherPage(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                Teacher teacher = await _teacherService.GetTeacherById(id);
                if (teacher == null)
                {
                    return NotFound();
                }
                return View(teacher);

            }
            catch (Exception)
            {
                throw;
            }

        }

        //public async Task<IActionResult> TeacherPage()
        //{
        //    try
        //    {
        //        if (User == null)
        //        {
        //            return NotFound();
        //        }
        //        if (User.IsInRole("Teacher"))
        //        {
        //            string email = User.Identity.Name;
        //            var usr = _userManager.FindByEmailAsync(email).Result;

        //        }
        //        Teacher teacher = await _teacherService.GetTeacherById(id);
        //        if (teacher == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(teacher);

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        public async Task<IActionResult> GetChildByClassRoom(int? teacherId)
        {
            try
            {
                if (teacherId == null)
                {
                    return NotFound();
                }
                Teacher teacher = await _teacherService.GetTeacherById(teacherId);
                if (teacher == null)
                {
                    return NotFound();
                }
                var childrenListbyclassroom = teacher.Classroom.Children.ToList();

                return View(childrenListbyclassroom);

            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpPost("ChildCheckIn/{id}")]
        public async Task<IActionResult> ChildCheckIn(int id)
        {
            try
            {
                if (id == 0)
                {
                    return NotFound();
                }
                //Child child = await _childService.GetChildById(id);
                Child child = await Task.Run(()=>Conversions.ToChild(_storgeChildService.GetChildEntityById(id)));
                if(child  == null)
                {
                    return NotFound();
                }
                child.Attendances.Add(new Attendance { ChildId = id, Date = DateTime.Now, Status = true }); ;
                //_childService.UpdateChildEnrollment(child);
                _storgeChildService.UpdateChildEntity(Conversions.ToChildEntity(child));

                //return RedirectToAction(nameof(GetChildByClassRoom), new { teacherId = id });
                return Ok($"{child.FirstName + child.LastName} Checked In");

            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpPost("ChildCheckOut/{id}")]
        public async Task<IActionResult> ChildCheckOut(int id)
        {
            try
            {
                if (id == 0)
                {
                    return NotFound();
                }
                //Child child = await _childService.GetChildById(id);
                Child child = await Task.Run(() => Conversions.ToChild(_storgeChildService.GetChildEntityById(id)));
                if (child == null)
                {
                    return NotFound();
                }
                child.Attendances.Add(new Attendance { ChildId = id, Date = DateTime.Now, Status = false }); ;
                //_childService.UpdateChildEnrollment(child);
                _storgeChildService.UpdateChildEntity(Conversions.ToChildEntity(child));

                //return RedirectToAction(nameof(GetChildByClassRoom), new { teacherId = id });
                return Ok($"{child.FirstName} {child.LastName} Checked Out");

            }
            catch (Exception)
            {
                throw;
            }

        }

        // GET: Teacherren/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _teacherService.GetTeachers() == null)
            {
                return NotFound();
            }

            var teacher = await _teacherService.GetTeacherById(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teacherren/Create
        public IActionResult Create()
        {

            ViewData["ClassId"] = new SelectList( _classroomService.GetClasses().Result, "Id", "Name");
            return View();
        }

        // POST: Teacherren/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<IFormFile> DocumentCopies, Teacher teacher)
        {
            if (ModelState.IsValid && DocumentCopies != null)
            {

                foreach (var img in DocumentCopies)
                {
                    string fileName = img.FileName;
                    string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\DocumentsCopies"));
                    using (var filestream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    { await img.CopyToAsync(filestream); }
                    if (teacher.DocumentsImage == null)
                    {
                        teacher.DocumentsImage = new List<DocumentsCopies>();
                    }
                    teacher.DocumentsImage.Add(new DocumentsCopies { ImageFile = img.FileName });
                }


                await Task.Run(() => _teacherService.RegistTeacher(teacher));

                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassroomId"] = new SelectList(await Task.Run(() => _classroomService.GetClasses().Result), "Id", "Name", teacher.ClassroomId);
            return View(teacher);
        }





        // GET: Teacherren/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
           

            var teacher = await _teacherService.GetTeacherById(id);
            if (teacher == null)
            {
                return NotFound();
            }
            TeacherVM teacherVm = new TeacherVM()
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                DateOfBirth = teacher.DateOfBirth,
                RegistedAt = teacher.RegistedAt,
                IsActive = teacher.IsActive,
                ClassroomId = teacher.ClassroomId,  
            };

            foreach (var docuemnt in teacher.DocumentsImage)
            {
                teacherVm.DocumentCopies.Add(docuemnt.ImageFile);
            }
            ViewData["ClassId"] = new SelectList(_classroomService.GetClasses().Result, "Id", "Name");
            return View(teacherVm);
        }

        // POST: Teacherren/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeacherVM teachervm, List<IFormFile> DocumentCopies)
        {
            var teacher = _teacherService.GetTeacherById(id).Result;
            teacher.Id = teachervm.Id;
            teacher.FirstName = teachervm.FirstName;
            teacher.LastName = teachervm.LastName;
            teacher.DateOfBirth = teachervm.DateOfBirth;
            teacher.IsActive = teachervm.IsActive;
            teacher.ClassroomId = teachervm.ClassroomId;
            var oldTeacherDocuments = teacher.DocumentsImage.ToList();

            if (id != teacher.Id)
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
                        if (teacher.DocumentsImage == null)
                        {
                            teacher.DocumentsImage = new List<DocumentsCopies>();
                        }
                        if (!oldTeacherDocuments.Any(d => d.ImageFile == img.FileName))
                        {
                            teacher.DocumentsImage.Add(new DocumentsCopies { ImageFile = img.FileName });
                        }
                    }
                    await Task.Run(() => _teacherService.UpdateTeacherRegistration(teacher));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.Id))
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
            return View(teacher);
        }

        // GET: Teacherren/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var teacher = await _teacherService.GetTeacherById(id);
                if (teacher == null)
                {
                    return NotFound();
                }

                return View(teacher);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: Teacherren/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var teacher = await _teacherService.GetTeacherById(id);
                if (_teacherService.IsExists(id))
                {
                    _teacherService.RemoveTeacher(teacher);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }

        }

        private bool TeacherExists(int id)
        {
            return _teacherService.GetTeacherById(id) != null;
        }
    }
}
