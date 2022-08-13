using System;
using System.Collections.Generic;
using AllBeginningsMod.Utility.Extensions;
using Terraria;
using Terraria.ModLoader;

namespace AllBeginningsMod.Common.Systems.WorldGeneration.ChestLoot;

public abstract class ChestLoot : ModType
{
    public Dictionary<int, int> ItemAmountByType { get; protected set; }

    public abstract int ChestFrame { get; }

    public abstract void SetLoot(Chest chest);

    public override void Load() {
        ItemAmountByType = new Dictionary<int, int>();
    }

    public override void Unload() {
        ItemAmountByType?.Clear();
        ItemAmountByType = null;
    }

    protected sealed override void Register() {
        ModTypeLookup<ChestLoot>.Register(this);
    }
    
    protected bool TryAddItem(Chest chest, int type, int stack = 1, int chance = 1) {
        // So we guarantee at least one item in the entire world, instead of fully relying on RNG.
        bool alreadyExists = ItemAmountByType.TryGetValue(type, out int amount) && amount > 0;
        bool shouldAdd = WorldGen.genRand.NextBool(chance);
        
        if (alreadyExists && !shouldAdd)
            return false;

        bool success = chest.TryAddItem(type, stack);

        if (success)
            if (alreadyExists)
                ItemAmountByType[type] += stack;
            else 
                ItemAmountByType[type] = stack;

        return success;
    }

    protected bool TryRemoveItem(Chest chest, Predicate<Item> predicate) {
        return chest.TryRemoveItem(predicate);
    }

    protected bool TrySort(Chest chest) {
        return chest.TrySort();
    }
}