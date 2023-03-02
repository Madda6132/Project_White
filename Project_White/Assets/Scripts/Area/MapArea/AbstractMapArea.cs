using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.World {
    public class AbstractMapArea : AbstractArea {

        /*---Protected---*/

        protected virtual void ExtraStart() { }

        protected override void Awake() {

            base.Awake();
            FindAndSetAreaConnections();
        }

        /*---Private---*/

        private void Start() {
            ExtraStart();
        }

        private void FindAndSetAreaConnections() {
            //if (ParentArea) AddAreaConnection(ParentArea);
            ////Find connections and add them to the connections list
            //for (int i = 0; i < AreasContainer.childCount; i++) {
            //    Transform child = AreasContainer.GetChild(i);
            //    if (!child.TryGetComponent(out AbstractMapArea mapArea)) { 
            //        continue; 
            //    }
            //    AddAreaConnection(mapArea);
            //}
        }
    }
}
