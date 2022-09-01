using Application.Languages.Queries;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Languages.Queries;
public record LanguageByIdQuery([Required] Guid Id): IRequest<LanguageDto>;
