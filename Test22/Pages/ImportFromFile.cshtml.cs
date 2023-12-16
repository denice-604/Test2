using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Security.Policy;
using Test22.Data;
using Test22.Models;

namespace TestXls.Pages
{
    /// <summary>
    /// �������� ������ �� �����
    /// </summary>
    public class ImportFromFileModel : PageModel
    {
        /// <summary>
        /// ��������� �������� ��
        /// </summary>
        private readonly Test2Context? _context;
        public void OnGet()
        {
        }

        public ImportFromFileModel(Test2Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostUploadAsync()
        {

            var file = Request.Form.Files["xmlFile"];  // ��������� ����� �� ����� � ������� �����
            
            
            if (file != null && file.Length > 0)  
            {
                var fileName = file.FileName;      // ���������� ����� � ��

                Test22.Models.File file1 = new()
                {
                    Name = fileName
                };

                _context.Files.Add(file1);
                _context.SaveChanges(); // ��������� ��������� � ���� ������


                var filePath = "ONE.xls"; // ���� ��� ���������� ����� �� ������� �������

                using (var stream = new FileStream(filePath, FileMode.Create)) // ������ ���� �� ������������ ������
                                                                               // � ���������� ���� ���� ��������� ��������
                {
                    await file.CopyToAsync(stream);
                }


                using (FileStream filep = new FileStream(filePath, FileMode.Open, FileAccess.Read)) 
                {    

                    HSSFWorkbook workbook = new HSSFWorkbook(filep);   // ��������� ����� � ������� NPOI
                    ISheet sheet = workbook.GetSheetAt(0); // ��������� ������� �����
                    Class newcl = new();

                    // ������ ������ �� ������� �����
                    for (int i = 8; i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row != null) // �������� ������� ������
                        {
                            ICell cell = row.GetCell(0);  // ������ �������� ����� �������� � ���, ��� � ������ ������
                                                          // �������� ������, ��� �� ����� �� ������/���������

                            if (cell.CellType == CellType.String)
                            {
                                var str = cell.StringCellValue;

                                if (str.Contains("����� "))    // ���� ��������� ������ �������� ����� ����� � ������
                                                               // ����� ����, �� �� ������� ������ �������� ������
                                {
                                    newcl = new();
                                    newcl.Name = cell.StringCellValue;
                                    newcl.File = file1;
                                    file1.Classes.Add(newcl);   // ����� ����� � �������

                                    _context.Classes.Add(newcl);
                                    _context.SaveChanges(); // ������ ����� ����� � ��������� ��������� � ���� ������

                                    continue;
                                }
                                else if (str.Contains("�� ������"))  // ������������� ����� ����� ��������� �� ������ �� ��,
                                                                     // �� ����� �� ���������
                                {
                                    continue;
                                }
                                else if (str.Contains("������"))
                                    break;

                            }

                            Account account = new Account();    // ���� ������ �� ���� ���� �� ����������, ������ � ���
                                                                // ������ � ������� �����

                            account.Class = newcl;
                            
                            
                            if (cell.CellType == CellType.Numeric)
                                account.Bank = (int)cell.NumericCellValue;   // ����� ����� ����� ���� ������� ��� ������ � ���
                                                                             // �����, ������������ ��� ������
                            else if (cell.CellType == CellType.String)
                                account.Bank = int.Parse(cell.StringCellValue);
                            

                            cell = row.GetCell(1);
                            account.IncomingBalanceActive = cell.NumericCellValue;   // ��������� ��������� ���� �����

                            cell = row.GetCell(2);                           
                            account.IncomingBalancePassive = cell.NumericCellValue;
                            

                            cell = row.GetCell(3);
                            account.DebitTurnover = cell.NumericCellValue;

                            cell = row.GetCell(4);
                            account.CreditTurnover = cell.NumericCellValue;

                            cell = row.GetCell(5);
                            account.InitialBalanceActive = cell.NumericCellValue;

                            cell = row.GetCell(6);
                            account.InitialBalancePassive = cell.NumericCellValue;

                            newcl.Accounts.Add(account);     // ����� ������ � ������ �������
                            _context.Accounts.Add(account);
                            _context.SaveChanges(); // ��������� ��������� � ���� ������

                        }
                    }
                }



                System.IO.File.Delete(filePath);

            }


            return RedirectToPage("/Index"); // ����� ��������� ����� ��������������� �� ��������

        }
    }
}
