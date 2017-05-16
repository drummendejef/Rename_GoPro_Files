using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary_File_Interactions;

namespace Rename_GoPro_Files_.NETFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            //Properties
            List<FileInfo> allMP4filesinfolder; //All MP4 files found in the folder.
            GoPro_File_Interactions gpfi = new GoPro_File_Interactions();
            
            int counter = 1;

            //Ask user for folderpath
            Console.WriteLine("This program can be used to rename/order GoPro Files by the moment they were made.");
            Console.WriteLine("If a bunch of Gopro Files were already renamed, you can safely rerun this program.(it will delete the 'part' in front)");
            Console.WriteLine("Enter path to folder that contains GoPro video's and press enter");
            string path = Console.ReadLine();//Read folderpath when user presses the enter key

            while (path == string.Empty)
            {
                Console.WriteLine("Please give a path");
                path = Console.ReadLine();//Read folderpath when user presses the enter key
            }

            try
            {
                //Get all files that contain "GP" or "GoPR" and are an .MP4 file. (GoPro files standard are like this, GOPR for the first file, GP for all the rest)
                //Orderby LastWriteTime to look at the time they were created on the GoPro card itself.
                //allMP4filesinfolder = new DirectoryInfo(path).GetFiles().Where(f => (f.Name.Contains("GP") || f.Name.Contains("GOPR")) && f.Extension == ".MP4").OrderBy(f => f.LastWriteTime).ToList();
                allMP4filesinfolder = gpfi.GetGoProFilesFromFolder(path);

                //Run over all files 
                foreach (FileInfo file in allMP4filesinfolder)
                {
                    //Check if the file was already renamed, clean up name before sending it back.
                    //string name = CheckAlreadyRenamed(file);
                    string name = gpfi.CheckAlreadyRenamed(file);

                    Console.WriteLine("Item " + counter + " : " + name);//Print the files we are converting to show the user.
                    gpfi.ChangeName(file, counter, name);
                    //File.Move(file.FullName, path + "/Part" + counter + "_" + name);//Add File_number to each file in the right order.
                    counter++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong: " + ex.Message);
            }


            Console.WriteLine("End Program, press anykey to exit.");
            Console.ReadKey();
        }

        //Check if a file was already renamed, in that case we remove the "PartX_" from the name before we rename it.
        //static string CheckAlreadyRenamed(FileInfo file)
        //{
        //    //File was already renamed
        //    if (file.Name.Contains("Part"))
        //    {
        //        //We split the "part" off the name and take the "not-part" part of the name. (We hope there aren't 2 "Part"s in the name.
        //        return file.Name.Split('_')[1];
        //    }
        //    return file.Name;
        //}
    }
}
