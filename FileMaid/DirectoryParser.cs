using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMaid
{
    public static class DirectoryParser
    {
        public static List<string> getTopFiles(string rootDir)
        {
            List<string> files = new List<string>();
            foreach(string f in System.IO.Directory.EnumerateFiles(rootDir))
            {
                files.Add(f);
            }
            return files;
        }
        public static List<string> getTopDir(string rootDir)
        {
            List<string> folders = new List<string>();
            foreach (string f in System.IO.Directory.EnumerateDirectories(rootDir))
            {
                folders.Add(f);
            }
            return folders;
        } 
    }
}

