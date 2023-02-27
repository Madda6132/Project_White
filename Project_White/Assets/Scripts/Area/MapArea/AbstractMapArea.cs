using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.World {
    public class AbstractMapArea : AbstractArea {


        /*---Protected---*/

        protected virtual void ExtraStart() { }


        protected override void Awake() {
            base.Awake();

            //Find connections and add them to the connections list
            for (int i = 0; i < transform.childCount; i++) {

                Transform child = transform.GetChild(i);

                if (!child.TryGetComponent(out AbstractMapArea mapArea)) continue;

                conectedAreas.Add(mapArea);
            }

            AbstractArea parentArea = ParentArea;
            if (parentArea) conectedAreas.Add(parentArea);
        }

        /*---Private---*/

        private void Start() {
            ExtraStart();
        }

    }

}
