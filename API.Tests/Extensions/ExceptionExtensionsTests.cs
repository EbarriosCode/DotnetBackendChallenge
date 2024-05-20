using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Extensions.Exceptions;

namespace API.Tests.Extensions
{
    public class ExceptionExtensionsTests
    {
        private readonly Mock<HttpContext> _httpContextMock;
        private readonly Mock<HttpRequest> _httpRequestMock;
        private readonly Mock<IHeaderDictionary> _headersMock;

        public ExceptionExtensionsTests()
        {
            _httpContextMock = new Mock<HttpContext>();
            _httpRequestMock = new Mock<HttpRequest>();
            _headersMock = new Mock<IHeaderDictionary>();

            _headersMock.Setup(x => x["Referer"]).Returns("http://wwww.dotnetconf-gt.com.gt");
            _httpRequestMock.Setup(x => x.Headers).Returns(_headersMock.Object);
            _httpContextMock.Setup(x => x.Request).Returns(_httpRequestMock.Object);
        }

        [Fact]
        public void Test_ExtensionMethod_Sending_ArgumentException_And_Return_BadRequestObjectResult()
        {
            var argumentException = new ArgumentException("Value field is invalid.");

            var result = argumentException.ConvertToActionResult(this._httpContextMock.Object);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public void Test_ExtensionMethod_Sending_ArgumentNullException_And_Return_BadRequestObjectResult()
        {
            var argumentNullException = new ArgumentNullException("Value cannot be null.");
        
            var result = argumentNullException.ConvertToActionResult(this._httpContextMock.Object);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public void Test_ExtensionMethod_Sending_UnauthorizedAccessException_And_Return_UnauthorizedObjectResult()
        {
            var unauthorizedAccessException = new UnauthorizedAccessException("Unauthorized Access.");
           
            var result = unauthorizedAccessException.ConvertToActionResult(this._httpContextMock.Object);

            var unauthorizedObjectResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal(StatusCodes.Status401Unauthorized, unauthorizedObjectResult.StatusCode);
        }

        [Fact]
        public void Test_ExtensionMethod_Sending_InvalidOperationException_And_Return_ObjectResult_With_StatusCode500()
        {
            var invalidOperationException = new InvalidOperationException("Invalid Operation.");
          
            var result = invalidOperationException.ConvertToActionResult(this._httpContextMock.Object);

            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        [Fact]
        public void Test_ExtensionMethod_Sending_HttpRequestException_And_Return_ObjectResult_With_StatusCode500()
        {
            var httpRequestException = new HttpRequestException("Invalid Request.");
          
            var result = httpRequestException.ConvertToActionResult(this._httpContextMock.Object);

            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        [Fact]
        public void Test_ExtensionMethod_Sending_HttpRequestException_And_Return_ObjectResult_With_StatusCode408()
        {
            var httpRequestException = new HttpRequestException("Operation timed out");
          
            var result = httpRequestException.ConvertToActionResult(this._httpContextMock.Object);

            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status408RequestTimeout, objectResult.StatusCode);
        }

        [Fact]
        public void Test_ExtensionMethod_Sending_BaseException_And_Return_Exception()
        {
            var exception = new Exception("xxxxxxx");
           
            var result = exception.ConvertToActionResult(this._httpContextMock.Object);

            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
    }
}
