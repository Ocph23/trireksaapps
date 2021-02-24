using ModelsShared.Models;
using ShareModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrireksaMobile.Services
{
    public interface IPenjualanService
    {
        Task<Penjualan> GetBySTT(string stt);
        Task<Deliverystatus> UpdateDeliveryStatusById(int sTT, Deliverystatus deliveryStatus);
    }

    public class PenjualanService : IPenjualanService
    {
        private string controller = "api/penjualans";

        public async Task<Penjualan> GetBySTT(string stt)
        {
            try
            {
                int astt = Convert.ToInt32(stt);
                using (var client = new RestService())
                {
                    var uri = $"{controller}/GetBySTT/{astt}";
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.GetResult<Penjualan>();
                    }
                    throw new SystemException(await client.Error(response));
                }
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<Deliverystatus> UpdateDeliveryStatusById(int id, Deliverystatus deliveryStatus)
        {
            try
            {
                using (var client = new RestService())
                {
                    var uri = $"{controller}/UpdateDeliveryStatus/{id}";
                    var response = await client.PutAsync(uri, client.GenerateHttpContent(deliveryStatus));
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.GetResult<Deliverystatus>();
                    }
                    throw new SystemException(await client.Error(response));
                }
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }
    }
}
