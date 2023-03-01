using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG.UI {
    public class UIButtonManager : MonoBehaviour {

        [SerializeField] Button _Button;
        [SerializeField] TextMeshProUGUI _TMPro;
        [SerializeField] Image _Frame;

        [SerializeField] Transform _Layer_Back;
        [SerializeField] Transform _Layer_Middle;
        [SerializeField] Transform _Layer_Front;

        public Button Button => _Button;
        public TextMeshProUGUI TMPro  => _TMPro;
        public Image Frame => _Frame;
        public Transform Layer_Back => _Layer_Back;
        public Transform Layer_Middle => _Layer_Middle;
        public Transform Layer_Front => _Layer_Front;
    }
}

