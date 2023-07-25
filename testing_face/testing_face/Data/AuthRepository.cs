namespace testing_face.Data;

public class AuthRepository : IAuthRepository
{
    public Task<ServiceResponse<int>> Register(User user, string password)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<string>> Register(string userName, string password)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UserExists(string userName)
    {
        throw new NotImplementedException();
    }
}