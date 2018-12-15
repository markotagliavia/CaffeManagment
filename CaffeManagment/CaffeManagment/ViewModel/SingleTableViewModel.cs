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
            if (t.Poruceno.Count > 0)
            {
                t.StanjeStola = State.BUSY;
            }
            else
            {
                t.StanjeStola = State.EMPTY;
            }
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
            if (t.Id == SelectedTable.Id)
            {
                SelectedTable = t;
                SetColor(t.StanjeStola);
                OnPropertyChanged(nameof(SelectedTable));
            }
           
            
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
                    c1 = System.Windows.Media.Color.FromRgb(255, 77, 77);
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
