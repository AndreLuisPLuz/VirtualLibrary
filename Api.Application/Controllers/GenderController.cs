using Api.Domain.DataTransfer.Payload.Gender;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Representations;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Application.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/v1/gender")]
    public class GenderController : ControllerBase
    {
        [HttpPost]
        async public Task<ActionResult> createGender(
                [FromServices] IGenderService genderService,
                [FromBody] GenderPayload payload)
        {
            IDataTransfer<Gender>? createdGender = await genderService.CreateAsync(payload);

            if (createdGender is null)
                return BadRequest();

            return Ok(JsonConvert.SerializeObject(createdGender));
        }

        [HttpGet]
        async public Task<ActionResult> getAllGenders(
            [FromServices] IGenderService genderService)
        {
            ICollection<IDataTransfer<Gender>> allGenders = await genderService.GetAsync();
            return Ok(JsonConvert.SerializeObject(allGenders));
        }

        [HttpPut]
        [Route("{id}")]
        async public Task<ActionResult> updateById(
            [FromServices] IGenderService genderService,
            [FromBody] GenderPayload payload,
            Guid id)
        {
            IDataTransfer<Gender>? updatedGender = await genderService.UpdateAsync(id, payload);

            if (updatedGender is null)
                return NotFound();

            return Ok(JsonConvert.SerializeObject(updatedGender));
        }
    }
}
