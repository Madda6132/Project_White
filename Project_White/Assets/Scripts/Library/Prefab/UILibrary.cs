using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.Library {
    [CreateAssetMenu(fileName = "UI Library", menuName = "RPG/Data/Librarys/UI/Main Library", order = 0)]
    public class UILibrary : ScriptableObject {

        [SerializeField] Buttons.UILibraryButtons _UILibraryButtons;

        public Buttons.UILibraryButtons UILibraryButtons => _UILibraryButtons;
    }

}
