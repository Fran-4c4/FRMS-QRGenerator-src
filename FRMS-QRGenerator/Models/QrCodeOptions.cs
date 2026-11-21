using System.Drawing;

namespace FRMS.QRGenerator.Models;

/// <summary>
/// Defines the rendering contract for one QR image.
/// </summary>
/// <remarks>
/// <para>
/// <see cref="PixelsPerModule"/> controls PNG resolution in pixels per QR
/// module and must be between <see cref="MinimumPixelsPerModule"/> and
/// <see cref="MaximumPixelsPerModule"/>, inclusive. Zero and negative values
/// are invalid rather than aliases for an automatic size.
/// </para>
/// <para>
/// <see cref="ErrorCorrection"/> selects the QR recovery domain from low to
/// high. Higher recovery reduces payload capacity. <see cref="Foreground"/>
/// and <see cref="Background"/> are passed unchanged to the PNG renderer and
/// must not be identical because that would produce an unreadable image.
/// </para>
/// <para>
/// <see cref="Default"/> is the authoritative application default consumed by
/// the UI and tests. Changes affect PNG dimensions, capacity, appearance and
/// scanner compatibility and require regression tests for both default and
/// explicit-option paths.
/// </para>
/// </remarks>
public sealed record QrCodeOptions
{
    public const int MinimumPixelsPerModule = 2;
    public const int MaximumPixelsPerModule = 30;

    public static QrCodeOptions Default { get; } = new();

    public int PixelsPerModule { get; init; } = 10;

    public QrErrorCorrectionLevel ErrorCorrection { get; init; } =
        QrErrorCorrectionLevel.High;

    public Color Foreground { get; init; } = Color.Black;

    public Color Background { get; init; } = Color.White;

    public void Validate()
    {
        if (PixelsPerModule is < MinimumPixelsPerModule or > MaximumPixelsPerModule)
        {
            throw new ArgumentOutOfRangeException(
                nameof(PixelsPerModule),
                PixelsPerModule,
                $"El tamaño debe estar entre {MinimumPixelsPerModule} y {MaximumPixelsPerModule} píxeles por módulo.");
        }

        if (!Enum.IsDefined(ErrorCorrection))
        {
            throw new ArgumentOutOfRangeException(
                nameof(ErrorCorrection),
                ErrorCorrection,
                "El nivel de corrección no es válido.");
        }

        if (Foreground.ToArgb() == Background.ToArgb())
        {
            throw new ArgumentException(
                "Los colores del código y del fondo deben ser diferentes.",
                nameof(Foreground));
        }
    }
}

public enum QrErrorCorrectionLevel
{
    Low,
    Medium,
    Quartile,
    High,
}
