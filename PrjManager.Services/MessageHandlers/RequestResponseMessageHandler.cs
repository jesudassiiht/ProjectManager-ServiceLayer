using Newtonsoft.Json;
using PrjManager.Infrastructure.Logging;
using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrjManager.Services.MessageHandlers
{
    public class RequestResponseMessageHandler : DelegatingHandler
    {
        readonly ILogger _logger;
        public RequestResponseMessageHandler()
        {
            _logger = new TextFileLogger();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!IsLoggingEnabled())
            {
                return await base.SendAsync(request, cancellationToken);
            }

            var message = new StringBuilder();

            try
            {
                if (request.Content != null)
                {
                    var requestBody = await request.Content.ReadAsStringAsync();

                    message.AppendFormat("Request Uri: {0} {1}, ", request.Method, request.RequestUri);
                    if (!string.IsNullOrEmpty(requestBody))
                    {
                        message.AppendLine();
                        message.AppendFormat("Request: '{0}'", requestBody);
                    }
                }

                var response = await base.SendAsync(request, cancellationToken);

                if (response != null && response.Content != null)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var serializedContent = (responseContent != null)
                        ? JsonConvert.SerializeObject(responseContent)
                        : string.Empty;

                    message.AppendLine();
                    message.AppendFormat("Response: {0}", serializedContent);
                }

                return response;
            }
            finally
            {
                _logger.Info(message.ToString());
            }

        }

        private bool IsLoggingEnabled()
        {
            var result = ConfigurationManager.AppSettings["EnableRequestResponseLog"];
            return string.Equals(result, "true", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}