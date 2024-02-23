namespace Aplicativo.Abstractions
{
    public interface IJwtProvider
    {
        string Generate(UserEntity user);
    }
}
