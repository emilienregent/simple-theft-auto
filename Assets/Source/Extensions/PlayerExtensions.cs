public static class PlayerExtensions
{
    public static int LimitToRange(this int value, int inclusiveMinimum, int inclusiveMaximum, bool loop = false)
    {
        int result = value;

        if (loop == true)
        {
            if (value < inclusiveMinimum) { result = inclusiveMaximum; }
            if (value > inclusiveMaximum) { result = inclusiveMinimum; }
        }
        else
        {
            if (value < inclusiveMinimum) { result = inclusiveMinimum; }
            if (value > inclusiveMaximum) { result = inclusiveMaximum; }
        }

        return result;
    }

    public static float LimitToRange(this float value, float inclusiveMinimum, float inclusiveMaximum, bool loop = false)
    {
        float result = value;

        if (loop == true)
        {
            if (value < inclusiveMinimum) { result = inclusiveMaximum; }
            if (value > inclusiveMaximum) { result = inclusiveMinimum; }
        }
        else
        {
            if (value < inclusiveMinimum) { result = inclusiveMinimum; }
            if (value > inclusiveMaximum) { result = inclusiveMaximum; }
        }

        return result;
    }
}