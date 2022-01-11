using GarisKBT.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GarisKBT.Data
{
    public class RestServices : IRestService
    {
        HttpClient client;
        public List<Marga> Items { get; private set; }
        public RestServices()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }
        public async Task<List<Marga>> RefreshDataAsync()
        {
            Items = new List<Marga>();
            var uri = new Uri(string.Format(Constants.RestUrl, string.Empty));
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<Marga>>(content);
                }
            }
            catch (Exception ex)
            {
                App.error = ex.Message;
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            return Items;
        }
        //public async Task<bool> SearchDataPostAsync()
        //{
        //    Items = new List<Marga>();
        //    var uri = new Uri(string.Format(Constants.SaveSearchData, string.Empty));
        //    try
        //    {
        //        var response = await client.GetAsync(uri);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var content = await response.Content.ReadAsStringAsync();
        //            Items = JsonConvert.DeserializeObject<List<Marga>>(content);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        App.error = ex.Message;
        //        Debug.WriteLine(@"ERROR {0}", ex.Message);
        //    }
        //    return Items;
        //}
        public async Task SaveSearchDataAsync(string jsonData)
        {
            var uriPost = new Uri(string.Format(Constants.SaveSearchData, string.Empty)); 
            //var uriPut = new Uri(string.Format(Constants.RestUrl, item.ID)); 
            try
            {
                var parameters = new Dictionary<string, string> { { "data", jsonData } };
                var encodedContent = new FormUrlEncodedContent(parameters);
                //var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                
                response = await client.PostAsync(uriPost, encodedContent);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"TodoItem successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
        }

    }
}
