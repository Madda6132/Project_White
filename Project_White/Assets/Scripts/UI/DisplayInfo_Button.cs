using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.UI {
    public struct DisplayInfo_Button {

        public string DisplayName { get; private set; }
        public string Path { get; private set; }
        //Tags? Ex add particle effect, or add a image representing something
        public string Description { get; private set; }
        public bool isAllowed { get; private set; }
        public UIButtonManager ButtonManager { get; private set; }
        public UnityAction OnClickAction { get; private set; }

        public DisplayInfo_Button(
            string DisplayName, 
            string Path, 
            string Description, 
            bool isAllowed,
            UnityAction OnClickAction,
            UIButtonManager ButtonManager) {

                this.DisplayName = DisplayName;
                this.Path = Path;
                this.Description = Description;
                this.isAllowed = isAllowed;
                this.OnClickAction = OnClickAction;
                this.ButtonManager = ButtonManager;
        }

        public DisplayInfo_Button(
            string DisplayName,
            string Path,
            string Description,
            bool isAllowed,
            UnityAction OnClickAction,
            Library.Buttons.ButtonType ButtonType) {

                this.DisplayName = DisplayName;
                this.Path = Path;
                this.Description = Description;
                this.isAllowed = isAllowed;
                this.OnClickAction = OnClickAction;

                this.ButtonManager = Core.PlayerVisuals.UILibrary.UILibraryButtons.GetButton(ButtonType);
        }

            
    }
}

