using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Test22.Data;

namespace Test22.Pages
{

    public class ChoiceFileModel : PageModel
    {
        /// <summary>
        /// локальный контекст базы данных
        /// </summary>
        public readonly Test2Context? _context;

        public DbSet<Test22.Models.File> Files;

        [BindProperty(SupportsGet = true)]
        public string SelectedName { get; set; }

        /// <summary>
        /// инициализация контекста базы данных
        /// </summary>
        /// <param name="context"> данные выгруженные из СУБД </param>
        public ChoiceFileModel(Test2Context context)
        {
            _context = context;
        }

        /// <summary>
        /// при открытии страницы отрабатывает, создаёт список файлов которые были выгружены
        /// </summary>
        public void OnGet()
        {
            DbSet<Models.File> files = _context.Files;
            Files = files;
        }


        /// <summary>
        /// передаёт выбранный файл на страницу для вывода информации
        /// </summary>
        /// <returns> id выбранного файла в строковом формате </returns>
        public IActionResult OnPostChoise()
        {
            return RedirectToPage("/PrintElements", new { selectedItem = SelectedName });
        }




    }
}
