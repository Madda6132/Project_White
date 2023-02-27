using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Creature;

namespace RPG.World {
    public class AreaBuilding : AbstractMapArea {

        [SerializeField] AbstractExploreArea startArea;
        
        [SerializeField] Character owner;


        /*---Protected---*/

        protected override void Awake() {
            base.Awake();
        }

        protected override void ExtraStart() {
            base.ExtraStart();
            conectedAreas.Add(startArea);

            AbstractArea parentArea = ParentArea;
            if(parentArea) conectedAreas.Add(parentArea);

        }
    }
}
