using System.Collections.Generic;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.CustomEntities
{
    [Autoload(Side = ModSide.Client)]
    public abstract class CustomEntitySystem<T> : ModSystem where T : CustomEntity
    {
        public abstract int MaxEntities { get; }

        public static T[] Entities { get; private set; }
        public static Queue<int> FreeIndices { get; private set; }

        public override void OnModLoad()
        {
            Entities = new T[MaxEntities];
            FreeIndices = new Queue<int>(MaxEntities);

            for (int i = 0; i < MaxEntities; i++)
            {
                FreeIndices.Enqueue(i);
            }
        }

        public override void Unload()
        {
            Entities = null;
            
            FreeIndices?.Clear();
            FreeIndices = null;
        }

        public static T Spawn(T entity)
        {
            if (FreeIndices.TryDequeue(out int index))
            {
                Entities[index] = entity;
                Entities[index].WhoAmI = index;
                Entities[index].OnSpawn();
            }

            return entity;
        }

        public static void Kill(T entity)
        {
            Entities[entity.WhoAmI].OnKill();
            Entities[entity.WhoAmI] = null;

            FreeIndices.Enqueue(entity.WhoAmI);
        }
    }
}