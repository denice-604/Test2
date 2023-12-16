using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NPOI.OpenXmlFormats.Spreadsheet;
using Test22.Data;
using Test22.Models;
using Test22.Data;

namespace Test22.Pages
{
    public class PrintElementsModel : PageModel
    {
        public readonly Test2Context? _context;

        /// <summary>
        /// ������ ������������� �������, ������� ����������� ���������� �����
        /// </summary>
        public List<Test22.Models.Account> acco;

        /// <summary>
        /// ������ ������������� ���������� �����
        /// </summary>
        public List<Test22.Models.Class> classes;   

        public PrintElementsModel(Test2Context context)
        {
            _context = context;
        }
        public void OnGet(String selectedItem)
        {
            DbSet<Models.Account> ac = _context.Accounts;       // �������� �� ��������� ��������� ���� ������� � �������
            acco = ac.ToList();
            DbSet<Models.Class> cl  = _context.Classes;
            classes = cl.ToList();

            if (classes.Count != 0 || acco.Count != 0)   // � ������� LINQ �� ������ ���� ������� � �������
                                                   // �������� ������ ��, ��� ����������� ���������� �����
            {
                classes = classes.Where(item => item.FileId.ToString() == selectedItem).ToList();

                acco = acco.Where(item => classes.Any(other => other.Id == item.ClassId)).ToList();
            }
        }

    }
}
