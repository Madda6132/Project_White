using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG.UI {
    public class UICategoryLabel : MonoBehaviour {

        [SerializeField] TextMeshProUGUI tmPro;
        [SerializeField] Image background;

        public TextMeshProUGUI TMPro => tmPro;
        public Image Background => background;
    }
}
