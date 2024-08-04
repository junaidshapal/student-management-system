using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SMS01.Data;
using SMS01.Models;

namespace SMS01.Controllers
{
    //[Authorize]
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _hostEnvironment;

        public StudentsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Students
        //public async Task<IActionResult> Index()
        //{
        //      return _context.Students != null ? 
        //                  View(await _context.Students.ToListAsync()) :
        //                  Problem("Entity set 'ApplicationDbContext.Students'  is null.");
        //}

        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Student' is null.");
            }

            var students = from m in _context.Students
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name!.Contains(searchString));
            }

            return View(await students.ToListAsync());
        }
        
        public async Task<IActionResult> IndexPartial(string searchString)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Student' is null.");
            }

            var students = from m in _context.Students
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.ToLower()!.Contains(searchString.ToLower()));
            }

            return PartialView("_ViewAll",await students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            //student.ImagePath = "/Image/no-img-jpg";

            return View(student);
        }

        // GET: Students/Create
        public async Task<IActionResult> AddOrUpdate(int id = 0)
        {
            if (id == 0)
            {
                return View(new Student());
            }
            else
            {
                var student = await _context.Students.FindAsync(id);
                if (student == null)
                {
                    return NotFound();
                }
                return View(student);
            }
        }

        public async Task<IActionResult> CheckRegisterationNumber(int Id, int RegNo, string Cnic)
        {
            var isExist = false;
            if (Id == 0)
            {
                isExist = await _context.Students.AnyAsync(x => x.RegNo == RegNo && x.CNIC == Cnic);
            }
            else
            {
                isExist = await _context.Students.AnyAsync(x => x.StudentId != Id && x.RegNo == RegNo && x.CNIC == Cnic);
            }

            return Json(isExist);
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        //
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(Student student)
        {

            //if (await _context.Students.AnyAsync(x => x.RegNo == student.RegNo))
            //{
            //    ViewBag.RegnoError = "Registration number already exists";
            //    return View(student);
            //}
            if (ModelState.IsValid)
            {

                string fileNameToSave = "";
                if (student.Image != null)
                {

                    ///Saving Image to wwwRoot/Image
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(student.Image.FileName);
                    string extension = Path.GetExtension(student.Image.FileName);

                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await student.Image.CopyToAsync(fileStream);
                    }

                    fileNameToSave = "/Image/" + fileName;
                }

                student.ImagePath = fileNameToSave;

                if (student.StudentId == 0)
                {
                    _context.Add(student);
                }
                else
                {
                    _context.Update(student);
                }
                await _context.SaveChangesAsync();

                var data = await _context.Students.ToListAsync();
                return PartialView("_ViewAll", data);
                //return RedirectToAction(nameof(Index));
            }
            return PartialView(student);
        }



        //// GET: Students/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Students == null)
        //    {
        //        return NotFound();
        //    }

        //    var student = await _context.Students.FindAsync(id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(student);
        //}

        //// POST: Students/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("StudentId,Age,Name,FatherName,RegNo,Address")] Student student)
        //{
        //    if (id != student.StudentId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(student);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!StudentExists(student.StudentId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(student);
        //}

        // GET: Students/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Students == null)
        //    {
        //        return NotFound();
        //    }

        //    var student = await _context.Students
        //        .FirstOrDefaultAsync(m => m.StudentId == id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(student);
        //}

        // POST: Students/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Students.ToListAsync()) });
        }

        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.StudentId == id)).GetValueOrDefault();
        }
    }
}
