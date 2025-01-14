namespace PlanShare.Domain.Dtos;
public record TokensDto
{
    public string Access { get; set; } = string.Empty;
    public string Refresh { get; set; } = string.Empty;
}