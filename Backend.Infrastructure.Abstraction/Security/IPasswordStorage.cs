namespace Backend.Infrastructure.Abstraction.Security
{
    public interface IPasswordStorage
    {
        string Create(string password);

        bool Match(string password, string goodHash);
    }
}