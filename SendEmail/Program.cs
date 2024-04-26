using SendEmail;

var outlook = new Email("smtp.office365.com", "luis.carlos.worker@hotmail.com", ENV.PASSWORD);

outlook.SendEmail(
emailsTo: new List<string>
{
    "luis.carlos.lider@icloud.com"
},
subject: "Teste",
body: "Segue Anexo teste Worker_Service luis",
attachments: new List<string>
{
    @"C:\Users\CorteStiloTatuape\Documents\NormasRegrasdoSalaoemTXT.txt"
});