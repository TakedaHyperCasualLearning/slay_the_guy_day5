using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    public Vector3 Parabola(Vector3 startPos, Vector3 endPos, float deg, float timeRatio)
    {
        float b = Mathf.Tan(deg * Mathf.Deg2Rad);
        float a = (endPos.y - b * endPos.x) / (endPos.x * endPos.x);

        float result = a * timeRatio * timeRatio + b * timeRatio;
        if (deg >= 90 && deg <= 270)
        {
            timeRatio *= -1;
        }
        return new Vector3(timeRatio, result, 0) + startPos;
    }
}
