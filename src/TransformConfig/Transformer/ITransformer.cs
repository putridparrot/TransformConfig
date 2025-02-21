namespace TransformConfig.Transformer;

public interface ITransformer
{
    bool Transform(string? sourcePath, string? transformPath, string? destinationPath);
}