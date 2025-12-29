using Android.Graphics;
using System.Runtime.CompilerServices;

namespace ShortDev.Android.Graphics;

public static class Extensions
{
    public static int AsInt32(this Color color)
        => Unsafe.BitCast<Color, int>(color);

    public static Color AsColor(this int color)
        => Unsafe.BitCast<int, Color>(color);
}
