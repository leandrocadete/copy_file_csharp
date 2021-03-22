using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace copyFilesAndDirectories {
    class Program {
        static void Main (string[] args) {
            
            if (args.Length <= 1) {
                Console.Error.WriteLine("Missing source or targets");
                #if DEBUG
                return;
                #endif
            }
            string sourcePath;
            string targetPath;
#if DEBUG

            sourcePath = args[0];
            targetPath = args[1];
#else
            sourcePath = "t_target";
            targetPath = "temp";
#endif
            if (!Directory.Exists (targetPath)) {
                Directory.CreateDirectory (targetPath);
            }

            List<String> dirs = new List<string>(){sourcePath};
            dirs.AddRange(FindDirectories(sourcePath));

            // ================== Create Directory structure ===========================
            foreach (string str in dirs) {
                string strPath = $"{targetPath}/{str}";
                if (!System.IO.Directory.Exists(strPath)){
                    System.IO.Directory.CreateDirectory(strPath);
                    System.Console.WriteLine("{0} created", strPath);
                } else {
                    System.Console.WriteLine("{0} already exists", strPath);
                }
            }
            // ==========================================================================
            // ================== Copy files to a directory structure =============
            foreach (string strSource in dirs) {
                string strDestPath = $"{targetPath}/{strSource}";

                //System.Console.WriteLine("=======================GetFiles of {0}", strSource);
                var files = System.IO.Directory.GetFiles(strSource);
                
                foreach (String f in files) {
                    System.IO.FileInfo _fi = new System.IO.FileInfo(f);
                    // TODO verify if is a directory

                    System.Console.WriteLine("Files###############: {0}", $"{f}");
                    System.Console.WriteLine("Files@@@@@@@@@@@@@@@: {0}", $"{targetPath}/{f}");
                    System.IO.File.Copy($"{f}", $"{targetPath}/{f}", true);
                }
            }
            // ==========================================================================

            // --------------------------------- Inner function ---------------------------------
            List<string> FindDirectories(string path) {
                List<string> drs = new List<string>(System.IO.Directory.GetDirectories(path));
                List<string> temp = new List<string>();
                foreach(var d in drs) {
                    temp.AddRange(FindDirectories(d));
                }
                drs.AddRange(temp);
                return drs;

            }
            // ----------------------------------------------------------------------------------


        }
    }
}