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
    /// выгрузка данных из файла
    /// </summary>
    public class ImportFromFileModel : PageModel
    {
        /// <summary>
        /// локальный контекст БД
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

            var file = Request.Form.Files["xmlFile"];  // Получение файла из формы с выбором файла
            
            
            if (file != null && file.Length > 0)  
            {
                var fileName = file.FileName;      // Сохранение файла в БД

                Test22.Models.File file1 = new()
                {
                    Name = fileName
                };

                _context.Files.Add(file1);
                _context.SaveChanges(); // Сохраняем изменения в базу данных


                var filePath = "ONE.xls"; // Путь для сохранения файла на стороне сервера

                using (var stream = new FileStream(filePath, FileMode.Create)) // Создаём файл по прописанному адресу
                                                                               // и записываем туда файл выбранный клиентом
                {
                    await file.CopyToAsync(stream);
                }


                using (FileStream filep = new FileStream(filePath, FileMode.Open, FileAccess.Read)) 
                {    

                    HSSFWorkbook workbook = new HSSFWorkbook(filep);   // Обработка файла с помощью NPOI
                    ISheet sheet = workbook.GetSheetAt(0); // Получение первого листа
                    Class newcl = new();

                    // Чтение данных из первого листа
                    for (int i = 8; i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row != null) // Проверка наличия строки
                        {
                            ICell cell = row.GetCell(0);  // Данное значение может говорить о том, что в данной строке
                                                          // название класса, или же итоги по классу/документу

                            if (cell.CellType == CellType.String)
                            {
                                var str = cell.StringCellValue;

                                if (str.Contains("КЛАСС "))    // Если считанная строка содержит слово КЛАСС и пробел
                                                               // после него, то мы считали строку названия класса
                                {
                                    newcl = new();
                                    newcl.Name = cell.StringCellValue;
                                    newcl.File = file1;
                                    file1.Classes.Add(newcl);   // Связь файла с классом

                                    _context.Classes.Add(newcl);
                                    _context.SaveChanges(); // Создаём новый класс и сохраняем изменения в базу данных

                                    continue;
                                }
                                else if (str.Contains("ПО КЛАССУ"))  // Промежуточные итоги можно посчитать по данным из БД,
                                                                     // их можно не сохранять
                                {
                                    continue;
                                }
                                else if (str.Contains("БАЛАНС"))
                                    break;

                            }

                            Account account = new Account();    // Если ничего из кода выше не отработало, значит у нас
                                                                // строка с данными счёта

                            account.Class = newcl;
                            
                            
                            if (cell.CellType == CellType.Numeric)
                                account.Bank = (int)cell.NumericCellValue;   // Номер банка может быть записан как строка и как
                                                                             // число, обрабатываем оба случая
                            else if (cell.CellType == CellType.String)
                                account.Bank = int.Parse(cell.StringCellValue);
                            

                            cell = row.GetCell(1);
                            account.IncomingBalanceActive = cell.NumericCellValue;   // Считываем остальные поля счёта

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

                            newcl.Accounts.Add(account);     // связь класса с данной строкой
                            _context.Accounts.Add(account);
                            _context.SaveChanges(); // Сохраняем изменения в базу данных

                        }
                    }
                }



                System.IO.File.Delete(filePath);

            }


            return RedirectToPage("/Index"); // После обработки файла перенаправление на домашнюю

        }
    }
}
