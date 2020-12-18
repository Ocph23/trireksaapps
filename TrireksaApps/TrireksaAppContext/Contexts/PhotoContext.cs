using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

using System.Threading.Tasks;
using TrireksaAppContext.Models;

namespace TrireksaAppContext
{
    public class PhotoContext
    {
        private readonly ApplicationDbContext db;

        public PhotoContext(string Path)
        {
            this.AppPath = Path;
        }

        public PhotoContext(ApplicationDbContext _db)
        {
            db = _db;
        }

        public string AppPath { get; }

        public List<Photo> GetPhotoByPenjualanId(int Id)
        {
            var data = from a in db.Photo.Where(O => O.PenjualanId == Id)
                       select new Photo { Ext = a.Ext, File = a.File, Id = a.Id, Path = a.Path, PenjualanId = a.PenjualanId };
            List<Photo> list = new List<Photo>();
            foreach (var item in data)
            {
                list.Add(item);
            }
            return list;
        }


        public Task<byte[]> GetPicture(Photo item)
        {
            try
            {
                var path = string.Format(@"{0}{1}.{2}", AppPath, item.File, item.Ext);
                using (var ms = new MemoryStream())
                {
                    var stream = new FileStream(path, FileMode.Open);
                    stream.CopyTo(ms);
                    byte[] data = ms.ToArray();
                    stream.Close();
                    return Task.FromResult(data);
                }

            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }

        }

        public Photo GetPhotoById(int id)
        {
            var item = db.Photo.Where(O => O.Id == id).FirstOrDefault();
            if (item != null)
            {
                return new Photo { Ext = item.Ext, File = item.File, Id = item.Id, PenjualanId = item.PenjualanId, Path = item.Path };
            }
            else
                throw new SystemException("Photo Tidak Ditemukan");
        }

        public Task<byte[]> GetThumb(Photo item)
        {
            var path = string.Format(@"{0}{1}.{2}", AppPath, item.File, item.Ext);
            //@"D:\Trireksa\JustDesktop\Trireksa\TrireksaWebsite\TrireksaWebsite\PenjualanPhotoGalery\gambar.jpg";
            using (var ms = new MemoryStream())
            {
                var stream = new FileStream(path, FileMode.Open);
                stream.CopyTo(ms);
                byte[] data = ms.ToArray();
                stream.Close();
                return Task.FromResult(Scale(data));

            }
        }

        public async Task<Photo> AddNewPhoto(Photo ph, string dir)
        {

            DateTime date = DateTime.Now;
            var random = new Random(DateTime.Now.Second);
            try
            {
                if (ph.PenjualanId == 0 && ph.Stt > 0)
                {
                    var penj = db.Penjualan.Where(O => O.Stt == ph.Stt).FirstOrDefault();
                    if (penj != null)
                        ph.PenjualanId = penj.Id;
                    else
                    {
                        throw new SystemException("Nomor SPB Tidak Ditemukan");
                    }
                }

                if (ph.PenjualanId > 0)
                {
                    string fileName = string.Format("{0:D10}-{1}", ph.PenjualanId, random.Next(1000000, 9999999));
                    string fullFileName = string.Format("{0}{1}.{2}", AppPath, fileName, ph.Ext);

                    using (FileStream file = new FileStream(fullFileName, FileMode.Create, System.IO.FileAccess.Write))
                    {
                        var ms = new MemoryStream(ph.Picture);
                        byte[] bytes = new byte[ms.Length];
                        ms.Read(bytes, 0, (int)ms.Length);
                        file.Write(bytes, 0, bytes.Length);
                        ms.Close();
                    }

                    ph.File = fileName;

                    db.Photo.Add(new Photo { Ext = ph.Ext, File = fileName, Path = dir, PenjualanId = ph.PenjualanId });
                    if (ph.Id > 0)
                    {
                        ph.Thumb = await GetThumb(ph);
                        return ph;
                    }
                    else
                        throw new SystemException("Data Tidak Tersimpan");
                }
                else
                {
                    throw new SystemException("Tentukan Penjualan/SPB ");
                }



            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }


        }

        public async Task<bool> DeletePhoto(Photo result)
        {
            string fullFileName = string.Format("{0}{1}.{2}", AppPath, result.File, result.Ext);
            var trans = db.Database.BeginTransaction();
            try
            {
                var exitsPhoto = db.Photo.Where(x => x.Id == result.Id).FirstOrDefault();
                if (exitsPhoto != null)
                {
                    db.Photo.Remove(exitsPhoto);
                    if (await db.SaveChangesAsync() > 0)
                    {
                        File.Delete(fullFileName);
                        trans.Commit();
                        return true;
                    }
                    throw new SystemException("Data Not Deleted !");
                }
                else
                    throw new SystemException("Data Not Found !");
                
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new SystemException(ex.Message);
            }

        }

        public byte[] Scale(byte[] byteArray)
        {

            try
            {
                int height = 70;
                int width = 70;
                //Grab the Original Image From SQL
                Image imThumbnailImage;

                Image OriginalImage;
                MemoryStream ms = new MemoryStream();

                // Stream / Write Image to Memory Stream from the Byte Array.
                ms.Write(byteArray, 0, byteArray.Length);

                OriginalImage = Image.FromStream(ms);

                // Shrink the Original Image to a thumbnail size.
                imThumbnailImage = OriginalImage.GetThumbnailImage(width, height, new Image.GetThumbnailImageAbort(ThumbnailCallBack), IntPtr.Zero);

                // Save Thumbnail to Memory Stream for Conversion to Byte Array.
                MemoryStream myMS = new MemoryStream();
                imThumbnailImage.Save(myMS, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] test_imge = myMS.ToArray();

                // Finally, Update Gallery's Thumbnail.

                imThumbnailImage.Dispose();
                OriginalImage.Dispose();
                return test_imge;
            }
            catch (Exception ex)
            {
                throw new SystemException("Resize Error.", ex);
            }
        }

        private bool ThumbnailCallBack()
        {
            return true;
        }
    }
}
