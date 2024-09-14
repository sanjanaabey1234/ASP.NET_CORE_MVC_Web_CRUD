using Microsoft.AspNetCore.Mvc;
using Web_CRUD.Context;
using Web_CRUD.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using System.IO;

namespace Web_CRUD.Controllers
{
    public class AnalyticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnalyticsController(ApplicationDbContext context)
        {
            _context = context;
            ExcelPackage.LicenseContext = LicenseContext.Commercial; // or LicenseContext.NonCommercial based on your license
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ExportPdf()
        {
            var employees = _context.Employees.ToList();

            using (var stream = new MemoryStream())
            {
                var document = new Document();
                PdfWriter.GetInstance(document, stream);
                document.Open();

                document.Add(new Paragraph("Employee Report"));
                document.Add(new Paragraph("Generated on: " + DateTime.Now.ToString()));

                var table = new PdfPTable(6);
                table.AddCell("ID");
                table.AddCell("First Name");
                table.AddCell("Middle Name");
                table.AddCell("Last Name");
                table.AddCell("Email Address");
                table.AddCell("Phone No");

                foreach (var employee in employees)
                {
                    table.AddCell(employee.Id.ToString());
                    table.AddCell(employee.FirstName);
                    table.AddCell(employee.MiddleName);
                    table.AddCell(employee.LastName);
                    table.AddCell(employee.EmailAddress);
                    table.AddCell(employee.PhoneNo.ToString());
                }

                document.Add(table);
                document.Close();

                byte[] byteInfo = stream.ToArray();
                stream.Close();

                return File(byteInfo, "application/pdf", "EmployeeReport.pdf");
            }
        }

        public IActionResult ExportExcel()
        {
            var employees = _context.Employees.ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Employees");
                worksheet.Cells["A1"].Value = "ID";
                worksheet.Cells["B1"].Value = "First Name";
                worksheet.Cells["C1"].Value = "Middle Name";
                worksheet.Cells["D1"].Value = "Last Name";
                worksheet.Cells["E1"].Value = "Email Address";
                worksheet.Cells["F1"].Value = "Phone No";

                int row = 2;
                foreach (var employee in employees)
                {
                    worksheet.Cells["A" + row].Value = employee.Id;
                    worksheet.Cells["B" + row].Value = employee.FirstName;
                    worksheet.Cells["C" + row].Value = employee.MiddleName;
                    worksheet.Cells["D" + row].Value = employee.LastName;
                    worksheet.Cells["E" + row].Value = employee.EmailAddress;
                    worksheet.Cells["F" + row].Value = employee.PhoneNo;
                    row++;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);

                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EmployeeReport.xlsx");
            }
        }

    }
}
