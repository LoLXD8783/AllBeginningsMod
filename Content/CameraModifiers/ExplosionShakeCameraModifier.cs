using AllBeginningsMod.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.CameraModifiers;
using Terraria.ModLoader;

namespace AllBeginningsMod.Content.CameraModifiers
{
    internal class ExplosionShakeCameraModifier : ICameraModifier
    {
        private float strength;
        private readonly float diminish;
        private readonly Vector2? sourcePosition; 
        private readonly float? maxRange;
        private readonly string uniqueIdentity;
        public ExplosionShakeCameraModifier(float strength, float diminish, Vector2? sourcePosition, float? maxRange, string uniqueIdentity = null) {
            this.strength = strength;
            this.diminish = diminish;
            this.sourcePosition = sourcePosition;
            this.maxRange = maxRange;
            this.uniqueIdentity = uniqueIdentity;
        }

        public ExplosionShakeCameraModifier(float strength, float diminish, string uniqueIdentity = null) : this(strength, diminish, null, null, uniqueIdentity) { }

        public string UniqueIdentity => uniqueIdentity;
        public bool Finished => strength < 0.001f;
        public void Update(ref CameraInfo cameraPosition) {
            float rangeMultiplier = 1f;
            if (maxRange is not null && sourcePosition is not null) {
                rangeMultiplier = 1f - MathF.Min(Main.LocalPlayer.DistanceSQ(sourcePosition.Value) / (maxRange.Value * maxRange.Value), 1f);
            }

            // what ?    ?
            /*ScreenShaderHandler.ApplyShader(
                "AllBeginningsMod:WaterFilter", 
                effect => {
                    effect.Parameters["noise"].SetValue(
                        ModContent.Request<Texture2D>("AllBeginningsMod/Assets/Images/Sample/Noise3", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value
                    );

                    effect.Parameters["strength"].SetValue(
                        strength / 5f
                    );
                }
            );*/

            cameraPosition.CameraPosition += Main.rand.NextVector2Unit() * strength * rangeMultiplier;
            strength *= diminish;
        }
    }
}
