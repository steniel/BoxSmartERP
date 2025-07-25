using BoxSmart_ERP.Services;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static BoxSmart_ERP.Services.RequisitionData;
using Image = MigraDoc.DocumentObjectModel.Shapes.Image;
using Orientation = MigraDoc.DocumentObjectModel.Orientation; // Added for .Last() in AddItemsTable

public class RequisitionDocumentGenerator
{
    private Document document;
    private const string FontName = "Arial"; // Or another common font like Calibri, Times New Roman

    public RequisitionDocumentGenerator()
    {
        document = new Document();
        document.Info.Title = "Requisition for Rubber Die / Die Cut Mould";
        document.Info.Author = "Steniel Mindanao Packaging Corporation"; // From PDF 

        DefineStyles();
    }

    public Document CreateDocument(RequisitionData data)
    {
        DefinePageSetup();
        AddHeader(data);
        AddSubHeader(data);
        AddItemsTable(data);
        AddSignatureSection(data);
        AddFooter(data);

        return document;
    }

    public void SavePdf(Document document, string filePath)
    {
        var renderer = new PdfDocumentRenderer(true);
        renderer.Document = document;
        renderer.RenderDocument();
        renderer.PdfDocument.Save(filePath);
    }

    private void DefineStyles()
    {
        Style style = document.Styles["Normal"];
        style.Font.Name = FontName;
        style.Font.Size = 9;

        style = document.Styles.AddStyle("Bold", "Normal");
        style.Font.Bold = true;

        style = document.Styles.AddStyle("SmallBold", "Normal");
        style.Font.Bold = true;
        style.Font.Size = 7;

        style = document.Styles.AddStyle("SmallNormal", "Normal");
        style.Font.Size = 7;

        style = document.Styles.AddStyle("TableHeader", "Normal");
        style.Font.Bold = true;
        style.ParagraphFormat.Alignment = ParagraphAlignment.Center;
        style.Font.Size = 8;
    }

    private void DefinePageSetup()
    {
        Section section = document.AddSection();
        section.PageSetup.PageFormat = PageFormat.Letter;
        section.PageSetup.Orientation = Orientation.Portrait;

        // Define margins.
        section.PageSetup.LeftMargin = Unit.FromCentimeter(2.0);
        section.PageSetup.RightMargin = Unit.FromCentimeter(2.0);
        section.PageSetup.TopMargin = Unit.FromCentimeter(2.0);
        section.PageSetup.BottomMargin = Unit.FromCentimeter(2.0);

        // Add the page border using a TextFrame in the header
        HeaderFooter header = section.Headers.Primary;
        TextFrame borderFrame = header.AddTextFrame();
        borderFrame.RelativeHorizontal = RelativeHorizontal.Page;
        borderFrame.RelativeVertical = RelativeVertical.Page;
        borderFrame.Left = ShapePosition.Left;
        borderFrame.Top = ShapePosition.Top;
        borderFrame.Width = document.LastSection.PageSetup.PageWidth - Unit.FromCentimeter(0.2);
        borderFrame.Height = document.LastSection.PageSetup.PageHeight - Unit.FromCentimeter(0.2);
        borderFrame.LineFormat.Width = 1;
        borderFrame.LineFormat.Color = Colors.Black;
        borderFrame.WrapFormat.Style = WrapStyle.None;

        // --- CORRECTED LINES FOR MAIN CONTENT COLUMNS ---
        // This is the correct way to define general text columns for the section's main content area.
        //section.PageSetup.TextColumns.Count = 2; // Sets the number of columns
        //section.PageSetup.TextColumns.Spacing = Unit.FromCentimeter(0.5); // Sets the spacing between columns
        //section.AddTable();
    }

