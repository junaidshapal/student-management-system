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
    public class LecturersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LecturersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lecturers
        public async Task<IActionResult> Index()
        {
              return _context.Lecturers != null ? 
                          View(await _context.Lecturers.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Lecturers'  is null.");
        }

        // GET: Lecturers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lecturers == null)
            {
                return NotFound();
            }

            var lecturer = await _context.Lecturers
                .FirstOrDefaultAsync(m => m.LecturerId == id);
            if (lecturer == null)
            {
                return NotFound();
            }

            return View(lecturer);
        }

        // GET: Lecturers/Create
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            var list = GetRoles();
            ViewBag.RolesList = list;
            if (id == 0)
            {
                return View(new Lecturer());
            }
            else
            {
                var lecturer = await _context.Lecturers.FindAsync(id);
                if (lecturer == null)
                {
                    return NotFound();
                }
                return View(lecturer);
            }
        }

        // POST: Lecturers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                if (lecturer.LecturerId == 0)
                {
                    _context.Add(lecturer);
                }
                else
                {
                    _context.Update(lecturer);
                }
                await _context.SaveChangesAsync();
                var data = await _context.Lecturers.ToListAsync();
                return PartialView("_ViewLecturers", data);
            }
            return PartialView(lecturer);
        }


        // POST: Lecturers/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lecturers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Lecturers'  is null.");
            }
            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer != null)
            {
                _context.Lecturers.Remove(lecturer);
            }
            
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewLecturer", _context.Lecturers.ToListAsync()) });

        }

        private bool LecturerExists(int id)
        {
          return (_context.Lecturers?.Any(e => e.LecturerId == id)).GetValueOrDefault();
        }

        public List<SelectListItem> GetRoles()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text="HOD", Value="1" },
                new SelectListItem{Text="Lab Engineer", Value="2" },
                new SelectListItem{Text="Course Coordinator", Value="3" },
                new SelectListItem{Text="Course Advisor", Value="4" },
            };
        }
    }
}
