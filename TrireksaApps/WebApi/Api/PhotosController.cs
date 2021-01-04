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

        private readonly PhotoContext context;
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
                return Ok(await context.GetPicture(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [ApiAuthorize]
        [HttpPost]
        public async Task<IActionResult> Post(Photo ph)
        {
            ph.Path = $"wwwroot/bukti/pictures/";
            try
            {
                if (ph != null)
                {
                    return Ok(await context.AddNewPhoto(ph));
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
                return Ok(await context.DeletePhoto(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
