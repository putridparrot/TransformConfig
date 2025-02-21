using PowerArgs;

namespace TransformConfig;

internal class Arguments
{
    [ArgRequired(PromptIfMissing = true), ArgShortcut("-s"), ArgDescription("The source file")]
    public string? SourceFile { get; set; }
    [ArgRequired(PromptIfMissing = true), ArgShortcut("-t"), ArgDescription("The transformation file")]
    public string? TransformFile { get; set; }
    [ArgRequired(PromptIfMissing = true), ArgShortcut("-d"), ArgDescription("The destination file")]
    public string? DestinationFile { get; set; }
    [ArgShortcut("-json"), ArgDescription("Use the Json Transformer (default if not specified)")]
    public bool? JsonTransformer { get; set; }
    [ArgShortcut("-xml"), ArgDescription("The destination file")]
    public bool? XmlTransformer { get; set; }
}