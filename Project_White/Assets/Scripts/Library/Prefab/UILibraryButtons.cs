using UnityEngine;

namespace RPG.UI.Library.Buttons {

    public enum ButtonType {
        DEFAULT,
        TRAVEL,
        BUSINESS,
        CATEGORY,
        CRAFTING
    }

    [CreateAssetMenu(fileName = "UI Button Section", menuName = "RPG/Data/Library/UI/Sections", order = 0)]
    public class UILibraryButtons : ScriptableObject {

        [SerializeField] UIButtonManager defaultButton;
        [SerializeField] UIButtonManager categoryButton;

        /// <summary>
        /// Ex Moving
        /// </summary>
        [SerializeField] UIButtonManager travelButton;
        /// <summary>
        /// Ex Purchasing or selling
        /// </summary>
        [SerializeField] UIButtonManager businessButton;
        [SerializeField] UIButtonManager craftingButton;

        public UIButtonManager DefaultButton => defaultButton;
        public UIButtonManager CategoryButton => categoryButton;
        public UIButtonManager TravelButton => travelButton;
        public UIButtonManager BusinessButton => businessButton; // Ex Purchasing or selling
        public UIButtonManager CraftingButton => craftingButton;

        public UIButtonManager GetButton(ButtonType buttonType = ButtonType.DEFAULT) {

            switch (buttonType) {

                case ButtonType.TRAVEL:
                    return TravelButton;

                case ButtonType.BUSINESS:
                    return BusinessButton;

                case ButtonType.CATEGORY:
                    return CategoryButton;

                case ButtonType.CRAFTING:
                    return CraftingButton;

                default:
                case ButtonType.DEFAULT:
                    return DefaultButton;
            }
        }
    }

}
