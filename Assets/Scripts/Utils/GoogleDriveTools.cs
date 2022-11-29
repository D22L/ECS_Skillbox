using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityGoogleDrive;
using UnityGoogleDrive.Data;
using System.Threading.Tasks;

namespace ECS_Project
{
    public static class GoogleDriveTools
    {
        public static  List<File> FileList()
        {
            List<File> list = new List<File>();
            GoogleDriveFiles.List().Send().OnDone += fileList => { list = fileList.Files; };
            return list;
        }

        public static async Task<File> UploadNewFile(string obj)
        {
            var file = new File { Name = "GameData.json", Content = Encoding.ASCII.GetBytes(obj) };

            return await GoogleDriveFiles.Create(file).Send();             
        }
        public static async Task<File> UploadFileData(File file, string obj)
        {
            var newFile = new File { Name = file.Name, Content = Encoding.ASCII.GetBytes(obj) };                     
            return await GoogleDriveFiles.Update(file.Id, newFile).Send();
        }

        public static async Task<File> Download(string fileId)
        {
            return await GoogleDriveFiles.Download(fileId).Send();                             
        }

    }
}
