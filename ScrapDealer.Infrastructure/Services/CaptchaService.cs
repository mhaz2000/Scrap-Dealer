using ScrapDealer.Application.Services;
using ScrapDealer.Infrastructure.Caching;
using SkiaSharp;

namespace ScrapDealer.Infrastructure.Services
{
    internal sealed class CaptchaService : ICaptchaService
    {
        private readonly IMemoryCacheService _cache;
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public CaptchaService(IMemoryCacheService cache)
            => _cache = cache;
        public (Guid CaptchaId, byte[] CaptchaImage) GenerateCaptcha(int length = 6)
        {
            var random = new Random();
            var captchaCode = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            var captchaImage = GenerateCaptchaImage(captchaCode);
            var captchaId = Guid.NewGuid();

            // Store CAPTCHA in the cache
            _cache.Set(captchaId.ToString(), captchaCode, TimeSpan.FromMinutes(5));

            return (captchaId, captchaImage);
        }

        public bool ValidateCaptcha(string captchaId, string captchaCode)
        {
            var cachedCode = _cache.Get<string>(captchaId);
            return cachedCode != null && captchaCode.Equals(cachedCode, StringComparison.OrdinalIgnoreCase);

        }

        private byte[] GenerateCaptchaImage(string captchaCode)
        {
            const int width = 200;
            const int height = 50;

            using (var surface = SKSurface.Create(new SKImageInfo(width, height)))
            {
                var canvas = surface.Canvas;

                // Fill background
                canvas.Clear(SKColors.White);

                // Define text font and paint
                using (var typeface = SKTypeface.FromFamilyName("Arial"))
                using (var font = new SKFont(typeface, 32))
                using (var paint = new SKPaint())
                {
                    paint.Color = SKColors.Black;
                    paint.IsAntialias = true;

                    // Measure text width and height
                    var textBounds = new SKRect();
                    font.MeasureText(captchaCode, out textBounds);

                    var x = (width - textBounds.Width) / 2;
                    var y = (height + textBounds.Height) / 2;

                    // Draw the text
                    canvas.DrawText(captchaCode, new SKPoint(x, y), font, paint);
                }

                // Add noise
                using (var noisePaint = new SKPaint())
                {
                    noisePaint.Color = SKColors.Gray;
                    noisePaint.StrokeWidth = 1;
                    noisePaint.IsAntialias = true;

                    var random = new Random();
                    for (int i = 0; i < 50; i++)
                    {
                        float startX = random.Next(0, width);
                        float startY = random.Next(0, height);
                        float endX = random.Next(0, width);
                        float endY = random.Next(0, height);
                        canvas.DrawLine(startX, startY, endX, endY, noisePaint);
                    }
                }

                // Save image as PNG
                using (var image = surface.Snapshot())
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                {
                    return data.ToArray();
                }
            }
        }

    }
}
