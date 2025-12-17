using Mediaspot.Domain.Enums;

namespace Mediaspot.Api.DTOs;

public sealed record TitleDto(string Name, TitleType Type, string? Description, DateTime? ReleaseDate);