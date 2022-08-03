using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace ArabDT.Framwork.NetTypes
{
    public class StringFile
    {
        public string Base64 { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string FileName => Name + "." + Extension;
    }
    public static class StringFileExtension
    {
        public static List<IFormFile> ToFormFiles(this List<StringFile> stringFile)
        {
            List<IFormFile> formFiles = new List<IFormFile>();

            foreach (var fileString in stringFile)
            {
                formFiles.Add(fileString.ToFormFile());

            }
            return formFiles;
        }

        public static IFormFile ToFormFile(this StringFile fileString)
        {
            byte[] bytes = Convert.FromBase64String(fileString.Base64);
            MemoryStream stream = new MemoryStream(bytes);

            return new FormFile(stream, 0, bytes.Length, fileString.Name, fileString.FileName);
        }
    }
}
