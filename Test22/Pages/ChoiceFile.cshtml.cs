using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Test22.Data;

namespace Test22.Pages
{

    public class ChoiceFileModel : PageModel
    {
        /// <summary>
        /// ��������� �������� ���� ������
        /// </summary>
        public readonly Test2Context? _context;

        public DbSet<Test22.Models.File> Files;

        [BindProperty(SupportsGet = true)]
        public string SelectedName { get; set; }

        /// <summary>
        /// ������������� ��������� ���� ������
        /// </summary>
        /// <param name="context"> ������ ����������� �� ���� </param>
        public ChoiceFileModel(Test2Context context)
        {
            _context = context;
        }

        /// <summary>
        /// ��� �������� �������� ������������, ������ ������ ������ ������� ���� ���������
        /// </summary>
        public void OnGet()
        {
            DbSet<Models.File> files = _context.Files;
            Files = files;
        }


        /// <summary>
        /// ������� ��������� ���� �� �������� ��� ������ ����������
        /// </summary>
        /// <returns> id ���������� ����� � ��������� ������� </returns>
        public IActionResult OnPostChoise()
        {
            return RedirectToPage("/PrintElements", new { selectedItem = SelectedName });
        }




    }
}
