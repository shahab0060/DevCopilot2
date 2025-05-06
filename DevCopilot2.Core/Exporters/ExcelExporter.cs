using DevCopilot2.Core.Extensions.BasicExtensions;
using ClosedXML.Excel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace DevCopilot2.Core.Exporters
{
    public class ExcelExporter<T>
    {
        public IXLWorksheet AddHeaders(IXLWorksheet workSheet, string title)
        {
            workSheet.Cell(1, 1).Value = title;
            workSheet.Cell(1, +1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Range("A1:S1").Row(1).Merge();
            workSheet.Cell(2, 1).Value = DateTime.Now.ToShamsi();
            workSheet.Cell(2, +1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Range("A2:S2").Row(1).Merge();
            return workSheet;
        }
        public IXLWorksheet AddColumn(IXLWorksheet workSheet, string headerTitle, int columnIndex, int rowIndex = 3)
        {
            workSheet.Cell(rowIndex, columnIndex).Value = headerTitle;
            workSheet.Cell(rowIndex, columnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            return workSheet;
        }
        public MemoryStream Export(List<T> data, string fileName)
        {
            using (var wbook = new XLWorkbook() { RightToLeft = true })
            {
                var type = typeof(T);
                var ws = wbook.Worksheets.Add(fileName);
                ws.Columns().AdjustToContents();
                var properties = type.GetProperties();
                ws.Cell(1, 1).Value = fileName;
                ws.Cell(1, +1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                ws.Range("A1:S1").Row(1).Merge();
                ws.Cell(2, 1).Value = DateTime.Now.ToShamsi();
                ws.Cell(2, +1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                ws.Range("A2:S2").Row(1).Merge();
                for (int i = 0; i < properties.Length; i++)
                {
                    string displayName = properties[i].Name;
                    var displayNameAttribute = properties[i].GetCustomAttributes(typeof(DisplayAttribute), true)
                    .Cast<DisplayAttribute>().FirstOrDefault();
                    if (displayNameAttribute != null && displayNameAttribute.ResourceType != null && displayNameAttribute.Name is not null)
                    {
                        var resourceManager = new System.Resources.ResourceManager(displayNameAttribute.ResourceType);
                        displayName = resourceManager.GetString(displayNameAttribute.Name);
                    }

                    ws.Cell(3, i + 1).Value = displayName;
                    ws.Cell(3, i + 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                }
                for (int i = 0; i < data.Count; i++)
                {
                    int j = 0;
                    foreach (var property in properties)
                    {
                        var propertyValue = property.GetValue(data[i]);
                        if (property.PropertyType.IsEnum && propertyValue is not null)
                        {
                            var enumValue = (Enum)propertyValue;
                            var displayAttribute = enumValue.GetType()
                                .GetMember(enumValue.ToString())
                                .First()
                                .GetCustomAttribute<DisplayAttribute>();
                            var displayName = displayAttribute?.Name ?? enumValue.ToString();
                            propertyValue = displayName;
                        }
                        //var value = propertyValue is null ? new XLCellValue() : (XLCellValue)propertyValue;
                        ws.Cell(i + 4, j + 1).Value = XLCellValue.FromObject(propertyValue);
                        j++;
                    }
                }
                using (var stream = new MemoryStream())
                {
                    wbook.SaveAs(stream);
                    stream.Position = 0;
                    return stream;
                }
            }
        }

    }
}