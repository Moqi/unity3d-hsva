using System;
using UnityEngine;

namespace Noptor.Util
{
    /// <summary>
    /// HSV color model with alpha channel supporting.
    /// <seealso cref="http://en.wikipedia.org/wiki/HSL_and_HSV"/>.
    /// </summary>
    public class ColorHSVA
    {

        /// <summary>
        /// Hue component of the color.
        /// </summary>
        public float hue;
        
        /// <summary>
        /// Saturation component of the color.
        /// </summary>
        public float saturation;
        
        /// <summary>
        /// Value component of the color.
        /// </summary>
        public float value;
        
        /// <summary>
        /// Alpha component of the color.
        /// </summary>
        public float alpha;
        
        /// <summary>
        /// Initialize a new HSV color with alpha channel.
        /// </summary>
        /// <param name="hue">Hue component of the color.</param>
        /// <param name="saturation">Saturation component of the color.</param>
        /// <param name="value">Value component of the color.</param>
        /// <param name="alpha">Alpha component of the color. 0 — transperent; 1 — opaque.</param>
        public ColorHSVA(float hue, float saturation, float value, float alpha)
        {
            this.hue = hue;
            this.saturation = saturation;
            this.value = value;
            this.alpha = alpha;
        }
        
        /// <summary>
        /// Initialize a new opaque HSV color.
        /// </summary>
        /// <param name="hue">Hue component of the color.</param>
        /// <param name="saturation">Saturation component of the color.</param>
        /// <param name="value">Value component of the color.</param>
        public ColorHSVA(float hue, float saturation, float value) : this(hue, saturation, value, 1f)
        {
            
        }
        
        /// <summary>
        /// Convert RGB to HSV (alpha channel will be saved).
        /// </summary>
        /// <param name="color">Object of RGB color.</param>
        public ColorHSVA(Color color)
        {
            float min = Mathf.Min(Mathf.Min(color.r, color.g), color.b);
            float max = Mathf.Max(Mathf.Max(color.r, color.g), color.b);
            float diff = max - min;
            
            alpha = color.a;
            value = max;
            saturation = Mathf.Approximately(0, max) ? 0 : diff / max;
            
            if(Mathf.Approximately(max, color.r))
            {
                hue = (color.g - color.b) / diff;
            }
            else if(Mathf.Approximately(max, color.g))
            {
                hue = (color.b - color.r) / diff + 2;
            }
            else if(Mathf.Approximately(max, color.b))
            {
                hue = (color.r - color.g) / diff + 4;
            }
            
            hue *= 60;
            
            if(hue < 0)
            {
                hue += 360;
            }
        }
        
        /// <summary>
        /// Convert HSV to RGB (alpha channel will be saved).
        /// </summary>
        /// <returns>Object of RBG color.</returns>
        public Color ToRGB()
        {
            if(saturation == 0)
            {
                return new Color(value, value, value, alpha);
            }
            
            if(hue > 360)
            {
                hue -= 360;
            }
            
            float sector = hue / 60;
            
            int i = (int)sector;
            float f = sector - i;
            
            float p = value * (1 - saturation);
            float q = value * (1 - saturation * f);
            float t = value * (1 - saturation * (1 - f));
            
            Color color = new Color(0, 0, 0, alpha);
            
            switch(i)
            {
                case 0:
                    color.r = value;
                    color.g = t;
                    color.b = p;
                    break;
                    
                case 1:
                    color.r = q;
                    color.g = value;
                    color.b = p;
                    break;
                    
                case 2:
                    color.r = p;
                    color.g = value;
                    color.b = t;
                    break;
                    
                case 3:
                    color.r = p;
                    color.g = q;
                    color.b = value;
                    break;
                    
                case 4:
                    color.r = t;
                    color.g = p;
                    color.b = value;
                    break;
                    
                default:
                    color.r = value;
                    color.g = p;
                    color.b = q;
                    break;
            }
            
            return color;
        }
        
        /// <summary>
        /// Returns a formatted string with components of the color.
        /// </summary>
        public override string ToString()
        {
            return "HSVA(" + hue + ", " + saturation + ", " + value + ", " + alpha + ")";
        }
    }
}