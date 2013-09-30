using System;
using Microsoft.Xna.Framework;

namespace InfiniteIsland.Engine
{
    public delegate float TweenScale(float linearProgress);
    public delegate void TweenStep(float value);

    public static class Tweening
    {
        public static bool Tween(float start, float end, float progress, TweenStep step, TweenScale scale)
        {
            progress = MathHelper.Clamp(progress, 0f, 1f);
            step(MathHelper.Lerp(start, end, scale(progress)));
            return progress == 1;
        }
    }

    public static class TweenScales
    {
        public static TweenScale Linear = LinearScale;
        public static TweenScale Quadratic = QuadraticScale;
        public static TweenScale Cubic = CubicScale;
        public static TweenScale Quartic = QuarticScale;
        public static TweenScale Quintic = QuinticScale;

        private static float LinearScale(float progress)
        {
            return progress;
        }

        private static float QuadraticScale(float progress)
        {
            return progress*progress;
        }

        private static float CubicScale(float progress)
        {
            return progress*progress*progress;
        }

        private static float QuarticScale(float progress)
        {
            return progress*progress*progress*progress;
        }

        private static float QuinticScale(float progress)
        {
            return progress * progress * progress * progress * progress;
        }
    }
}