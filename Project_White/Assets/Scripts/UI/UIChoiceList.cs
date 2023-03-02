using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.UI {
    public class UIChoiceList : MonoBehaviour {

        [SerializeField] Transform choicesTransform;
        [SerializeField] Transform categoryLabelListTransform;

        [SerializeField] UICategoryLabel labelPrefab;

        public Transform ChoicesTransform => choicesTransform;
        public UIButtonManager DisplayChoice(string displayText, UnityAction action, UIButtonManager buttonManager) {
            UIButtonManager button = Instantiate(buttonManager, choicesTransform);
            button.TMPro.text = displayText;
            button.Button.onClick.AddListener(action);
            return button;
        }

        public void DisplayLabels(string path) {
            //Clear labels
            for (int i = 0; i < categoryLabelListTransform.childCount; i++) {
                Destroy(categoryLabelListTransform.GetChild(i).gameObject);
            }
            //Separate the Path
            //Display label for each non empty path
            List<string> labels = new(path.Split("/", System.StringSplitOptions.RemoveEmptyEntries));
            labels.Reverse();
            int count = labels.Count < 3 ? labels.Count : 3;
            for (int i = 1; i <= count; i++) {
                CreateLabel(labels[count - i]);
            }
            //Could implement a notifier that more than 3 categories are up
        }

        /*---Private---*/

        private void CreateLabel(string labelText) {
            UICategoryLabel label = Instantiate(labelPrefab, categoryLabelListTransform);
            label.TMPro.text = labelText;
        }
    }
}
