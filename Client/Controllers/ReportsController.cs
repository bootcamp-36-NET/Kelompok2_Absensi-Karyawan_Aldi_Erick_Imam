using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;
using Client.Report;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace Client.Controllers
{
    public class ReportsController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44341/api/")
        };

        public ActionResult Pdf(Absence absence)
        {
            AbsenceReport absenceReport = new AbsenceReport();
            byte[] abytes = absenceReport.PrepareReport(GetAbsence());
            return File(abytes, "application/pdf");
        }

        public ActionResult Excel(Absence absence)
        {
            var ColumnHeaders = new string[]
            {
                "No",
                "ID",
                "Name",
                "Date",
                "Check In",
                "Check Out"
            };

            byte[] result;

            using (var package = new ExcelPackage())
            {
                var workSheet = package.Workbook.Worksheets.Add("Absence List");
                using (var cells = workSheet.Cells[1, 1, 1, 6])
                {
                    cells.Style.Font.Bold = true;
                }

                for (var i = 0; i < ColumnHeaders.Count(); i++)
                {
                    workSheet.Cells[1, i + 1].Value = ColumnHeaders[i];
                }

                var j = 2;
                var index = 1;
                foreach (var data in GetAbsence())
                {
                    workSheet.Cells["A" + j].Value = index++;
                    workSheet.Cells["B" + j].Value = data.User.UserName;
                    workSheet.Cells["C" + j].Value = data.User.Employee.Name;
                    workSheet.Cells["D" + j].Value = data.TimeIn.ToString("yyyy-MM-dd");
                    workSheet.Cells["E" + j].Value = data.TimeIn.ToString("HH:mm");
                    workSheet.Cells["F" + j].Value = data.TimeOut.Year < 2000 ? "Not Check out yet" : data.TimeOut.ToString("HH:mm");

                    j++;
                }
                result = package.GetAsByteArray();
            }
            return File(result, "application/ms-excel", $"Absences.xlsx");
        }
        public List<Absence> GetAbsence()
        {
            List<Absence> absences = null;
            var getTask = client.GetAsync("Absences");
            getTask.Wait();

            var content = getTask.Result.Content.ReadAsStringAsync().Result;
            absences = JsonConvert.DeserializeObject<List<Absence>>(content);
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                absences = absences.Where(a => a.TimeIn.ToString("yyyy-MM-dd") == DateTimeOffset.Now.ToString("yyyy-MM-dd")).ToList();
            }

            return absences;
        }

    }
}