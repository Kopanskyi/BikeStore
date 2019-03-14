using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManager;
using System.Data;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Forms;

namespace ReportManager
{
    public class WordReport
    {
        private Word.Application application;
        private Word.Document document;
        private object missing;
        private string path = System.IO.Directory.GetCurrentDirectory();

        public void CreateWordReport()
        {
            try
            {
                //Create an instance for word app
                application = new Word.Application
                {
                    //Set animation status for word application
                    ShowAnimation = false,
                    //Set status for word application is to be visible or not.
                    Visible = false
                };

                //Create a missing variable for missing value
                missing = System.Reflection.Missing.Value;

                //Create a new document
                document = application.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                ////Add header into the document
                //foreach (Word.Section section in document.Sections)
                //{
                //    //Get the header range and add the header details.
                //    Word.Range headerRange =
                //        section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;

                //    headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage);

                //    headerRange.ParagraphFormat.Alignment =
                //        Word.WdParagraphAlignment.wdAlignParagraphCenter;

                //    headerRange.Font.ColorIndex = Word.WdColorIndex.wdBlue;

                //    headerRange.Font.Size = 12;

                //    headerRange.Text = "Header text goes here";
                //}


                ////Add the footers into the document
                //foreach (Word.Section section in document.Sections)
                //{
                //    //Get the footer range and add the footer details.
                //    Word.Range footerRange = section.Footers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;

                //    footerRange.Font.ColorIndex = Word.WdColorIndex.wdDarkRed;

                //    footerRange.Font.Size = 10;

                //    footerRange.ParagraphFormat.Alignment =
                //        Word.WdParagraphAlignment.wdAlignParagraphCenter;

                //    footerRange.Text = "Footer text goes here";
                //}


                ////adding text to document
                //document.Content.SetRange(0, 0);

                //document.Content.Text = "This is test document" + Environment.NewLine;


                ////Add paragraph with Heading 1 style
                Word.Paragraph paragraph1 =
                    document.Content.Paragraphs.Add(ref missing);

                //object styleHeading1 = "Heading 1";

                //paragraph1.Range.set_Style(ref styleHeading1);

                //paragraph1.Range.Text = "Paragraph 1 text";

                //paragraph1.Range.InsertParagraphAfter();



                ////Add paragraph with Heading 2 style
                //Word.Paragraph paragraph2 =
                //    document.Content.Paragraphs.Add(ref missing);

                //object styleHeading2 = "Heading 2";

                //paragraph2.Range.set_Style(ref styleHeading2);

                //paragraph2.Range.Text = "Paragraph 2 text";

                //paragraph2.Range.InsertParagraphAfter();



                CreateTable(ref paragraph1);


                SaveReport();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void CreateTable(ref Word.Paragraph paragraph)
        {
            //Create a table and insert some records
            Word.Table table = document.Tables.Add(paragraph.Range,
                Report.Instance.DataTableForReport.Rows.Count + 1,
                Report.Instance.DataTableForReport.Columns.Count,
                ref missing, ref missing);

            table.Borders.Enable = 1;

            foreach (Word.Row row in table.Rows)
            {
                foreach (Word.Cell cell in row.Cells)
                {
                    //Header row
                    if (cell.RowIndex == 1)
                    {
                        cell.Range.Text = Report.Instance.DataTableForReport.Rows[0].
                            Table.Columns[cell.ColumnIndex - 1].ToString();

                        cell.Range.Font.Bold = 1;

                        //other format properties goes here
                        cell.Range.Font.Name = "verdana";

                        cell.Range.Font.Size = 10;

                        //cell.Range.Font.ColorIndex = WdColorIndex.wdGray25;                            
                        cell.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray25;

                        //Center alignment for the Header cells
                        cell.VerticalAlignment =
                            Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                        cell.Range.ParagraphFormat.Alignment =
                            Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    }
                    else
                    {
                        //Data row
                        cell.Range.Text = Report.Instance.DataTableForReport.Rows[row.Index - 2].
                            ItemArray[cell.ColumnIndex - 1].ToString();
                    }

                }
            }
        }

        private void SaveReport()
        {
            //Save the document
            object filename = path + "\\Word_Report.docx";

            document.SaveAs2(ref filename);

            document.Close(ref missing, ref missing, ref missing);

            document = null;

            application.Quit(ref missing, ref missing, ref missing);

            application = null;

            MessageBox.Show("Report created successfully");
        }

    }
}
