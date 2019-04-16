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
        //Newtonsoft.Json utilized here
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
                }
            }
            catch
            {
                //read the default highScores.txt first time the app is loaded
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(HighScoreReplay)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("test.HighScores.highScores.txt");
                using (var reader = new StreamReader(stream))
                {
                    fileString = reader.ReadToEnd();
                }
            }
            //convert to list of scoreposition - score board
            list = JsonConvert.DeserializeObject<List<scorePosition>>(fileString);
            return list;
        }
        //add method for saving data also
        public static void SaveHighScoreList(List<scorePosition> saveList)
        {
            //save to the local for the next time the player plays
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string filename = Path.Combine(path, "highScores.txt");
            using (var writer = new StreamWriter(filename, false))
            {
                string jsonText = JsonConvert.SerializeObject(saveList);
                writer.WriteLine(jsonText);
            }
        }
    }
}
