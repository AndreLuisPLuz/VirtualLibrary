using Api.Domain.DataTransfer.Payload.Author;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Representations;
using Api.Domain.Interfaces.Services;
using Api.Domain.Requests.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Application.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/v1/author")]
    public class AuthorController : ControllerBase
    {
        [HttpPost]
        async public Task<ActionResult> createAuthor(
                [FromServices] IAuthorService service,
                [FromBody] AuthorPayload payload)
        {
            IDataTransfer<Author> result = await service.create(payload);

            if (result == null)
                return BadRequest();

            return Created();
        }

        [HttpGet]
        async public Task<ActionResult> getPaginated(
                [FromServices] IAuthorService service,
                [FromQuery] PaginationParams pagination,
                [FromQuery] string? name)
        {
            var result = await service.getPaginated(pagination, name);
            return Ok(JsonConvert.SerializeObject(result));
        }

        [HttpGet]
        [Route("/{id}")]
        async public Task<ActionResult> getOne(
                [FromServices] IAuthorService service,
                [FromRoute] Guid id)
        {
            var result = await service.fetch(id);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPut]
        [Route("/{id}")]
        async public Task<ActionResult> updateAuthor(
                [FromServices] IAuthorService service,
                [FromRoute] Guid id,
                [FromBody] AuthorPayload payload)
        {
            var result = service.update(id, payload);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }
    }
}
