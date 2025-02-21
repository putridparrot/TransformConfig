using Microsoft.Web.XmlTransform;
using TransformConfig.Common;

namespace TransformConfig.Transformer;

public class XmlTransformer : ITransformer
{
    public bool Transform(string? sourcePath, string? transformPath, string? destinationPath)
    {
        Validate.ThrowIfNullOrWhitespace(sourcePath);
        Validate.ThrowIfNullOrWhitespace(transformPath);
        Validate.ThrowIfNullOrWhitespace(destinationPath);
        Validate.ThrowIfFileDoesNotExist(sourcePath);
        Validate.ThrowIfFileDoesNotExist(transformPath);

        using var document = new XmlTransformableDocument();
        document.PreserveWhitespace = true;
        document.Load(sourcePath);

        using var transformation = new XmlTransformation(transformPath);
        var success = transformation.Apply(document);

        if (success)
        {
            document.Save(destinationPath);
        }

        return success;
    }
}