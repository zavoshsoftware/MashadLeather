using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using Helpers;
using Models;
using ViewModels;

namespace Khoshdast.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ExcelDataController : Controller
    {
        public ActionResult Import()
        {
            UploadFile uploadFile = new UploadFile();
            return View(uploadFile);
        }

        [HttpPost]
        public ActionResult Import(UploadFile UploadFile)
        {
            if (ModelState.IsValid)
            {
                if (UploadFile.ExcelFile.ContentLength > 0)
                {
                    if (UploadFile.ExcelFile.FileName.EndsWith(".xlsx") || UploadFile.ExcelFile.FileName.EndsWith(".xls"))
                    {
                        XLWorkbook Workbook;
                        try
                        {
                            Workbook = new XLWorkbook(UploadFile.ExcelFile.InputStream);
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError(String.Empty, $"Check your file. {ex.Message}");
                            return View();
                        }
                        IXLWorksheet WorkSheet = null;
                        try
                        {
                            WorkSheet = Workbook.Worksheet("Sheet1");

                        }
                        catch
                        {
                            ModelState.AddModelError(String.Empty, "sheet not found!");
                            return View();
                        }
                        WorkSheet.FirstRow().Delete();//if you want to remove ist row

                        foreach (var row in WorkSheet.RowsUsed())
                        {
                            string code = row.Cell(4).Value.ToString();
                            var dis = db.DiscountCodes.FirstOrDefault(c => c.Code == code);
                            if (dis == null)
                            {
                                string wallet = null;

                                if (!string.IsNullOrEmpty(row.Cell(5).Value.ToString()))
                                    wallet = row.Cell(5).Value.ToString();

                                UpdateRow(row.Cell(1).Value.ToString(), row.Cell(2).Value.ToString(),
                                    row.Cell(3).Value.ToString(),
                                    row.Cell(4).Value.ToString(),
                                    wallet);
                            }
                        }
                        db.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, "Only .xlsx and .xls files are allowed");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Not a valid file");
                    return View();
                }
            }
            return View();
        }

        public void test()
        {
            decimal a = Convert.ToDecimal("");
            decimal ab = Convert.ToInt32("");
            decimal aaaDecimal = Convert.ToDecimal(null);
            decimal aaasdb = Convert.ToInt32(null);
        }

        public bool ConvertMattresVal(string hasMattres)
        {
            if (hasMattres.ToLower() == "y")
                return true;

            return false;
        }
        public decimal DecimalConvertor(string amount)
        {
            try
            {
                if (string.IsNullOrEmpty(amount))
                    return 0;

                return Convert.ToDecimal(amount);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void UpdateRow(string cellnumber, string amount, string maxAmount, string code,string wallet)
        {

            User user = db.Users.FirstOrDefault(c => c.CellNum == cellnumber && c.IsDeleted == false);

            if (user == null)
            {
                string cellWithZero = "0" + cellnumber;
                user = db.Users.FirstOrDefault(c => c.CellNum == cellWithZero && c.IsDeleted == false);
            }

            if (user != null)
            {
                if (wallet != null)
                {
                    user.Amount = Convert.ToDecimal(wallet);
                    user.LastModifiedDate = DateTime.Now;
                }

                decimal amountDecimal = Convert.ToDecimal(amount);
                decimal maxAmountDecimal = Convert.ToDecimal(maxAmount);
                DiscountCode discountCode=new DiscountCode()
                {
                    Id = Guid.NewGuid(),
                    Amount = amountDecimal,
                    MaxAmount = maxAmountDecimal,
                    IsUsed = false,
                    IsActive = true,
                    UserId = user.Id,
                    Code = code,
                    CreationDate = DateTime.Now,
                    ExpireDate = DateTime.Now.AddDays(18),
                    IsDeleted = false,
                    IsMultiUsing = false,
                    IsPublic = false,
                    
                };
                db.DiscountCodes.Add(discountCode);

            }
        }



        private DatabaseContext db = new DatabaseContext();

       
 
       
    }
}