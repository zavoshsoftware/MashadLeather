using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Helper
{
    public class DownloadFileActionResult: ActionResult
    {
        public GridView ExcelGridView { get; set; }
        public string fileName { get; set; }


        public DownloadFileActionResult(GridView gv, string pFileName)
        {
            ExcelGridView = gv;
            fileName = pFileName;
        }


        public override void ExecuteResult(ControllerContext context)
        {

            //Create a response stream to create and write the Excel file
            HttpContext curContext = HttpContext.Current;
            curContext.Response.Clear();
            curContext.Response.Buffer = true;
            curContext.Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
            curContext.Response.Charset = "";
            curContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            curContext.Response.ContentType = "application/vnd.ms-excel";

            //Convert the rendering of the gridview to a string representation 

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            ExcelGridView.RenderControl(htw);

            string style = @"<style> .textmode { mso-number-format:\@; } </style> <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>";
            //Write the stream back to the response
            curContext.Response.Write(style);
            curContext.Response.Output.Write(sw.ToString());
            curContext.Response.Flush();
            curContext.Response.End();

        }

    }

}