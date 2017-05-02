using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rename_GoPro_Files
{
    class Program
    {
        static void Main(string[] args)
        {
            //Properties
            List<FileInfo> allMP4filesinfolder; //All MP4 files found in the folder.
            int counter = 1;

            //Ask user for folderpath
            Console.WriteLine("Enter path to folder that contains GoPro video's and press enter");
            string path = Console.ReadLine();//Read folderpath when user presses the enter key

            if (path == string.Empty)//Check if user filled something in
            {
                Console.WriteLine("Please give a path");
            }
            else //User filled in the path
            {
                //Get all files that contain "GP" or "GoPR" and are an .MP4 file. (GoPro files standard are like this, GOPR for the first file, GP for all the rest)
                //Orderby LastWriteTime to look at the time they were created on the GoPro card itself.
                allMP4filesinfolder = new DirectoryInfo(path).GetFiles().Where(f => (f.Name.Contains("GP") || f.Name.Contains("GOPR")) && f.Extension == ".MP4").OrderBy(f => f.LastWriteTime).ToList();

                //Run over all files 
                foreach (FileInfo file in allMP4filesinfolder)
                {
                    Console.WriteLine("Item " + counter + " : " + file.Name);//Print the files we are doing to show the user.
                    File.Move(file.FullName, path + "/Part" + counter + "_" + file.Name);//Add File_number to each file in the right order.
                    counter++;
                }
            }


            Console.WriteLine("End Program, press anykey to exit.");
            Console.ReadKey();
        }
    }
}