using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.World;

namespace RPG.Creature {
    public interface IController {

        public Character Character { get; }

        public void MoveToArea(AbstractArea area);

    }

}
