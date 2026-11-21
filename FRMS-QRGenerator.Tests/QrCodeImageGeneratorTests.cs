using System.Drawing;
using FRMS.QRGenerator.Models;
using FRMS.QRGenerator.Services;
using QRCoder.Exceptions;
using Xunit;

namespace FRMS.QRGenerator.Tests;

public sealed class QrCodeImageGeneratorTests
{
    private static readonly byte[] PngSignature =
        [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A];

    [Fact]
    public void CreatePngReturnsPngForUnicodeContent()
    {
        byte[] result = QrCodeImageGenerator.CreatePng("FRMS - código QR ✓");

        Assert.True(result.Length > PngSignature.Length);
        Assert.Equal(PngSignature, result[..PngSignature.Length]);
    }

    [Theory]
    [InlineData(QrErrorCorrectionLevel.Low)]
    [InlineData(QrErrorCorrectionLevel.Medium)]
    [InlineData(QrErrorCorrectionLevel.Quartile)]
    [InlineData(QrErrorCorrectionLevel.High)]
    public void CreatePngSupportsEveryErrorCorrectionLevel(
        QrErrorCorrectionLevel errorCorrection)
    {
        var options = QrCodeOptions.Default with
        {
            PixelsPerModule = QrCodeOptions.MinimumPixelsPerModule,
            ErrorCorrection = errorCorrection,
            Foreground = Color.Navy,
            Background = Color.White,
        };

        byte[] result = QrCodeImageGenerator.CreatePng("explicit options", options);

        Assert.Equal(PngSignature, result[..PngSignature.Length]);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void CreatePngRejectsEmptyContent(string content)
    {
        Assert.Throws<ArgumentException>(() => QrCodeImageGenerator.CreatePng(content));
    }

    [Fact]
    public void CreatePngRejectsInvalidModuleSize()
    {
        QrCodeOptions options = QrCodeOptions.Default with
        {
            PixelsPerModule = QrCodeOptions.MaximumPixelsPerModule + 1,
        };

        Assert.Throws<ArgumentOutOfRangeException>(
            () => QrCodeImageGenerator.CreatePng("invalid size", options));
    }

    [Fact]
    public void CreatePngRejectsMatchingColors()
    {
        QrCodeOptions options = QrCodeOptions.Default with
        {
            Foreground = Color.Teal,
            Background = Color.Teal,
        };

        Assert.Throws<ArgumentException>(
            () => QrCodeImageGenerator.CreatePng("invisible", options));
    }

    [Fact]
    public void CreatePngRejectsContentBeyondQrCapacity()
    {
        string content = new('x', 10_000);

        Assert.Throws<DataTooLongException>(
            () => QrCodeImageGenerator.CreatePng(content));
    }
}
