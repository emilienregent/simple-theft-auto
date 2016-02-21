using System;

public static class Extensions
{
    public static T Previous<T>(this T src) where T : struct
    {
        return src.GetSibling(-1);          
    }

    public static T Next<T>(this T src) where T : struct
    {
        return src.GetSibling(1);
    }

    private static T GetSibling<T>(this T src, int order) where T : struct
    {
        if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argumnent {0} is not an Enum", typeof(T).FullName));

        T[] values = (T[])Enum.GetValues(src.GetType());

        int index = Array.IndexOf<T>(values, src) + order;

        if (index >= values.Length)
        {
            return values[0];
        }
        else if (index < 0)
        {
            return values[values.Length - 1];
        }
        else
        {
            return values[index];
        }
    }
}