
namespace PlanShare.App.Data.Storage.Preferences.User;

public class UserStorage : IUserStorage
{
    private const string ID_KEY = "id";
    private const string NAME_KEY = "name";

    public void Clear() => Microsoft.Maui.Storage.Preferences.Default.Clear();

    public Models.ValueObjects.User Get()
    {
        var id = Microsoft.Maui.Storage.Preferences.Default.Get(ID_KEY, string.Empty);
        var name = Microsoft.Maui.Storage.Preferences.Default.Get(NAME_KEY, string.Empty);

        return new Models.ValueObjects.User(Guid.Parse(id), name);
    }

    public void Save(Models.ValueObjects.User user)
    {
        Microsoft.Maui.Storage.Preferences.Default.Set(ID_KEY, user.Id.ToString());
        Microsoft.Maui.Storage.Preferences.Default.Set(NAME_KEY, user.Name);
    }

    public bool IsLoggedIn() => Microsoft.Maui.Storage.Preferences.Default.ContainsKey(ID_KEY);
}
