namespace AllBeginningsMod.Common.Systems.Physics
{
    public abstract class PhysicsModule
    {
        public abstract void Update();

        public void Register() {
            PhysicsSystem.Modules.Add(this);
        }

        public void Unregister() {
            PhysicsSystem.Modules.Remove(this);
        }
    }
}