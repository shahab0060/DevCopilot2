using DevCopilot2.Core.Extensions.BasicExtensions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.Drawing;

namespace DevCopilot2.Core.Exporters
{
    public static class PdfExporter
    {
        public static MemoryStream CreatePDF(this DataTable dataTable, string title)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var document = new Document())
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                    writer.CloseStream = false;
                    try
                    {
                        document.Open();

                        string fontPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "B-NAZANIN.TTF");

                        BaseFont basefont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, true);

                        var chunk = new Chunk();

                        iTextSharp.text.Font f2 = new iTextSharp.text.Font(basefont, chunk.Font.Size,
                                                        chunk.Font.Style, chunk.Font.Color);

                        iTextSharp.text.Font f2Bold = new iTextSharp.text.Font(basefont, chunk.Font.Size,
                                        (int)FontStyle.Bold, chunk.Font.Color);

                        chunk.Font = f2;

                        //Creating Date
                        PdfPTable date = new PdfPTable(1)
                        {
                            RunDirection = PdfWriter.RUN_DIRECTION_RTL,

                            SpacingAfter = 20,
                            SpacingBefore = 20,
                            WidthPercentage = 100
                        };

                        date.AddCell(CreateDate(f2Bold));

                        document.Add(CreateMainHeader(f2Bold, title));

                        document.Add(date);

                        document.Add(CreateContentTable(dataTable, f2Bold, chunk));

                        document.Close();


                    }
                    catch (DocumentException de)
                    {
                        // this.Message = de.Message;
                    }
                    catch (IOException ioe)
                    {
                        // this.Message = ioe.Message;
                    }

                }

                //If you want to test, uncomment this code
                /* byte[] docArray = memoryStream.ToArray();
                 File.WriteAllBytes("DevCopilot2.pdf", docArray);*/

                return memoryStream;
            }
        }
        public static MemoryStream CreatePDF(this string title, string text)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var document = new Document())
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                    writer.CloseStream = false;
                    try
                    {
                        document.Open();

                        string fontPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "B-NAZANIN.TTF");

                        BaseFont basefont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, true);

                        var chunk = new Chunk();

                        iTextSharp.text.Font f2 = new iTextSharp.text.Font(basefont, chunk.Font.Size,
                                                        chunk.Font.Style, chunk.Font.Color);

                        iTextSharp.text.Font f2Bold = new iTextSharp.text.Font(basefont, chunk.Font.Size,
                                        (int)FontStyle.Bold, chunk.Font.Color);

                        chunk.Font = f2;

                        //Creating Date
                        PdfPTable date = new PdfPTable(1)
                        {
                            RunDirection = PdfWriter.RUN_DIRECTION_RTL,

                            SpacingAfter = 20,
                            SpacingBefore = 20,
                            WidthPercentage = 100
                        };

                        document.Add(CreateMainHeader(f2Bold, title));
                        document.Add(CreateMainHeader(f2Bold, text));

                        document.Add(date);

                        document.Close();


                    }
                    catch (DocumentException de)
                    {
                        // this.Message = de.Message;
                    }
                    catch (IOException ioe)
                    {
                        // this.Message = ioe.Message;
                    }

                }

                //If you want to test, uncomment this code
                /* byte[] docArray = memoryStream.ToArray();
                 File.WriteAllBytes("DevCopilot2.pdf", docArray);*/

                return memoryStream;
            }
        }
        private static PdfPTable CreateContentTable(DataTable dataTable, iTextSharp.text.Font f2Bold, Chunk chunk)
        {

            PdfPTable table = new((dataTable.Columns.Count * 2) - 1);

            table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            table.HorizontalAlignment = 1;

            table.SpacingAfter = 20;
            table.SpacingBefore = 20;
            table.WidthPercentage = 100;

            for (int j = 0; j < dataTable.Columns.Count; j++)
            {

                PdfPCell cell = new(new Phrase(10, dataTable.Columns[j].ColumnName, f2Bold));
                cell.UseBorderPadding = true;
                cell.VerticalAlignment = 1;
                cell.HorizontalAlignment = 1;
                cell.MinimumHeight = 25;
                cell.Colspan = 2;

                if (j == 0)
                {
                    cell.Colspan = 1;
                }

                table.AddCell(cell);
            }


            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    var str = dataTable.Rows[i].ItemArray[j].ToString();
                    PdfPCell cell = new(new Phrase(str, chunk.Font));
                    cell.MinimumHeight = 25;
                    cell.PaddingBottom = 10;
                    //cell.PaddingTop = 5;
                    cell.UseBorderPadding = true;
                    cell.VerticalAlignment = (int)iText.Layout.Properties.VerticalAlignment.MIDDLE;
                    cell.HorizontalAlignment = (int)iText.Layout.Properties.HorizontalAlignment.CENTER;

                    cell.Colspan = 2;
                    if (j == 0)
                    {
                        cell.Colspan = 1;
                    }

                    table.AddCell(cell);

                }
            }

            return table;
        }

        private static PdfPTable CreateMainHeader(iTextSharp.text.Font f2Bold, string title)
        {
            //Creating main header
            PdfPTable mainHeader = new(1);

            mainHeader.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            mainHeader.HorizontalAlignment = 1;

            mainHeader.SpacingAfter = 20;
            mainHeader.SpacingBefore = 20;
            mainHeader.WidthPercentage = 100;

            var fontTitle = new iTextSharp.text.Font(f2Bold);
            fontTitle.Size = 16f;
            PdfPCell mainHeaderContent = new(new Phrase(10, title, fontTitle));
            mainHeaderContent.BorderColor = BaseColor.WHITE;
            mainHeaderContent.UseBorderPadding = true;
            mainHeaderContent.VerticalAlignment = 1;
            mainHeaderContent.HorizontalAlignment = 1;
            mainHeaderContent.MinimumHeight = 25;

            mainHeader.AddCell(mainHeaderContent);

            return mainHeader;

        }

        private static PdfPCell CreateDate(iTextSharp.text.Font f2Bold)
        {
            PdfPCell dateContent = new(new Phrase(10, $"تاریخ گزارش: {DateTime.Now.ToShamsi()}", f2Bold));
            dateContent.BorderColor = BaseColor.WHITE;
            dateContent.UseBorderPadding = true;
            dateContent.MinimumHeight = 25;
            return dateContent;
        }

        public static DataTable AddHeader(this DataTable data, string title)
        {
            data.Columns.Add(new DataColumn(title));
            return data;
        }

        public static DataTable AddContentRow(this DataTable data, params object[] values)
        {
            data.Rows.Add(values);
            return data;
        }
    }
}