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
        DefinePageSetup(); // Setup page margins, etc. (no TextColumns here now)
        AddHeader(data);
        AddSubHeader(data);

        Section section = document.LastSection;

        // Create a main content table for the two-column layout
        Table mainContentLayoutTable = section.AddTable();
        mainContentLayoutTable.Borders.Visible = false; // This table is just for layout, no visible borders
        mainContentLayoutTable.Format.SpaceAfter = Unit.FromMillimeter(5); // Space after this whole section

        // Define the two columns for the main content area
        // Adjust widths (FromCentimeter) as needed to fit your content and desired spacing.
        // The sum of column widths should be less than (PageWidth - LeftMargin - RightMargin - SpacingBetweenColumns)
        mainContentLayoutTable.AddColumn(Unit.FromCentimeter(9.0));   // Left column for items table
        mainContentLayoutTable.AddColumn(Unit.FromCentimeter(8.0));   // Right column for date and signatures

        Row contentRow = mainContentLayoutTable.AddRow();
        contentRow.VerticalAlignment = VerticalAlignment.Top; // Align content at the top of the cells
                                                              // contentRow.Height = Unit.FromCentimeter(X); // Optional: if you need a minimum height for the row

        // Add content to the left column (Items Table)
        AddItemsTable(data, contentRow.Cells[0]); // Pass the left cell

        // Add content to the right column (Date and Signatures)
        AddDateBelowTable(data, contentRow.Cells[1]); // Pass the right cell
        AddSignatureSection(data, contentRow.Cells[1]); // Pass the right cell

        AddFooter(data); // Footer will be below the mainContentLayoutTable

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

        // Margins for content within the page
        section.PageSetup.LeftMargin = Unit.FromCentimeter(1.5);
        section.PageSetup.RightMargin = Unit.FromCentimeter(2.0);
        section.PageSetup.TopMargin = Unit.FromCentimeter(1.5);
        section.PageSetup.BottomMargin = Unit.FromCentimeter(1.0);

        // REMOVED: Page border (as it's not in the sales-specimen-request-format-proposal.pdf)
        // REMOVED: The problematic TextColumns definitions as they caused errors.
    }

    private void AddHeader(RequisitionData data)
    {
        Section section = document.LastSection;

        Table headerTable = section.AddTable();
        headerTable.Borders.Visible = false; // No borders for the header table itself
        headerTable.Format.SpaceAfter = Unit.FromMillimeter(2); // Space after the entire header block

        // Define 3 columns: Left (for logo), Center (for main title), Right (for control #)
        // Adjust widths (FromCentimeter) as needed to fit your content and desired spacing.
        headerTable.AddColumn(Unit.FromCentimeter(4.0));   // Column 1: For "Steniel" 
        headerTable.AddColumn(Unit.FromCentimeter(9.0));   // Column 2: For centered titles 
        headerTable.AddColumn(Unit.FromCentimeter(5.0));   // Column 3: For Control # 

        Row row = headerTable.AddRow();
        row.VerticalAlignment = VerticalAlignment.Top; // Align content at the very top of this row
        row.Height = Unit.FromCentimeter(2.0); // Give the row enough height

        // Cell 0: Steniel Logo (Left aligned) 
        //Paragraph pLogo = row.Cells[0].AddParagraph("Steniel");
        //pLogo.Format.Alignment = ParagraphAlignment.Left;
        //pLogo.Format.Font.Size = 18;
        Image logo = row.Cells[0].AddImage("steniel-logo.png"); // Add image directly to the cell
        //logo.Width = Unit.FromCentimeter(3.5); // Adjust width as needed for your logo's size
        logo.ScaleWidth = 0.2;
        logo.LockAspectRatio = true; // Maintain aspect ratio to prevent distortion
        logo.Left = ShapePosition.Left; // Align to the left within the cell
        logo.Top = ShapePosition.Top;   // Align to the top within the cell

        // Cell 1: Main Company Name and Requisition Title (Centered) 
        Paragraph p = row.Cells[1].AddParagraph("STENIEL MINDANAO PACKAGING CORPORATION");
        p.Format.Alignment = ParagraphAlignment.Center;
        p.Format.Font.Size = 10;        
        p.Format.Font.Bold = true;
        p.Format.SpaceAfter = Unit.FromMillimeter(1);

        p = row.Cells[1].AddParagraph("REQUISITION FOR");
        p.Format.Alignment = ParagraphAlignment.Center;
        p.Format.Font.Size = 12;
        p.Format.Font.Bold = true;

        p = row.Cells[1].AddParagraph("RUBBER DIE / DIE CUT MOULD");
        p.Format.Alignment = ParagraphAlignment.Center;
        p.Format.Font.Size = 14;
        p.Format.Font.Bold = true;

        // Cell 2: Control Number (Right aligned) 
        p = row.Cells[2].AddParagraph($"Control #: {data.ControlNumber}");
        p.Format.Alignment = ParagraphAlignment.Right;
        p.Format.Font.Size = 8;
        p.Format.Font.Bold = true;
        // No Date here anymore based on sales-specimen-request-format-proposal.pdf

        // REMOVED: Horizontal line separator below the header, as it's not present in sales-specimen-request-format-proposal.pdf
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

    private void AddItemsTable(RequisitionData data, Cell targetCell)
    {
        Section section = document.LastSection;

        //Table table = section.AddTable();
        Table table = targetCell.Elements.AddTable();
        table.Borders.Width = 0.5;
        table.Borders.Color = Colors.Black;
        table.Format.SpaceAfter = Unit.FromMillimeter(5);

        // Define columns (widths adjusted slightly as per visual inspection)
        Column column = table.AddColumn(Unit.FromCentimeter(1.5)); // QTY 
        column.Format.Alignment = ParagraphAlignment.Center;
        column = table.AddColumn(Unit.FromCentimeter(3.0)); // UNIT (increased for "2 Outs") 
        column.Format.Alignment = ParagraphAlignment.Center;
        column = table.AddColumn(Unit.FromCentimeter(7.5)); // DESCRIPTION 
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
            customerHeaderRow.Cells[0].MergeRight = 3; // Span across all columns
            customerHeaderRow.Cells[0].Borders.Top.Width = 0.5;
            customerHeaderRow.Cells[0].Borders.Bottom.Width = 0.5;
            customerHeaderRow.Cells[0].Format.Font.Size = 8;
            customerHeaderRow.Cells[0].Format.Font.Bold = true;
            customerHeaderRow.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            customerHeaderRow.Cells[0].AddParagraph("Customer: " + customer.Name.ToUpper());

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

        // Add "NOTHING FOLLOWS" 
        if (data.ShowNothingFollows)
        {
            Row nothingRow = table.AddRow();
            nothingRow.Cells[0].MergeRight = 3; // Span across all columns
            nothingRow.Cells[0].Borders.Top.Width = 0.5;
            nothingRow.Cells[0].Format.Font.Bold = true;
            nothingRow.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            nothingRow.Cells[0].AddParagraph("********** NOTHING FOLLOWS **********");
        }
    }

    // NEW METHOD: Add Date Below Table
    private void AddDateBelowTable(RequisitionData data, Cell targetCell) // Changed signature
    {
        // NO LONGER ADD TO SECTION DIRECTLY. Add to targetCell.
        Paragraph pDate = targetCell.AddParagraph(); // Add paragraph inside the targetCell
        pDate.Format.SpaceBefore = Unit.FromCentimeter(0.5);
        pDate.AddText("Date: ");
        pDate.AddFormattedText(data.RequisitionDate.ToString("MMMM dd, yyyy"), TextFormat.Bold);
        pDate.Format.Alignment = ParagraphAlignment.Left;
        pDate.Format.SpaceAfter = Unit.FromCentimeter(0.5);
    }

    // Update signature and content addition:
    private void AddSignatureSection(RequisitionData data, Cell targetCell) // Changed signature
    {
        // REMOVED: section.AddColumnBreak(); as we are now using a table for layout

        // All signature blocks will now be added inside the targetCell
        // For each signature block, call AddSignatureBlock and pass the targetCell
        // Example:
        AddSignatureBlock(targetCell, "Recommending Approval:", data.RecommendingApproval);
        AddSignatureBlock(targetCell, "Prepared by:", data.PreparedBy);
        AddSignatureBlock(targetCell, "Reviewed by:", data.ReviewedBySalesMarketing);
        AddSignatureBlock(targetCell, "Reviewed by:", data.ReviewedByProcurement);
        AddSignatureBlock(targetCell, "Received by:", data.ReceivedBy);
        AddSignatureBlock(targetCell, "", data.AssistantGeneralManager);
    }

    // Update signature for AddSignatureBlock as well
    private void AddSignatureBlock(Cell targetCell, string label, SignaturePerson person) // Changed signature
    {
        if (person != null)
        {
            Paragraph p = targetCell.AddParagraph(); // Add paragraph inside the targetCell
            p.Format.SpaceBefore = Unit.FromCentimeter(0.5);
            if (!string.IsNullOrEmpty(label))
            {
                p.AddFormattedText(label, "SmallNormal");
                p.AddLineBreak();
            }
            p.AddLineBreak();
            p.AddLineBreak();
            p.AddFormattedText(person.Name, "SmallBold");
            if (!string.IsNullOrEmpty(person.Title))
            {
                p.AddLineBreak();
                p.AddFormattedText(person.Title, "SmallNormal");
            }
            p.Format.SpaceAfter = Unit.FromCentimeter(0.5);
        }
    }

    private void AddFooter(RequisitionData data)
    {
        Section section = document.LastSection;

        // Get the Primary Footer object. MigraDoc automatically provides this container.
        // You don't need to assign anything to section.Footers.Primary itself here.
        HeaderFooter footer = section.Footers.Primary;

        // Set general formatting for paragraphs that will be added to this footer.
        footer.Format.Alignment = ParagraphAlignment.Left;
        footer.Format.Font.Size = 7;
        footer.Format.SpaceBefore = Unit.FromCentimeter(1); // Space from main content to footer

        // CC: line with horizontal spacing
        // Add a new Paragraph specifically for the "CC:" line within the footer
        Paragraph ccLine = footer.AddParagraph();
        ccLine.AddText("CC: SALES    FINANCE    GRAPHICS    DESIGNING");
        // Formatting is typically inherited from the 'footer' object, but can be set on 'ccLine' too.

        // Document code and revision line
        // Add another new Paragraph for the document code and revision within the footer
        Paragraph docCodeLine = footer.AddParagraph();
        docCodeLine.AddText($"{data.DocumentCode}/{data.DocumentRevision} {data.DocumentRevisionDate.ToString("MMMM yyyy")}");

        // No need for LastParagraph, as we have direct references to 'ccLine' and 'docCodeLine'
        // The alignment and font size were already set on the 'footer' object or can be set on each paragraph.
    }
}