using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace test
{
    class fileReaderHighScores
    {
        public static List<scorePosition> readInHighScoreList()
        {
            //try saved data first... if not then read local file
            string fileString;
            List<scorePosition> list = new List<scorePosition>();
            try  // reading the localApplicationFolder first
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string filename = Path.Combine(path, "highScores.txt");
                using (var reader = new StreamReader(filename))
                {
                    fileString = reader.ReadToEnd();
                    // need json library
                }
            }
            catch
            {
                //read the defaultData.txt
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(HighScoreReplay)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("test.HighScores.highScores.txt");
                using (var reader = new StreamReader(stream))
                {
                    fileString = reader.ReadToEnd();
                    //convert to list of classes (parse java)
                }
            }
            list = JsonConvert.DeserializeObject<List<scorePosition>>(fileString);
            return list;
        }
        //add method for saving data also

        public static void SaveHighScoreList(List<scorePosition> saveList)
        {
            // need the path to the file
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string filename = Path.Combine(path, "highScores.txt");
            // use a stream writer to write the list
            using (var writer = new StreamWriter(filename, false))
            {
                // stringify equivalent
                string jsonText = JsonConvert.SerializeObject(saveList);
                writer.WriteLine(jsonText);
            }
        }
    }
}
