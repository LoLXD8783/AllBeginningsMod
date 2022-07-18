namespace AllBeginningsMod.Common.Systems.Generation.ChestLoot
{
    public record struct ItemChestLootEntry(int Type, int MinStack = 1, int MaxStack = 1, int SpawnChance = 1);
}
