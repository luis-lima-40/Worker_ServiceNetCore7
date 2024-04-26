using Worker_ServiceNetCore7;
using Worker_ServiceNetCore7.Application.Interfaces;
using Worker_ServiceNetCore7.Application.Services;


//*#############################################################################################################*/
// Este Worker_Service tem como objetivo verificar se uma API ou Site est� on-line, se N�O  estiver
// retorna um not-ok para meu Worker_Service que ir� disparar um e-mail avisando que o site esta off-line
//*#############################################################################################################*/

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        //Inje��es de dependencia
        services.AddHostedService<Worker>()
        .AddSingleton<IEmail, EnviaEmail>() //AddSingleton porque o WorkService funciona com o AddSingleton pois ele vai ter um processo s� rodando o tempo todo
        .AddSingleton<IHttpService, HttpService>();
    })
    .Build();

host.Run();
