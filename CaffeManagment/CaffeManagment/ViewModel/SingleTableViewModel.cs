using CaffeManagment.Common;
using CaffeManagment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static CaffeManagment.Common.Enumerations;

namespace CaffeManagment.ViewModel
{
    public class SingleTableViewModel : BindableBase
    {
        private Table selectedTable;
        private System.Windows.Media.Brush tableColor;
        private System.Windows.Media.Color c1;
        public MyICommand TableClickedCommand { get; set; }

        public SingleTableViewModel(Table t)
        {
            SelectedTable = t;
            SetColor(t.StanjeStola);
            TableClickedCommand = new MyICommand(TableClicked);
            MainWindowViewModel.Instance.TableChanged += HandleTableChanged;


        }

        public void TableClicked()
        {
            MainWindowViewModel.Instance.NotifySelectionChanged(SelectedTable);
        }

        private void HandleTableChanged(Table t)
        {
            OnPropertyChanged(nameof(SelectedTable));
            
        }

        public Table SelectedTable
        {   get { return selectedTable; }
            set
            {
                selectedTable = value;
                OnPropertyChanged(nameof(SelectedTable));
            }
        }

        public State StanjeStola
        {
            get { return SelectedTable.StanjeStola; }
            set
            {
                SelectedTable.StanjeStola = value;
                SetColor(StanjeStola);
                OnPropertyChanged(nameof(StanjeStola));
            }
        }

        public Brush TableColor
        {
            get { return tableColor; }

            set
            {
                tableColor = value;
                OnPropertyChanged(nameof(TableColor));
            }
        }

        private void SetColor(State state)
        {
            switch (state)
            {
                case State.BUSY:
                    c1 = System.Windows.Media.Color.FromRgb(236, 109, 24);
                    TableColor = new SolidColorBrush(c1);
                    break;
                case State.EMPTY:
                    c1 = System.Windows.Media.Color.FromRgb(0, 255, 0);
                    TableColor = new SolidColorBrush(c1);
                    break;
                case State.RESERVED:
                    c1 = System.Windows.Media.Color.FromRgb(255, 0, 0);
                    TableColor = new SolidColorBrush(c1);
                    break;
            }
        }
    }
}
