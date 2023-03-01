using RPG.Creature;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI.Library.Buttons;

namespace RPG.Task {

    public class TaskSleep : AbstractTaskDirect {



        public TaskSleep(UI.UIButtonManager uIButtonManager, int _DurationMinutes = 480) :
            base(uIButtonManager, _DurationMinutes) {

        }

        public TaskSleep(int _DurationMinutes = 480, ButtonType buttonType = ButtonType.DEFAULT) :
            base(_DurationMinutes, buttonType) {

        }

        public override string DisplayName => "Sleep";

        public override bool isBusy => true;

        public override string TaskDescription => "Sleep and rest";

        public override void Cancel() {
           
        }

        public override void Perform(IController controler) {
            Debug.Log("ZZZzz...");
        }

        public override bool FulfilledRequirements(Character requestingCharacter) => true;
    }
}
