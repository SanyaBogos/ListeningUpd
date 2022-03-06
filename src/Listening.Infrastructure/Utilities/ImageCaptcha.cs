using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Text;

namespace Listening.Infrastructure.Utilities
{
    // Install linux libs if on linux: sudo apt install libc6-dev libgdiplus
    // For more details: https://stackoverflow.com/questions/37223861/how-to-create-captcha-in-asp-net-core-asp-net-mvc-6
    public static class ImageCaptcha
    {
        private const int ImageTextLength = 5;

        public static Bitmap Generate(int w, int h, out string validate)
        {
            Bitmap bmp = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Graphics graphics = Graphics.FromImage(bmp);
            var brush = new[] { Brushes.Brown, Brushes.LawnGreen, Brushes.Blue, Brushes.Coral,
                Brushes.Red, Brushes.Yellow, Brushes.Violet, Brushes.BurlyWood };

            var rand = new Random((int)DateTime.Now.Ticks);
            var result = new StringBuilder(ImageTextLength);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                for (int i = 0; i < ImageTextLength; i++)
                {
                    var text = Convert.ToChar(rand.Next(97, 122)).ToString();
                    result.Append(text);

                    if (i == 0)
                        g.TranslateTransform(bmp.Width / 2, bmp.Height / 2);

                    g.RotateTransform(-20 + i * 10);
                    var font = new Font("Arial", h * 0.25f + 8 * rand.Next(1, 6),
                        FontStyle.Bold | FontStyle.Italic);
                    SizeF textSize = g.MeasureString(text, font);
                    g.DrawString(text, font, brush[rand.Next(0, brush.Length - 1)],
                        38 * (i + 1) - (textSize.Width / 2),
                        -(textSize.Height / 2));
                }
            }

            validate = result.ToString();
            return bmp;
        }
    }
}
