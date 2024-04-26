using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendEmail;
using Worker_ServiceNetCore7.Application.Interfaces;

namespace Worker_ServiceNetCore7.Application.Services
{
    public class EnviaEmail : IEmail
    {
        public void SendEmail(string to, string subject, string body)
        {
            var outlook = new Email("smtp.office365.com", "luis.carlos.worker@hotmail.com", ENV.PASSWORD);
            outlook.SendEmail(new List<string> { to }, subject, body, new());// nossa classe de email do projeto SendEmail, tem 4 parametros, o ultimo é uma lista de arquivo em anexo ao e-mail, neste email aqui não vamos mandar anexos, então vamos dar um new() para criar uma lista vazia neste parametro especifico.

        }
    }
}
