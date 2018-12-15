using CaffeManagment.Model;
using CaffeManagment.View;
using CaffeManagment.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CaffeManagment.Common
{
    public class TablesUIConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var tables = value as ObservableCollection<Table>;
            if (tables != null)
            {
                var gridView = new Grid();
                int columns = 5;
                int rows = (tables.Count() / columns) + 1;

                for (int i = 0; i < columns; i++)
                {
                    ColumnDefinition gridCol = new ColumnDefinition();
                    gridCol.Width = new GridLength(1, GridUnitType.Star);
                    gridView.ColumnDefinitions.Add(gridCol);
                }

                for (int i = 0; i < rows; i++)
                {
                    RowDefinition gridRow = new RowDefinition();

                    gridRow.Height = new GridLength(1, GridUnitType.Star);

                    gridView.RowDefinitions.Add(gridRow);
                }

                int row = 0;
                int column = 0;
                foreach (var table in tables)
                {
                    var tablecontrol = new SingleTableView(table);
                    Grid.SetRow(tablecontrol, row);

                    Grid.SetColumn(tablecontrol, column);

                    gridView.Children.Add(tablecontrol);

                    if (column == (columns - 1))
                    {
                        column = 0;
                        ++row;
                    }
                    else
                    {
                        ++column;
                    }

                }
                return gridView;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
