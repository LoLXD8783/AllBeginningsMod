using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBeginningsMod.Utilities.Extensions
{
    internal static class Vector2Extensions 
    {
        public static Vector3 ToVector3(this Vector2 vector, float z = 0f) {
            return new Vector3(vector.X, vector.Y, z);
        } 
    }
}
