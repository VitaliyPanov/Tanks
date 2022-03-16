using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace Tanks.UI
{
    public sealed class MapElement : VisualElement
    {
        public MapElement()
        {
            var icon = new Image();
            Add(icon);
            icon.AddToClassList("element-icon");
            AddToClassList("element-container");
        }

        [Preserve]
        public new class UxmlFactory : UxmlFactory<MapElement, UxmlTraits> { }
        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits { }
    }
}