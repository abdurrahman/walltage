using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Walltage.WebUI.Infrastructures
{
    public class StaticHelper
    {
        public static Image CreateThumbnail(Image image, Size thumbnailSize, bool needToFill)
        {
            #region много арифметики
            int sourceWidth = image.Width;
            int sourceHeight = image.Height;
            int sourceX = 0;
            int sourceY = 0;
            double destX = 0;
            double destY = 0;

            double nScale = 0;
            double nScaleW = 0;
            double nScaleH = 0;

            nScaleW = ((double)thumbnailSize.Width / (double)sourceWidth);
            nScaleH = ((double)thumbnailSize.Height / (double)sourceHeight);
            if (!needToFill)
            {
                nScale = Math.Min(nScaleH, nScaleW);
            }
            else
            {
                nScale = Math.Max(nScaleH, nScaleW);
                destY = (thumbnailSize.Height - sourceHeight * nScale) / 2;
                destX = (thumbnailSize.Width - sourceWidth * nScale) / 2;
            }

            if (nScale > 1)
                nScale = 1;

            int destWidth = (int)Math.Round(sourceWidth * nScale);
            int destHeight = (int)Math.Round(sourceHeight * nScale);
            #endregion

            Image thumbnail = null;
            try
            {
                thumbnail = new System.Drawing.Bitmap(destWidth + (int)Math.Round(2 * destX), destHeight + (int)Math.Round(2 * destY));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("destWidth:{0}, destX:{1}, destHeight:{2}, desxtY:{3}, Width:{4}, Height:{5}",
                    destWidth, destX, destHeight, destY, thumbnailSize.Width, thumbnailSize.Height), ex);
            }
            using (System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(thumbnail))
            {
                grPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                grPhoto.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                grPhoto.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                Rectangle to = new System.Drawing.Rectangle((int)Math.Round(destX), (int)Math.Round(destY), destWidth, destHeight);
                Rectangle from = new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight);
                grPhoto.DrawImage(image, to, from, System.Drawing.GraphicsUnit.Pixel);
            }

            return thumbnail;
        }

        //private static float CalculateScalingRatio(Size originalSize, Size targetSize)
        //{
        //    float originalAspectRatio = (float)originalSize.Width / (float)originalSize.Height;
        //    float targetAspectRatio = (float)targetSize.Width / (float)targetSize.Height;

        //    float scalingRatio = 0;

        //    if (targetAspectRatio >= originalAspectRatio)
        //    {
        //        scalingRatio = (float)targetSize.Width / (float)originalSize.Width;
        //    }
        //    else
        //    {
        //        scalingRatio = (float)targetSize.Width / (float)originalSize.Height;
        //    }

        //    return scalingRatio;
        //}
    }
}