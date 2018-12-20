using CaffeManagment.Common;
using CaffeManagment.Model;
using CaffeManagment.SecurityManager;
using CaffeManagment.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static CaffeManagment.Common.Enumerations;

namespace CaffeManagment.ViewModel
{
    public class TablesViewModel : BindableBase
    {
        private Visibility isAdmin;
        private User userOnSession;
        private string username;
        private string nazivStola;
        private bool selektovanSto;
        private ObservableCollection<DrinkWithPriceAndQuantity> picaDataSource;
        private float ukupnoZaSto;
        private float ukupnoZaSve;
        private bool stampajSto;
        private bool stampajSve;
        private ObservableCollection<Table> tables;
        private Table selectedTable;

        #region Commands
        public MyICommand OtvoriStoCommand { get; private set; }
        public MyICommand MojNalogCommand { get; private set; }
        public MyICommand SviNaloziCommand { get; private set; }
        public MyICommand StampajStoCommand { get; private set; }
        public MyICommand StampajSveCommand { get; private set; }
        
        #endregion

        public TablesViewModel()
        {
            this.userOnSession = MainWindowViewModel.Instance.UserOnSession;
            isAdmin = userOnSession.Role.Equals(Role.Admin) ? Visibility.Visible : Visibility.Hidden;
            nazivStola = "";
            selektovanSto = false;
            picaDataSource = new ObservableCollection<DrinkWithPriceAndQuantity>();
            ukupnoZaSto = 0;
            ukupnoZaSve = 0;
            stampajSto = false;
            stampajSve = false;
            username = userOnSession.Username;
            OtvoriStoCommand = new MyICommand(OtvoriStoExecute);
            MojNalogCommand = new MyICommand(MojNalogExecute);
            SviNaloziCommand = new MyICommand(SviNaloziExecute);
            StampajStoCommand = new MyICommand(StampajStoExecute);
            StampajSveCommand = new MyICommand(StampajSveExecute);
            Test();
            MainWindowViewModel.Instance.TableChanged += HandleTableChanged;
        }

        #region Commands Implementation
        private void StampajSveExecute()
        {
            //TO DO
        }

        private void StampajStoExecute()
        {
            //int uplata = 0; to do: uzeti vrednost uplate
            try
            {
                string ticks = DateTime.Now.Ticks.ToString();
                string sub = ticks.Substring(3, 15);
                var csv = new StringBuilder();
                csv.AppendLine($"48,1,___,_,__;{sub};1;1234;1;;");
                DateTime d = DateTime.Now;
                string nazivFajla = $"{NazivStola}_{d.Year}_{d.Month}_{d.Day}_{d.Hour}_{d.Minute}_{d.Second}.inv";
                string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "KomandeUlaz", nazivFajla);

                foreach (DrinkWithPriceAndQuantity item in PicaDataSource)
                {
                    csv.AppendLine($"S,1,___,_,__;{item.Naziv};{item.Cena};{item.Kolicina};1;1;{(int)item.PoreskaGrupa};0;{item.Sifra};");
                }

                csv.AppendLine($"T,1,___,_,__;{(int)NacinPlacanja.KES};{UkupnoZaSto};;;;");
                File.WriteAllText(filePath, csv.ToString());
                NapraviRacun();
                SelectedTable.Poruceno.Clear();
                SelectedTable.StanjeStola = State.EMPTY;
                PicaDataSource = new ObservableCollection<DrinkWithPriceAndQuantity>();
                Tables.FirstOrDefault(x => x.Id == SelectedTable.Id).Poruceno.Clear();
                Tables.FirstOrDefault(x => x.Id == SelectedTable.Id).StanjeStola = State.EMPTY;
                DataSourceUtil.Instance.WriteTables(Tables);
                HandleTableChanged(SelectedTable);

                MessageBox.Show($"Racun istampan! Kusur je 0 RSD.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Neuspešno upisivanje fajlova. Originalna greška: " + ex.Message);
            }
        }

        private void NapraviRacun()
        {
            DateTime d = DateTime.Now;
            ObservableCollection<Check> racuni = DataSourceUtil.Instance.ReadChecks(d);
            if (racuni == null)
            {
                racuni = new ObservableCollection<Check>();
            }
            Check c = new Check();
            c.Kusur = 0;
            c.Uplaceno = UkupnoZaSto;
            c.Pica = PicaDataSource;
            c.Waiter = UserOnSession.Username;
            c.UkupnoPara = UkupnoZaSto;
            c.NazivStola = NazivStola;
            racuni.Add(c);
            DataSourceUtil.Instance.WriteChecks(racuni, d);
        }

        private void SviNaloziExecute()
        {
            MessageBox.Show("U procesu razvoja...");
            //TO DO
        }

        private void MojNalogExecute()
        {
            MessageBox.Show("U procesu razvoja...");
            //TO DO
        }

