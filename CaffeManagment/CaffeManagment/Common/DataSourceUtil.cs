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
        private String licenca = "licenca.lic";

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
                f_binaryFormatter.Serialize(f_fileStream, priceList);
                f_fileStream.Close();
                return true;
            }
            catch (Exception exe)
            {
                Console.WriteLine($"nisam sacuvao cenovnik zbog {exe.Message}");
                return false;
            }
        }

        public bool WriteChecks(ObservableCollection<Check> checks, DateTime dateTime)
        {
            string destPath1 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Racuni", $"{dateTime.Year}_{dateTime.Month}_{dateTime.Day}_{racuni}");
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

        public bool WriteLicence(DateTime dateTime)
        {
            string destPath1 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, licenca);
            try
            {
                var f_fileStream = new FileStream(destPath1, FileMode.Create, FileAccess.Write);
                var f_binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                string checkSum = "MarkovLicencniString";
                Licenca lic = new Licenca(dateTime, checkSum);
                f_binaryFormatter.Serialize(f_fileStream, lic);
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

        public ObservableCollection<Check> ReadChecks(DateTime dateTime)
        {
            string destPath1 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Racuni", $"{dateTime.Year}_{dateTime.Month}_{dateTime.Day}_{racuni}");
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

        /// <summary>
        /// Returns true if licence is up to date and check sum is valid. Otherwise false.
        /// </summary>
        /// <returns></returns>
        public bool ReadLicence()
        {
            string destPath1 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, licenca);
            try
            {
                var f_fileStream = File.OpenRead(destPath1);
                var f_binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                Licenca lic = (Licenca)f_binaryFormatter.Deserialize(f_fileStream);
                f_fileStream.Close();
                if (lic.checkSum.Equals("MarkovLicencniString"))
                {
                    if (lic.dateTime > DateTime.Now)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exe)
            {
                Console.WriteLine($"neuspesna deserijalizacija licence zbog {exe.Message}");
                return false;
            }
        }
    }

    [Serializable]
    public struct Licenca
    {
        public DateTime dateTime;
        public string checkSum;

        public Licenca(DateTime dateTime, string checkSum)
        {
            this.dateTime = dateTime;
            this.checkSum = checkSum;
        }
    }
}