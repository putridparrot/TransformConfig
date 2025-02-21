using Microsoft.VisualStudio.Jdt;
using System.Diagnostics;
using TransformConfig.Common;

namespace TransformConfig.Transformer;

public class JsonTransformer : ITransformer
{
    public bool Transform(string? sourcePath, string? transformPath, string? destinationPath)
    {
        Validate.ThrowIfNullOrWhitespace(sourcePath);
        Validate.ThrowIfNullOrWhitespace(transformPath);
        Validate.ThrowIfNullOrWhitespace(destinationPath);
        Validate.ThrowIfFileDoesNotExist(sourcePath);
        Validate.ThrowIfFileDoesNotExist(transformPath);

        var transformation = new JsonTransformation(transformPath);

        try
        {
            using var result = transformation.Apply(sourcePath);
            string contents;
            using (var reader = new StreamReader(result, true))
            {
                contents = reader.ReadToEnd();
            }
            File.WriteAllText(destinationPath, contents, StreamEncoding.GetEncoding(sourcePath));

            return true;
        }
        catch
        {
            return false;
        }
    }
}