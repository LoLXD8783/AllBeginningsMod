using System.Collections.Generic;
using AllBeginningsMod.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.WorldGeneration;

public abstract class ChestLoot : ModType
{
    public readonly Dictionary<int, int> ItemAmountByType = new();

    public abstract int ChestFrame { get; }

    public abstract void SetLoot(Chest chest);

    protected override sealed void Register() {
        ModTypeLookup<ChestLoot>.Register(this);
    }

    protected bool TryAddItem(Chest chest, int type, int stack = 1, int chance = 1) {
        bool alreadyExists = ItemAmountByType.TryGetValue(type, out int amount) && amount > 0;
        bool shouldAdd = WorldGen.genRand.NextBool(chance);

        if (alreadyExists && !shouldAdd) {
            return false;
        }

        bool success = chest.TryAddLootItem(type, stack);

        if (success) {
            if (alreadyExists) {
                ItemAmountByType[type] += stack;
            }
            else {
                ItemAmountByType[type] = stack;
            }
        }

        return success;
    }
}