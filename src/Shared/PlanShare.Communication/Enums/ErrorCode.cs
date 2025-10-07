namespace PlanShare.Communication.Enums;
public enum UserConnectionErrorCode
{
    Unknown = 0,
    InvalidCode = 1,
    ConnectionAlreadyExists = 2,
    ConnectingToSelf = 3,
    UserNotFound = 4,
    NotAuthorized = 5
}
