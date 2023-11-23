using SkiaSharp;
using SkiaSharp.QrCode;

namespace Demo;

public static class SkiaSharpDemo
{
    public static void GenerateQrcode(string content)
    {
        using var generator = new QRCodeGenerator();
        using var qRCodeData = generator.CreateQrCode(content, ECCLevel.H);
        var bitmap = new SKBitmap(256, 256);
        using var canvas = new SKCanvas(bitmap);
        canvas.Clear(SKColors.White);
        // canvas.DrawText(qRCodeData, SKRect.Create(256, 256));
    }

    public static void IOOpen()
    {
        File.ReadAllLinesAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""), CancellationToken.None).Wait();
    }
}