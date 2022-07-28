using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace AllBeginningsMod.Utility.Extensions;

public static class EffectExtensions
{
    public static bool HasParameter(this Effect effect, string parameterName) => effect.Parameters.Any(parameter => parameter.Name == parameterName);
}