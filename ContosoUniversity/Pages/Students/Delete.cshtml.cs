using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Students
{
    public class DeleteModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public DeleteModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Student Student { get; set; }
        //修改1
        public string ErrorMessage { get; set; }
        //修改2
        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Students.AsNoTracking().SingleOrDefaultAsync(m => m.ID == id);

            if (Student == null)
            {
                return NotFound();
            }
            //修改3
            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = "Delete failed.Try again";
            }
            return Page();
        }

        //public async Task<IActionResult> OnPostAsync(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    Student = await _context.Students.FindAsync(id);

        //    if (Student != null)
        //    {
        //        _context.Students.Remove(Student);
        //        await _context.SaveChangesAsync();
        //    }

        //    return RedirectToPage("./Index");
        //}

        //修改4
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);

            if (student==null)
            {
                return NotFound();
            }

            try
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException)
            {
                return RedirectToAction("./Delete", new { id = id, saveChangesError = true });
            }
        }
    }
}
