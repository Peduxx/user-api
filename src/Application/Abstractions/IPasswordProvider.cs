namespace Application.Abstractions
{
    public interface IPasswordProvider
    {
        byte[] GenerateSalt();
        string Generate(string password, byte[] salt);
        bool Compare(string password, string storagedPassword, byte[] salt);
    }
}