    private void AddHeader(RequisitionData data)
    {
        Section section = document.LastSection;

         
         Image logo = section.AddImage("steniel-logo.png");
        logo.ScaleWidth = 0.2; // Scale to fit


        // Company Name 
         Paragraph p = section.AddParagraph("STENIEL MINDANAO PACKAGING CORPORATION", "Bold"); 
        p.Format.Alignment = ParagraphAlignment.Center;
        p.Format.Font.Size = 10;
        p.Format.SpaceAfter = Unit.FromMillimeter(2);

        // Requisition Title 
         p = section.AddParagraph("REQUISITION FOR", "Bold");
        p.Format.Alignment = ParagraphAlignment.Center;
        p.Format.Font.Size = 12;

         p = section.AddParagraph("RUBBER DIE / DIE CUT MOULD", "Bold"); 
        p.Format.Alignment = ParagraphAlignment.Center;
        p.Format.Font.Size = 14;
        p.Format.SpaceAfter = Unit.FromMillimeter(5);

        // Control Number and Date (aligned right)
        TextFrame headerRightFrame = section.AddTextFrame();
        headerRightFrame.RelativeHorizontal = RelativeHorizontal.Page;
        headerRightFrame.RelativeVertical = RelativeVertical.Page;
        headerRightFrame.Left = ShapePosition.Right;
        headerRightFrame.Top = Unit.FromCentimeter(1.5); // Adjust this top position to align with main content
        headerRightFrame.Width = Unit.FromCentimeter(6);
        headerRightFrame.Height = Unit.FromCentimeter(2);
        headerRightFrame.WrapFormat.Style = WrapStyle.Through;

         p = headerRightFrame.AddParagraph($"Control #: {data.ControlNumber}"); 
        p.Format.Alignment = ParagraphAlignment.Right;
        p.Format.Font.Size = 8;

         p = headerRightFrame.AddParagraph($"Date: {data.RequisitionDate.ToString("yyyy-MM-dd")}");
        p.Format.Alignment = ParagraphAlignment.Right;
        p.Format.Font.Size = 8;
    }

    private void AddSubHeader(RequisitionData data)
    {
        Section section = document.LastSection;

        Paragraph p = section.AddParagraph();
        p.AddText("REQUISITIONING DEPARTMENT: ");
        p.AddFormattedText(data.RequisitioningDepartment, TextFormat.Bold);
        p.Format.SpaceAfter = Unit.FromMillimeter(3);


        p = section.AddParagraph();
        p.AddText("ITEM TYPE: ");
        p.AddText(" ( ");
        if (data.IsRubberDie)
        {
            var check = p.AddFormattedText("✔");
            check.Font.Name = "Segoe UI Symbol";
        }
        p.AddText(") RUBBER DIE   ");

        p.AddText(" ( ");
        if (data.IsDieCutMould)
        {
            var check = p.AddFormattedText("✔");
            check.Font.Name = "Segoe UI Symbol";
        }
        p.AddText(") DIE CUT MOULD   ");

        p.AddText(" ( ");
        if (data.IsNegativeFilm)
        {
            var check = p.AddFormattedText("✔");
            check.Font.Name = "Segoe UI Symbol";
        }
        p.AddText(") NEGATIVE FILM");
        p.Format.SpaceAfter = Unit.FromMillimeter(5);
    }

