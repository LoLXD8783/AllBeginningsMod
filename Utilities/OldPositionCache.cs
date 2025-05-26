using Microsoft.Xna.Framework;

namespace AllBeginningsMod.Utilities; 
internal class OldPositionCache {
    public Vector2[] Positions { get; private set; }
    public int Count => Positions.Length;
    public OldPositionCache(int length) {
        Positions = new Vector2[length];
    }

    public void SetAll(Vector2 position) {
        for(int i = 0; i < Positions.Length; i++) {
            Positions[i] = position;
        }
    }

    public void Add(Vector2 position) {
        for(int i = Positions.Length - 1; i > 0; i--) {
            Positions[i] = Positions[i - 1];
        }

        Positions[0] = position;
    }
}