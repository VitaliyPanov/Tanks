using UnityEngine;

namespace Tanks.Core
{
    internal sealed class GameStarter : MonoBehaviour
    {
        [SerializeField] private Bootstrapper _bootstrapperPrefab;

        private void Awake()
        {
            Bootstrapper bootstrapper = FindObjectOfType<Bootstrapper>();
            if (bootstrapper == null)
            {
                Instantiate(_bootstrapperPrefab);
            }
            Destroy(gameObject);
        }
    }
}
