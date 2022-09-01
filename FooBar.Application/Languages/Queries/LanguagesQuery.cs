using System.ComponentModel.DataAnnotations;
using MediatR;
namespace Application.Languages.Queries;
public record LanguagesQuery() : IRequest<IEnumerable<LanguageDto>>;
