namespace UstaPlatform.Infrastructure.Helpers;

public static class Guard
{
    public static void AgainstNull(object value, string parameterName)
    {
        if (value == null)
            throw new ArgumentNullException(parameterName);
    }

    public static void AgainstNullOrEmpty(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{parameterName} boş olamaz", parameterName);
    }
}