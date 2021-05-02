using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using Helper;
using Helpers;
using Models;
using ViewModels;

namespace Khoshdast.Controllers
{
    [Authorize(Roles = "Administrator,SuperAdministrator,eshopadmin")]
    public class ExcelDataController : Controller
    {
        public ActionResult ExportProducts()
        {

            return View(GetAvailableProduct());
        }

        public List<ViewModels.ProductExportViewModel> GetAvailableProduct()
        {
            List<ViewModels.ProductExportViewModel> result = new List<ProductExportViewModel>();


            var products = db.Products.Where(c => c.IsDeleted == false && c.Quantity > 0 && c.ParentId != null && c.Parent.Quantity > 0 && c.Parent.IsAvailable && c.IsAvailable).Select(c => new
            {
                c.Id,
                c.Barcode,
                c.Quantity,
                ColorTitle = c.Color.Title,
                c.Amount,
                c.DiscountAmount,
                Title = c.Title,
                c.SizeId,
            });

            foreach (var product in products.ToList())
            {
                string size = "-";

                if (product.SizeId != null)
                    size = db.Sizes.Find(product.SizeId).Title;

                string code = product.Barcode;
                if (code.Length == 20)
                {
                    code = product.Barcode.Substring(5, 5);
                }

                string discount = "-";
                if (product.DiscountAmount != null)
                    discount = product.DiscountAmount.Value.ToString("N0");

                result.Add(new ProductExportViewModel()
                {
                    Id = product.Id,
                    Barcode = product.Barcode,
                    Amount = product.Amount.Value.ToString("n0"),
                    DiscountAmount = discount,
                    Title = product.Title,
                    Color = product.ColorTitle,
                    Size = size,
                    Code = code,
                    Quantity = product.Quantity.ToString("N0")
                });
            }
            return result;
        }
        public ActionResult DownloadProductExcel()
        {

            List<ProductExportViewModel> products = GetAvailableProduct();

            List<ProductExportForExcelViewModel> excellProducts = new List<ProductExportForExcelViewModel>();
            foreach (ProductExportViewModel product in products)
            {
                excellProducts.Add(new ProductExportForExcelViewModel()
                {
                    Amount = product.Amount,
                    Quantity = product.Quantity,
                    Barcode = product.Barcode,
                    Code = product.Code,
                    Title = product.Title,
                    DiscountAmount = product.DiscountAmount,
                    Color = product.Color,
                    Size = product.Size
                });
            }


            GridView gv = new GridView();
            gv.DataSource = excellProducts;
            gv.DataBind();
            gv.HeaderRow.Cells[0].Text = "عنوان محصول";
            gv.HeaderRow.Cells[1].Text = "کد محصول";
            gv.HeaderRow.Cells[2].Text = "بارکد";
            gv.HeaderRow.Cells[3].Text = "رنگ";
            gv.HeaderRow.Cells[4].Text = "سایز";
            gv.HeaderRow.Cells[5].Text = "قیمت";
            gv.HeaderRow.Cells[6].Text = "قیمت بعد از تخفیف";
            gv.HeaderRow.Cells[7].Text = "موجودی";


            Session["orders-distinc"] = gv;


            if (Session["orders-distinc"] != null)
            {
                return new DownloadFileActionResult((GridView)Session["orders-distinc"], "available-products.xls");
            }
            else
            {
                return null;
            }
        }
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
                                else
                                    wallet = "0";

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

        public void UpdateRow(string cellnumber, string amount, string maxAmount, string code, string wallet)
        {

            User user = db.Users.FirstOrDefault(c => c.CellNum == cellnumber && c.IsDeleted == false);

            string cellWithZero = "0" + cellnumber;


            if (user == null)
            {
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
                DiscountCode discountCode = new DiscountCode()
                {
                    Id = Guid.NewGuid(),
                    Amount = amountDecimal,
                    MaxAmount = maxAmountDecimal,
                    IsUsed = false,
                    IsActive = true,
                    UserId = user.Id,
                    Code = code,
                    CreationDate = DateTime.Now,
                    ExpireDate = DateTime.Now.AddDays(208),
                    IsDeleted = false,
                    IsMultiUsing = false,
                    IsPublic = false,

                };
                db.DiscountCodes.Add(discountCode);

            }
            else
            {
                Guid roleId = new Guid((System.Configuration.ConfigurationManager.AppSettings["customerRoleId"]));
                user = new User()
                {
                    Id = Guid.NewGuid(),
                    Username = cellWithZero,
                    Password = code,
                    CellNum = cellWithZero,
                    IsActive = false,
                    Code = GenerateUserCode(),
                    RoleId = roleId,
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                };

                db.Users.Add(user);

                decimal amountDecimal = Convert.ToDecimal(amount);
                decimal maxAmountDecimal = Convert.ToDecimal(maxAmount);
                DiscountCode discountCode = new DiscountCode()
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

                db.SaveChanges();
            }
        }

        public int GenerateUserCode()
        {
            return FindeLastUserCode() + 1;
        }

        public int FindeLastUserCode()
        {
            var user = db.Users.Where(current => current.IsDeleted == false).OrderByDescending(current => current.Code).Select(x => new { x.Code }).FirstOrDefault();

            if (user != null)
                return user.Code;
            else
                return 999;
        }


        private DatabaseContext db = new DatabaseContext();




    }
}