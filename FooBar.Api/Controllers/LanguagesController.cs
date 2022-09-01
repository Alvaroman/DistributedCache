using Application.Languages.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class LanguagesController
{
    readonly IMediator _mediator = default!;

    public LanguagesController(IMediator mediator) => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet]
    public async Task<IEnumerable<LanguageDto>> GetAsync() => await _mediator.Send(new LanguagesQuery());
    [HttpGet("{Id}")]
    public async Task<LanguageDto> GetByIdAsync(Guid Id) => await _mediator.Send(new LanguageByIdQuery(Id));

}
