using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;

namespace ExcelHelperCont
{
    public class ExcelHelper
    {
        public List<BarkodHizmet> ReadExcelFile(string filePath)
        {
            var barkodHizmetler = new List<BarkodHizmet>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RangeUsed().RowsUsed(); 

                
                var dataRows = rows.Skip(1); /

                foreach (var row in dataRows)
                {
                   
                    var islemCell = row.Cell(1).GetValue<string>();
                    var optikKoduCell = row.Cell(2).GetValue<string>();

                   
                    if (!string.IsNullOrEmpty(islemCell) && int.TryParse(optikKoduCell, out int optikKodu))
                    {
                        
                        barkodHizmetler.Add(new BarkodHizmet
                        {
                            Islem = islemCell,
                            OptikKodu = optikKodu
                        });
                    }
                    else
                    {
                       
                        Console.WriteLine($"Geçersiz veri atlandı: İşlem - {islemCell}, Optik Kodu - {optikKoduCell}");
                    }
                }
            }

            return barkodHizmetler;
        }
    }

    public class BarkodHizmet
    {
        public string Islem { get; set; }
        public int OptikKodu { get; set; }
    }
}
