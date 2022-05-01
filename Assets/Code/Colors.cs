using UnityEngine;

public static class Colors
{
    public static Color ClearWhite => new Color(1.0f, 1.0f, 1.0f, 0.0f);
}

public static class ColorUtility
{
    public static Color Lerped(this Color color, Color other, float factor)
    {
        return Color.Lerp(color, other, factor);
    }

    public static Color AlphaChangedTo(this Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }
}
