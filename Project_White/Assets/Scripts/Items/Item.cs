using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG.Items {
    [CreateAssetMenu(fileName = "New Item", menuName = "Create new Item", order = 0)]
    public class Item : ScriptableObject {

        public string Name { get => _ItemName; private set => _ItemName = value; }
        public string Description { get => _ItemDescription; private set => _ItemDescription = value; }
        public int Value { get => _Value; private set => _Value = value; }

        [SerializeField] string _ItemName = "";
        [SerializeField] string _ItemDescription = "";
        [SerializeField] int _Value = 1;

        [SerializeField] string _UniqueID;

        private void Awake() {

            if (String.IsNullOrEmpty(_UniqueID)) {

                _UniqueID = Guid.NewGuid().ToString();
            }
        }
    }

}
