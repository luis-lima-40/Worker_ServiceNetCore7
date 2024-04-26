using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Worker_ServiceNetCore7.Application.Interfaces
{
    public interface IHttpService
    {
        Task<HttpStatusCode> VerificaStatusSite(string url);
    }
}
