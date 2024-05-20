
namespace Infraestructure.Tests.Services
{
    public class HttpMessageHandlerMockForTests : HttpMessageHandler
    {
        private readonly HttpResponseMessage _responseMessage;

        public HttpMessageHandlerMockForTests(HttpResponseMessage responseMessage)
        {
            _responseMessage = responseMessage;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_responseMessage);
        }
    }
}