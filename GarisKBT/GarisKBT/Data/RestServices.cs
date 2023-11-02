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
        public RestServices()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public List<Marga> Items { get; private set; }
        public async Task<MargaDetail> GetMargaDetailAsync(int id)
        {
            MargaDetail marga;
            var uri = new Uri(string.Format(Constants.DetailMarga + "/" + id, string.Empty));
            try
            {

                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    marga = JsonConvert.DeserializeObject<MargaDetail>(content);
                    return marga;
                }
            }
            catch (Exception ex)
            {
                App.error = ex.Message;
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            return null;
        }

        public async Task<List<GetSilsilahResult>> GetSilsilahAsync(int id)
        {
            List<GetSilsilahResult> silsilahResult;
            var uri = new Uri(string.Format(Constants.GetSilsilah + "/" + id, string.Empty));
            try
            {

                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    silsilahResult = JsonConvert.DeserializeObject<List<GetSilsilahResult>>(content);
                    return silsilahResult;
                }
            }
            catch (Exception ex)
            {
                App.error = ex.Message;
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            return null;
        }

        public async Task<List<GetSilsilahResult>> GetChildsAsync(int id)
        {
            List<GetSilsilahResult> silsilahResult;
            var uri = new Uri(string.Format(Constants.GetChilds + "/" + id, string.Empty));
            try
            {

                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    silsilahResult = JsonConvert.DeserializeObject<List<GetSilsilahResult>>(content);
                    return silsilahResult;
                }
            }
            catch (Exception ex)
            {
                App.error = ex.Message;
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            return null;
        }
        public async Task<List<Marga>> GetTasksAsyncSearchByName(string name)
        {

            Items = new List<Marga>();
            var uri = new Uri(string.Format(Constants.RestUrlSearchByName + name, string.Empty));
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
        public async Task<string> GetGuidanceLink(string key)
        {
            var keyValue = new KeyValue();
            var uri = new Uri(string.Format(Constants.GetGuidance + "?key=" + key, string.Empty));
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    keyValue = JsonConvert.DeserializeObject<KeyValue>(content);
                }
            }
            catch (Exception ex)
            {
                App.error = ex.Message;
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            return keyValue.Value;
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
