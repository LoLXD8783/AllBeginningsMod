using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.WorldGeneration;

public abstract class ChestLoot : ModType
{
    public abstract int ChestFrame { get; }

    public abstract void SetLoot(Chest chest);

    protected override sealed void Register() {
        ModTypeLookup<ChestLoot>.Register(this);
    }
}