namespace KomaeiSample.Client.Configs;
public class ConstantsClient
{
    public static readonly DialogOptions DialogOptionsExtraSmall = new()
    {
        CloseButton = true,
        MaxWidth = MaxWidth.ExtraSmall,
        FullWidth = true,
        CloseOnEscapeKey = true
    };

    public static readonly DialogOptions DialogOptionsSmall = new()
    {
        CloseButton = true,
        MaxWidth = MaxWidth.Small,
        FullWidth = true,
        CloseOnEscapeKey = true
    };

    public static readonly DialogOptions DialogOptionsMedium = new()
    {
        CloseButton = true,
        MaxWidth = MaxWidth.Medium,
        FullWidth = true,
        CloseOnEscapeKey = true
    };

    public static readonly DialogOptions DialogOptionsLarge = new()
    {
        CloseButton = true,
        MaxWidth = MaxWidth.Large,
        FullWidth = true,
        CloseOnEscapeKey = true
    };

    public static readonly DialogOptions DialogOptionsExtraLarge = new()
    {
        CloseButton = true,
        MaxWidth = MaxWidth.ExtraLarge,
        FullWidth = true,
        CloseOnEscapeKey = true
    };
}