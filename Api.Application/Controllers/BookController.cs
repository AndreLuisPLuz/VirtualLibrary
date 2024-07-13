using Api.Domain.DataTransfer.Payload.BookPayloads;
using Api.Domain.Interfaces.Services;
using Api.Domain.Requests.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Application.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/v1/book")]
    public class BookController : ControllerBase
    {
        [HttpPost]
        async public Task<ActionResult> createBook(
                [FromServices] IBookService service,
                [FromBody] BookCreatePayload payload)
        {
            var result = await service.createAsync(payload);

            if (result is null)
                return BadRequest(result);

            return Ok(JsonConvert.SerializeObject(result));
        }

        [HttpGet]
        async public Task<ActionResult> getPaginatedBooks(
                [FromServices] IBookService service,
                [FromQuery] PaginationParams pagination,
                [FromQuery] string? title,
                [FromQuery] string? authorName,
                [FromQuery] string? ISBN)
        {
            var result = await service.fetchPaginatedAsync(
                pagination,
                title,
                authorName,
                ISBN);

            return Ok(JsonConvert.SerializeObject(result));
        }

        [HttpGet]
        [Route("{id}")]
        async public Task<ActionResult> getSingleBook(
            [FromServices] IBookService service,
            [FromRoute] Guid id)
        {
            var result = await service.getBookByIdAsync(id);

            if (result is null)
                return NotFound();

            return Ok(JsonConvert.SerializeObject(result));
        }

        [HttpPost]
        [Route("{id}/author/{authorId}")]
        async public Task<ActionResult> addAuthorToBook(
                [FromServices] IBookService service,
                [FromRoute] Guid id,
                [FromRoute] Guid authorId)
        {
            var result = await service.addAuthorAsync(id, authorId);

            if (!result)
                return BadRequest();

            return NoContent();
        }

        [HttpPost]
        [Route("{id}/gender/{genderId}")]
        async public Task<ActionResult> addGenderToBook(
                [FromServices] IBookService service,
                [FromRoute] Guid id,
                [FromRoute] Guid genderId)
        {
            var result = await service.addGenderAsync(id, genderId);

            if (!result)
                return BadRequest();

            return NoContent();
        }
    }
}
