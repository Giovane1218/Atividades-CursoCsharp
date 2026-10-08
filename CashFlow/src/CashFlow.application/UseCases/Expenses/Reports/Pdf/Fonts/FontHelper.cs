using PdfSharp.Fonts;

namespace CashFlow.application.UseCases.Expenses.Reports.Pdf.Fonts;

public sealed class FontHelper : PdfSharp.Fonts.IFontResolver
{
    private const string MinecrafterFace = "Minecrafter";
    private const string RobotoFace = "Roboto";
    private const string StarJediFace = "StarWars";

    private static readonly Lazy<byte[]> MinecrafterFont =
        new(() => LoadFontResource("Minecrafter.Reg.ttf"));

    private static readonly Lazy<byte[]> RobotoFont =
        new(() => LoadFontResource("Roboto-VariableFont_wdth,wght.ttf"));

    private static readonly Lazy<byte[]> StarJediFont =
        new(() => LoadFontResource("STJEDISE.TTF"));

    public string DefaultFontName => MinecrafterFace;

    public FontResolverInfo? ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        var faceName = familyName.Trim().ToLowerInvariant() switch
        {
            "star wars" or "starwars" or "starjedi" or "starjedi-regular" => StarJediFace,
            "roboto" => RobotoFace,
            "minecrafter" => MinecrafterFace,
            _ => MinecrafterFace
        };

        return new FontResolverInfo(faceName);
    }

    public byte[]? GetFont(string faceName) => faceName switch
    {
        MinecrafterFace => MinecrafterFont.Value,
        RobotoFace => RobotoFont.Value,
        StarJediFace => StarJediFont.Value,
        _ => null
    };

    private static byte[] LoadFontResource(string fontFileName)
    {
        var assembly = typeof(FontHelper).Assembly;
        var resourceName = assembly.GetManifestResourceNames()
            .SingleOrDefault(name => name.EndsWith($".{fontFileName}", StringComparison.OrdinalIgnoreCase));

        if (resourceName is null)
        {
            throw new InvalidOperationException(
                $"Embedded font resource '{fontFileName}' was not found. Check the project EmbeddedResource entries.");
        }

        using var fontStream = assembly.GetManifestResourceStream(resourceName)!;
        using var memoryStream = new MemoryStream();
        fontStream.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }
}
