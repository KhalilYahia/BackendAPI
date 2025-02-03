using YaznGhanem.Common;
using YaznGhanem.Services.DTO;
using YaznGhanem.Services.Iservices;
using LLama;
using LLama.Common;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using Microsoft.AspNetCore.Hosting;
using NuGet.Packaging.Signing;
using System.Net.NetworkInformation;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using iText.IO.Font.Otf;
using System.Globalization;
using NumberToWord;
using Humanizer;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Authorization;

namespace YaznGhanem.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
   
        public class BillingController : ApiBaseController
        {
            private readonly IBillingService _BillingService;

            public BillingController(IBillingService repositoryServices)
            {
            _BillingService = repositoryServices;
            }

        [HttpPost("GeneratePdf")]
        public async Task<IActionResult> GeneratePdf( Search_BillingDto dto)
        {
            var models = await _BillingService.GetBilling(dto);
            var pdfBytes= GenerateBillingPdf(models);

            return File(pdfBytes, "application/pdf", "BillingStatement.pdf");
        }
        //string fontPath = Path.Combine("font", "arial.ttf");
        private byte[] GenerateBillingPdf(BillingDto billing)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            nfi.CurrencySymbol = ""; // Remove the currency symbol

            using (var memoryStream = new MemoryStream())
            {
                // Create a new document
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                writer.PageEvent = new PDFFooter();

                // Load the Arabic font
                string fontPath = Path.Combine("font", "arial.ttf");
                var baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                var titleFont = new Font(baseFont, 18, Font.BOLD);
                var SubtitleFont = new Font(baseFont, 16, Font.BOLD);
                var normalFont = new Font(baseFont, 10, Font.NORMAL);
                var boldFont = new Font(baseFont, 12, Font.BOLD);

                // Add header information with RTL direction
                PdfPTable table1 = new PdfPTable(3);
                table1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                table1.WidthPercentage = 100;

                #region add subtable

                    // Add subtable
                    PdfPTable table1_1 =new PdfPTable(1);
                table1_1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                table1_1.WidthPercentage = 100;
                // Add title cell
                PdfPCell titleCell = new PdfPCell(new Phrase("الشركة العربية لتجارة الخضار والفواكه", titleFont));
                    titleCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    titleCell.Border = Rectangle.NO_BORDER;
                    table1_1.AddCell(titleCell);

                    // Add subtitle cell
                    PdfPCell subtitleCell = new PdfPCell(new Phrase("استيراد - تصدير", SubtitleFont));
                    subtitleCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    subtitleCell.Border = Rectangle.NO_BORDER;
                    table1_1.AddCell(subtitleCell);



                #endregion

                // Add the nested table to a cell in the main table
                PdfPCell mainCell = new PdfPCell(table1_1);
                mainCell.Border = Rectangle.NO_BORDER; // Optional: Remove border around the nested table in the main table
                table1.AddCell(mainCell);

                //table1.AddCell(table1_1);

                // Add the logo cell with rowspan of 2
                string imagepath = Path.Combine("logo", "logo2.jpeg");
                var image = iTextSharp.text.Image.GetInstance(imagepath);
                image.ScaleAbsolute(80, 60);
                PdfPCell logoCell = new PdfPCell(image);
                logoCell.HorizontalAlignment = Element.ALIGN_CENTER;
                logoCell.Border = Rectangle.NO_BORDER;                
                table1.AddCell(logoCell);

                #region add subtable

                    // Add subtable
                    PdfPTable table1_3 = new PdfPTable(1);
                table1_3.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                table1_3.WidthPercentage = 100;
                // Add location information cell
                PdfPCell locationInfoCell = new PdfPCell(new Phrase("سوريا - اللاذقية", boldFont));
                    locationInfoCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    locationInfoCell.Border = Rectangle.NO_BORDER;
                    table1_3.AddCell(locationInfoCell);

                //// Add phone info cell
                //PdfPCell phoneInfoCell = new PdfPCell(new Phrase("الهاتف: 0417530903", normalFont));
                //phoneInfoCell.HorizontalAlignment = Element.ALIGN_CENTER;
                //phoneInfoCell.Border = Rectangle.NO_BORDER;
                //table1_3.AddCell(phoneInfoCell);


                // Add mobile info cell
                    PdfPCell mobileInfoCell = new PdfPCell(new Phrase("982 982 0982", boldFont));
                    mobileInfoCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    mobileInfoCell.Border = Rectangle.NO_BORDER;
                    table1_3.AddCell(mobileInfoCell);

                PdfPCell SiglInfoCell = new PdfPCell(new Phrase("س.ت 50602 س.ص 109", boldFont));
                SiglInfoCell.HorizontalAlignment = Element.ALIGN_CENTER;
                SiglInfoCell.Border = Rectangle.NO_BORDER;
                table1_3.AddCell(SiglInfoCell);

                PdfPCell dateCell = new PdfPCell(new Phrase($"التاريخ: {DateTime.Now:yyyy-MM-dd}", boldFont));
                    dateCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    dateCell.Border = Rectangle.NO_BORDER;
                    table1_3.AddCell(dateCell);

                #endregion

                // Add the nested table to a cell in the main table
                PdfPCell main2Cell = new PdfPCell(table1_3);
                main2Cell.Border = Rectangle.NO_BORDER; // Optional: Remove border around the nested table in the main table
                table1.AddCell(main2Cell);


                // Add the table to the document
                document.Add(table1);

                document.Add(new iTextSharp.text.Paragraph("\n\n")); // Add space

                // Add date and invoice number with RTL direction
                PdfPTable headerTable = new PdfPTable(1);
                headerTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                headerTable.WidthPercentage = 100;
                headerTable.SetWidths(new float[] { 1 });
                PdfPCell invoiceCell = new PdfPCell(new Phrase("", normalFont));
                string date_="";
                string Past_Payments = "";
                if (billing.StartDate.HasValue)
                {
                    date_ = "من تاريخ: " + billing.StartDate.Value.ToString("dd/MM/yyyy");
                   // Past_Payments = "المتبقي من تاريخ سابق: " +((int) billing.Remainder_Past).ToString("N");
                }
                if(billing.EndDate.HasValue)
                {
                    date_ += "  حتى تاريخ : " + billing.EndDate.Value.ToString("dd/MM/yyyy");
                    
                }
                invoiceCell = new PdfPCell(new Phrase(date_, normalFont));
                invoiceCell.HorizontalAlignment = Element.ALIGN_LEFT;
                invoiceCell.Border = Rectangle.NO_BORDER;
                headerTable.AddCell(invoiceCell);
                headerTable.AddCell(new PdfPCell(new Phrase(Past_Payments, normalFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = Rectangle.NO_BORDER });

                document.Add(headerTable);

                // Add dealer name with RTL direction
                PdfPTable dealerTable = new PdfPTable(1);
                dealerTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                dealerTable.WidthPercentage = 100;

                PdfPCell dealerCell;
                if (billing.Daily && billing.SupplierId != 0)
                {
                    if (billing.DealerId == 0 && billing.SupplierId != 0)
                    {
                        dealerCell = new PdfPCell(new Phrase($" السيد: {billing.SupplierName}  المحترم", boldFont));
                    }
                    else
                    {
                        dealerCell = new PdfPCell(new Phrase($" السيد: {billing.SupplierName} المحترم", boldFont));
                    }
                }
                else
                {
                    dealerCell = new PdfPCell(new Phrase($" السيد: {billing.DelearName} المحترم", boldFont));
                }
              
                dealerCell.HorizontalAlignment = Element.ALIGN_LEFT;
                dealerCell.PaddingBottom = 10;
                dealerCell.Border = Rectangle.BOTTOM_BORDER;
                
                dealerTable.AddCell(dealerCell);



                document.Add(dealerTable);

                document.Add(new iTextSharp.text.Paragraph("\n"));
                document.Add(new iTextSharp.text.Paragraph("\n"));

                ////////////////////////////////////
                //Chunk linebreak1 = new Chunk(new LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1));//من أجل إضافة سطر فاصل
                //document.Add(linebreak1);
                //document.Add(new Paragraph("\n"));// new line
                ////////////////////////////////////

                // Create table for billing details with RTL direction
                PdfPTable table2;
                if (billing.CoolingRooms)
                {
                    table2= new PdfPTable(6);
                    table2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    table2.SetWidths(new float[] { 3, 2, 2, 2, 2, 2 });
                    table2.AddCell(new PdfPCell(new Phrase("التاريخ", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("النوع", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("العدد", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("الوزن", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                   
                    table2.AddCell(new PdfPCell(new Phrase("كلفة الكيلو", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("المجموع", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });

                }
                else if (billing.Refrigerator)
                {
                    table2 = new PdfPTable(7);
                    table2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    table2.SetWidths(new float[] { 3, 2, 2, 2, 2, 2,2 });
                    table2.AddCell(new PdfPCell(new Phrase("التاريخ", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("عدد الطبالي الكلي", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("وزن طبالي", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("قائم", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("صافي", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("السعر الاجمالي", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY,Colspan=2 });
                    

                }
                else if (billing.ExternalEnvoices)
                {
                    table2 = new PdfPTable(6);
                    table2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    table2.SetWidths(new float[] { 3, 2, 2, 2, 2, 2 });
                    table2.AddCell(new PdfPCell(new Phrase("التاريخ", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("النوع", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("العدد", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("الوزن", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    
                    table2.AddCell(new PdfPCell(new Phrase("السعر الافرادي", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("المجموع", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });

                }
                else if (billing.Daily)
                {
                    if(billing.SupplierId!=0)
                    {
                        table2 = new PdfPTable(8);
                        table2.SetWidths(new float[] { 3, 2, 2, 2, 2, 2,2,2 });

                        table2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                        table2.AddCell(new PdfPCell(new Phrase("التاريخ", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                        table2.AddCell(new PdfPCell(new Phrase("المزارع", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                        table2.AddCell(new PdfPCell(new Phrase("النوع", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                        table2.AddCell(new PdfPCell(new Phrase("العدد", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                        table2.AddCell(new PdfPCell(new Phrase("قائم", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                        table2.AddCell(new PdfPCell(new Phrase("صافي", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                        table2.AddCell(new PdfPCell(new Phrase("كلفة القص للكيلو", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                        table2.AddCell(new PdfPCell(new Phrase("إجمالي كلفة القص", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });

                    }
                    else
                    {
                        table2 = new PdfPTable(7);
                        table2.SetWidths(new float[] { 3, 2, 2, 2, 2, 2, 2 });

                        table2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                        table2.AddCell(new PdfPCell(new Phrase("التاريخ", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                        table2.AddCell(new PdfPCell(new Phrase("النوع", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                        table2.AddCell(new PdfPCell(new Phrase("العدد", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                        table2.AddCell(new PdfPCell(new Phrase("قائم", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                        table2.AddCell(new PdfPCell(new Phrase("صافي", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                        table2.AddCell(new PdfPCell(new Phrase("السعر الافرادي", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                        table2.AddCell(new PdfPCell(new Phrase("المجموع", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });

                    }


                }
                else if (billing.Tabali)
                {
                    table2 = new PdfPTable(5);
                    table2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    table2.SetWidths(new float[] { 3, 2, 2, 2, 2 });
                    table2.AddCell(new PdfPCell(new Phrase("التاريخ", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("النوع", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("العدد", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("السعر الافرادي", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("السعر الاجمالي", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                   
                }
                else if (billing.Plastic)
                {
                    table2 = new PdfPTable(5);
                    table2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    table2.SetWidths(new float[] { 3, 2, 2, 2, 2 });
                    table2.AddCell(new PdfPCell(new Phrase("التاريخ", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("النوع", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("العدد", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("السعر الافرادي", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("السعر الاجمالي", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });

                }
                else if (billing.Karasta)
                {
                    table2 = new PdfPTable(5);
                    table2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    table2.SetWidths(new float[] { 3, 2, 2, 2, 2 });
                    table2.AddCell(new PdfPCell(new Phrase("التاريخ", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("النوع", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("العدد", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("السعر الافرادي", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("السعر الاجمالي", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });

                }
                else if (billing.Cars)
                {
                    table2 = new PdfPTable(4);
                    table2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    table2.SetWidths(new float[] { 3, 2, 2, 2 });
                    table2.AddCell(new PdfPCell(new Phrase("التاريخ", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                   
                    table2.AddCell(new PdfPCell(new Phrase("عدد النقلات", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("الكلفة الافرادية", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("الكلفة الاجمالية", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });

                }
                else if (billing.Fuel)
                {
                    table2 = new PdfPTable(5);
                    table2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    table2.SetWidths(new float[] { 3, 2, 2, 2, 2 });
                    table2.AddCell(new PdfPCell(new Phrase("التاريخ", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("النوع", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("الكمية", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("السعر الافرادي", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("السعر الاجمالي", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });

                }
                else if (billing.Employees)
                {
                    table2 = new PdfPTable(9);
                    table2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    table2.SetWidths(new float[] { 3, 2, 2, 2, 2,2,2,2,3 });
                    table2.AddCell(new PdfPCell(new Phrase("التاريخ", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("عدد الشباب", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("عدد البنات", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("ساعات العمل النظامي", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("ساعات العمل الإضافي", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("الأجر اليومي", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("أجر السرفيس", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("الحسم", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                    table2.AddCell(new PdfPCell(new Phrase("الأجر الناتج", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });


                }
                else
                {
                    table2 = new PdfPTable(6);
                }


                table2.WidthPercentage = 100;


                // Add table headers

                // Add billing details with RTL direction
                if (billing.Employees)
                {
                    foreach (var detail in billing.DailyChekEmployeesDtos)
                    {
                        if (!(detail.MenCount == 0 && detail.GirlsCount == 0
                            && detail.NormJobHCount == 0 && detail.AddJobHCount == 0
                            && detail.TotalWage == 0 && detail.Reward == 0 && detail.Discount == 0
                            && detail.ResultWage == 0))
                        {

                            table2.AddCell(new PdfPCell(new Phrase(detail.Date.ToString("dd/MM/yyyy"), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                            table2.AddCell(new PdfPCell(new Phrase(detail.MenCount.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                            table2.AddCell(new PdfPCell(new Phrase(detail.GirlsCount.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });

                            table2.AddCell(new PdfPCell(new Phrase(detail.NormJobHCount.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                            table2.AddCell(new PdfPCell(new Phrase(detail.AddJobHCount.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                            table2.AddCell(new PdfPCell(new Phrase(detail.TotalWage.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                            table2.AddCell(new PdfPCell(new Phrase(detail.Reward.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                            table2.AddCell(new PdfPCell(new Phrase(detail.Discount.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });

                            table2.AddCell(new PdfPCell(new Phrase(detail.ResultWage.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                        }
                    }
                }
                else
                {
                    if(billing.Refrigerator)
                    {
                        foreach (var detail in billing.RefrigeratorDtos)
                        {
                            if (billing.Refrigerator)
                            {
                                table2.AddCell(new PdfPCell(new Phrase(detail.Date.ToString("dd/MM/yyyy"), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.TotalBoxes.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.TotalEmptyBoxesWeight.ToString()+" كغ", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.TotalBalanceCardWeight.ToString() + " كغ", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.TotalWeightAfterDiscount_2Percent.ToString() + " كغ", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.TotalSalesPriceOfAll.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4,Colspan=2 });

                            }
                        }
                        if(billing.RefrigeratorDtos.Count()==1)
                        {
                            table2.AddCell(new PdfPCell(new Phrase("محتويات البراد", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, 
                                          BackgroundColor= BaseColor.LIGHT_GRAY, BorderWidthTop=2, Colspan=7});
                            
                            table2.AddCell(new PdfPCell(new Phrase("المادة", boldFont)){ HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                            table2.AddCell(new PdfPCell(new Phrase("عدد الطبالي", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                            table2.AddCell(new PdfPCell(new Phrase("وزن الطبلية", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                            table2.AddCell(new PdfPCell(new Phrase("قائم", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                            table2.AddCell(new PdfPCell(new Phrase("صافي", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                            table2.AddCell(new PdfPCell(new Phrase("سعر الكيلو", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                            table2.AddCell(new PdfPCell(new Phrase("السعر الاجمالي", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });

                            foreach (var detail in billing.RefrigeratorDtos.FirstOrDefault().RefrigeratorDetailsDtos)
                            {
                                if (billing.Refrigerator)
                                {
                                    table2.AddCell(new PdfPCell(new Phrase(detail.MaterialName.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                    table2.AddCell(new PdfPCell(new Phrase(detail.CountOfBoxes.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                    table2.AddCell(new PdfPCell(new Phrase(detail.EmptyBoxesWeight.ToString() + " كغ", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                    table2.AddCell(new PdfPCell(new Phrase(detail.BalanceCardWeight.ToString() + " كغ", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                    table2.AddCell(new PdfPCell(new Phrase(detail.WeightAfterDiscount_2Percent.ToString() + " كغ", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                    table2.AddCell(new PdfPCell(new Phrase(detail.SalesPriceOfUnit.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                    table2.AddCell(new PdfPCell(new Phrase(detail.SalesPriceOfAll.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4});

                                }
                            }

                        }
                    }
                    else
                    {
                        foreach (var detail in billing.DetailsBillingDto)
                        {
                            if (billing.CoolingRooms)
                            {
                                table2.AddCell(new PdfPCell(new Phrase(detail.Date.ToString("dd/MM/yyyy"), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.Statement.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.Count.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.WeightAfterDiscount_2Percent.ToString() + " كغ", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.UnitPrice.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.TotalPrice.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });

                            }
                            else if (billing.ExternalEnvoices)
                            {
                                table2.AddCell(new PdfPCell(new Phrase(detail.Date.ToString("dd/MM/yyyy"), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.Statement.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.Count.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.WeightAfterDiscount_2Percent.ToString() + " كغ", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.UnitPrice.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.TotalPrice.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });

                            }
                            else if (billing.Daily)
                            {
                                if (billing.SupplierId != 0)
                                {
                                    table2.AddCell(new PdfPCell(new Phrase(detail.Date.ToString("dd/MM/yyyy"), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                    table2.AddCell(new PdfPCell(new Phrase(detail.DelearName.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.LIGHT_GRAY });
                                    table2.AddCell(new PdfPCell(new Phrase(detail.Statement.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                    table2.AddCell(new PdfPCell(new Phrase(detail.Count.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                    table2.AddCell(new PdfPCell(new Phrase(detail.BalanceCardWeight.ToString("C", nfi) + " كغ", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                    table2.AddCell(new PdfPCell(new Phrase(detail.WeightAfterDiscount_2Percent.ToString("C", nfi) + " كغ", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                    table2.AddCell(new PdfPCell(new Phrase(detail.CuttingCostOfUnit.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                    table2.AddCell(new PdfPCell(new Phrase(detail.CuttingCostOfAll.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                }
                                else
                                {
                                    table2.AddCell(new PdfPCell(new Phrase(detail.Date.ToString("dd/MM/yyyy"), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                    table2.AddCell(new PdfPCell(new Phrase(detail.Statement.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                    table2.AddCell(new PdfPCell(new Phrase(detail.Count.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                    table2.AddCell(new PdfPCell(new Phrase(detail.BalanceCardWeight.ToString("C", nfi) + " كغ", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                    table2.AddCell(new PdfPCell(new Phrase(detail.WeightAfterDiscount_2Percent.ToString("C", nfi) + " كغ", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                    table2.AddCell(new PdfPCell(new Phrase(detail.UnitPrice.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                    table2.AddCell(new PdfPCell(new Phrase(detail.TotalPrice.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                }
                                
                               
                            }
                            else if (billing.Tabali)
                            {
                                table2.AddCell(new PdfPCell(new Phrase(detail.Date.ToString("dd/MM/yyyy"), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.Statement.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.Count.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });

                                table2.AddCell(new PdfPCell(new Phrase(detail.UnitPrice.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.TotalPrice.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                            }
                            else if (billing.Plastic)
                            {
                                table2.AddCell(new PdfPCell(new Phrase(detail.Date.ToString("dd/MM/yyyy"), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.Statement.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.Count.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });

                                table2.AddCell(new PdfPCell(new Phrase(detail.UnitPrice.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.TotalPrice.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                            }
                            else if (billing.Karasta)
                            {
                                table2.AddCell(new PdfPCell(new Phrase(detail.Date.ToString("dd/MM/yyyy"), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.Statement.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.Count.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });

                                table2.AddCell(new PdfPCell(new Phrase(detail.UnitPrice.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.TotalPrice.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                            }
                            else if (billing.Cars)
                            {
                                table2.AddCell(new PdfPCell(new Phrase(detail.Date.ToString("dd/MM/yyyy"), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });

                                table2.AddCell(new PdfPCell(new Phrase(detail.Count.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });

                                table2.AddCell(new PdfPCell(new Phrase(detail.UnitPrice.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.TotalPrice.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                            }
                            else if (billing.Fuel)
                            {
                                table2.AddCell(new PdfPCell(new Phrase(detail.Date.ToString("dd/MM/yyyy"), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.Statement.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.Count.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });

                                table2.AddCell(new PdfPCell(new Phrase(detail.UnitPrice.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                                table2.AddCell(new PdfPCell(new Phrase(detail.TotalPrice.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4 });
                            }

                        }
                    }                 

                   
                }
                List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Syria));

                if (!(billing.CoolingRooms || billing.Refrigerator || billing.ExternalEnvoices))
                {
                    if (billing.Daily)
                    {
                        if (billing.Daily && (billing.SupplierId != 0))
                        {
                            table2.AddCell(new PdfPCell(new Phrase("المجموع", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.GRAY, Colspan = 3 });
                            table2.AddCell(new PdfPCell(new Phrase(billing.TotalBoxes.ToString(), boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.GRAY });
                            table2.AddCell(new PdfPCell(new Phrase(billing.TotalBalanceCardWeight.ToString()+" كغ", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.GRAY });
                            table2.AddCell(new PdfPCell(new Phrase(billing.WeightAfterDiscount_2Percent.ToString() + " كغ", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.GRAY });
                            table2.AddCell(new PdfPCell(new Phrase(billing.TotalCuttingCostOfAll.ToString("C", nfi) + " ل.س", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.GRAY, Colspan = 2 });

                            if (billing.DealerId == 0)
                            {
                                if (billing.PaymentsBillingDtos.Any())
                                {
                                    table2.AddCell(new PdfPCell(new Phrase("الدفعات", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 8 });

                                    table2.AddCell(new PdfPCell(new Phrase("التاريخ", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 1 });
                                    table2.AddCell(new PdfPCell(new Phrase("قيمة الدفعة", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 7 });


                                    foreach (var singlePayment in billing.PaymentsBillingDtos)
                                    {
                                        table2.AddCell(new PdfPCell(new Phrase(singlePayment.Date.ToString("dd/MM/yyyy"), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                                        table2.AddCell(new PdfPCell(new Phrase(singlePayment.Payment.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 7 });
                                    }

                                }
                                table2.AddCell(new PdfPCell(new Phrase("مجموع الدفعات", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                                table2.AddCell(new PdfPCell(new Phrase(billing.TotalPayments.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 7 });

                                table2.AddCell(new PdfPCell(new Phrase("المتبقي", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                                table2.AddCell(new PdfPCell(new Phrase(billing.Remainder.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 7 });



                                if (billing.Remainder_Past != 0)
                                {
                                    var Remainder_sum = billing.Remainder + billing.Remainder_Past;
                                    table2.AddCell(new PdfPCell(new Phrase("المتبقي من تاريخ سابق", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                                    table2.AddCell(new PdfPCell(new Phrase(billing.Remainder_Past.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 7 });

                                    table2.AddCell(new PdfPCell(new Phrase("مجموع المتبقي", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                                    table2.AddCell(new PdfPCell(new Phrase(Remainder_sum.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 7 });

                                    ToWord toWord = new ToWord(Convert.ToDecimal(Remainder_sum.ToString()), currencies[0]);
                                    string Numberword = toWord.ConvertToArabic();

                                    table2.AddCell(new PdfPCell(new Phrase("المتبقي كتابة", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                                    table2.AddCell(new PdfPCell(new Phrase(Numberword, normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 6 });
                                }
                                else
                                {
                                    ToWord toWord = new ToWord(Convert.ToDecimal(billing.Remainder.ToString()), currencies[0]);
                                    string Numberword = toWord.ConvertToArabic();

                                    table2.AddCell(new PdfPCell(new Phrase("المتبقي كتابة", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                                    table2.AddCell(new PdfPCell(new Phrase(Numberword, normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 6 });
                                }

                            }

                        }
                        //if (billing.SupplierId != 0)
                        //{
                        //    table2.AddCell(new PdfPCell(new Phrase("المجموع", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 1 });
                        //    table2.AddCell(new PdfPCell(new Phrase(billing.Total.ToString("C", nfi) + " ل.س", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 7 });

                        //    ToWord toWord = new ToWord(Convert.ToDecimal(billing.Total.ToString()), currencies[0]);
                        //    string Numberword = toWord.ConvertToArabic();
                        //    table2.AddCell(new PdfPCell(new Phrase("المجموع كتابة", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 1 });
                        //    table2.AddCell(new PdfPCell(new Phrase(Numberword, boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 7 });
                        //}
                        else
                        {
                            PdfPCell totalCell_domainDate = new PdfPCell(new Phrase("المجموع", boldFont));
                            totalCell_domainDate.HorizontalAlignment = Element.ALIGN_CENTER;
                            totalCell_domainDate.Padding = 5;
                            PdfPCell totalresCell_domainDate = new PdfPCell(new Phrase(billing.Total.ToString("C", nfi) + " ل.س", boldFont));
                            totalresCell_domainDate.Colspan = 6;
                            totalresCell_domainDate.HorizontalAlignment = Element.ALIGN_CENTER;
                            totalresCell_domainDate.Padding = 5;

                            table2.AddCell(totalCell_domainDate);
                            table2.AddCell(totalresCell_domainDate);

                            if (billing.PaymentsBillingDtos.Any())
                            {
                                table2.AddCell(new PdfPCell(new Phrase("الدفعات", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 7 });

                                table2.AddCell(new PdfPCell(new Phrase("التاريخ", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 1 });
                                table2.AddCell(new PdfPCell(new Phrase("قيمة الدفعة", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 6 });


                                foreach (var singlePayment in billing.PaymentsBillingDtos)
                                {
                                    table2.AddCell(new PdfPCell(new Phrase(singlePayment.Date.ToString("dd/MM/yyyy"), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                                    table2.AddCell(new PdfPCell(new Phrase(singlePayment.Payment.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 6 });
                                }

                            }
                            table2.AddCell(new PdfPCell(new Phrase("مجموع الدفعات", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                            table2.AddCell(new PdfPCell(new Phrase(billing.TotalPayments.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 6 });

                            table2.AddCell(new PdfPCell(new Phrase("المتبقي", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                            table2.AddCell(new PdfPCell(new Phrase(billing.Remainder.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 6 });
                           
                           

                            if (billing.Remainder_Past!=0)
                            {
                                var Remainder_sum = billing.Remainder + billing.Remainder_Past;
                                table2.AddCell(new PdfPCell(new Phrase("المتبقي من تاريخ سابق", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                                table2.AddCell(new PdfPCell(new Phrase(billing.Remainder_Past.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 6 });

                                table2.AddCell(new PdfPCell(new Phrase("مجموع المتبقي", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                                table2.AddCell(new PdfPCell(new Phrase(Remainder_sum.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 6 });

                                ToWord toWord = new ToWord(Convert.ToDecimal(Remainder_sum.ToString()), currencies[0]);
                                string Numberword = toWord.ConvertToArabic();

                                table2.AddCell(new PdfPCell(new Phrase("المتبقي كتابة", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                                table2.AddCell(new PdfPCell(new Phrase(Numberword, normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 6 });
                            }
                            else
                            {
                                ToWord toWord = new ToWord(Convert.ToDecimal(billing.Remainder.ToString()), currencies[0]);
                                string Numberword = toWord.ConvertToArabic();

                                table2.AddCell(new PdfPCell(new Phrase("المتبقي كتابة", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                                table2.AddCell(new PdfPCell(new Phrase(Numberword, normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 6 });
                            }

                           

                            
                        }
                    }
                    else if (billing.Tabali)
                    {
                        PdfPCell totalCell_domainDate = new PdfPCell(new Phrase("المجموع", boldFont));
                        totalCell_domainDate.HorizontalAlignment = Element.ALIGN_CENTER;
                        totalCell_domainDate.Padding = 5;
                        PdfPCell totalresCell_domainDate = new PdfPCell(new Phrase(billing.Total.ToString("C", nfi) + " ل.س", boldFont));
                        totalresCell_domainDate.Colspan = 4;
                        totalresCell_domainDate.HorizontalAlignment = Element.ALIGN_CENTER;
                        totalresCell_domainDate.Padding = 5;

                        table2.AddCell(totalCell_domainDate);
                        table2.AddCell(totalresCell_domainDate);

                        if (billing.PaymentsBillingDtos.Any())
                        {
                            table2.AddCell(new PdfPCell(new Phrase("الدفعات", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 5 });

                            table2.AddCell(new PdfPCell(new Phrase("التاريخ", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 1 });
                            table2.AddCell(new PdfPCell(new Phrase("قيمة الدفعة", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 4 });


                            foreach (var singlePayment in billing.PaymentsBillingDtos)
                            {
                                table2.AddCell(new PdfPCell(new Phrase(singlePayment.Date.ToString("dd/MM/yyyy"), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                                table2.AddCell(new PdfPCell(new Phrase(singlePayment.Payment.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 4 });
                            }

                        }
                        table2.AddCell(new PdfPCell(new Phrase("مجموع الدفعات", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                        table2.AddCell(new PdfPCell(new Phrase(billing.TotalPayments.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 4 });

                        table2.AddCell(new PdfPCell(new Phrase("المتبقي", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                        table2.AddCell(new PdfPCell(new Phrase(billing.Remainder.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 4 });

                        if (billing.Remainder_Past != 0)
                        {
                            var Remainder_sum = billing.Remainder + billing.Remainder_Past;
                            table2.AddCell(new PdfPCell(new Phrase("المتبقي من تاريخ سابق", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                            table2.AddCell(new PdfPCell(new Phrase(billing.Remainder_Past.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 6 });

                            table2.AddCell(new PdfPCell(new Phrase("مجموع المتبقي", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                            table2.AddCell(new PdfPCell(new Phrase(Remainder_sum.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 6 });

                            ToWord toWord = new ToWord(Convert.ToDecimal(Remainder_sum.ToString()), currencies[0]);
                            string Numberword = toWord.ConvertToArabic();

                            table2.AddCell(new PdfPCell(new Phrase("المتبقي كتابة", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                            table2.AddCell(new PdfPCell(new Phrase(Numberword, normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 6 });
                        }
                        else
                        {
                            ToWord toWord = new ToWord(Convert.ToDecimal(billing.Remainder.ToString()), currencies[0]);
                            string Numberword = toWord.ConvertToArabic();

                            table2.AddCell(new PdfPCell(new Phrase("المتبقي كتابة", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                            table2.AddCell(new PdfPCell(new Phrase(Numberword, normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 6 });
                        }

                    }
                    else if (billing.Plastic)
                    {
                        PdfPCell totalCell_domainDate = new PdfPCell(new Phrase("المجموع", boldFont));
                        totalCell_domainDate.HorizontalAlignment = Element.ALIGN_CENTER;
                        totalCell_domainDate.Padding = 5;
                        PdfPCell totalresCell_domainDate = new PdfPCell(new Phrase(billing.Total.ToString("C", nfi) + " ل.س", boldFont));
                        totalresCell_domainDate.Colspan = 4;
                        totalresCell_domainDate.HorizontalAlignment = Element.ALIGN_CENTER;
                        totalresCell_domainDate.Padding = 5;

                        table2.AddCell(totalCell_domainDate);
                        table2.AddCell(totalresCell_domainDate);

                        if (billing.PaymentsBillingDtos.Any())
                        {
                            table2.AddCell(new PdfPCell(new Phrase("الدفعات", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 5 });

                            table2.AddCell(new PdfPCell(new Phrase("التاريخ", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 1 });
                            table2.AddCell(new PdfPCell(new Phrase("قيمة الدفعة", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 4 });


                            foreach (var singlePayment in billing.PaymentsBillingDtos)
                            {
                                table2.AddCell(new PdfPCell(new Phrase(singlePayment.Date.ToString("dd/MM/yyyy"), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                                table2.AddCell(new PdfPCell(new Phrase(singlePayment.Payment.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 4 });
                            }

                        }
                        table2.AddCell(new PdfPCell(new Phrase("مجموع الدفعات", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                        table2.AddCell(new PdfPCell(new Phrase(billing.TotalPayments.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 4 });

                        table2.AddCell(new PdfPCell(new Phrase("المتبقي", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                        table2.AddCell(new PdfPCell(new Phrase(billing.Remainder.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 4 });

                        if (billing.Remainder_Past !=0)
                        {
                            var Remainder_sum = billing.Remainder + billing.Remainder_Past;
                            table2.AddCell(new PdfPCell(new Phrase("المتبقي من تاريخ سابق", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                            table2.AddCell(new PdfPCell(new Phrase(billing.Remainder_Past.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 6 });

                            table2.AddCell(new PdfPCell(new Phrase("مجموع المتبقي", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                            table2.AddCell(new PdfPCell(new Phrase(Remainder_sum.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 6 });

                            ToWord toWord = new ToWord(Convert.ToDecimal(Remainder_sum.ToString()), currencies[0]);
                            string Numberword = toWord.ConvertToArabic();

                            table2.AddCell(new PdfPCell(new Phrase("المتبقي كتابة", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                            table2.AddCell(new PdfPCell(new Phrase(Numberword, normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 6 });
                        }
                        else
                        {
                            ToWord toWord = new ToWord(Convert.ToDecimal(billing.Remainder.ToString()), currencies[0]);
                            string Numberword = toWord.ConvertToArabic();

                            table2.AddCell(new PdfPCell(new Phrase("المتبقي كتابة", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                            table2.AddCell(new PdfPCell(new Phrase(Numberword, normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 6 });
                        }


                    }
                    else if (billing.Karasta)
                    {
                        PdfPCell totalCell_domainDate = new PdfPCell(new Phrase("المجموع", boldFont));
                        totalCell_domainDate.HorizontalAlignment = Element.ALIGN_CENTER;
                        totalCell_domainDate.Padding = 5;
                        PdfPCell totalresCell_domainDate = new PdfPCell(new Phrase(billing.Total.ToString("C", nfi) + " ل.س", boldFont));
                        totalresCell_domainDate.Colspan = 4;
                        totalresCell_domainDate.HorizontalAlignment = Element.ALIGN_CENTER;
                        totalresCell_domainDate.Padding = 5;

                        table2.AddCell(totalCell_domainDate);
                        table2.AddCell(totalresCell_domainDate);

                        if (billing.PaymentsBillingDtos.Any())
                        {
                            table2.AddCell(new PdfPCell(new Phrase("الدفعات", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 5 });

                            table2.AddCell(new PdfPCell(new Phrase("التاريخ", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 1 });
                            table2.AddCell(new PdfPCell(new Phrase("قيمة الدفعة", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 4 });


                            foreach (var singlePayment in billing.PaymentsBillingDtos)
                            {
                                table2.AddCell(new PdfPCell(new Phrase(singlePayment.Date.ToString("dd/MM/yyyy"), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                                table2.AddCell(new PdfPCell(new Phrase(singlePayment.Payment.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 4 });
                            }

                        }
                        table2.AddCell(new PdfPCell(new Phrase("مجموع الدفعات", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                        table2.AddCell(new PdfPCell(new Phrase(billing.TotalPayments.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 4 });

                        table2.AddCell(new PdfPCell(new Phrase("المتبقي", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                        table2.AddCell(new PdfPCell(new Phrase(billing.Remainder.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 4 });

                        if (billing.Remainder_Past != 0)
                        {
                            var Remainder_sum = billing.Remainder + billing.Remainder_Past;
                            table2.AddCell(new PdfPCell(new Phrase("المتبقي من تاريخ سابق", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                            table2.AddCell(new PdfPCell(new Phrase(billing.Remainder_Past.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 6 });

                            table2.AddCell(new PdfPCell(new Phrase("مجموع المتبقي", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                            table2.AddCell(new PdfPCell(new Phrase(Remainder_sum.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 6 });

                            ToWord toWord = new ToWord(Convert.ToDecimal(Remainder_sum.ToString()), currencies[0]);
                            string Numberword = toWord.ConvertToArabic();

                            table2.AddCell(new PdfPCell(new Phrase("المتبقي كتابة", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                            table2.AddCell(new PdfPCell(new Phrase(Numberword, normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 6 });
                        }
                        else
                        {
                            ToWord toWord = new ToWord(Convert.ToDecimal(billing.Remainder.ToString()), currencies[0]);
                            string Numberword = toWord.ConvertToArabic();

                            table2.AddCell(new PdfPCell(new Phrase("المتبقي كتابة", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                            table2.AddCell(new PdfPCell(new Phrase(Numberword, normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 6 });
                        }

                    }
                    else if (billing.Cars)
                    {
                        PdfPCell totalCell_domainDate = new PdfPCell(new Phrase("المجموع", boldFont));
                        totalCell_domainDate.HorizontalAlignment = Element.ALIGN_CENTER;
                        totalCell_domainDate.Padding = 5;
                        PdfPCell totalresCell_domainDate = new PdfPCell(new Phrase(billing.Total.ToString("C", nfi) + " ل.س", boldFont));
                        totalresCell_domainDate.Colspan = 3;
                        totalresCell_domainDate.HorizontalAlignment = Element.ALIGN_CENTER;
                        totalresCell_domainDate.Padding = 5;

                        table2.AddCell(totalCell_domainDate);
                        table2.AddCell(totalresCell_domainDate);

                        if (billing.PaymentsBillingDtos.Any())
                        {
                            table2.AddCell(new PdfPCell(new Phrase("الدفعات", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 4 });

                            table2.AddCell(new PdfPCell(new Phrase("التاريخ", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 1 });
                            table2.AddCell(new PdfPCell(new Phrase("قيمة الدفعة", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 3 });


                            foreach (var singlePayment in billing.PaymentsBillingDtos)
                            {
                                table2.AddCell(new PdfPCell(new Phrase(singlePayment.Date.ToString("dd/MM/yyyy"), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                                table2.AddCell(new PdfPCell(new Phrase(singlePayment.Payment.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 3 });
                            }

                        }
                        table2.AddCell(new PdfPCell(new Phrase("مجموع الدفعات", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                        table2.AddCell(new PdfPCell(new Phrase(billing.TotalPayments.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 3 });

                        table2.AddCell(new PdfPCell(new Phrase("المتبقي", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                        table2.AddCell(new PdfPCell(new Phrase(billing.Remainder.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 3 });

                        ToWord toWord = new ToWord(Convert.ToDecimal(billing.Remainder.ToString()), currencies[0]);
                        string Numberword = toWord.ConvertToArabic();

                        table2.AddCell(new PdfPCell(new Phrase("المتبقي كتابة", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                        table2.AddCell(new PdfPCell(new Phrase(Numberword, normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 3 });

                    }
                    else if (billing.Fuel)
                    {
                        PdfPCell totalCell_domainDate = new PdfPCell(new Phrase("المجموع", boldFont));
                        totalCell_domainDate.HorizontalAlignment = Element.ALIGN_CENTER;
                        totalCell_domainDate.Padding = 5;
                        PdfPCell totalresCell_domainDate = new PdfPCell(new Phrase(billing.Total.ToString("C", nfi) + " ل.س", boldFont));
                        totalresCell_domainDate.Colspan = 4;
                        totalresCell_domainDate.HorizontalAlignment = Element.ALIGN_CENTER;
                        totalresCell_domainDate.Padding = 5;

                        table2.AddCell(totalCell_domainDate);
                        table2.AddCell(totalresCell_domainDate);

                        if (billing.PaymentsBillingDtos.Any())
                        {
                            table2.AddCell(new PdfPCell(new Phrase("الدفعات", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 5 });

                            table2.AddCell(new PdfPCell(new Phrase("التاريخ", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 1 });
                            table2.AddCell(new PdfPCell(new Phrase("قيمة الدفعة", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 4 });


                            foreach (var singlePayment in billing.PaymentsBillingDtos)
                            {
                                table2.AddCell(new PdfPCell(new Phrase(singlePayment.Date.ToString("dd/MM/yyyy"), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                                table2.AddCell(new PdfPCell(new Phrase(singlePayment.Payment.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 4 });
                            }

                        }
                        table2.AddCell(new PdfPCell(new Phrase("مجموع الدفعات", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                        table2.AddCell(new PdfPCell(new Phrase(billing.TotalPayments.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 4 });

                        table2.AddCell(new PdfPCell(new Phrase("المتبقي", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                        table2.AddCell(new PdfPCell(new Phrase(billing.Remainder.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 4 });

                        ToWord toWord = new ToWord(Convert.ToDecimal(billing.Remainder.ToString()), currencies[0]);
                        string Numberword = toWord.ConvertToArabic();

                        table2.AddCell(new PdfPCell(new Phrase("المتبقي كتابة", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                        table2.AddCell(new PdfPCell(new Phrase(Numberword, normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 4 });

                    }
                    else if (billing.Employees)
                    {
                        PdfPCell totalCell_domainDate = new PdfPCell(new Phrase("المجموع", boldFont));
                        totalCell_domainDate.HorizontalAlignment = Element.ALIGN_CENTER;
                        totalCell_domainDate.Padding = 5;
                        PdfPCell totalresCell_domainDate = new PdfPCell(new Phrase(billing.Total.ToString("C", nfi) + " ل.س", boldFont));
                        totalresCell_domainDate.Colspan = 8;
                        totalresCell_domainDate.HorizontalAlignment = Element.ALIGN_CENTER;
                        totalresCell_domainDate.Padding = 5;

                        table2.AddCell(totalCell_domainDate);
                        table2.AddCell(totalresCell_domainDate);

                        if (billing.PaymentsBillingDtos.Any())
                        {
                            table2.AddCell(new PdfPCell(new Phrase("الدفعات", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 9, BorderWidthTop = 2 });

                            table2.AddCell(new PdfPCell(new Phrase("التاريخ", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 1 });
                            table2.AddCell(new PdfPCell(new Phrase("قيمة الدفعة", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, BackgroundColor = BaseColor.GRAY, Colspan = 8 });


                            foreach (var singlePayment in billing.PaymentsBillingDtos)
                            {
                                table2.AddCell(new PdfPCell(new Phrase(singlePayment.Date.ToString("dd/MM/yyyy"), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                                table2.AddCell(new PdfPCell(new Phrase(singlePayment.Payment.ToString("C", nfi) + " ل.س", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 8 });
                            }

                        }
                        table2.AddCell(new PdfPCell(new Phrase("مجموع الدفعات", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                        table2.AddCell(new PdfPCell(new Phrase(billing.TotalPayments.ToString("C", nfi) + " ل.س", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 8 });

                        table2.AddCell(new PdfPCell(new Phrase("المتبقي", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1, BorderWidthTop = 2 });
                        table2.AddCell(new PdfPCell(new Phrase(billing.Remainder.ToString("C", nfi) + " ل.س", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 8, BorderWidthTop = 2 });

                        ToWord toWord = new ToWord(Convert.ToDecimal(billing.Remainder.ToString()), currencies[0]);
                        string Numberword = toWord.ConvertToArabic();

                        table2.AddCell(new PdfPCell(new Phrase("المتبقي كتابة", boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 1 });
                        table2.AddCell(new PdfPCell(new Phrase(Numberword, boldFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4, Colspan = 8 });

                    }
                }
                else if ((!billing.Refrigerator) ||(billing.Refrigerator && billing.RefrigeratorDtos.Count() != 1))
                {
                    // Add total amount at the bottom with RTL direction
                    document.Add(new Paragraph("\n")); // Add space


                    ToWord toWord = new ToWord(Convert.ToDecimal(billing.Total.ToString()), currencies[0]);

                    string Numberword = toWord.ConvertToArabic();

                    PdfPCell totalCell = new PdfPCell(new Phrase("المجموع", boldFont));
                    PdfPCell totalresCell = new PdfPCell(new Phrase(billing.Total.ToString("C", nfi) + " ل.س", boldFont));
                    PdfPCell totalWrittenCell = new PdfPCell(new Phrase("المجموع كتابة", boldFont));
                    PdfPCell totalWrittenresCell = new PdfPCell(new Phrase(Numberword, boldFont));

                    if (billing.CoolingRooms)
                    {
                        totalresCell.Colspan = 5;
                        totalWrittenresCell.Colspan = 5;
                    }
                    else if (billing.Refrigerator)
                    {
                        totalresCell.Colspan = 6;
                        totalWrittenresCell.Colspan = 6;
                    }
                    else
                    {
                        totalresCell.Colspan = 5;
                        totalWrittenresCell.Colspan = 5;
                    }

                    totalCell.HorizontalAlignment = Element.ALIGN_CENTER;

                    totalCell.Padding = 5;
                    //totalCell.Border = Rectangle.NO_BORDER;
                    table2.AddCell(totalCell);


                    totalresCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //totalCell.Border = Rectangle.NO_BORDER;

                    totalresCell.Padding = 5;
                    table2.AddCell(totalresCell);


                    totalWrittenCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    totalWrittenCell.Padding = 5;
                    //totalCell.Border = Rectangle.NO_BORDER;
                    table2.AddCell(totalWrittenCell);


                    totalWrittenresCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //totalCell.Border = Rectangle.NO_BORDER;

                    totalWrittenresCell.Padding = 5;
                    table2.AddCell(totalWrittenresCell);
                    
                }

                document.Add(table2);

               

                // Add signature line with RTL direction
                document.Add(new Paragraph("\n\n")); // Add space

                PdfPTable signatureTable = new PdfPTable(1);
                signatureTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                signatureTable.WidthPercentage = 100;

                PdfPCell signatureCell = new PdfPCell(new Phrase("التوقيع", boldFont));
                signatureCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                signatureCell.Border = Rectangle.NO_BORDER;
                signatureCell.PaddingLeft = 50;
                signatureTable.AddCell(signatureCell);
                
                document.Add(signatureTable);

                document.Close();
                writer.Close();

                return memoryStream.ToArray();
            }
        }



    }

    public class PDFFooter : PdfPageEventHelper
    {
        // write on top of document
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            base.OnOpenDocument(writer, document);
            //PdfPTable tabFot = new PdfPTable(new double[] { 1F });
            //tabFot.SpacingAfter = 10F;
            //PdfPCell cell;
            //tabFot.TotalWidth = 300F;
            //cell = new PdfPCell(new Phrase("Header"));
            //tabFot.AddCell(cell);
            //tabFot.WriteSelectedRows(0, -1, 150, document.Top, writer.DirectContent);
        }

        // write on start of each page
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
        }

        // write on end of each page
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);


            PdfPTable tabFot = new PdfPTable(1);
            PdfPCell cell;
            tabFot.TotalWidth = 300F;
            tabFot.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
            string fontLoc = @"c:\windows\fonts\arial.ttf"; // make sure to have the correct path to the font file
            BaseFont bf = BaseFont.CreateFont(fontLoc, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            iTextSharp.text.Font f = new iTextSharp.text.Font(bf, 8, 1);
            Phrase text_arabic1 = new Phrase("(+963) 933 467 492" + "     (+7) 916 948-39-62" + "     smart-kiwi برمجة ", f);


            cell = new PdfPCell(text_arabic1);
            cell.Border = 0;
            cell.BorderWidthTop = 1;
            cell.PaddingLeft = 40;
            cell.PaddingBottom = -10;
            tabFot.AddCell(cell);

            tabFot.WriteSelectedRows(0, -1, 150, document.Bottom, writer.DirectContent);
        }

        //write on close of document
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
        }
    }
}
