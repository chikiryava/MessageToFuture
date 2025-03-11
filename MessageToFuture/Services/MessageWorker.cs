
using MailKit.Net.Smtp;
using MessageToFuture.Interfaces;
using MessageToFuture.Models;
using MimeKit;
using System.Diagnostics;


namespace MessageToFuture.Services
{
    public class MessageWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public MessageWorker(IServiceScopeFactory scopeFactory, ILogger<MessageWorker> logger,IConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _configuration = configuration;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {

            

                while (!stoppingToken.IsCancellationRequested)
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    _logger.LogInformation("Worker запущен!");
                    _logger.LogInformation("таймер запущен!");

                    using (var scope = _scopeFactory.CreateScope()) // Создаём Scope
                    {
                        var messageService = scope.ServiceProvider.GetRequiredService<IMessageService>(); // Получаем Scoped сервис

                        List<Message> messagesToSend = await messageService.GetMessageToDelivery();
                        await SendMessages(messagesToSend);
                        await messageService.UpdateMessages(messagesToSend);
                    }


                    _logger.LogInformation("Worker отработал");
                    stopwatch.Stop();

                    _logger.LogInformation($"Время потраченное на отправку: {stopwatch.Elapsed}");
                    await Task.Delay(4000000);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка программы: {ex.Message}");
            }
        }
           

        private async Task SendMessages(List<Message> messageToSend)
        {
            _logger.LogInformation($"{_configuration["MailStrings:Mail"]} {_configuration["MailStrings:Key"]}");
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_configuration["MailStrings:Mail"], _configuration["MailStrings:Key"]);

            int i = 1;
            foreach (Message message in messageToSend)
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("Письмо из прошлого", _configuration["MailStrings:Mail"]));
                emailMessage.To.Add(new MailboxAddress(message.User.Name, message.User.Email));
                emailMessage.Subject = $"Письмо из прошлого!";

                emailMessage.Body = new TextPart("plain")
                {
                    Text = $@"Привет! Если ты помнишь, то {message.CreatedAt} ты захотел отправить письмо, вот оно!

{message.Title}

{message.Content}"
                };

                try
                {
                    await smtp.SendAsync(emailMessage);
                    _logger.LogInformation($"Письмо пользователю {message.User.Name} ({message.User.Email}) отправлено");
                    _logger.LogInformation($"Отправлено сообщений: {i}");
                    message.IsDelivered = true;
                    using (var scope = _scopeFactory.CreateScope()) // Создаём Scope
                    {
                        var messageService = scope.ServiceProvider.GetRequiredService<IMessageService>(); // Получаем Scoped сервис

                        await messageService.UpdateMessage(message);
                    }
                    i++;
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Ошибка отправки письма пользователю {message.User.Email}: {ex.Message}");
                }
            }

            await smtp.DisconnectAsync(true);
            _logger.LogInformation($"Отправлено писем: {i}");
        }

    }
}
