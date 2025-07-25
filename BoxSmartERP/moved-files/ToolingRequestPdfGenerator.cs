using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;

public class ToolingRequestPdfGenerator
{
    public void GeneratePdf(string outputPath, ToolingRequestData data, List<ToolingRequestItem> items)
    {
        GlobalFontSettings.UseWindowsFontsUnderWindows = true;
        // Create a new PDF document
        PdfDocument document = new PdfDocument();
        document.Info.Title = "Tooling Requisition Form";

        // Add a page
        PdfPage page = document.AddPage();
        XGraphics gfx = XGraphics.FromPdfPage(page);

        System.Drawing.Text.PrivateFontCollection pfc = new System.Drawing.Text.PrivateFontCollection();
        pfc.AddFontFile(@"C:\Windows\Fonts\Arial.ttf"); // Explicit path


        // Set up fonts
        XFont titleFont = new XFont(pfc.Families[0].Name, 16, XFontStyleEx.Bold);
        XFont headerFont = new XFont(pfc.Families[0].Name, 12, XFontStyleEx.Bold);
        XFont normalFont = new XFont(pfc.Families[0].Name, 10, XFontStyleEx.Regular);
        XFont smallFont = new XFont(pfc.Families[0].Name, 8, XFontStyleEx.Regular);

        // Draw company header
        gfx.DrawString("STENIEL MINDANAO PACKAGING CORPORATION", titleFont, XBrushes.Black,
            new XRect(0, 40, page.Width, page.Height), XStringFormats.TopCenter);

        // Draw form title
        gfx.DrawString("REQUISITION FOR", headerFont, XBrushes.Black,
            new XRect(0, 70, page.Width, page.Height), XStringFormats.TopCenter);
        gfx.DrawString("RUBBER DIE / DIE CUT MOULD", headerFont, XBrushes.Black,
            new XRect(0, 90, page.Width, page.Height), XStringFormats.TopCenter);

        // Draw horizontal line
        XPen pen = new XPen(XColors.Black, 0.5);
        gfx.DrawLine(pen, 50, 110, page.Width - 50, 110);

        // Draw requisitioning department
        gfx.DrawString("REQUISITIONING DEPARTMENT: SALES / MARKETING DEPARTMENT", normalFont, XBrushes.Black,
            new XPoint(50, 130));

        // Draw item type checkboxes
        gfx.DrawString("ITEM TYPE:", normalFont, XBrushes.Black, new XPoint(50, 150));

        // Draw checkboxes based on data
        DrawCheckbox(gfx, 120, 150, data.RubberDie);
        gfx.DrawString("RUBBER DIE", normalFont, XBrushes.Black, new XPoint(130, 150));

        DrawCheckbox(gfx, 220, 150, data.DieCut);
        gfx.DrawString("DIE CUT MOULD", normalFont, XBrushes.Black, new XPoint(230, 150));

        DrawCheckbox(gfx, 350, 150, data.NegativeFilm);
        gfx.DrawString("NEGATIVE FILM", normalFont, XBrushes.Black, new XPoint(360, 150));

        // Create table for items
        double tableTop = 170;
        double col1 = 50;   // QTY
        double col2 = 90;   // UNIT
        double col3 = 140;  // DESCRIPTION
        double col4 = 400;  // REMARKS

        // Draw table headers
        gfx.DrawString("QTY", normalFont, XBrushes.Black, new XPoint(col1, tableTop));
        gfx.DrawString("UNIT", normalFont, XBrushes.Black, new XPoint(col2, tableTop));
        gfx.DrawString("DESCRIPTION", normalFont, XBrushes.Black, new XPoint(col3, tableTop));
        gfx.DrawString("REMARKS", normalFont, XBrushes.Black, new XPoint(col4, tableTop));

        // Draw horizontal line under headers
        gfx.DrawLine(pen, col1, tableTop + 5, col4 + 100, tableTop + 5);

        double currentY = tableTop + 20;

        // Group items by customer
        var itemsByCustomer = items.GroupBy(i => i.CustomerName);

        foreach (var customerGroup in itemsByCustomer)
        {
            // Draw customer name
            gfx.DrawString($"Customer: {customerGroup.Key}", normalFont, XBrushes.Black,
                new XPoint(col1, currentY));
            currentY += 15;

            // Draw items for this customer
            foreach (var item in customerGroup)
            {
                gfx.DrawString(item.Quantity.ToString(), normalFont, XBrushes.Black,
                    new XPoint(col1, currentY));
                gfx.DrawString(item.UomCode, normalFont, XBrushes.Black,
                    new XPoint(col2, currentY));

                string description = $"{item.NumOuts} {item.ItemDescription}";
                if (!string.IsNullOrEmpty(item.PrintCardNo) && item.PrintCardNo != "None")
                {
                    description += $" (PrintCard: {item.PrintCardNo})";
                }

                gfx.DrawString(description, normalFont, XBrushes.Black,
                    new XPoint(col3, currentY));
                gfx.DrawString(item.DesignNotes, normalFont, XBrushes.Black,
                    new XPoint(col4, currentY));

                currentY += 15;
            }

            // Add some space between customers
            currentY += 10;
        }

        // Draw control number and date at the bottom
        gfx.DrawString($"Control #: {data.RequisitionNumber}", normalFont, XBrushes.Black,
            new XPoint(50, page.Height - 120));
        gfx.DrawString($"Date: {DateTime.Now:yyyy-MM-dd}", normalFont, XBrushes.Black,
            new XPoint(50, page.Height - 100));

        // Draw signature sections
        double signatureY = page.Height - 80;
        double signatureWidth = 150;

        gfx.DrawString("Prepared by:", smallFont, XBrushes.Black, new XPoint(50, signatureY));
        gfx.DrawLine(pen, 50, signatureY + 15, 50 + signatureWidth, signatureY + 15);
        gfx.DrawString("SALES & MARKETING STAFF", smallFont, XBrushes.Black,
            new XPoint(50, signatureY + 25));

        gfx.DrawString("Reviewed by:", smallFont, XBrushes.Black, new XPoint(250, signatureY));
        gfx.DrawLine(pen, 250, signatureY + 15, 250 + signatureWidth, signatureY + 15);
        gfx.DrawString("SALES & MARKETING MANAGER / SALES ASSIST. MANAGER", smallFont, XBrushes.Black,
            new XPoint(250, signatureY + 25));

        signatureY += 50;

        gfx.DrawString("Recommending Approval:", smallFont, XBrushes.Black, new XPoint(50, signatureY));
        gfx.DrawLine(pen, 50, signatureY + 15, 50 + signatureWidth, signatureY + 15);
        gfx.DrawString("SR. PROCUREMENT & ADMIN MANAGER", smallFont, XBrushes.Black,
            new XPoint(50, signatureY + 25));

        gfx.DrawString("Received by:", smallFont, XBrushes.Black, new XPoint(250, signatureY));
        gfx.DrawLine(pen, 250, signatureY + 15, 250 + signatureWidth, signatureY + 15);
        gfx.DrawString("ASSISTANT GENERAL MANAGER", smallFont, XBrushes.Black,
            new XPoint(250, signatureY + 25));

        // Draw footer
        gfx.DrawString("CC: SALES / FINANCE / GRAPHICS / DESIGNING", smallFont, XBrushes.Black,
            new XPoint(50, page.Height - 20));
        gfx.DrawString("FM-SMPC 02-175 / Rev. 03/01 April 2024", smallFont, XBrushes.Black,
            new XPoint(page.Width - 200, page.Height - 20));

        // Save the document
        document.Save(outputPath);
    }

    private void DrawCheckbox(XGraphics gfx, double x, double y, bool isChecked)
    {
        gfx.DrawRectangle(XPens.Black, x, y - 10, 10, 10);
        if (isChecked)
        {
            gfx.DrawLine(XPens.Black, x, y - 10, x + 10, y);
            gfx.DrawLine(XPens.Black, x, y, x + 10, y - 10);
        }
    }
}

// Data model classes
public class ToolingRequestData
{
    public string RequisitionNumber { get; set; }
    public DateTime DueDate { get; set; }
    public bool RubberDie { get; set; }
    public bool DieCut { get; set; }
    public bool NegativeFilm { get; set; }
}

public class ToolingRequestItem
{
    public string CustomerName { get; set; }
    public int Quantity { get; set; }
    public string UomCode { get; set; }
    public string ItemDescription { get; set; }
    public string NumOuts { get; set; }
    public string PrintCardNo { get; set; }
    public string DesignNotes { get; set; }
}