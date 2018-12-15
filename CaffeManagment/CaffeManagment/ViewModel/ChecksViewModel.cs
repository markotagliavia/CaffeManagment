using CaffeManagment.Common;
using CaffeManagment.Model;
using CaffeManagment.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace CaffeManagment.ViewModel
{
    public class ChecksViewModel : BindableBase
    {
        #region Members
        private User userOnSession;
        private DateTime datumOd;
        private int selektovaniIndeks = -1;
        private bool selektovanRacun = false;
        private float ukupnoSve;
        private ObservableCollection<Check> racuni;
        private ObservableCollection<DrinkWithPriceAndQuantity> pica;
        #endregion

        #region Commands
        public MyICommand<object> OtkaziCommand { get; private set; }
        public MyICommand<object> StampajCommand { get; private set; }
        public MyICommand<object> StornirajCommand { get; private set; }
        public MyICommand<object> ExportCommand { get; private set; }
        #endregion

        public ChecksViewModel()
        {
            StampajCommand = new MyICommand<object>(StampajExecute);
            StornirajCommand = new MyICommand<object>(StornirajExecute);
            OtkaziCommand = new MyICommand<object>(OtkaziExecute);
            ExportCommand = new MyICommand<object>(ExportExecute);
            userOnSession = MainWindowViewModel.Instance.UserOnSession;
            racuni = new ObservableCollection<Check>();
            pica = new ObservableCollection<DrinkWithPriceAndQuantity>();
            datumOd = DateTime.Now;
            ukupnoSve = 0;
            PopulateGrid();
        }

        #region CommandsImplementation

        private void OtkaziExecute(object obj)
        {
            foreach (Window w in Application.Current.Windows)
            {
                if (w.GetType().Equals(typeof(ChecksView)))
                {
                    w.Close();
                }
            }
        }

        private void StampajExecute(object obj)
        {
            // TO DO
        }

        private void ExportExecute(object obj)
        {
            System.IO.Stream myStream;
            OpenFileDialog thisDialog = new OpenFileDialog();

            thisDialog.InitialDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Racuni");
            thisDialog.Filter = "bin files (*.bin)|*.bin|All files (*.*)|*.*";
            thisDialog.FilterIndex = 2;
            thisDialog.RestoreDirectory = true;
            thisDialog.Multiselect = true;
            thisDialog.Title = "Izaberite fajlove";

            if (thisDialog.ShowDialog() ?? true)
            {
                foreach (String file in thisDialog.FileNames)
                {
                    try
                    {
                        if ((myStream = thisDialog.OpenFile()) != null)
                        {
                            using (myStream)
                            {
                                //listBox1.Items.Add(file);
                                //TO DO: open every file and build CSV
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Neuspešno učitavanje fajlova. Originalna greška: " + ex.Message);
                    }
                }
            }
        }

        private void StornirajExecute(object obj)
        {
            if (SelektovaniIndeks > -1)
            {
                if (!racuni.ElementAt(selektovaniIndeks).Storniran)
                {
                    racuni.ElementAt(selektovaniIndeks).Storniran = true;
                    //TO DO : Serijalizuj
                }
                else
                {
                    MessageBox.Show("Račun je već storniran!");
                }
            }
        }
        #endregion

        #region Properties
        public User UserOnSession { get => userOnSession; set => userOnSession = value; }

        public ObservableCollection<DrinkWithPriceAndQuantity> Pica
        {
            get => pica;
            set
            {
                pica = value;
                OnPropertyChanged("Pica");
            }
        }

        public ObservableCollection<Check> Racuni
        {
            get => racuni;
            set
            {
                racuni = value;
                UkupnoSve = racuni.Where(x => x.Storniran == false).Sum(item => item.UkupnoPara);
                OnPropertyChanged("Racuni");
            }
        }

        public int SelektovaniIndeks
        {
            get => selektovaniIndeks;
            set
            {
                selektovaniIndeks = value;
                OnPropertyChanged("SelektovaniIndeks");
                if (selektovaniIndeks > -1)
                {
                    selektovanRacun = true;
                    pica = racuni.ElementAt(selektovaniIndeks).Pica;
                }
                else
                {
                    selektovanRacun = false;
                    pica = new ObservableCollection<DrinkWithPriceAndQuantity>();
                }
            }
        }

        public bool SelektovanRacun
        {
            get => selektovanRacun;
            set
            {
                selektovanRacun = value;
                OnPropertyChanged("SelektovanRacun");
            }
        }

        public DateTime DatumOd
        {
            get => datumOd;
            set
            {
                datumOd = value;
                PopulateGrid();
                OnPropertyChanged("DatumOd");
            }
        }

        public float UkupnoSve
        {
            get => ukupnoSve;
            set
            {
                ukupnoSve = value;
                OnPropertyChanged("UkupnoSve");
            }
        }
        #endregion

        private void PopulateGrid()
        {
            ObservableCollection<Check> pom;
            if ((pom = DataSourceUtil.Instance.ReadChecks(DatumOd)) != null)
            {
                racuni = pom;
            }
        }
    }
}
