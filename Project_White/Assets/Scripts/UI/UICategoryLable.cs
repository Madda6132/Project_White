using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG.UI {
    public class UICategoryLable : MonoBehaviour {


        [SerializeField] TextMeshProUGUI _TMPro;
        [SerializeField] Image _Background;

        public TextMeshProUGUI TMPro => _TMPro;
        public Image Background => _Background;
    }
}
