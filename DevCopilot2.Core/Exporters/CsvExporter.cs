using System.Reflection;
using System.Text;

namespace DevCopilot2.Core.Exporters
{
    public class CsvExporter<T>
    {
        private IEnumerable<T> _data;
        private string _filename;
        private Type _type;

        public CsvExporter(IEnumerable<T> data, string filename)
        {
            _data = data;
            _filename = filename;
            _type = typeof(T);
        }

        public MemoryStream Export()
        {
            var rows = new List<string>();

            rows.Add(CreateHeader());

            foreach (var item in _data)
                rows.Add(CreateRow(item));

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            foreach (var row in rows)
            {
                writer.Write(row);
                writer.Flush();
            }
            stream.Position = 0;
            return stream;
        }

        private string CreateHeader()
        {
            PropertyInfo[] properties = _type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (PropertyInfo property in properties)
            {
                stringBuilder.Append(property.Name).Append(",");
            }
            return stringBuilder.ToString()[..^1];
        }

        private string CreateRow(T item)
        {
            PropertyInfo[] properties = _type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (PropertyInfo property in properties)
            {
                stringBuilder.Append(CreateItem((dynamic)property.GetValue(item))).Append(",");
            }
            return stringBuilder.ToString()[..^1];
        }

        string CreateItem(object item) => item.ToString();
        string CreateItem(DateTime item) => item.ToShortDateString();
        string CreateItem(string item) => item.ToUpper();
    }

}
