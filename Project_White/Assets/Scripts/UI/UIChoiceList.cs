using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;

namespace RPG.UI {
    public class UIChoiceList : MonoBehaviour {

        [SerializeField] Transform _ChoicesTransform;
        [SerializeField] Transform CategoryLabelListTransform;

        [SerializeField] UICategoryLable LabelPrefab;

        public Transform ChoicesTransform => _ChoicesTransform;
        public UIButtonManager DisplayChoice(string displayText, UnityAction action, UIButtonManager buttonManager) {

            UIButtonManager button = Instantiate(buttonManager, _ChoicesTransform);
            button.TMPro.text = displayText;
            button.Button.onClick.AddListener(action);
            return button;
        }

        public void DisplayLables(string Path) {

            //Clear labels
            for (int i = 0; i < CategoryLabelListTransform.childCount; i++) {
                Destroy(CategoryLabelListTransform.GetChild(i).gameObject);
            }

            //Separate the Path
            //Display label for each non empty path
            List<string> lables = new(Path.Split("/", System.StringSplitOptions.RemoveEmptyEntries));
            lables.Reverse();
            int Count = lables.Count < 3 ? lables.Count : 3;
            for (int i = 1; i <= Count; i++) {
                CreateLabel(lables[Count - i]);
            }

            //Could implement a notifier that more than 3 categories are up
        }

        /*---Private---*/

        private void CreateLabel(string labelText) {

            UICategoryLable label = Instantiate(LabelPrefab, CategoryLabelListTransform);
            label.TMPro.text = labelText;
        }
    }
}
