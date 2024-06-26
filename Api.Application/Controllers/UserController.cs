using Api.Domain.DataTransfer.Answer;
using Api.Domain.DataTransfer.Payload;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Api.Application.Controllers
{
    [ApiController]
    [Route("/api/v1/user")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> createUser(
                [FromServices] IUserService service,
                [FromBody] UserPayload payload)
        {
            User user = payload.BuildEntity();
            IDataTransfer<User>? createdUser = await service.CreateAsync(user);

            if (createdUser == null)
                return BadRequest();

            return Created();
        }

        [HttpGet]
        public async Task<ActionResult> fetchAllUsers(
                [FromServices] IUserService service)
        {
            var allUsers = await service.FetchAllAsync();
            return Ok(allUsers);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> fetchById(
                [FromServices] IUserService service,
                Guid id)
        {
            IDataTransfer<User>? fetchedUser = await service.FetchAsync(id);

            if (fetchedUser == null)
                return NotFound();

            return Ok(fetchedUser);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult> updateById(
                [FromServices] IUserService service,
                [FromBody] UserPayload payload,
                Guid id)
        {
            IDataTransfer<User>? updatedUser = await service.UpdateAsync(id, payload);

            if (updatedUser == null)
                return BadRequest();

            return Ok(updatedUser);
        }
    }
}
