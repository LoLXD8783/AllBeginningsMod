namespace AllBeginningsMod.Common.Systems.WorldGeneration;

public readonly struct ChestLootEntry
{
    public readonly int Type;
    public readonly int Chance;
    public readonly int MinStack;
    public readonly int MaxStack;

    public readonly int[] ChestFrames;

    public ChestLootEntry(int type, int chance, int minStack, int maxStack, params int[] chestFrames) {
        Type = type;
        Chance = chance;
        MinStack = minStack;
        MaxStack = maxStack;
        ChestFrames = chestFrames;
    }
}