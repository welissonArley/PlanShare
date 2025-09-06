namespace PlanShare.Domain.Dtos;
public record TokensDto
{
    public string Access { get; init; } = string.Empty;
    public string Refresh { get; init; } = string.Empty;
}