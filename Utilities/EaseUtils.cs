namespace AllBeginningsMod.Utilities;

public static class EaseUtils
{
    public static float QuadEaseIn(float x) {
        return x * x;
    }

    public static float HexicEaseIn(float x) {
        return x * x * x * x * x * x;
    }
}