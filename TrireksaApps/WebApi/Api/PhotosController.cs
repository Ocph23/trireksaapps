using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrireksaAppContext;
using System.IO;
using TrireksaAppContext.Models;

namespace WebApi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorize]
    public class PhotosController : ControllerBase
    {

        private PhotoContext context;
        public PhotosController(PhotoContext _context)
        {
            context = _context;
        }

        [HttpGet("GetPhotosByPenjualanId/{id}")]
        public async Task<IActionResult> GetPhotosByPenjualanId(int Id)
        {
            List<Photo> photos = context.GetPhotoByPenjualanId(Id);


            foreach (var item in photos)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/bukti/images/");
                context = new PhotoContext(path);
                item.Thumb = await context.GetThumb(item);
            }
            return Ok(photos);
        }

        [HttpGet("GetPictureById/{id}")]
        public async Task<IActionResult> GetPictureById(int id)
        {
            try
            {
                var result = context.GetPhotoById(id);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/bukti/images/");
                context = new PhotoContext(path);
                return Ok(await context.GetPicture(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [ApiAuthorize]
        [HttpPost]
        public async Task<IActionResult> AddNewPhoto(Photo ph)
        {
            var pathName = "PenjualanPhotoGalery";
            ph.Path = pathName;
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/bukti/{pathName}/");
            //if (!Request.Content.IsMimeMultipartContent())
            //    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable,
            //    "This request is not properly formatted"));
            //var streamProvider = new MultipartFileStreamProvider(path);
            //var res= await Request.Content.ReadAsMultipartAsync(streamProvider);
            try
            {
                if (ph != null)
                {
                    var context = new PhotoContext(path);
                    return Ok(await context.AddNewPhoto(ph, pathName));
                }
                else
                    throw new SystemException("Tidak ada gambar yang dikirim");

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [ApiAuthorize(Roles = "Administrator, Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            try
            {
                var result = context.GetPhotoById(id);
                var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/bukti/{result.Path}/");
                context = new PhotoContext(path);
                return Ok(await context.DeletePhoto(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
