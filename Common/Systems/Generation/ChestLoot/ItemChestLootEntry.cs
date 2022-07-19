namespace AllBeginningsMod.Common.Systems.Generation.ChestLoot
{
    public record class ItemChestLootEntry(int Type, int MinStack = 1, int MaxStack = 1, int MaxAmountPerWorld = 1, int ExtraSpawnChance = 1) {
        public int CurrentWorldAmount { get; set; }
    }
}