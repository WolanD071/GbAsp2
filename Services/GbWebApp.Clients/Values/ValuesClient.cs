using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using GbWebApp.Clients.Base;
using System.Collections.Generic;
using GbWebApp.Interfaces.TestAPI;
using Microsoft.Extensions.Configuration;

namespace GbWebApp.Clients.Values
{
    public class ValuesClient : BaseClient, IValuesService
    {
        public ValuesClient(IConfiguration Configuration) : base(Configuration, "api/values") { }

        public IEnumerable<string> Get()
        {
            var response = Http.GetAsync(Address).Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<IEnumerable<string>>().Result
                : Enumerable.Empty<string>();
        }

        public string Get(int id)
        {
            var response = Http.GetAsync($"{Address}/{id}").Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<string>().Result : string.Empty;
        }

        public Uri Create(string value)
        {
            var response = Http.PostAsJsonAsync(Address, value).Result;
            return response.EnsureSuccessStatusCode().Headers.Location;
        }

        public HttpStatusCode Edit(int id, string value)
        {
            var response = Http.PutAsJsonAsync($"{Address}/{id}", value).Result;
            return response.EnsureSuccessStatusCode().StatusCode;
        }

        public bool Remove(int id)
        {
            var response = Http.DeleteAsync($"{Address}/{id}").Result;
            return response.IsSuccessStatusCode;
        }
    }
}
