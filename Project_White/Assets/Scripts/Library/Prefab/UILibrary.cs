using UnityEngine;

namespace RPG.UI.Library {
    [CreateAssetMenu(fileName = "UI Library", menuName = "RPG/Data/Library/UI/Main Library", order = 0)]
    public class UILibrary : ScriptableObject {

        [SerializeField] Buttons.UILibraryButtons uiLibraryButtons;

        public Buttons.UILibraryButtons UILibraryButtons => uiLibraryButtons;
    }

}
