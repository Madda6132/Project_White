using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG.UI {
    public class UIButtonManager : MonoBehaviour {

        [SerializeField] Button button;
        [SerializeField] TextMeshProUGUI tmPro;
        [SerializeField] Image frame;

        [SerializeField] Transform backLayer;
        [SerializeField] Transform middleLayer;
        [SerializeField] Transform frontLayer;

        public Button Button => button;
        public TextMeshProUGUI TMPro  => tmPro;
        public Image Frame => frame;
        public Transform LayerBack => backLayer;
        public Transform LayerMiddle => middleLayer;
        public Transform LayerFront => frontLayer;
    }
}