    private void AddItemsTable(RequisitionData data)
    {
        Section section = document.LastSection;

        Table table = section.AddTable();
        table.Borders.Width = 0.5;
        table.Borders.Color = Colors.Black;
        table.Format.SpaceAfter = Unit.FromMillimeter(5);

        // Define columns (widths adjusted for new content, like num_outs)
        Column column = table.AddColumn(Unit.FromCentimeter(1.5)); // QTY 
        column.Format.Alignment = ParagraphAlignment.Center;
        column = table.AddColumn(Unit.FromCentimeter(2.5)); // UNIT  (increased for "2 Outs")
        column.Format.Alignment = ParagraphAlignment.Center;
        column = table.AddColumn(Unit.FromCentimeter(8.5)); // DESCRIPTION 
        column = table.AddColumn(Unit.FromCentimeter(4.0)); // REMARKS 

        // Add Table Header
        Row headerRow = table.AddRow();
        headerRow.Shading.Color = Colors.LightGray;
        headerRow.Format.Font.Bold = true;
        headerRow.Format.Alignment = ParagraphAlignment.Center;
        headerRow.VerticalAlignment = VerticalAlignment.Center;
        headerRow.Height = Unit.FromCentimeter(0.8);

         headerRow.Cells[0].AddParagraph("QTY"); 
         headerRow.Cells[1].AddParagraph("UNIT"); 
         headerRow.Cells[2].AddParagraph("DESCRIPTION"); 
         headerRow.Cells[3].AddParagraph("REMARKS");

        // Add Customer Sections and Items
        foreach (var customer in data.Customers)
        {
            // Customer Header Row
            Row customerHeaderRow = table.AddRow();
            customerHeaderRow.Cells[0].MergeRight = 3;
            customerHeaderRow.Cells[0].Borders.Top.Width = 0.5;
            customerHeaderRow.Cells[0].Borders.Bottom.Width = 0.5;
            customerHeaderRow.Cells[0].Format.Font.Size = 8;
            customerHeaderRow.Cells[0].Format.Font.Bold = true;
            customerHeaderRow.Cells[0].Format.Alignment = ParagraphAlignment.Left;
             customerHeaderRow.Cells[0].AddParagraph("CUSTOMER: " + customer.Name.ToUpper()); 

            foreach (var item in customer.Items)
            {
                Row itemRow = table.AddRow();
                itemRow.Format.Font.Size = 7;
                itemRow.VerticalAlignment = VerticalAlignment.Top;
                itemRow.Height = Unit.FromCentimeter(0.6);

                 itemRow.Cells[0].AddParagraph(item.Quantity);

                // Combine Unit and NumOuts for the Unit column
                 string unitText = item.Unit; 
                if (!string.IsNullOrEmpty(item.NumOuts))
                {
                     unitText += $" {item.NumOuts}"; 
                }
                itemRow.Cells[1].AddParagraph(unitText);

                 itemRow.Cells[2].AddParagraph(item.Description);

                // Combine Remarks and PrintcardNo for the Remarks column
                 string remarksText = item.Remarks;
                if (!string.IsNullOrEmpty(item.PrintcardNo) && item.PrintcardNo != "None")
                {
                    if (!string.IsNullOrEmpty(remarksText))
                    {
                        remarksText += Environment.NewLine; // Add a new line if both exist
                    }
                    remarksText += $"Printcard No: {item.PrintcardNo}";
                }
                itemRow.Cells[3].AddParagraph(remarksText);
            }
            // Add a blank row for spacing if needed between customers
            if (data.Customers.Count > 1 && customer != data.Customers.Last())
            {
                table.AddRow().Height = Unit.FromMillimeter(2); // Small blank row
            }
        }

        // "NOTHING FOLLOWS" is not in the updated sample, so setting ShowNothingFollows to false by default
        // If data.ShowNothingFollows is true, uncomment this block
        /*
        if (data.ShowNothingFollows)
        {
            Row nothingRow = table.AddRow();
            nothingRow.Cells[0].MergeRight = 3;
            nothingRow.Cells[0].Borders.Top.Width = 0.5;
            nothingRow.Cells[0].Format.Font.Bold = true;
            nothingRow.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            nothingRow.Cells[0].AddParagraph("******** NOTHING FOLLOWS ********");
        }
        */
    }

    private void AddSignatureSection(RequisitionData data)
    {
        Section section = document.LastSection;
        //section.AddColumnBreak(); // Advance to the next column for signatures
        //section.AddTextFrame(); // Advance to the next column for signatures

        // Adjusted spacing and content based on the new PDF sample
        AddSignatureBlock(section, "Prepared by:", data.PreparedBy); 
         AddSignatureBlock(section, "Reviewed by:", data.ReviewedBy); 
         AddSignatureBlock(section, "Recommending Approval:", data.RecommendingApproval);
         AddSignatureBlock(section, "Reviewed by:", data.ReviewedByProcurement); 
         AddSignatureBlock(section, "Received by:", data.ReceivedBy);

        // Assistant General Manager is now a regular signature block too
        AddSignatureBlock(section, "", data.AssistantGeneralManager); //  Empty label, name/title will follow
    }

    private void AddSignatureBlock(Section section, string label, SignaturePerson person)
    {
        if (person != null)
        {
            Paragraph p = section.AddParagraph();
            p.Format.SpaceBefore = Unit.FromCentimeter(0.5);
            if (!string.IsNullOrEmpty(label))
            {
                p.AddFormattedText(label, "SmallNormal");
                p.AddLineBreak(); // Blank line after label
            }
            p.AddLineBreak(); // Space for signature
            p.AddLineBreak(); // Another line break for spacing before name
            p.AddFormattedText(person.Name, "SmallBold");
            p.AddLineBreak();
            p.AddFormattedText(person.Title, "SmallNormal");
            p.Format.SpaceAfter = Unit.FromCentimeter(0.5);
        }
    }

    private void AddFooter(RequisitionData data)
    {
        Section section = document.LastSection;

        // Create a new HeaderFooter object for the footer
        HeaderFooter footer = section.Footers.Primary;

        // Add content to the footer
        Paragraph paragraph = footer.AddParagraph();
        paragraph.Format.Alignment = ParagraphAlignment.Left;
        paragraph.Format.Font.Size = 7;
        paragraph.Format.SpaceBefore = Unit.FromCentimeter(1);

        // CC: line
        paragraph.AddText("CC: SALES / FINANCE / GRAPHICS / DESIGNING");
        paragraph.AddLineBreak();
        paragraph.AddText($"{data.DocumentCode}/{data.DocumentRevision} {data.DocumentRevisionDate.ToString("MMMM yyyy")}");
    }
}