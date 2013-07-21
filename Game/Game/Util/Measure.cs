using Microsoft.Xna.Framework;

namespace InfiniteIsland.Util
{
    internal static class Measure
    {
        public const int PixelsPerMeter = 64;

        /// <summary>
        ///     Convert meters to pixels according to pre-defined ratio
        /// </summary>
        /// <param name="meters">Value in meters</param>
        /// <returns>Value in Pixels</returns>
        public static int ToPixels(this float meters)
        {
            return (int) (meters*PixelsPerMeter);
        }

        /// <summary>
        ///     Convert meters to pixels according to pre-defined ratio
        /// </summary>
        /// <param name="meters">Value in meters</param>
        /// <returns>Value in Pixels</returns>
        public static Vector2 ToPixels(this Vector2 meters)
        {
            meters.X *= PixelsPerMeter;
            meters.Y *= PixelsPerMeter;
            return meters;
        }

        /// <summary>
        ///     Convert meters to pixels according to pre-defined ratio
        /// </summary>
        /// <param name="meters">Value in meters</param>
        /// <returns>Value in Pixels</returns>
        public static Rectangle ToPixels(this RectangleF meters)
        {
            return new Rectangle(x: (int) (meters.TopLeft.X*PixelsPerMeter),
                                 y: (int) (meters.TopLeft.Y*PixelsPerMeter),
                                 width: (int) (meters.Dimensions.X*PixelsPerMeter),
                                 height: (int) (meters.Dimensions.Y*PixelsPerMeter));
        }

        /// <summary>
        ///     Convert pixels to meters according to pre-defined ratio
        /// </summary>
        /// <param name="pixels">Value in pixels</param>
        /// <returns>Value in meters</returns>
        public static float ToMeters(this float pixels)
        {
            return pixels/PixelsPerMeter;
        }

        /// <summary>
        ///     Convert pixels to meters according to pre-defined ratio
        /// </summary>
        /// <param name="pixels">Value in pixels</param>
        /// <returns>Value in meters</returns>
        public static Vector2 ToMeters(this Vector2 pixels)
        {
            pixels.X /= PixelsPerMeter;
            pixels.Y /= PixelsPerMeter;
            return pixels;
        }
    }
}