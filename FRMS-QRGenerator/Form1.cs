using System.Text;
using FRMS.QRGenerator.Models;
using FRMS.QRGenerator.Services;
using QRCoder.Exceptions;

namespace FRMS.QRGenerator;

public partial class Form1 : Form
{
    private byte[]? generatedPng;
    private Color foregroundColor;
    private Color backgroundColor;

    public Form1()
    {
        InitializeComponent();

        QrCodeOptions defaults = QrCodeOptions.Default;
        numModuleSize.Minimum = QrCodeOptions.MinimumPixelsPerModule;
        numModuleSize.Maximum = QrCodeOptions.MaximumPixelsPerModule;
        numModuleSize.Value = defaults.PixelsPerModule;
        cmbErrorCorrection.DataSource = Enum.GetValues<QrErrorCorrectionLevel>();
        cmbErrorCorrection.SelectedItem = defaults.ErrorCorrection;
        foregroundColor = defaults.Foreground;
        backgroundColor = defaults.Background;
        UpdateColorButton(btnForegroundColor, foregroundColor);
        UpdateColorButton(btnBackgroundColor, backgroundColor);
        UpdatePayloadCounter();
    }

    private void btnGenerate_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtCode.Text))
        {
            ShowInformation("Introduce el texto que quieres codificar.", "Contenido requerido");
            txtCode.Focus();
            return;
        }

        try
        {
            byte[] png = QrCodeImageGenerator.CreatePng(txtCode.Text, BuildOptions());
            DisplayPng(png);
            generatedPng = png;
            btnSaveAs.Enabled = true;
            btnCopy.Enabled = true;
            lblStatus.Text = "QR generado. Puedes guardarlo o copiarlo al portapapeles.";
        }
        catch (DataTooLongException)
        {
            ShowInformation(
                "El contenido es demasiado largo para el nivel de corrección seleccionado. " +
                "Reduce el texto o elige un nivel de corrección menor.",
                "Contenido demasiado largo");
            lblStatus.Text = "El contenido supera la capacidad del código QR.";
        }
        catch (ArgumentException exception)
        {
            ShowInformation(exception.Message, "Opciones no válidas");
            lblStatus.Text = "Revisa las opciones del código QR.";
        }
        catch (Exception exception)
        {
            ShowError(exception.Message, "Error al generar el código QR");
            lblStatus.Text = "No se pudo generar el código QR.";
        }
    }

    private void btnSaveAs_Click(object? sender, EventArgs e)
    {
        if (generatedPng is null)
        {
            return;
        }

        using var dialog = new SaveFileDialog
        {
            AddExtension = true,
            DefaultExt = "png",
            FileName = "qrcode.png",
            Filter = "Imagen PNG (*.png)|*.png",
            OverwritePrompt = true,
            Title = "Guardar código QR",
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            File.WriteAllBytes(dialog.FileName, generatedPng);
            lblStatus.Text = $"Guardado en {dialog.FileName}";
        }
        catch (Exception exception)
        {
            ShowError(exception.Message, "No se pudo guardar el archivo");
        }
    }

    private void btnCopy_Click(object? sender, EventArgs e)
    {
        if (imgQR.Image is null)
        {
            return;
        }

        try
        {
            Clipboard.SetImage(imgQR.Image);
            lblStatus.Text = "Código QR copiado al portapapeles.";
        }
        catch (Exception exception)
        {
            ShowError(exception.Message, "No se pudo copiar la imagen");
        }
    }

    private void btnForegroundColor_Click(object? sender, EventArgs e)
    {
        if (TryChooseColor(foregroundColor, out Color selectedColor))
        {
            foregroundColor = selectedColor;
            UpdateColorButton(btnForegroundColor, selectedColor);
            InvalidateGeneratedResult();
        }
    }

    private void btnBackgroundColor_Click(object? sender, EventArgs e)
    {
        if (TryChooseColor(backgroundColor, out Color selectedColor))
        {
            backgroundColor = selectedColor;
            UpdateColorButton(btnBackgroundColor, selectedColor);
            InvalidateGeneratedResult();
        }
    }

    private void txtCode_TextChanged(object? sender, EventArgs e)
    {
        UpdatePayloadCounter();
        InvalidateGeneratedResult();
    }

    private void options_ValueChanged(object? sender, EventArgs e)
    {
        InvalidateGeneratedResult();
    }

    private void cmbErrorCorrection_Format(object? sender, ListControlConvertEventArgs e)
    {
        if (e.ListItem is not QrErrorCorrectionLevel errorCorrection)
        {
            return;
        }

        e.Value = errorCorrection switch
        {
            QrErrorCorrectionLevel.Low => "Baja (L)",
            QrErrorCorrectionLevel.Medium => "Media (M)",
            QrErrorCorrectionLevel.Quartile => "Cuartil (Q)",
            QrErrorCorrectionLevel.High => "Alta (H)",
            _ => errorCorrection.ToString(),
        };
    }

    private QrCodeOptions BuildOptions() => new()
    {
        PixelsPerModule = Decimal.ToInt32(numModuleSize.Value),
        ErrorCorrection = (QrErrorCorrectionLevel)cmbErrorCorrection.SelectedItem!,
        Foreground = foregroundColor,
        Background = backgroundColor,
    };

    private void DisplayPng(byte[] png)
    {
        using var stream = new MemoryStream(png);
        using Image decodedImage = Image.FromStream(stream);
        Image? previousImage = imgQR.Image;
        imgQR.Image = new Bitmap(decodedImage);
        previousImage?.Dispose();
    }

    private void InvalidateGeneratedResult()
    {
        generatedPng = null;
        btnSaveAs.Enabled = false;
        btnCopy.Enabled = false;
        imgQR.Image?.Dispose();
        imgQR.Image = null;
        lblStatus.Text = "Pulsa Generar QR para actualizar la vista previa.";
    }

    private void UpdatePayloadCounter()
    {
        int utf8Bytes = Encoding.UTF8.GetByteCount(txtCode.Text);
        lblPayloadSize.Text = $"{txtCode.Text.Length:N0} caracteres · {utf8Bytes:N0} bytes UTF-8";
    }

    private bool TryChooseColor(Color initialColor, out Color selectedColor)
    {
        using var dialog = new ColorDialog
        {
            AllowFullOpen = true,
            AnyColor = true,
            Color = initialColor,
            FullOpen = true,
        };

        bool accepted = dialog.ShowDialog(this) == DialogResult.OK;
        selectedColor = accepted ? dialog.Color : initialColor;
        return accepted;
    }

    private static void UpdateColorButton(Button button, Color color)
    {
        button.BackColor = color;
        button.ForeColor = color.GetBrightness() < 0.5f ? Color.White : Color.Black;
        button.Text = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
    }

    private void ShowInformation(string message, string title) =>
        MessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);

    private void ShowError(string message, string title) =>
        MessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
}
