/// <summary>
/// Цвет в формате HSV с поддержкой альфа-канала.
/// </summary>
namespace Noptor.Util
{
    public class ColorHSVA
    {
        /// <summary>
        /// Цветовой тон.
        /// </summary>
        public float hue;

        /// <summary>
        /// Насыщенность.
        /// </summary>
        public float saturation;

        /// <summary>
        /// Значение цвета.
        /// </summary>
        public float value;

        /// <summary>
        /// Альфа-канал.
        /// </summary>
        public float alpha;

        /// <summary>
        /// Создаёт цвет в формате HSV с поддержкой альфа-канала.
        /// </summary>
        /// <param name="hue"></param>
        /// <param name="saturation"></param>
        /// <param name="value"></param>
        /// <param name="alpha"></param>
        public ColorHSVA(float hue, float saturation, float value, float alpha)
        {
            this.hue = hue;
            this.saturation = saturation;
            this.value = value;
            this.alpha = alpha;
        }

        /// <summary>
        /// Создаёт цвет в формате HSV с максимальной непрозрачностью.
        /// </summary>
        /// <param name="hue"></param>
        /// <param name="saturation"></param>
        /// <param name="value"></param>
        public ColorHSVA(float hue, float saturation, float value) : this(hue, saturation, value, 1f);

        /// <summary>
        /// Преобразует RGB в HSV. Аьлфа-канал сохраняется.
        /// </summary>
        /// <param name="color"></param>
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
        /// Переводит цвет из модели HSV в RGB. Альфа-канал сохраняется.
        /// </summary>
        /// <returns>Объект цвета в формате RGB.</returns>
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
        /// Возвращает строкое представление данного цвета.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "HSVA(" + hue + ", " + saturation + ", " + value + ", " + alpha + ")";
        }
    }
}