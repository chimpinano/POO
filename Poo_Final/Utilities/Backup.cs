using Poo_Final.Model;
using Poo_Final.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Poo_Final.Utilities
{
    public class Backup
    {
        private static string diretorioArquivo = Directory.GetCurrentDirectory() + @"\backup\backup.xml";

        internal static MainViewModel RestoreBackup(string login)
        {
            try
            {
                List<MainViewModel> result = Backup.getBackup();

                if (result != null)
                    return result.FirstOrDefault(r => r.Login.Equals(login));
            }
            catch (Exception) { }

            return null;
        }

        internal static void ExecuteBackup(MainViewModel item)
        {
            if (item == null) return;

            try
            {
                List<MainViewModel> result = Backup.getBackup();

                if (result != null)
                {
                    foreach (var e in result)
                    {
                        if (e.Login.Equals(item.Login))
                        {
                            e.Items = item.Items;
                            e.Hashtag = item.Hashtag;
                            setBackup(result);
                            return;
                        }
                    }

                    result.Add(item);
                    setBackup(result);
                }
                else
                {
                    result = new List<MainViewModel>();
                    result.Add(item);
                    setBackup(result);
                }
            }
            catch (Exception) { }
        }

        private static List<MainViewModel> getBackup()
        {
            try
            {
                if (File.Exists(diretorioArquivo))
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(List<MainViewModel>));
                    TextReader textReader = new StreamReader(diretorioArquivo);
                    List<MainViewModel> result = (List<MainViewModel>)deserializer.Deserialize(textReader);
                    textReader.Close();

                    return result;
                }
                else return null;
            }
            catch (Exception) { return null; }
        }

        private static void setBackup(List<MainViewModel> items)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(diretorioArquivo)))
                    Directory.CreateDirectory(Path.GetDirectoryName(diretorioArquivo));

                XmlSerializer serializer = new XmlSerializer(typeof(List<MainViewModel>));
                TextWriter textWriter = new StreamWriter(diretorioArquivo);
                serializer.Serialize(textWriter, items);
                textWriter.Close();

            }
            catch (Exception) { }
        }
    }
}
