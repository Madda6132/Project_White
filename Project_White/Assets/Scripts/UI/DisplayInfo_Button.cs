using UnityEngine.Events;

namespace RPG.UI {
    public struct DisplayInfoButton {

        public string DisplayName { get; private set; }
        public string Path { get; private set; }
        //Tags? Ex add particle effect, or add a image representing something
        public string Description { get; private set; }
        public bool IsAllowed { get; private set; }
        public UIButtonManager ButtonManager { get; private set; }
        public UnityAction OnClickAction { get; private set; }

        public DisplayInfoButton(
            string displayName, 
            string path, 
            string description, 
            bool isAllowed,
            UnityAction onClickAction,
            UIButtonManager buttonManager) {

                this.DisplayName = displayName;
                this.Path = path;
                this.Description = description;
                this.IsAllowed = isAllowed;
                this.OnClickAction = onClickAction;
                this.ButtonManager = buttonManager;
        }

        public DisplayInfoButton(
            string displayName,
            string path,
            string description,
            bool isAllowed,
            UnityAction onClickAction,
            Library.Buttons.ButtonType buttonType) {

                this.DisplayName = displayName;
                this.Path = path;
                this.Description = description;
                this.IsAllowed = isAllowed;
                this.OnClickAction = onClickAction;

                this.ButtonManager = Core.PlayerVisuals.UILibrary.UILibraryButtons.GetButton(buttonType);
        }
    }
}

