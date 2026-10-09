using UnityEngine;
using UnityEngine.UIElements;

namespace FoGD.UI.Behaviour
{
    [RequireComponent(typeof(UIDocument))]
    public class UIBehaviour : MonoBehaviour
    {
        private UIDocument _document;

        protected UIDocument Document
        {
            get
            {
                if (_document == null)
                    _document = GetComponent<UIDocument>();

                return _document;
            }
        }

        protected VisualElement Root
        {
            get => Document.rootVisualElement;
        }
    }
}
