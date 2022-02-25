using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace TanksGB.Core
{
    public sealed class SceneLoader
    {
        public void Load(string name, Action onLoaded = null) =>
            LoadScene(name, onLoaded);

        private async void LoadScene(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                return;
            }
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);
            while (!waitNextScene.isDone)
                await Task.Yield();
            onLoaded?.Invoke();
        }
        
        public async void LoadAddressableScene(string name, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();
                return;
            }
            await LoadScene(name);
            onLoaded?.Invoke();
        }

        private Task LoadScene(string name) => Addressables.LoadSceneAsync(name).Task;
    }
}