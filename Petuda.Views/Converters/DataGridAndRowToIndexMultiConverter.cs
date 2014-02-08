using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace Petuda.Views.Converters
{
    public sealed class DataGridAndRowToIndexMultiConverter : MarkupExtension, IMultiValueConverter
    {
        // MarkupExtension
        static DataGridAndRowToIndexMultiConverter converter;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (converter == null) converter = new DataGridAndRowToIndexMultiConverter();
            return converter;
        }

        public DataGridAndRowToIndexMultiConverter()
        {
        }

        // IMultiValueConverter
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length == 0)
                throw new ArgumentException();

            DataGrid dataGrid = values[0] as DataGrid;
            DataGridRow row = values[1] as DataGridRow;

            if (dataGrid == null && row == null)
            {
                return -1;
            }

            var codes = GetItemsCodesList(dataGrid);
            var ind = codes.Select((r, index) => new { Code = r, Position = index })
                            .FirstOrDefault(tmp => tmp.Code == row.Item.GetHashCode())
                            .Position;

            //var rows = GetRowsList(dataGrid);
            //var ind =
            //    rows.Select((r, index) => new { Row = r, Position = index })
            //    .FirstOrDefault(tmp => tmp.Row.GetHashCode() == row.GetHashCode())
            //    .Position;

            return ind.ToString();
        }

        private List<int> GetItemsCodesList(DataGrid dataGrid)
        {
            var codes = new List<int>();
            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                var code = dataGrid.Items[i].GetHashCode();
                codes.Add(code);
            }

            return codes;
        }

        private List<DataGridRow> GetRowsList(DataGrid dataGrid)
        {
            var rows = new List<DataGridRow>();
            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(i);
                rows.Add(row);
            }

            return rows;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}