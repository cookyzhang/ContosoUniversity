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
            /*�ϵķ���
             *  _context.Students.Add(Student);
             *await _context.SaveChangesAsync();
             *return RedirectToPage("./Index");
             * 
             * */

            //TryUpdateModelAsync<Student> ����ʹ�� PageModel �� PageContext �������ѷ����ı�ֵ���� emptyStudent ���� 
            //TryUpdateModelAsync �������г������� (s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate)��
            //�ڶ����Ա��� ("student", // Prefix) �����ڲ���ֵ��ǰ׺�� ���Ա��������ִ�Сд��
            //�ѷ����ı�ֵͨ��ģ�Ͱ�ת��Ϊ Student ģ���е����͡�
            //ʹ�� TryUpdateModel ���¾����ѷ���ֵ���ֶ���һ����ѵİ�ȫ��������Ϊ������ֹ���෢��
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