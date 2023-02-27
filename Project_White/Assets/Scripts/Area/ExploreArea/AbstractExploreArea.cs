using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.World {
    public class AbstractExploreArea : AbstractArea {

        //Tell core to display task to move to connection
        

        [SerializeField] List<AbstractArea> _RoomConnectedAreas = new();


        /*---Protected---*/
        protected override void Awake() {
            base.Awake();
            _RoomConnectedAreas.ForEach(x => conectedAreas.Add(x));
        }
    }

}
