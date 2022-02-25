using Tanks.Data;

namespace General.Services
{
    public interface IDataService
    {
        void Load();
        SceneStaticData StaticData(string sceneName);
        RuntimeData RuntimeData { get; }
    }
}