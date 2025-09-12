namespace PlanShare.Communication.Requests;
public class RequestNewTokenJson
{
    public string RefreshToken { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
}
