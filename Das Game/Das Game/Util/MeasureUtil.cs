using Microsoft.Xna.Framework;

namespace DasGame.Util
{
    internal class MeasureUtil
    {
        private const int PixelsPerMeter = 64;

        /// <summary>
        /// Convert meters to pixels according to pre-defined ratio
        /// </summary>
        /// <param name="meters">Value in meters</param>
        /// <returns>Value in Pixels</returns>
        public static int ToPixels(float meters)
        {
            return (int)(meters * PixelsPerMeter);
        }

        public static Vector2 ToPixels(Vector2 meters)
        {
            meters.X *= PixelsPerMeter;
            meters.Y *= PixelsPerMeter;
            return meters;
        }


        /// <summary>
        /// Convert pixels to meters according to pre-defined ratio
        /// </summary>
        /// <param name="pixels">Value in pixels</param>
        /// <returns>Value in meters</returns>
        public static float ToMeters(float pixels)
        {
            return (float)pixels / PixelsPerMeter;
        }

        public static Vector2 ToMeters(Vector2 pixels)
        {
            pixels.X /= PixelsPerMeter;
            pixels.Y /= PixelsPerMeter;
            return pixels;
        }
    }
}