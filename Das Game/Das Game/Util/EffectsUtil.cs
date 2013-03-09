using System;

namespace DasGame.Util
{
    internal class EffectsUtil
    {
        /// <summary>
        /// Used for easing
        /// </summary>
        /// <param name="maxValue">Target Value</param>
        /// <param name="currentValue">Current Value</param>
        /// <param name="epsilon">Threshold</param>
        /// <returns>Next value in the Tweening effect</returns>
        public static float Tweening(float maxValue, float currentValue, float epsilon = .1f)
        {
            if (Math.Abs(maxValue - currentValue) > epsilon)
                return (maxValue - currentValue) * .1f;
            else //Redundant but legible
                return maxValue;
        }
    }
}