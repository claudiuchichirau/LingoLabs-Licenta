﻿using LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.CreateLanguage;
using LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.DeleteLanguage;
using LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.UpdateLanguage;
using LingoLabs.Application.Features.LanguagesFeatures.Languages.Queries.GetAll;
using LingoLabs.Application.Features.LanguagesFeatures.Languages.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace LingoLabs.API.Controllers.LanguageControllers
{
    public class LanguagesController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateLanguageCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result.ValidationsErrors);
            }
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllLanguagesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetByIdLanguageQuery(id));

            if (result.LanguageId == Guid.Empty)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(UpdateLanguageCommand command)
        {
            var result = await Mediator.Send(command);

            if (!result.Success)
            {
                return NotFound(result.ValidationsErrors);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new DeleteLanguageCommand { LanguageId = id });

            if (!result.Success)
            {
                return NotFound(result.ValidationsErrors);
            }

            return Ok(result);
        }
    }
}
