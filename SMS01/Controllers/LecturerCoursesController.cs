using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SMS01.Data;
using SMS01.Models;

namespace SMS01.Controllers
{
    public class LecturerCoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LecturerCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LecturerCourses
        public async Task<IActionResult> Index()
        {
              return _context.LecturerCourses != null ? 
                          View(await _context.LecturerCourses.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.LecturerCourses'  is null.");
        }

        // GET: LecturerCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LecturerCourses == null)
            {
                return NotFound();
            }

            var lecturerCourse = await _context.LecturerCourses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lecturerCourse == null)
            {
                return NotFound();
            }

            return View(lecturerCourse);
        }

        // GET: LecturerCourses/Create
        public IActionResult Create()
        {
            //ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            //ViewData["LecturerId"] = new SelectList(_context.LecturerCourses, "Id", "Name");
            return View();
        }

        // POST: LecturerCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,LecturerId,StartDate,EndDate")] LecturerCourse lecturerCourse)
        {

            if (ModelState.IsValid)
            {
            _context.Add(lecturerCourse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }
            //ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", lecturerCourse.CourseId);
            //ViewData["LecturerId"] = new SelectList(_context.LecturerCourses, "Id", "Id", lecturerCourse.LecturerId);

            return View(lecturerCourse);
        }

        // GET: LecturerCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LecturerCourses == null)
            {
                return NotFound();
            }

            var lecturerCourse = await _context.LecturerCourses.FindAsync(id);
            if (lecturerCourse == null)
            {
                return NotFound();
            }
            return View(lecturerCourse);
        }

        // POST: LecturerCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,LecturerId,StartDate,EndDate")] LecturerCourse lecturerCourse)
        {
            if (id != lecturerCourse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lecturerCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LecturerCourseExists(lecturerCourse.Id))
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
            return View(lecturerCourse);
        }

        // GET: LecturerCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LecturerCourses == null)
            {
                return NotFound();
            }

            var lecturerCourse = await _context.LecturerCourses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lecturerCourse == null)
            {
                return NotFound();
            }

            return View(lecturerCourse);
        }

        // POST: LecturerCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LecturerCourses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LecturerCourses'  is null.");
            }
            var lecturerCourse = await _context.LecturerCourses.FindAsync(id);
            if (lecturerCourse != null)
            {
                _context.LecturerCourses.Remove(lecturerCourse);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LecturerCourseExists(int id)
        {
          return (_context.LecturerCourses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
