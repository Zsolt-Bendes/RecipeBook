namespace Home.Recipes.Api.Infrastructure.Exceptions;

[Serializable]
public sealed class PostgresConnectionStringInsNullOrEmptyException : Exception
{
    public PostgresConnectionStringInsNullOrEmptyException()
    {
    }

    public PostgresConnectionStringInsNullOrEmptyException(string message)
        : base(message)
    {
    }

    public PostgresConnectionStringInsNullOrEmptyException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
