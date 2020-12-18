using System;
using System.Linq;
using System.Threading.Tasks;
using TrireksaAppContext.Models;

namespace TrireksaAppContext
{
    public class ColliesContext
    {
        private readonly ApplicationDbContext db;

        public ColliesContext(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public Task<Colly> Get(int id)
        {
            var result = db.Colly.Where(O => O.Id == id).FirstOrDefault();
            if (result != null)
                return Task.FromResult(result);
            else
                throw new SystemException("Data Tidak Ditemukan");
        }

        public async Task<Colly> Post(Colly value)
        {
            try
            {
                Tuple<bool, string> validateResult = ValidateColly(value);
                if (validateResult.Item1 == true)
                {
                    db.Colly.Add(value);
                    var result = await db.SaveChangesAsync();
                    if (result>0)
                        return value;
                    else
                        throw new SystemException("Data Tidak Tersimpan");
                }
                else
                {
                    throw new SystemException(validateResult.Item2);
                }
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        private Tuple<bool, string> ValidateColly(Colly value)
        {
            string message = "";
            bool valid = true;
            if (value.PenjualanId <= 0)
            {
                message = "Id Penjualan < 0";
                valid = false;
            }

            if (value.Weight <= 0)
            {
                valid = false;
                message = "Berat < 0";
            }

            if (value.CollyNumber <= 0)
            {
                valid = false;
                message = "Colly Number 0";
            }


            if (value.TypeOfWeight == TypeOfWeight.None)
            {
                valid = false;
                message = "Type Of Weight Can Not None";
            }

            return Tuple.Create(valid, message);
        }

        public async Task<Colly> Put(Colly value)
        {
            Tuple<bool, string> validateResult = ValidateColly(value);
            if (validateResult.Item1 == true)
            {

                db.Entry(value).CurrentValues.SetValues(value);

                var result = await db.SaveChangesAsync();

                if (result > 0)
                    return value;
                else
                    throw new SystemException("Data Tidak Tersimpan");
            }
            else
            {
                throw new SystemException(validateResult.Item2);
            }
        }

        public async Task<Colly> Delete(int id)
        {
            var existsData = db.Colly.Where(O => O.Id == id).FirstOrDefault();
            if (existsData != null)
            {
                db.Colly.Remove(existsData);
                var result = await db.SaveChangesAsync();
                if (result <= 0)
                    throw new SystemException("Data Tidak terhapus");
                return existsData;
            }
            throw new SystemException("Data Not Found !");

        }
    }

}