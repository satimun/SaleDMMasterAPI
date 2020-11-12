using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SALEDM_API.Service
{
    public class FilesUtil
    {
        private static FilesUtil instant;

        public static FilesUtil GetInstant(IConfiguration configuration)
        {
            if (instant == null) instant = new FilesUtil(configuration);
            return instant;
        }

        private  IConfiguration Configuration;
        public FilesUtil(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public FileStream getFile(string path)
        {
            string url = Configuration["IMG_PATH"];
            if (!String.IsNullOrEmpty(path) && System.IO.File.Exists(path))
            {
                var filepath = Path.Combine(path);
                return new FileStream(filepath, FileMode.Open, FileAccess.Read);
            }
            return null;
        }

        public   string getImageURL(string imgpath)
        {
            string imgurl = null;
            if(!String.IsNullOrEmpty(imgpath) && System.IO.File.Exists(imgpath))
            {
                byte[] imageByteData = System.IO.File.ReadAllBytes(imgpath);
                string imageBase64Data = Convert.ToBase64String(imageByteData);
                imgurl = string.Format("data:image/png;base64,{0}", imageBase64Data);

            }

            return imgurl;
        }

        public int deleteFile(string imgpath)
        {
            var ret = 0;
            if (!String.IsNullOrEmpty(imgpath) && System.IO.File.Exists(imgpath))
            {
                
                try
                {
                    System.IO.File.Delete(imgpath);
                    ret = 1;
                }
                catch (System.IO.IOException ex)
                {
                    Console.WriteLine(ex.Message);
                    ret = 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ret = 0;
                }

            }
            return ret;

        }

        public string uploadPath()
        {
            string path = Configuration["IMG_PATH"];
            var uploads = Path.Combine(path, "uploads");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            return uploads;
        }

        public string attachPath()
        {
            string path = Configuration["IMG_PATH"];
            var uploads = Path.Combine(path, "fileupload");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            return uploads;
        }



        public  bool validImage(string imgpath)
        {
            return !String.IsNullOrEmpty(imgpath) && System.IO.File.Exists(imgpath);
        }

        public  string uploadCamera(string imgpath)
        {
            string fullpath = null;
            if (!String.IsNullOrEmpty(imgpath))
            {
                string extension = ".png";
                string newFileName = Guid.NewGuid() + extension;
                string newPath = Path.Combine(uploadPath(), newFileName);

                var arrImg = imgpath.Split(new char[] { ',' });

                imgpath = arrImg != null && arrImg.Length > 1 ? arrImg[1] : imgpath;
                byte[] mImageArr = Convert.FromBase64String(imgpath);
                System.IO.FileStream mFile = new System.IO.FileStream(newPath, System.IO.FileMode.CreateNew);
                mFile.Write(mImageArr, 0, mImageArr.Length);
                mFile.Flush();
                mFile.Close();
                mFile.Dispose();

                if (validImage(newPath))
                {
                    fullpath = newPath;
                }
            }

            return fullpath;
        }

        public  async Task<dynamic> uploadFile(IFormFile file)
        {
            string fullpath = null;
            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine(uploadPath(), file.FileName);
                string extension = Path.GetExtension(file.FileName);
                string newFileName = Guid.NewGuid() + extension;
                string newPath = Path.Combine(uploadPath(), newFileName);
                using (var fileStream = new FileStream(newPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                if (validImage(newPath))
                {
                    fullpath = newPath;
                }

            }

                return fullpath;
        }

        public async Task<dynamic> attachFile(IFormFile file)
        {
            string fullpath = null;
            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine(attachPath(), file.FileName);
                string extension = Path.GetExtension(file.FileName);
                string newFileName = Guid.NewGuid() + extension;
                string newPath = Path.Combine(attachPath(), newFileName);
                using (var fileStream = new FileStream(newPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                if (validImage(newPath))
                {
                    fullpath = newPath;
                }

            }

            return fullpath;
        }

        public  string uploadImgFile(IFormFile file)
        {            
            string fullpath = null;
            var obj = uploadFile(file);
            fullpath = obj != null ? obj.Result : null;
            return fullpath;
        }

        public string uploadAttachFile(IFormFile file)
        {
            string fullpath = null;
            var obj = uploadFile(file);
            fullpath = obj != null ? obj.Result : null;
            return fullpath;
        }


    }
}
