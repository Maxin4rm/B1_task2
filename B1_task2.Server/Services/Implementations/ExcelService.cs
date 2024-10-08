using B1_task2.Server.Data;
using B1_task2.Server.DTO.ResponseDTO;
using B1_task2.Server.Models;
using B1_task2.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using File = B1_task2.Server.Models.File;

namespace B1_task2.Server.Services.Implementations
{
    public class ExcelService : IExcelService
    {
        private readonly AppDbContext _context;

        public ExcelService(AppDbContext context) 
        { 
            _context = context; 
        }

        public async Task<List<File>> GetFilesAsync()
        {
            var files = await _context.Files.ToListAsync();
            return files;
        }

        public async Task<FileDataResponseDTO?> GetFileDataAsync(int fileId)
        {
            // Получаем файл
            var file = await _context.Files
                .Include(f => f.Balance)
                .FirstOrDefaultAsync(f => f.FileID == fileId);

            if (file == null)
            {
                return null;
            }

            // Получаем балансы, включая классы и их балансы
            var balances = await _context.Balances
                .Include(b => b.Account)
                    .ThenInclude(a => a.Class) // Загружаем классы
                        .ThenInclude(c => c.ClassBalance) // Загружаем балансы классов
                .Where(b => b.Account!.FileID == fileId)
                .ToListAsync();

            // Группируем данные по классам
            var classGroups = balances
                .GroupBy(b => b.Account!.Class)
                .Select(g => new ClassInfoResponseDTO
                {
                    Class = g.Key,
                    AccountBalances = g.Select(b => new AccountBalanceResponseDTO
                    {
                        AccountID = b.Account!.AccountID,
                        AccountNumber = b.Account.AccountNumber,
                        IncomingActive = b.IncomingActive,
                        IncomingPassive = b.IncomingPassive,
                        Debit = b.Debit,
                        Credit = b.Credit,
                        OutgoingActive = b.OutgoingActive,
                        OutgoingPassive = b.OutgoingPassive
                    }).ToList(),
                    ClassBalance = g.Key.ClassBalance 
                })
                .ToList();

            // Создаем ответ
            var response = new FileDataResponseDTO
            {
                File = new FileInfoResponseDTO
                {
                    FileID = file.FileID,
                    FileName = file.FileName,
                    FileInfoDetails = file.FileInfo
                },
                Classes = classGroups,
                FileBalance = file.Balance 
            };

            return response;
        }

        public async Task<bool> UploadFileAsync(IFormFile inputfile)
        {
            if (inputfile == null || inputfile.Length == 0)
                return false;

            Class? currClass = null;

            using (var stream = inputfile.OpenReadStream())
            {
                //Открытие файла .xls с помощью NPOI
                IWorkbook workbook = WorkbookFactory.Create(stream);
                ISheet worksheet = workbook.GetSheetAt(0);
                int rowCount = worksheet.LastRowNum;

                var currFile = new File
                {
                    FileName = inputfile.FileName,
                    FileInfo = GetFileInfo(ref worksheet)
                };
                _context.Files.Add(currFile);
                List<Account> accounts = [];
                for (int row = 8; row <= rowCount; row++)
                {
                    IRow sourceRow = worksheet.GetRow(row);
                    if (sourceRow == null)
                        continue;

                    string currCell = sourceRow.GetCell(0)?.ToString() ?? string.Empty;

                    if (!Int32.TryParse(currCell, out _))
                    {
                        if (currCell.StartsWith("КЛАСС"))
                        {
                            currClass = new Class
                            {
                                ClassName = currCell
                            };
                            _context.Classes.Add(currClass);
                        }
                        else if (currCell.StartsWith("ПО КЛАССУ"))
                        {
                            currClass!.ClassBalance = new Balance
                            {
                                IncomingActive = Decimal.Parse(sourceRow.GetCell(1)?.ToString() ?? "0"),
                                IncomingPassive = Decimal.Parse(sourceRow.GetCell(2)?.ToString() ?? "0"),
                                Debit = Decimal.Parse(sourceRow.GetCell(3)?.ToString() ?? "0"),
                                Credit = Decimal.Parse(sourceRow.GetCell(4)?.ToString() ?? "0"),
                                OutgoingActive = Decimal.Parse(sourceRow.GetCell(5)?.ToString() ?? "0"),
                                OutgoingPassive = Decimal.Parse(sourceRow.GetCell(6)?.ToString() ?? "0"),
                            };
                        }
                        else if (currCell.StartsWith("БАЛАНС"))
                        {
                            currFile.Balance = new Balance
                            {
                                IncomingActive = Decimal.Parse(sourceRow.GetCell(1)?.ToString() ?? "0"),
                                IncomingPassive = Decimal.Parse(sourceRow.GetCell(2)?.ToString() ?? "0"),
                                Debit = Decimal.Parse(sourceRow.GetCell(3)?.ToString() ?? "0"),
                                Credit = Decimal.Parse(sourceRow.GetCell(4)?.ToString() ?? "0"),
                                OutgoingActive = Decimal.Parse(sourceRow.GetCell(5)?.ToString() ?? "0"),
                                OutgoingPassive = Decimal.Parse(sourceRow.GetCell(6)?.ToString() ?? "0"),
                            };
                        }
                        continue;
                    }
                    // Чтение данных из Excel и сохранение в БД
                    var account = new Account
                    {
                        AccountNumber = sourceRow.GetCell(0)?.ToString() ?? string.Empty,
                        File = currFile,
                        Class = currClass!,
                    };

                    _context.Accounts.Add(account);
                    var balance = new Balance
                    {
                        Account = account,
                        IncomingActive = Decimal.Parse(sourceRow.GetCell(1)?.ToString() ?? "0"),
                        IncomingPassive = Decimal.Parse(sourceRow.GetCell(2)?.ToString() ?? "0"),
                        Debit = Decimal.Parse(sourceRow.GetCell(3)?.ToString() ?? "0"),
                        Credit = Decimal.Parse(sourceRow.GetCell(4)?.ToString() ?? "0"),
                        OutgoingActive = Decimal.Parse(sourceRow.GetCell(5)?.ToString() ?? "0"),
                        OutgoingPassive = Decimal.Parse(sourceRow.GetCell(6)?.ToString() ?? "0"),
                    };
                    _context.Balances.Add(balance);
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }
        private static string GetFileInfo(ref ISheet worksheet)
        {
            string info = "";
            for (int row = 0; row <= 5; row++)
            {
                IRow sourceRow = worksheet.GetRow(row);
                if (sourceRow == null)
                    continue;
                info += sourceRow.GetCell(0)?.ToString() ?? string.Empty + " ";
            }
            return info;
        }
    }
}