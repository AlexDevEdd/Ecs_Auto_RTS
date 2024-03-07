namespace _Project.Scripts.EcsEngine._OOP.Systems.FXSystem.Interfaces
{
    public interface IVfxFactory
    {
        public VfxType Type { get; }
        public IVfx Spawn();
        public void Remove(IVfx vfx);
    }
}