using System.Reflection;

namespace AllBeginningsMod.Utilities;

public static class ReflectionUtils
{
    public const BindingFlags FlagsAll = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

    public const BindingFlags FlagsPublicInstance = BindingFlags.Public | BindingFlags.Instance;
    public const BindingFlags FlagsPrivateInstance = BindingFlags.NonPublic | BindingFlags.Instance;
}