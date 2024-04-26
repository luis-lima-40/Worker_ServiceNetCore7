using Worker_ServiceNetCore7;
using Worker_ServiceNetCore7.Application.Interfaces;
using Worker_ServiceNetCore7.Application.Services;


//*#############################################################################################################*/
// Este Worker_Service tem como objetivo verificar se uma API ou Site está on-line, se NÃO  estiver
// retorna um not-ok para meu Worker_Service que irá disparar um e-mail avisando que o site esta off-line
//*#############################################################################################################*/

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        //Injeções de dependencia
        services.AddHostedService<Worker>()
        .AddSingleton<IEmail, EnviaEmail>() //AddSingleton porque o WorkService funciona com o AddSingleton pois ele vai ter um processo só rodando o tempo todo
        .AddSingleton<IHttpService, HttpService>();
    })
    .Build();

host.Run();
