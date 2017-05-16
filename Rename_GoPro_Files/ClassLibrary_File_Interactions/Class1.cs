using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_File_Interactions
{
    public class Class1
    {
        public List<FileInfo> GetGoProFilesFromFolder(string folderpath)
        {
            //Get all files that contain "GP" or "GoPR" and are an .MP4 file. (GoPro files standard are like this, GOPR for the first file, GP for all the rest)
            //Orderby LastWriteTime to look at the time they were created on the GoPro card itself.
            return new DirectoryInfo(folderpath).GetFiles().Where(f => (f.Name.Contains("GP") || f.Name.Contains("GOPR")) && f.Extension == ".MP4").OrderBy(f => f.LastWriteTime).ToList();
        }
    }
}
