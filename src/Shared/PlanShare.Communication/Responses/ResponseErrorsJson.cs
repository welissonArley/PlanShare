using System.Text.Json.Serialization;

namespace PlanShare.Communication.Responses;
public class ResponseErrorJson
{
    public IList<string> Errors { get; set; } = [];

    [JsonConstructor]
    public ResponseErrorJson(IList<string> errors) => Errors = errors;
    public ResponseErrorJson(string error) => Errors.Add(error);
}