using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.World {
    public class AbstractExploreArea : AbstractArea {

        [SerializeField] List<AbstractArea> _RoomConnectedAreas = new();

        /*---Protected---*/
        protected override void Awake() {
            base.Awake();
            _RoomConnectedAreas.ForEach(x => AddAreaConnection(x));
        }
    }

}
