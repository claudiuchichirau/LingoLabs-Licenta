using LingoLabs.Application.Features.LanguagesFeatures.EntityTags.Commands.CreateEntityTag;
using LingoLabs.Application.Features.LanguagesFeatures.EntityTags.Commands.DeleteEntityTag;
using LingoLabs.Application.Features.LanguagesFeatures.EntityTags.Queries.GetAll;
using LingoLabs.Application.Features.LanguagesFeatures.EntityTags.Queries.GetById;
using LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.DeleteLanguage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LingoLabs.API.Controllers.LanguageControllers
{
    public class EntityTagController : ApiControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateEntityTagCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result.ValidationsErrors);
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new DeleteEntityTagCommand { EntityTagId = id });
            if (!result.Success)
            {
                return NotFound(result.ValidationsErrors);
            }
            return Ok(result);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllEntityTagsQuery());
            return Ok(result);
        }

        [Authorize(Roles = "Admin, Student")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetByIdEntityTagQuery(id));

            if (result.EntityTagId == Guid.Empty)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
