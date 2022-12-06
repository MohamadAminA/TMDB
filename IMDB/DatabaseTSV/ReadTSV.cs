using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DatabaseTSV
{
    public class ReadTSV
    {
        public static List<List<string>> Read(string filename)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "top200IMDB", filename + ".tsv");
            string result = File.ReadAllText(filePath);
            List<string> records = result.Split("\r\n").ToList();
            List<List<string>> Finallresult = new List<List<string>>();
            foreach (var item in records.SkipLast(1))
            {
                Finallresult.Add(item.Split("\t").ToList());
            }
            return Finallresult;
        }
    }
}
