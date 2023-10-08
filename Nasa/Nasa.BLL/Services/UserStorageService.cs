using Nasa.BLL.ServicesContracts;

namespace Nasa.BLL.Services;

public class UserStorageService: IUserIdGetter, IUserIdSetter
{
    private int _userId;
    public int GetCurrentUserId()
    {
        if (_userId == 0)
        {
            throw new Exception("Invalid access");
        }

        return _userId;
    }

    public void SetCurrentUserId(int id)
    {
        _userId = id;
    }
}