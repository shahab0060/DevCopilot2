using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.Drawing;

namespace DevCopilot2.Core.Exporters
{
    public static class TextSharpHelper
    {
        private static string[] values = new string[] { "نام پسماند", "روش مدیریت پسماند", "به جهت", "وزن خالص" };

        private record Data { public string Name; public string Way; public string For; public string Weight; }

        private static List<Data> datas = new List<Data>() {

                new Data()
                {
                    Name = "تست پسماند خطرناک",
                    For = "کاهش در مبدا",
                    Way = "مدیریت در م  حل",
                    Weight = "0",
                },
                new Data()
                {
                    Name = "تست پسماند خطرناک",
                    Way = "تحویل به شرکت های مورد تایید سازمان محیط زیست",
                    For = "استفاده مجدد",
                    Weight = "0",
                },
                new Data()
                {
                    Name = "تست پسماند خطرناک",
                    For = "استفاده مجدد",
                    Way = "مدیریت در محل",
                    Weight = "0",
                },
                new Data()
                {
                    Name = "تست پسماند خطرناک",
                    For = "تحویل به شرکت های مورد تایید سازمان محیط زیست",
                    Way = "استفاده مجدد",
                    Weight = "0",
                },
                new Data()
                {
                    Name = "تست پسماند خطرناک",
                    For = "تحویل به شرکت های مورد تایید سازمان محیط زیست",
                    Way = "استفاده مجدد",
                    Weight = "0",
                },
                new Data()
                {
                    Name = "تست پسماند خطرناک",
                    For = "تحویل به شرکت های مورد تایید سازمان محیط زیست",
                    Way = "استفاده مجدد",
                    Weight = "0",
                },
                new Data()
                {
                    Name = "تست پسماند خطرناک",
                    For = "تحویل به شرکت های مورد تایید سازمان محیط زیست",
                    Way = "استفاده مجدد",
                    Weight = "0",
                },
                new Data()
                {
                    Name = "تست پسماند خطرناک",
                    For = "تحویل به شرکت های مورد تایید سازمان محیط زیست",
                    Way = "استفاده مجدد",
                    Weight = "0",
                },
                new Data()
                {
                    Name = "تست پسماند خطرناک",
                    For = "تحویل به شرکت های مورد تایید سازمان محیط زیست",
                    Way = "استفاده مجدد",
                    Weight = "0",
                },
                new Data()
                {
                    Name = "تست پسماند خطرناک",
                    For = "تحویل به شرکت های مورد تایید سازمان محیط زیست",
                    Way = "استفاده مجدد",
                    Weight = "0",
                },
                new Data()
                {
                    Name = "تست پسماند خطرناک",
                    For = "تحویل به شرکت های مورد تایید سازمان محیط زیست",
                    Way = "استفاده مجدد",
                    Weight = "0",
                }
        };

        public static MemoryStream CreatePDF()
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

                        var dataTable = getData();


                        //Creating Date
                        PdfPTable date = new PdfPTable(1)
                        {
                            RunDirection = PdfWriter.RUN_DIRECTION_RTL,

                            SpacingAfter = 20,
                            SpacingBefore = 20,
                            WidthPercentage = 100
                        };

                        date.AddCell(CreateDate(f2Bold));

                        document.Add(CreateMainHeader(f2Bold));

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

        private static PdfPTable CreateMainHeader(iTextSharp.text.Font f2Bold)
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
            PdfPCell mainHeaderContent = new(new Phrase(10, "گزارش روش های مدیریت یک پسماند در بازه زمانی مشخص", fontTitle));
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
            PdfPCell dateContent = new(new Phrase(10, "تاریخ گزارش: 28/06/1402 10:55", f2Bold));
            dateContent.BorderColor = BaseColor.WHITE;
            dateContent.UseBorderPadding = true;
            dateContent.MinimumHeight = 25;
            return dateContent;
        }


        public static DataTable getData()
        {
            DataTable data = new DataTable();

            data.Columns.Add(new DataColumn("ردیف"));

            foreach (var item in values)
            {
                data.Columns.Add(new DataColumn(item));
            }

            for (int i = 0; i < datas.Count; i++)
            {
                data.Rows.Add(i + 1, datas[i].Name, datas[i].Way, datas[i].For, datas[i].Weight);
            }

            return data;


        }
    }
}
