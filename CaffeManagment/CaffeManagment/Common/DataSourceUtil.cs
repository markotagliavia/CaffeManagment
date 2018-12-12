using CaffeManagment.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeManagment.Common
{
    public class DataSourceUtil
    {
        private String racuni = "racuni.bin";
        private String cenovnik = "cenovnik.bin";
        private String stolovi = "stolovi.bin";

        private static DataSourceUtil instance;
        public static DataSourceUtil Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataSourceUtil();
                }
                return instance;
            }
        }

        private DataSourceUtil()
        {

        }

        public bool WritePriceList(PriceList priceList)
        {
            string destPath1 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, cenovnik);
            try
            {
                var f_fileStream = new FileStream(destPath1, FileMode.Create, FileAccess.Write);
                var f_binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                f_binaryFormatter.Serialize(f_fileStream, cenovnik);
                f_fileStream.Close();
                return true;
            }
            catch (Exception exe)
            {
                Console.WriteLine($"nisam sacuvao cenovnik zbog {exe.Message}");
                return false;
            }
        }

        public bool WriteChecks(ObservableCollection<Check> checks)
        {
            string destPath1 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, racuni);
            try
            {
                var f_fileStream = new FileStream(destPath1, FileMode.Create, FileAccess.Write);
                var f_binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                f_binaryFormatter.Serialize(f_fileStream, checks);
                f_fileStream.Close();
                return true;
            }
            catch (Exception exe)
            {
                Console.WriteLine($"nisam sacuvao racune zbog {exe.Message}");
                return false;
            }
        }

        public bool WriteTables(ObservableCollection<Table> tables)
        {
            string destPath1 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, stolovi);
            try
            {
                var f_fileStream = new FileStream(destPath1, FileMode.Create, FileAccess.Write);
                var f_binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                f_binaryFormatter.Serialize(f_fileStream, tables);
                f_fileStream.Close();
                return true;
            }
            catch (Exception exe)
            {
                Console.WriteLine($"nisam sacuvao stolove zbog {exe.Message}");
                return false;
            }
        }

        public PriceList ReadPriceList()
        {
            string destPath1 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, cenovnik);
            try
            {
                var f_fileStream = File.OpenRead(destPath1);
                var f_binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                PriceList priceList = (PriceList)f_binaryFormatter.Deserialize(f_fileStream);
                f_fileStream.Close();
                return priceList;
            }
            catch (Exception exe)
            {
                Console.WriteLine($"neuspesna deserijalizacija zbog {exe.Message}");
                return null;
            }
        }

        public ObservableCollection<Check> ReadChecks()
        {
            string destPath1 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, racuni);
            try
            {
                var f_fileStream = File.OpenRead(destPath1);
                var f_binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                ObservableCollection<Check> checks = (ObservableCollection<Check>)f_binaryFormatter.Deserialize(f_fileStream);
                f_fileStream.Close();
                return checks;
            }
            catch (Exception exe)
            {
                Console.WriteLine($"neuspesna deserijalizacija zbog {exe.Message}");
                return null;
            }
        }

        public ObservableCollection<Table> ReadTables()
        {
            string destPath1 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, stolovi);
            try
            {
                var f_fileStream = File.OpenRead(destPath1);
                var f_binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                ObservableCollection<Table> stolovi = (ObservableCollection<Table>)f_binaryFormatter.Deserialize(f_fileStream);
                f_fileStream.Close();
                return stolovi;
            }
            catch (Exception exe)
            {
                Console.WriteLine($"neuspesna deserijalizacija zbog {exe.Message}");
                return null;
            }
        }
    }
}
