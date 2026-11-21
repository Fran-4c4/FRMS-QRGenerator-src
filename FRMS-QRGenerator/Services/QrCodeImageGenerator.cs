using FRMS.QRGenerator.Models;
using QRCoder;

namespace FRMS.QRGenerator.Services;

public static class QrCodeImageGenerator
{
    public static byte[] CreatePng(string content, QrCodeOptions? options = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(content);

        QrCodeOptions effectiveOptions = options ?? QrCodeOptions.Default;
        effectiveOptions.Validate();

        using var generator = new QRCodeGenerator();
        using QRCodeData data = generator.CreateQrCode(
            content,
            MapErrorCorrection(effectiveOptions.ErrorCorrection),
            forceUtf8: true,
            utf8BOM: false);
        using var qrCode = new PngByteQRCode(data);

        return qrCode.GetGraphic(
            effectiveOptions.PixelsPerModule,
            effectiveOptions.Foreground,
            effectiveOptions.Background,
            drawQuietZones: true);
    }

    private static QRCodeGenerator.ECCLevel MapErrorCorrection(
        QrErrorCorrectionLevel errorCorrection) => errorCorrection switch
        {
            QrErrorCorrectionLevel.Low => QRCodeGenerator.ECCLevel.L,
            QrErrorCorrectionLevel.Medium => QRCodeGenerator.ECCLevel.M,
            QrErrorCorrectionLevel.Quartile => QRCodeGenerator.ECCLevel.Q,
            QrErrorCorrectionLevel.High => QRCodeGenerator.ECCLevel.H,
            _ => throw new ArgumentOutOfRangeException(
                nameof(errorCorrection),
                errorCorrection,
                "El nivel de corrección no es válido."),
        };
}
