using Tanks.Data;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace Tanks.UI
{
    public class MapElement : VisualElement
    {
        private const string c_elementStyle = "element-icon";
        private const string c_containerStyle = "element-container";

        public MapElement()
        {
            var icon = new Image();
            Add(icon);
            icon.AddToClassList(c_elementStyle);
            AddToClassList(c_containerStyle);
        }

        [Preserve]
        public new class UxmlFactory : UxmlFactory<MapElement, UxmlTraits> { }
        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits { }
    }
}