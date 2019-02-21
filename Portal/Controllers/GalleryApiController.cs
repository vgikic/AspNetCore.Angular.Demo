using Angular.Core.Models.BindingModel;
using Angular.Core.Models.Dto;
using Angular.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Angular.Core.Controllers
{
    [Route("api/gallery")]
    [ApiController]
    public class GalleryApiController : BaseApiController
    {
        private const string FolderName = "Uploads";
        private readonly string DirectoryPath;

        private IHostingEnvironment hostingEnvironment;
        public GalleryApiController(IHostingEnvironment hostingEnvironment, ILogger<BaseApiController> logger, IMapper mapper) : base(logger, mapper)
        {
            this.hostingEnvironment = hostingEnvironment;
            DirectoryPath = Path.Combine(hostingEnvironment.WebRootPath, FolderName);
        }

        [HttpPost("file")]
        [DisableRequestSizeLimit]
        public IActionResult UploadFile([FromBody]IEnumerable<FileBindingModel> model)
        {
            try
            {
                Directory.CreateDirectory(DirectoryPath);
                foreach (var file in model)
                {
                    string fullPath = Path.Combine(DirectoryPath, FileNameResolver.AppendGuidToFileName(file.Name));
                    System.IO.File.WriteAllBytes(fullPath, file.Bytes);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        [HttpGet("file")]
        public IActionResult GetUploadedFiles()
        {
            try
            {
                if (!Directory.Exists(DirectoryPath))
                {
                    return Ok();
                }

                var files = Directory.EnumerateFiles(DirectoryPath).Select(fileName => new FileDto
                {
                    Url = Url.Content(fileName),
                    FullName = fileName.Split($@"{FolderName}\").Last(),
                    Base64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName)),
                    Name = FileNameResolver.RemoveGuidFromFileName(fileName.Split($@"{FolderName}\").Last())
                });

                return Ok(files);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        [HttpGet("file/{fileName}")]
        public IActionResult GetFile(string fileName)
        {
            string fullPath = Path.Combine(DirectoryPath, fileName);

            if (!System.IO.File.Exists(fullPath))
            {
                ModelState.AddModelError("File", "File does not exist");
                return BadRequest(ModelState);
            }

            var buffer = System.IO.File.ReadAllBytes(fullPath);
            var fcr = new FileContentResult(buffer, "application/octet")
            {
                FileDownloadName = "SomeFileDownloadName.someExtensions.png"
            };
            return fcr;
        }

        [HttpDelete("file/{fileName}")]
        public IActionResult DeleteFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return BadRequest();
            }
            string path = Path.Combine(DirectoryPath, fileName);
            System.IO.File.Delete(path);
            return Ok();
        }

    }
}