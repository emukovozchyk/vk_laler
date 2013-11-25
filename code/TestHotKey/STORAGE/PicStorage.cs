using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using TestHotKey.Properties;

namespace TestHotKey.STORAGE
{
    internal static class PicStorage
    {
        private static FunPicCollection _picCol;


        static PicStorage()
        {
            _picCol = Deserialize();
        }
        
        public static void AddNewPic(string url, List<string> tags)
        {
            _picCol.PicsList.Add(new FunPicModel(url, tags));
        }

        public static List<FunPicModel> GetFunPic(List<string> tags)
        {
            if (_picCol == null)
            {
                _picCol = Deserialize();
            }
            return (from p in _picCol.PicsList from t in tags where p.Tags.Contains(t) select p).GroupBy(c => c.Url).Select(grp => grp.First()).ToList();
        }

        public static void Save()
        {
            Serialize(_picCol);
        }
        
        private static FunPicCollection Deserialize()
        {
            if (File.Exists(Settings.Default.DATA_FILE))
            {
                var colToReturn = new FunPicCollection();
                var serializer = new XmlSerializer(colToReturn.GetType());
                var reader = new StreamReader(Settings.Default.DATA_FILE);
                var deserialized = serializer.Deserialize(reader.BaseStream);
                reader.Close();
                return (FunPicCollection)deserialized;
            }
            return new FunPicCollection();
        }

        private static void Serialize(FunPicCollection toSerialize)
        {
            var serializer = new XmlSerializer(toSerialize.GetType());
            var writer = new StreamWriter(Settings.Default.DATA_FILE);
            serializer.Serialize(writer.BaseStream, toSerialize);
            writer.Close();
        }

    }
}
