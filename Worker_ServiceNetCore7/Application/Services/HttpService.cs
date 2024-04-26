using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Worker_ServiceNetCore7.Application.Interfaces;

namespace Worker_ServiceNetCore7.Application.Services
{
    public class HttpService : IHttpService //inplemente esta intercace com o botão direito implement interfaces
    {
        public async Task<HttpStatusCode> VerificaStatusSite(string url) // adicione o async
        {
            var cliente = new HttpClient();// instancie o cliente dando um new HttpClient();
            try
            {
                var response = await cliente.GetAsync(url); // aqui no () vai fazer uma requisição get para a url lã encima no VerificaStatusSite 
                return response.StatusCode; //agora retorne o state code
            }
            catch (HttpRequestException) //Se por acaso ele der erro acima e não achar o url, eu quero que ele retorne um HttpStatusCode.NotFound
            {

                return HttpStatusCode.NotFound;
            }
            finally 
            { 
                cliente.Dispose();
            }
        }
    }
}