        private void OtvoriStoExecute()
        {
            if (selectedTable != null)
            {
                AddRemoveDrinkForTableView a = new AddRemoveDrinkForTableView(userOnSession, selectedTable);
                a.ShowDialog();
            }
        }

        #endregion

        private void Test()
        {
            //kod za generisanje novih praznih stolova

            /* Tables = new ObservableCollection<Table>();
             for (int i = 0; i < 25; i++)
             {
                 Table t = new Table();
                 t.OznakaStola = $"sto_{i}";
                 t.StanjeStola = Enumerations.State.EMPTY;
                 t.Poruceno = new ObservableCollection<DrinkWithPriceAndQuantity>();
                 t.Waiter = Username;
                 Tables.Add(t);
             }

             if (DataSourceUtil.Instance.WriteTables(Tables))
             {
                 Console.WriteLine("Stolovi uspesno upisani.");
             }
             else
             {
                 Console.WriteLine("Stolovi nisu uspesno upisani!!!");
             }*/
            var pom = DataSourceUtil.Instance.ReadTables();
            if (pom != null)
            {
                Tables = pom;
                PicaDataSource = new ObservableCollection<DrinkWithPriceAndQuantity>();
            }
            else
            {
                Console.WriteLine("Neuspesno ucitavanje stolova");
            }
        }

        #region Properties

        public Visibility IsAdmin
        {
            get
            {
                return isAdmin;
            }
            set
            {
                isAdmin = value;
                OnPropertyChanged("IsAdmin");
            }
        }

        public User UserOnSession
        {
            get
            {
                return userOnSession;
            }
            set
            {
                userOnSession = value;
                OnPropertyChanged("UserOnSession");
            }
        }

        private void HandleTableChanged(Table t)
        {
            SelectedTable = t;
            OnPropertyChanged(nameof(NazivStola));
            var pom = DataSourceUtil.Instance.ReadTables();
            if (pom != null)
            {
                Tables = pom;
                PicaDataSource = t.Poruceno;
            }
        }

        public string NazivStola
        {
            get
            {
                return SelectedTable?.OznakaStola ?? "";
            }
            set
            {
                Tables.FirstOrDefault(x => x.Id == SelectedTable.Id).OznakaStola = value;
                SelectedTable.OznakaStola = value;
                OnPropertyChanged(nameof(NazivStola));
                OnPropertyChanged(nameof(Tables));
                DataSourceUtil.Instance.WriteTables(Tables);
                MainWindowViewModel.Instance.NotifySelectionChanged(SelectedTable);
            }
        }

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        public bool SelektovanSto
        {
            get
            {
                return selektovanSto;
            }
            set
            {
                selektovanSto = value;
                OnPropertyChanged("SelektovanSto");
            }
        }

        public bool StampajSto
        {
            get
            {
                return stampajSto;
            }
            set
            {
                stampajSto = value;
                OnPropertyChanged("StampajSto");
            }
        }

        public bool StampajSve
        {
            get
            {
                return stampajSve;
            }
            set
            {
                stampajSve = value;
                OnPropertyChanged("StampajSve");
            }
        }

        public float UkupnoZaSto
        {
            get
            {
                return ukupnoZaSto;
            }
            set
            {
                ukupnoZaSto = value;
                OnPropertyChanged("UkupnoZaSto");
            }
        }

        public float UkupnoZaSve
        {
            get
            {
                return ukupnoZaSve;
            }
            set
            {
                ukupnoZaSve = value;
                OnPropertyChanged("UkupnoZaSve");
            }
        }


        public ObservableCollection<Table> Tables
        {
            get { return tables; }
            set
            {
                tables = value;
                OnPropertyChanged(nameof(Tables));
            }
        }

        public Table SelectedTable
        {
            get
            { return selectedTable; }
            set
            {
                selectedTable = value;
                OnPropertyChanged(nameof(SelectedTable));
                OnPropertyChanged(NazivStola);
                if (selectedTable != null && selectedTable.OznakaStola.Length > 0)
                {
                    SelektovanSto = true;
                }
                PicaDataSource = SelectedTable.Poruceno;
                if (PicaDataSource.Count > 0)
                {
                    StampajSto = true;
                }
                else
                {
                    StampajSto = false;
                }
            }
        }

        public ObservableCollection<DrinkWithPriceAndQuantity> PicaDataSource
        {
            get => picaDataSource;
            set
            {
                picaDataSource = value;
                OnPropertyChanged(nameof(PicaDataSource));
                UkupnoZaSto = picaDataSource.Sum(item => item.Kolicina * item.Cena);
                OnPropertyChanged(nameof(UkupnoZaSto));
                UkupnoZaSve = Tables.Sum(item => item.Poruceno.Sum(item1 => item1.Kolicina * item1.Cena));
                OnPropertyChanged(nameof(UkupnoZaSve));
            }
        }



        #endregion

    }
}
