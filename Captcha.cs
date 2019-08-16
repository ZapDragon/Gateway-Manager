using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace GatewayManager
{
    public class Captcha
    {
        //Constructor Constants
        private int xSize { get; set; }
        private int ySize { get; set; }
        private int distortion { get; set; }
        private int CharCount { get; set; }

        //Additional Defaulted
        private Color BackgroundColor { get; set; } = Color.Black;
        private Color ForegroundColor { get; set; } = Color.White;
        private Font fontProperties { get; set; } = new Font("Comic Sans MS", 30, FontStyle.Strikeout);

        //Generated Values
        private string CaptchaCode { get; set; } = "";

        public string GetCaptchaCode() { return CaptchaCode; }

        /// <summary>
        /// Constructor sets the required properties for the captcha image generator.
        /// </summary>
        /// <param name="SizeX">Height of the image to be generated</param>
        /// <param name="SizeY">Width of the image to be generated</param>
        /// <param name="Distort">The value used to determine the level of distortion. 0 Being no Distortion, and 32 being unredadable.</param>
        /// <param name="CaptchaLength">The number of Characters used to generate a captcha. Between 1 and 16 recommended.</param>
        /// <returns>Creates a new Captcha object class to generate captcha images.</returns>
        public Captcha(uint SizeX, uint SizeY, uint Distort, uint CaptchaLength)
        {
            if (CaptchaLength < 1)
            {
                Console.WriteLine("The Argument CaptchaLength cannot be zero.");
                throw new ArgumentOutOfRangeException();
            }
            xSize = Convert.ToInt32(SizeX);
            ySize = Convert.ToInt32(SizeY);
            distortion = Convert.ToInt32(Distort);
            CharCount = Convert.ToInt32(CaptchaLength);
        }

        public void SetImageProperties(Color bkgndColor, Color frgndColor, string FontFamily = "Comic Sans MS", uint FontSize = 30, FontStyle fontStyle = FontStyle.Strikeout)
        {
            BackgroundColor = bkgndColor;
            ForegroundColor = ForegroundColor;
            fontProperties = new Font(FontFamily, FontSize, fontStyle);
        }

        public MemoryStream GenerateImage()
        {
            Random rand = new Random();
            CaptchaCode = new string(Enumerable.Repeat("ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz0123456789", CharCount).Select(s => s[rand.Next(s.Length)]).ToArray());
            int newX, newY;
            MemoryStream memoryStream = new MemoryStream();
            Bitmap captchaImage = new Bitmap(xSize, ySize, System.Drawing.Imaging.PixelFormat.Format64bppArgb);
            Bitmap cache = new Bitmap(xSize, ySize, System.Drawing.Imaging.PixelFormat.Format64bppArgb);
            Graphics graphicsTextHolder = Graphics.FromImage(captchaImage);
            graphicsTextHolder.Clear(BackgroundColor);
            graphicsTextHolder.DrawString(CaptchaCode, fontProperties, new SolidBrush(ForegroundColor), new PointF(8.4F, 20.4F));

            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    newX = (int)(x + (distortion * Math.Sin(Math.PI * y / 64.0)));
                    newY = (int)(y + (distortion * Math.Cos(Math.PI * x / 64.0)));
                    if (newX < 0 || newX >= xSize) newX = 0;
                    if (newY < 0 || newY >= ySize) newY = 0;
                    cache.SetPixel(x, y, captchaImage.GetPixel(newX, newY));
                }
            }
            cache.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
