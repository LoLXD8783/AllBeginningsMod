using System.Collections.Generic;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.CustomEntities
{
    public abstract class CustomEntitySystem<T> : ModSystem where T : CustomEntity
    {
        protected static List<T> Entities { get; set; } = new();

        public override void Load() => Entities = new List<T>();

        public override void Unload()
        {
            Entities?.Clear();
            Entities = null;
        }

        public static T Spawn(T entity)
        {
            Entities.Add(entity);
            return entity;
        }

        public static bool Kill(T entity) => Entities.Remove(entity);
    }
}