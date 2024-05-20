using Application.Commands.Products.Insert;
using Application.Commands.Products.Update;
using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Queries.Products.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using WebAPI.Extensions.Exceptions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
        {
            this._mediator = mediator;
            this._logger = logger;
        }

        // GET: api/Products/5
        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductResponseDTO>> Get(int productId)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                if (productId <= 0)
                    return BadRequest("Invalid ID");

                var query = new GetProductByIdQuery(productId);
                var sendQueryToBus = await this._mediator.Send(query);

                stopwatch.Stop();
                this._logger.LogInformation($"GET api/Products/{productId} took {stopwatch.ElapsedMilliseconds} ms");

                if (sendQueryToBus == null)
                    return NotFound();

                return Ok(sendQueryToBus);
            }
            catch (Exception exception)
            {
                stopwatch.Stop();
                this._logger.LogInformation($"GET api/Products/{productId} took {stopwatch.ElapsedMilliseconds} ms");

                return exception.ConvertToActionResult(HttpContext);
            }
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<string>> CreateProduct([FromBody] InsertProductRequestDTO request)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                if (request == null)
                    return BadRequest("Error when trying to create the Product");

                var command = new InsertProductCommand(request);
                var sendCommandToBus = await this._mediator.Send(command);

                stopwatch.Stop();
                this._logger.LogInformation($"POST api/Products took {stopwatch.ElapsedMilliseconds} ms");

                return StatusCode((int)HttpStatusCode.Created, sendCommandToBus);
            }
            catch (Exception exception)
            {
                stopwatch.Stop();
                this._logger.LogInformation($"POST api/Products took {stopwatch.ElapsedMilliseconds} ms");

                return exception.ConvertToActionResult(HttpContext);
            }
        }

        // PUT: api/Products        
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductRequestDTO request)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                if (request == null)
                    return BadRequest();

                var command = new UpdateProductCommand(request);
                await this._mediator.Send(command);

                stopwatch.Stop();
                this._logger.LogInformation($"PUT api/Products took {stopwatch.ElapsedMilliseconds} ms");

                return NoContent();
            }
            catch (Exception exception)
            {
                stopwatch.Stop();
                this._logger.LogInformation($"PUT api/Products took {stopwatch.ElapsedMilliseconds} ms");

                return exception.ConvertToActionResult(HttpContext);
            }
        }
    }
}
