using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.World;
using RPG.Creature;

namespace RPG.Task.Actions {
    public static class TaskActionLibrary {

        //Contains action blueprints that ITask can just reference instead of making them

        public static TaskMove TravelTask(AbstractArea fromArea, AbstractArea Toarea) => 
            new TaskMove(Toarea);

    }
}

