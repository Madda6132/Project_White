using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.Library.Buttons {

    public enum ButtonType {
        DEFAULT,
        TRAVEL,
        BUSINESS,
        CATEGORY,
        CRAFTING
    }

    [CreateAssetMenu(fileName = "UI Button Section", menuName = "RPG/Data/Librarys/UI/Sections", order = 0)]
    public class UILibraryButtons : ScriptableObject {

        [SerializeField] UIButtonManager _Default_Button;
        [SerializeField] UIButtonManager _Category_Button;

        /// <summary>
        /// Ex Moving
        /// </summary>
        [SerializeField] UIButtonManager _Travel_Button;
        /// <summary>
        /// Ex Purchasing or selling
        /// </summary>
        [SerializeField] UIButtonManager _Business_Button;
        [SerializeField] UIButtonManager _Crafting_Button;


        public UIButtonManager Default_Button => _Default_Button;
        public UIButtonManager Category_Button => _Category_Button;
        public UIButtonManager Travel_Button => _Travel_Button;
        public UIButtonManager Business_Button => _Business_Button; // Ex Purchasing or selling
        public UIButtonManager Crafting_Button => _Crafting_Button;

        public UIButtonManager GetButton(ButtonType buttonType = ButtonType.DEFAULT) {

            switch (buttonType) {

                case ButtonType.TRAVEL:
                    return Travel_Button;

                case ButtonType.BUSINESS:
                    return Business_Button;

                case ButtonType.CATEGORY:
                    return Category_Button;

                case ButtonType.CRAFTING:
                    return Crafting_Button;

                default:
                case ButtonType.DEFAULT:
                    return Default_Button;
            }
        }
    }

}
