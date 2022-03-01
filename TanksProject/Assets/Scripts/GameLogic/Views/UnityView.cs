using Entitas.Unity;
using Tanks.GameLogic.Services;
using Tanks.GameLogic.Views.Behaviours;
using UnityEngine;

namespace Tanks.GameLogic.Views
{
    [SelectionBase]
    [RequireComponent(typeof(EntityLink))]
    public sealed class UnityView : MonoBehaviour, IView
    {
        private EntityLink _entityLink;
        Transform IView.Transform => transform;
        GameObject IView.GameObject => gameObject;

        public void InitializeView(GameEntity entity)
        {
            _entityLink = gameObject.GetOrAddComponent<EntityLink>();

            entity.AddView(this);
            _entityLink = gameObject.Link(entity);
            
            var eventBehaviours = gameObject.GetComponents<IBehaviour>();
            foreach (var behaviour in eventBehaviours)
            {
                behaviour.Initialize(entity);
            }
            var eventListeners = gameObject.GetComponents<IEventListener>();
            foreach (var listener in eventListeners)
            {
                listener.AddListener(entity);
            }

            if (entity.hasPosition) SetPosition(entity.position.Value);
            if (entity.hasRotation) SetRotation(entity.rotation.Value);
        }

        private void SetRotation(Quaternion rotation) => transform.localRotation = rotation;

        private void SetPosition(Vector3 position) => transform.localPosition = position;

        private void UnlinkGameObject()
        {
            if (_entityLink.entity != null) 
                gameObject.Unlink();
        }

        public void DestroyView() => UnlinkGameObject();

        private void OnDestroy() => UnlinkGameObject();
    }
}