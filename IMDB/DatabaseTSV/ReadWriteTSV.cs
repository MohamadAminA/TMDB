using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Primitives;

namespace DatabaseTSV
{
    public class ReadWriteTSV
    {
        public List<List<string>> Read(string filename)
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
        public void Write(List<List<string>> input)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "top200IMDB", "Export.tsv");
            string result = "";
            foreach (var record in input)
            {
                foreach (var item in record)
                {
                    result += item+ '\t';
                }
                result += "\r\n";
            }
            File.WriteAllText(filePath,result.ToString());
            return ;
        }
    }
}
