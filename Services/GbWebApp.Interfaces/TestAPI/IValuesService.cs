using System;
using System.Net;
using System.Collections.Generic;

namespace GbWebApp.Interfaces.TestAPI
{
    public interface IValuesService
    {
        IEnumerable<string> Get();

        string Get(int id);

        Uri Create(string value);

        HttpStatusCode Edit(int id, string value);

        bool Remove(int id);
    }
}
