using UnityEngine;
using System;

namespace RPG.Items {
    [CreateAssetMenu(fileName = "New Item", menuName = "Create new Item", order = 0)]
    public class Item : ScriptableObject {

        [SerializeField] string itemName = "";
        [SerializeField] string itemDescription = "";
        [SerializeField] int value = 1;
        [SerializeField] string uniqueID;
        
        public string Name { get => itemName; private set => itemName = value; }
        public string Description { get => itemDescription; private set => itemDescription = value; }
        public int Value { get => value; private set => this.value = value; }


        private void Awake() {
            if (String.IsNullOrEmpty(uniqueID)) {
                uniqueID = Guid.NewGuid().ToString();
            }
        }
    }

}
