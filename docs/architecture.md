# Architecture

FRMS QR Generator is a single-process Windows Forms application targeting
`.NET 10` for Windows.

## Components

- `Form1` owns desktop interaction, validation, preview, Save As and clipboard
  output.
- `QrCodeOptions` owns the rendering contract and authoritative defaults for
  module size, error correction and colors.
- `QrCodeImageGenerator` converts a non-empty string and validated options into
  PNG bytes with QRCoder.
- `FRMS-QRGenerator.Tests` exercises the encoding boundary without opening a
  desktop window.

The QR encoder has no file-system or user-interface dependency. The form keeps
the returned bytes in memory until the user chooses a destination and creates
a separate bitmap for the preview, so streams and replaced images can be
disposed safely.

## Data flow

1. The user enters text and selects **Generar QR**.
2. The form rejects blank input and builds a validated `QrCodeOptions` value.
3. The encoder produces an in-memory PNG or reports that the content exceeds
   QR capacity for the selected correction level.
4. The form previews the image.
5. The user can save the PNG to an explicit path or copy it to the clipboard.

There is no network, database, telemetry or background service.
