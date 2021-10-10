namespace NetCoreBoilerplate.Application.Common.Interfaces.Services
{
    public interface IHashService
    {
        string HashPassword(string input);

        bool Verify(string text, string hashedInput);
    }
}
