namespace PlanShare.App.Data.Storage.Preferences.User;

public interface IUserStorage
{
    void Save(Models.ValueObjects.User user);
    Models.ValueObjects.User Get();
    void Clear();
}
