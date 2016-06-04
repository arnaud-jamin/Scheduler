using UnityEngine;
using System.Collections;

public class MathHelper : MonoBehaviour
{
    //-----------------------------------------------------------------------------------------
    public static Vector2 FilterInput(Vector2 input, float deadzone, float power)
    {
        float lenght = input.magnitude;
        float newLenght = RescaleAndClamp(lenght, deadzone, 1);
        input = input.normalized * newLenght;
        input = input.normalized * Mathf.Pow(input.magnitude, power);
        return input;
    }

    //-----------------------------------------------------------------------------------------
    public static float RescaleAndClamp(float value, float min, float max)
    {
        return Mathf.Clamp((value - min) / (max - min), 0.0f, 1.0f);
    }
}
