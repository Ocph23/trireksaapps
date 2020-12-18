using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using ModelsShared.Models;
using TrireksaApp.Common;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;

namespace TrireksaApp.CollectionsBase
{
    public class UserPorfileCollection
    {
        //  private UserProfileContext client = new UserProfileContext();
        Client client = new Client("UserProfile");
        public UserPorfileCollection()
        {
            Source = new ObservableCollection<Userprofile>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            InitAsync();
        }

        private async void InitAsync()
        {
            var result = await client.GetAsync<List<Userprofile>>("Get");
            if(result!=null)
                foreach(var item in result)
                {
                    Source.Add(item);
                }

            SourceView.Refresh();
        }

        internal async Task<Userprofile> GetProfile()
        {
            return  await client.GetAsync<Userprofile>("GetProfile");
            
        }

        public Userprofile SelectedItem { get; set; }

        internal async Task<Roles> AddNewUserRole(Userprofile selectedItem, Roles role)
        {
            string url = string.Format("AddNewRole?userId={0}", selectedItem.Id);
            return await client.PostAsync<Roles>(url,role);
        }

        internal async Task<Roles> RemoveRole(Userprofile selectedItem, Roles roleSelected)
        {
            string url = string.Format("RemoveRole?userId={0}&roleId={1}", selectedItem.Id, roleSelected.Id); ;
            var result= await client.ClientContext.DeleteAsync(url);
            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Roles>(result.Content.ReadAsStringAsync().Result);
            }
            else
            {
                return null;
            }
        }

        public ObservableCollection<Userprofile> Source { get; set; }

        public CollectionView SourceView { get; set; }

        public async Task<bool> Add(Userprofile item)
        {
            try
            {
                var obj = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(obj);
                var result = await client.ClientContext.PostAsync("/Api/UserProfile", content);
                if (result.IsSuccessStatusCode)
                {
                    var newitem = JsonConvert.DeserializeObject<Userprofile>(result.Content.ReadAsStringAsync().Result);
                    this.Source.Add(newitem);
                    return true;
                }else
                {
                    throw new SystemException("Data Tidak Tersimpan");
                }
            }
            catch (Exception ex)
            {

                ResourcesBase.ShowMessageError(ex.Message);
                return false;

            }
           
        }

        internal async Task<List<Roles>> GetRoles()
        {
            using (var client = new Client("Roles"))
            {
                return await client.GetAsync<List<Roles>>("GET");
            }
        }

        internal async Task<bool> Login(string userName, string password)
        {
            using (var client = new Client())
            {
                var strcontent = string.Format("grant_type=password&username={0}&password={1}", userName, password);
                HttpContent content = new StringContent(strcontent, Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = await client.ClientContext.PostAsync("Token", content);
                if (response.IsSuccessStatusCode)
                {
                    var x = await response.Content.ReadAsStringAsync();
                  var   Token = JsonConvert.DeserializeObject<AuthenticationToken>(x);
                    if (Token.Token != null)
                    {
                        ResourcesBase.Token = Token;
                        return true;
                    }
                    else
                        return false;
                   
                }
                else
                {
                    return false;
                }

            }
           // return client.Login(userName, password);
        }

        internal async  Task<bool> Update(int v, Userprofile item)
        {
            var res = await client.PutAsync<bool>("", item.UserId, item);
            if (res)
            {
                var userProfile = Source.Where(O => O.UserId == item.UserId).FirstOrDefault();
                if(userProfile!=null)
                {
                   item.FirstName= userProfile.FirstName;
                    item.LastName = userProfile.LastName;
                    item.Address = userProfile.Address;
                    item.PhoneNumber = userProfile.PhoneNumber;
                }
                return true;
            }
            else
                return false;
        }
    }
}
