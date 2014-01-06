using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]

public class RainbowBG : MonoBehaviour
{

    /// <summary>
    /// Скорость смены цветов.
    /// </summary>
    public float Speed = 100;

    /// <summary>
    /// Насыщенность цвета.
    /// </summary>
    public float Saturation = 0.7f;

    /// <summary>
    /// Яркость цвета фона.
    /// </summary>
    public float Value = 0.7f;

    /// <summary>
    /// Цвет фона в модели HSV.
    /// </summary>
    private ColorHSVA _hsv;

    /// <summary>
    /// Текущий цвет фона. <see cref="Camera.backgroundColor"/>
    /// </summary>
    public Color BGColor
    {
        get
        {
            return camera.backgroundColor;
        }

        set
        {
            camera.backgroundColor = value;
        }
    }

    /// <summary>
    /// Задаёт цвет фона в исходное положение.
    /// </summary>
    public void Awake()
    {
        _hsv = new ColorHSVA(0f, Saturation, Value);
    }

    /// <summary>
    /// Meow!
    /// </summary>
    public void Update()
    {
        _hsv.hue += Speed * Time.deltaTime;
        BGColor = _hsv.ToRGB();
    }
}