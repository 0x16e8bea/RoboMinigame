using UnityEngine;

public class SimpleQuadraticSolver
{
    public struct Coefficients
    {
        public float a;
        public float b;
        public float c;

        public Coefficients(float a, float b, float c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
    }

    public static float CalculateY(float x, Coefficients coefficients)
    {
        float y = coefficients.a * x * x + coefficients.b * x + coefficients.c;
        return y;
    }

    /// <summary>
    /// Calculate y but the function is normalized to 0-1 so the curve starts at x=0 and ends at x=1
    /// </summary>
    public static float CalculateNormalizedY(float x, Coefficients coefficients, float xStart, float xEnd)
    {
        float xNormalized = (x - xStart) / (xEnd - xStart);
        return CalculateY(xNormalized, coefficients);
    }

    public static Coefficients Calc3PointIntersectionCoefficients(float x1, float y1, float x2, float y2, float x3,
        float y3)
    {
        float determinant = x1 * x1 * (x2 - x3) + x2 * x2 * (x3 - x1) + x3 * x3 * (x1 - x2);

        float a = (y1 * (x2 - x3) + y2 * (x3 - x1) + y3 * (x1 - x2)) / determinant;
        float b = (y1 * (x3 * x3 - x2 * x2) + y2 * (x1 * x1 - x3 * x3) + y3 * (x2 * x2 - x1 * x1)) / determinant;
        float c = (y1 * (x2 * x3 * (x2 - x3)) + y2 * (x3 * x1 * (x3 - x1)) + y3 * (x1 * x2 * (x1 - x2))) / determinant;

        return new Coefficients(a, b, c);
    }

    public static Coefficients Calc3PointIntersectionCoefficients(Vector3 start, Vector3 middle, Vector3 end)
    {
        return Calc3PointIntersectionCoefficients(start.x, start.y, middle.x, middle.y, end.x, end.y);
    }
}