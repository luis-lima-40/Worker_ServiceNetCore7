using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SendEmail
{
    //*#############################################################################################################*/
    // Este projeto em console application .net7, é responsável por enviar e-mail inclusive com anexo
    //*#############################################################################################################*/


    public class Email //altere para publica
    {
                //vamos ter 3 propriedades, snipet prop tab + tab
        public string Provedor { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        
        public Email(string provedor, string username, string password)
        {
            //agora crie o construtor, clique no nome da classe com o totão direito, selecione refatorar e crie o construtor, adicione verificação de nulos
            Provedor = provedor ?? throw new ArgumentNullException(nameof(provedor));
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }

        //crie o metodo do tipo void, não retorna nada
        public void SendEmail(List<string> emailsTo, string subject, string body, List<string> attachments) // passe os atributos que vc quer enviar aqui, como a lista de email "emailsTo" é a lista para quem vamos enviar, depois o subject que é o assunto, o corpo do email que é o body  e por ultimo a lista de anexos que será um caminho onde estará um ou mais arquivos
        {
            //primeiro vamos preparar o email com uma classe, depois fazer o envio, vamos fazer com conceitos de clean code
            var message = PrepareteMessage(emailsTo, subject, body, attachments);

            SendEmailBySmtp(message);
        }

        private MailMessage PrepareteMessage (List<string> emailsTo, string subject, string body, List<string> attachments) 
        {
            var mail = new MailMessage(); //aqui vamos criar uma classe do tipo MailMessage e seus atributos estarão logo abaixo:
            mail.From = new MailAddress(Username);//este atributo não recebe  uma string, aqui é o email da minha conta pessoal que estarei fazendo o envio
            //os demais atributos é pra quem eu quero enviar que pode ser mais de uma pessoa

            foreach (var email in emailsTo)
            {
                //vamos colocar um validador de e-mail para não dar erro e pausar o processo, veja o metodo abaixo
                if (ValidateEmail(email))
                {
                    mail.To.Add(email);
                } 
            }

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true; //aqui é para utilizar HTML no body, coloque este atributo como true

            foreach (var file in attachments)
            {
                var data = new Attachment(file, MediaTypeNames.Application.Octet);
                ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(file);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(file);

                mail.Attachments.Add(data);
            }

            return mail;
        }

        private bool ValidateEmail(string email) //verifica se é um email valido
        {
            //Regex expression = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Regex expression = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");
            if (expression.IsMatch(email))
                return true;
            return false;
        }


        private void SendEmailBySmtp(MailMessage message)
        {
            //SmtpClient smtpClient = new SmtpClient("smtp.office365.com");
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = Provedor;
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Timeout = 50000;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(Username, Password);
            smtpClient.Send(message);
            smtpClient.Dispose();
        }



    }
}
