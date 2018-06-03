using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public CreateModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            /*老的方法
             *  _context.Students.Add(Student);
             *await _context.SaveChangesAsync();
             *return RedirectToPage("./Index");
             * 
             * */

            //TryUpdateModelAsync<Student> 尝试使用 PageModel 的 PageContext 属性中已发布的表单值更新 emptyStudent 对象。 
            //TryUpdateModelAsync 仅更新列出的属性 (s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate)。
            //第二个自变量 ("student", // Prefix) 是用于查找值的前缀。 该自变量不区分大小写。
            //已发布的表单值通过模型绑定转换为 Student 模型中的类型。
            //使用 TryUpdateModel 更新具有已发布值的字段是一种最佳的安全做法，因为这能阻止过多发布
            var emptyStudent = new Student();
            if (await TryUpdateModelAsync<Student>(emptyStudent,"student",
                s=>s.FirstMidName,s=>s.LastName,s=>s.EnrollmentDate))
            {
                _context.Students.Add(emptyStudent);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return null;
        }
    }
}