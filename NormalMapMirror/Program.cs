using System.Numerics;
using System.Runtime.InteropServices;
using DirectXTexNet;
using NormalMapMirror;

var config = new Config();

// Ensure a file was passed in
if (args.Length == 0)
{
    Console.Error.WriteLine("Please specify the image file to mirror");
    return;
}

var sourceFile = args[0];

// Convert texture to DDS
var extension = Path.GetExtension(sourceFile);
var texture = extension switch
{
    ".dds" => TexHelper.Instance.LoadFromDDSFile(sourceFile, DDS_FLAGS.NONE),
    ".tga" => TexHelper.Instance.LoadFromTGAFile(sourceFile),
    _ => TexHelper.Instance.LoadFromWICFile(sourceFile, WIC_FLAGS.NONE)
};

var metadata = texture.GetMetadata();
if (metadata.Format != DXGI_FORMAT.R8G8B8A8_UNORM)
{
    texture = TexHelper.Instance.IsCompressed(metadata.Format)
        ? texture.Decompress(DXGI_FORMAT.R8G8B8A8_UNORM)
        : texture.Convert(DXGI_FORMAT.R8G8B8A8_UNORM, TEX_FILTER_FLAGS.CUBIC | TEX_FILTER_FLAGS.SRGB, 0.5f);
}

using var ddsStream = texture.SaveToDDSMemory(DDS_FLAGS.NONE);
using var memoryStream = new MemoryStream();
ddsStream.CopyTo(memoryStream);

// Load up the DDS file and split into header and pixels
var dds = memoryStream.ToArray();
var header = dds[..128];
var originalPixels = dds[128..];

for (var axisIndex = 0; axisIndex < 3; axisIndex++)
{
    switch (axisIndex)
    {
        case 0 when !config.XMirror:
        case 1 when !config.YMirror:
        case 2 when !config.ZMirror:
            continue;
    }

    // Make a copy of the pixels as to not alter the originals if the other axes need them
    var pixels = new byte[originalPixels.Length];
    Array.Copy(originalPixels, pixels, originalPixels.Length);

    // Generate a mirror matrix
    var correctionMatrix = GetMirrorMatrix(axisIndex);

    // Iterate each pixel in the image
    for (var i = 0; i < pixels.Length / 4; i++)
    {
        // Get the RGB values from the pixel
        var r = pixels[i * 4];
        var g = pixels[i * 4 + 1];
        var b = pixels[i * 4 + 2];

        // Convert RGB to vector and mirror it
        var vector = new Vector3(r / 255f, g / 255f, b / 255f);
        var result = correctionMatrix * Matrix4x4.CreateTranslation(vector) * correctionMatrix;
        vector = result.Translation;

        // Convert back to RGB
        pixels[i * 4] = (byte)(vector.X * 255f);
        pixels[i * 4 + 1] = (byte)(vector.Y * 255f);
        pixels[i * 4 + 2] = (byte)(vector.Z * 255f);
    }

    // Write the DDS file into memory
    using var outputMemoryStream = new MemoryStream();
    outputMemoryStream.Write(header);
    outputMemoryStream.Write(pixels);

    // Load the DDS into DirectXTex
    var pinnedData = GCHandle.Alloc(outputMemoryStream.ToArray(), GCHandleType.Pinned);
    var pointer = pinnedData.AddrOfPinnedObject();
    var outputDds = TexHelper.Instance.LoadFromDDSMemory(pointer, outputMemoryStream.Length, DDS_FLAGS.NONE);
    pinnedData.Free();

    // Write the image based on input format
    var outputPath = sourceFile.Insert(sourceFile.LastIndexOf('.'), $"_{GetAxisName(axisIndex)}Mirror");
    switch (extension)
    {
        case ".dds":
            outputDds.SaveToDDSFile(DDS_FLAGS.NONE, outputPath);
            break;
        case ".tga":
            outputDds.SaveToTGAFile(0, outputPath);
            break;
        default:
            outputDds.SaveToWICFile(0, WIC_FLAGS.NONE, GetCodec(extension), outputPath);
            break;
    }
}

string GetAxisName(int index)
{
    return index switch
    {
        0 => "X",
        1 => "Y",
        2 => "Z",
        _ => throw new ArgumentException("Axis index must be in range 0, 1, 2", nameof(index))
    };
}

Guid GetCodec(string wicExtension)
{
    return wicExtension switch
    {
        ".bmp" => TexHelper.Instance.GetWICCodec(WICCodecs.BMP),
        ".gif" => TexHelper.Instance.GetWICCodec(WICCodecs.GIF),
        ".png" => TexHelper.Instance.GetWICCodec(WICCodecs.PNG),
        ".wmp" => TexHelper.Instance.GetWICCodec(WICCodecs.WMP),
        ".ico" => TexHelper.Instance.GetWICCodec(WICCodecs.ICO),
        ".jpg" or ".jpeg" => TexHelper.Instance.GetWICCodec(WICCodecs.JPEG),
        ".tif" or ".tiff" => TexHelper.Instance.GetWICCodec(WICCodecs.TIFF),
        _ => throw new ArgumentOutOfRangeException(nameof(wicExtension), wicExtension, "Unsupported image type")
    };
}

Matrix4x4 GetMirrorMatrix(int mirrorAxis)
{
    return mirrorAxis switch
    {
        0 => new Matrix4x4( // X
            -1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1
        ),
        1 => new Matrix4x4( // Y
            1, 0, 0, 0,
            0, -1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1
        ),
        2 => new Matrix4x4( // Z
            1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, -1, 0,
            0, 0, 0, 1
        ),
        _ => throw new ArgumentException("Axis must be X, Y, or Z", nameof(mirrorAxis))
    };
}