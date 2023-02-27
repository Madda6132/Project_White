using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.World {
    public class AreaRegion : AbstractMapArea {


        public void AddArea<AreaType>(AreaType area) where AreaType : AbstractArea {

            conectedAreas.Add(area);
        }


        /*---Protected---*/

        protected override void Awake() {
            base.Awake();
        }
    }
}
