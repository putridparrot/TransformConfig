using PowerArgs;
using TransformConfig;
using TransformConfig.Common;
using TransformConfig.Resources;
using TransformConfig.Transformer;

try
{
    var parsed = Args.Parse<Arguments>(args);

    Console.WriteLine($"-s {parsed.SourceFile} -t {parsed.TransformFile} -d {parsed.DestinationFile}");

    IfNotExistsCreateFolder(parsed.DestinationFile);

    ITransformer transformer = parsed.JsonTransformer.HasValue || parsed.XmlTransformer.GetValueOrDefault(false)
        ? new XmlTransformer()
        : new JsonTransformer();

    if (!transformer.Transform(parsed.SourceFile, parsed.TransformFile, parsed.DestinationFile))
    {
        return Failed();
    }

}
catch (ArgException ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine(ArgUsage.GenerateUsageFromTemplate<Arguments>());
    return Result.Failure;
}

return Result.Success;

static int Failed()
{
    Console.WriteLine(Resources.ErrorMessage_FailedTransform);
    return Result.Failure;
}

static void IfNotExistsCreateFolder(string destinationFolder)
{
    var path = Path.GetDirectoryName(destinationFolder);
    if (!Directory.Exists(path))
    {
        Directory.CreateDirectory(path);
    }
}

