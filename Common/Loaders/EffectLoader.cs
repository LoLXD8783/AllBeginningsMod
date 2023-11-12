using log4net.Repository.Hierarchy;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.IO;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Loaders
{
    internal class EffectLoader : ILoadable {
        public void Load(Mod mod) {
            foreach (string filePath in mod.GetFileNames()) {
                if (filePath.EndsWith(".xnb")) {
                    Asset<Effect> assetEffect = mod.Assets.Request<Effect>(filePath.Replace(".xnb", string.Empty), AssetRequestMode.ImmediateLoad);
                    if (assetEffect.Value is null) {
                        continue;
                    }

                    string name = Path.GetFileNameWithoutExtension(filePath);
                    string sceneShaderName = nameof(AllBeginningsMod) + ":" + name;

                    Filters.Scene[sceneShaderName] = new Filter(
                        new ScreenShaderData(new Ref<Effect>(assetEffect.Value), name + "Pass")
                    );
                    Filters.Scene[sceneShaderName].Load();
                }
            }

            foreach (FieldInfo[] fieldInfos in mod.Code.GetTypes().Select(type => type.GetRuntimeFields())) {
                foreach (FieldInfo fieldInfo in fieldInfos) {
                    EffectAttribute effectAttribute;
                    if ((effectAttribute = fieldInfo.GetCustomAttribute<EffectAttribute>()) is null) {
                        continue;
                    }

                    Asset<Effect> assetEffect = mod.Assets.Request<Effect>("Assets/Effects/" + effectAttribute.Name, AssetRequestMode.ImmediateLoad);
                    if (assetEffect.Value is null) {
                        continue;
                    }

                    if (fieldInfo.IsInitOnly) {
                        continue;
                    }

                    fieldInfo.SetValue(null, assetEffect.Value);
                }
            }
        }

        public void Unload() { }
    }

    [AttributeUsage(AttributeTargets.Field)]
    internal sealed class EffectAttribute : Attribute {
        public string Name { get; }
        public EffectAttribute(string name) {
            Name = name;
        }

    }
}
