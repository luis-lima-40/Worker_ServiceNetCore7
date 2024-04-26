using Worker_ServiceNetCore7.Application.Interfaces;

namespace Worker_ServiceNetCore7
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEmail _email; //aqui vamos aproveitar o Worker para validar a regra de negocios que nos criamos que é o email e o httpServices
        private readonly IHttpService _httpService;
        private readonly IHostApplicationLifetime _applicationLifetime; //tempo de vida da aplicação 

        public Worker(ILogger<Worker> logger, IEmail email, IHttpService httpService, IHostApplicationLifetime applicationLifetime)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _email = email ?? throw new ArgumentNullException(nameof(email));
            _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
            _applicationLifetime = applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                //vamos verificar o status
                var statusSite = await _httpService.VerificaStatusSite("https://localhost:7016/swagger/index.html");

                if(statusSite != System.Net.HttpStatusCode.OK)
                {
                    _email.SendEmail("Luis.carlos.lider@icloud.com", "Worker Service - Atenção Servidor.API Off-Line", $" API inacessível em: {DateTimeOffset.Now}");
                    // se o serviço constatar um erro de site ou api off line, use um private readonly IHostApplicationLifetime _applicationLifetime; para parar a aplicação e não ficar disparando um email a cada segundo.
                    _applicationLifetime.StopApplication();
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
