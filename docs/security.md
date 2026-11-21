# Security design

## Trust boundaries

The application accepts text entered by the local user and passes it to
QRCoder. Processing is local; the application does not transmit the payload or
load remote content.

The process runs with the caller's permissions and does not request elevation.
Saving requires an explicit destination, and the standard Windows dialog asks
before an existing file is replaced. Copying places the image on the shared
Windows clipboard, where other local applications may be able to read it.

## Sensitive information

A QR code is an encoding, not encryption. Anyone who can view the generated
image can recover its payload. Do not treat the PNG as a secure container for
credentials or personal information.

Tests and examples must use synthetic payloads. Payloads are not logged or
persisted by the application except when the user explicitly saves the
generated PNG. Clipboard copies follow Windows clipboard lifetime and access
rules.

## Dependency updates

QR parsing and image generation changes can affect memory use and output
correctness. Review QRCoder release notes and license changes, then run the
test and publication scripts before merging an update.
