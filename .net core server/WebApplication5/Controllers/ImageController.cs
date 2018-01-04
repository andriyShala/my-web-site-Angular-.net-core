using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Entities;
using WebApplication5.Helpers;
using WebApplication5.Services;

namespace WebApplication5.Controllers
{
    [Route("api/Image")]
    public class ImageController : Controller
    {
        public static string hoststring = "";
        private IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2","value3" };
        //}

        [HttpGet]
        public async Task<FileContentResult> Get(ImageString image)
        {
            if(hoststring=="")
            {
                hoststring = HttpContext.Request.Host.Value;
            }
            Image _image = _imageService.GetById(image.ImageKey, image.Username);
          
            return File(_image.bytes, "image/jpeg",_image.Name+".jpg");
        }
        [HttpPost]
        public HttpResponseMessage UploadJsonFile()
        {

            
            string name = "shala97";
            HttpResponseMessage response = new HttpResponseMessage();
            var httpRequest = HttpContext.Request;
           
            if (httpRequest.Form.Files.Count > 0)
            {
                foreach (var file in httpRequest.Form.Files)
                {
                    using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                    {
                        var fileContent = binaryReader.ReadBytes((int)file.Length);
                        _imageService.Create(new Image() { bytes = fileContent, Name = file.Name, type = file.ContentType }, name);
                        binaryReader.Close();
                    }
                   
                }
            }
            return response;
        }
        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            string name="shala97";
            if (file == null) throw new Exception("File is null");
            if (file.Length == 0) throw new Exception("File is empty");

            using (Stream stream = file.OpenReadStream())
            {
                using (var binaryReader = new BinaryReader(stream))
                {
                    var fileContent = binaryReader.ReadBytes((int)file.Length);
                     _imageService.Create(new Image() {bytes=fileContent,Name=file.Name,type=file.ContentType },name);

                }
            }
            return StatusCode(200);
        }
    }
}