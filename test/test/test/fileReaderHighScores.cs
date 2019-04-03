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
            List<scorePosition> list = new List<scorePosition>();

            string fileString;
            //read the defaultData.txt
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(HighScoreReplay)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("test.HighScores.highScores.txt");
            using (var reader = new StreamReader(stream))
            {
                fileString = reader.ReadToEnd();
                //convert to list of classes (parse java)
            }
            list = JsonConvert.DeserializeObject<List<scorePosition>>(fileString);
            return list;
        }
    }
}
