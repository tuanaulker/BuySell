using BuySell.Business.Application.Abstraction.Storage.Local;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using BuySell.Business.Application.Abstraction.Storage; // görmedi
using BuySell.Business.Application.Operations;

namespace BuySell.Business.Application.Services.Storage.Local
{
    public class LocalStorage : ILocalStorage//, Storage
    {
        readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment) 
        {
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task DeleteAsync(string pathOrContainerName, string fileName)
             => File.Delete($"{pathOrContainerName}\\{fileName}");
        


        public List<string> GetFiles(string pathOrContainerName)
        {
            DirectoryInfo directory = new(pathOrContainerName);
            return directory.GetFiles().Select(f => f.Name).ToList();
        }


        public bool HasFile(string pathOrContainerName, string fileName)
            => File.Exists($"{pathOrContainerName}\\{fileName}");


        async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write,
                    FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                //todo log!
                throw ex;
            }
        }

        private async Task<string> FileRenameAsync(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            string oldName = Path.GetFileNameWithoutExtension(fileName);
            string newFileName = $"{NameOperation.CharacterRegulatory(oldName) + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss")}{extension}";
            return newFileName;
        }
        

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        {
            //wwwroot/resource/product-images
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, pathOrContainerName);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string path)> datas = new();
            foreach (IFormFile file in files)
            {
                //string fileNewName = await FileRenameAsync(pathOrContainerName, file.Name, HasFile);
                string fileNewName = await FileRenameAsync(file.FileName);
                await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
                datas.Add((fileNewName, $"{pathOrContainerName}\\{fileNewName}"));
            }

            return datas;
        }
    }
}



 /*
         async Task<string> FileRenameAsync(string path, string fileName, bool first = true)
        {
            string newFileName = await Task.Run<string>(async () =>
                {
                    string extension = Path.GetExtension(fileName);
                    string newFileName = string.Empty;
                    if (first)
                    {
                        string oldName = Path.GetFileNameWithoutExtension(fileName);
                        newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";
                    }
                    else
                    {
                        newFileName = fileName;
                        int indexNo1 = newFileName.IndexOf("-");
                        if (indexNo1 == -1)
                            newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                        else
                        {
                            int lastIndex = 0;
                            while (true)
                            {
                                lastIndex = indexNo1;
                                indexNo1 = newFileName.IndexOf("-", indexNo1 + 1);
                                if(indexNo1 == -1)
                                {
                                    indexNo1 = lastIndex;
                                    break;
                                }
                            }
                            int indexNo2 = newFileName.IndexOf(".");
                            string fileNo = newFileName.Substring(indexNo1 + 1, indexNo2 - indexNo1 - 1);
                            if (int.TryParse(fileNo, out int _fileNo))
                            {
                                _fileNo++;
                                newFileName = newFileName.Remove(indexNo1 + 1, indexNo2 - indexNo1 - 1)
                                                    .Insert(indexNo1 + 1, _fileNo.ToString());
                            }else
                                newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                        }
                    }
                    if (File.Exists($"{path}\\{newFileName}"))
                        return await FileRenameAsync(path, newFileName, false);
                    else
                        return newFileName;
                });

            return newFileName;
        }
         */


