using Tanks.Data;

namespace Tanks.General.Services
{
    public interface IDataService
    {
        void Load();
        SceneStaticData StaticData(string sceneName);
        RuntimeData RuntimeData { get; }
        AmmoData AmmunitionData(AmmoType type);
        UIData UIData { get; }
    }
}