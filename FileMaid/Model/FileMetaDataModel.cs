using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMaid.Model
{
    public class FileMetaDataModel
    {
        //TODO: This should eventually be changed, every layer having a copy of FileInfo is a waste of space.  
        public FileInfo info { get; set; }
        public string extension { get; set; }
        public List<String> delimStrings { get; set; }
        public List<char> delimiters { get; set; }
        public FileMetaDataModel(string name)
        {
            this.info = new FileInfo(name);
            string clippedName = StripExtension(info.Name);
            List<char> tmp = new List<char>();
            delimStrings = DelimitText(clippedName);
        }
        /// <summary>
        /// How this should work, in my mind is to 
        /// 1. Check for common delims 
        /// 2. If they exist, add them to the delimiters list, use them to delimit the text
        /// 3. Check to see what type of capitalization is used.  Delimit based off of results
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<string> DelimitText(string text)
        {
            delimiters = new List<char>();
            List<int> delimIndices = new List<int>();
            //List of Delims it checks for
            List<char> commonDelims = new List<char> { ' ', ',', '-', '.', '_' };
            int i;
            foreach (char c in commonDelims)
            {
                i = 0;
                i = text.IndexOf(c, i);
                while (i != -1)
                {
                    delimIndices.Add(i);
                    if (!delimiters.Contains(c))
                        delimiters.Add(c);
                    i = text.IndexOf(c, i + 1);
                }

            }
            delimIndices.Sort();
            List<string> delimStrings = new List<string>();
            int prev = 0;
            int curr = 0;
            for (i = 0; i < delimIndices.Count; i++)
            {
                prev = curr;
                curr = delimIndices[i];
                delimStrings.Add(text.Substring(prev, (curr - prev)));
            }
            return delimStrings;
        }

        public string StripExtension(string text)
        {
            if (text.Contains("."))
            {
                int last = text.LastIndexOf('.');
                extension = text.Substring(last, text.Length - last);
                return text.Substring(0, last);
            }
            else
            {
                return "";
            }
        }
    }
}
