using System.Runtime.CompilerServices;

namespace TransformConfig.Common;

public static class Validate
{
    public static void ThrowIfNullOrWhitespace(string? value, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(string.Format(Resources.Resources.ErrorMessage_ThrowIfNullOrWhitespace, parameterName), parameterName);
        }
    }

    public static void ThrowIfFileDoesNotExist(string? filePath, [CallerArgumentExpression(nameof(filePath))] string? parameterName = null)
    {
        if (!File.Exists(filePath))
        {
            throw new ArgumentException(string.Format(Resources.Resources.ErrorMessage_FileNotFound, filePath), parameterName);
        }
    }
}
