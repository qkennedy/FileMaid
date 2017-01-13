using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileMaid.Model;

namespace FileMaid
{
    public class TimeClusterer
    {
        /// <summary>
        /// My goal with this method is to cluster the files by creation or write time
        /// I might just be able to do this in LINQ?
        /// Here is what I think I need to do here.  Have some way to keep 2 times for each group
        /// check if each file falls within those times, 
        /// 
        /// Easiest way might just be to sort them by time, and then start from the middle? and 
        /// do a density based cluster analysis
        /// </summary>
        /// <param name="list"></param>
        public static void timeCluster(List<FileDetailModel> list, double strengthReq)
        {
            int nextGroup = 1;
            FileDetailModel curr;
            DateTime currTime;
            foreach(FileDetailModel f in list)
            {
                
            }
        }
    }
}
