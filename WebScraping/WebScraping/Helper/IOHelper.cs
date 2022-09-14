using CsvHelper;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraping.Model;

namespace WebScraping.Helper
{
    internal class IOHelper
    {

        /********** Output Excel Comma Separated Values File (.CSV) *********/
        internal static void ExportToCSV(List<ProductModel> lst, string fileName)
        {
            using (var writer = new StreamWriter(GetExportFolderPath() + fileName + ".csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(lst);
            }
        }

        internal static void ExportToCSV(List<RowModel> lst, string fileName)
        {
            using (var writer = new StreamWriter(GetExportFolderPath() + fileName + ".csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(lst);
            }
        }


        /********** Output Text File (.TXT) *********************************/
        internal static void ExportToTXT(List<ProductModel> lst, string fileName)
        {
            using (var writer = new StreamWriter(GetExportFolderPath() + fileName + ".txt"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(lst);
            }
        }

        internal static void ExportToTXT(List<RowModel> lst, string fileName)
        {
            using (var writer = new StreamWriter(GetExportFolderPath() + fileName + ".txt"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(lst);
            }
        }


        /********** Output Excel File (.XLSX) *******************************/
        internal static void ExportToXLSX(List<ProductModel> lst, string fileName)
        {
            string file = GetExportFolderPath() + fileName + ".xlsx";

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("Scraping Result").Cells[1, 1].LoadFromCollection(lst, true);
                excel.SaveAs(new FileInfo(file));
            }
        }

        internal static void ExportToXLSX(List<RowModel> lst, string fileName)
        {
            string file = GetExportFolderPath() + fileName + ".xlsx";

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("Scraping Result").Cells[1, 1].LoadFromCollection(lst, true);
                excel.SaveAs(new FileInfo(file));
            }
        }





        /********** Import Values ************************************************/
        internal static string? GetInput(string input)
        {
            Console.Write($"\nEnter {input}: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string? val = Console.ReadLine();
            Console.ResetColor();
            return val;
        }


        /********** Get Export Folder Path **********************************/
        private static string GetExportFolderPath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Scraping/";

            if (System.IO.Directory.Exists(path))
                return path;
            else
                System.IO.Directory.CreateDirectory(path);

            return path;
        }
    }
}
