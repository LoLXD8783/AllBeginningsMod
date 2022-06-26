namespace AllBeginningsMod.Common.CustomEntities
{
    public abstract class CustomEntity
    {
        public int WhoAmI { get; internal set; }

        public virtual void OnSpawn() { }

        public virtual void OnKill() { }

        public virtual void OnUpdate() { }

        public virtual void OnDraw() { }
    }
}