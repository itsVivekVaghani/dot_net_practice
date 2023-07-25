namespace testing_face.Data;

public interface IAuthRepository
{
    Task<ServiceResponse<int>> Register(User user, string password);
    
    Task<ServiceResponse<string>> Register(string userName, string password);

    Task<bool> UserExists(string userName);
}