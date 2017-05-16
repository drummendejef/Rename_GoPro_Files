using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_File_Interactions
{
    public class GoPro_File_Interactions
    {
        public List<FileInfo> GetGoProFilesFromFolder(string folderpath)
        {
            //Get all files that contain "GP" or "GoPR" and are an .MP4 file. (GoPro files standard are like this, GOPR for the first file, GP for all the rest)
            //Orderby LastWriteTime to look at the time they were created on the GoPro card itself.
            return new DirectoryInfo(folderpath).GetFiles().Where(f => (f.Name.Contains("GP") || f.Name.Contains("GOPR")) && f.Extension == ".MP4").OrderBy(f => f.LastWriteTime).ToList();
        }

        //Check if a file was already renamed, in that case we remove the "PartX_" from the name before we rename it.
        public string CheckAlreadyRenamed(FileInfo file)
        {
            //File was already renamed
            if (file.Name.Contains("Part"))
            {
                //We split the "part" off the name and take the "not-part" part of the name. (We hope there aren't 2 "Part"s in the name.
                return file.Name.Split('_')[1];
            }
            return file.Name;
        }
    }
}
