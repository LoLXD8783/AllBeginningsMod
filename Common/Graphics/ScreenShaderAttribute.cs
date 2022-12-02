using System;

namespace AllBeginningsMod.Common.Graphics;

public sealed class ScreenShaderAttribute : Attribute
{
    public readonly string Name;

    public ScreenShaderAttribute(string name) {
        Name = name;
    }
}