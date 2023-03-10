using RPG.Creature;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.World;
using RPG.UI.Library.Buttons;
using RPG.Core;

namespace RPG.Task {
    public class TaskMove : AbstractTaskDirect {

        public override string DisplayName => _Discription + moveToArea.AreaName;
        public override bool isBusy => false;
        public override string TaskDescription => "";


        string _Discription = "Travel to ";

        AbstractArea moveToArea;



        public TaskMove(AbstractArea moveToArea, UI.UIButtonManager uIButtonManager, int _DurationMinutes = 1) :
            base(uIButtonManager, _DurationMinutes) {

            this.moveToArea = moveToArea;
        }



        public TaskMove(AbstractArea ToArea, int _DurationMinutes = 1, ButtonType buttonType = ButtonType.TRAVEL) :
            base(_DurationMinutes, buttonType) {


                this.moveToArea = ToArea;
            }



        public override void Cancel() {
            throw new System.NotImplementedException();
        }



        public override void Perform(IController controller) {
            AbstractArea currentLocation = controller.Character.Location;
            moveToArea.AddObjectToInteract(controller.Character.transform);
            GameBroadcast.CharacterMoved.Broadcast(controller.Character, currentLocation);
        }



        public override bool FulfilledRequirements(Character requestingCharacter) => true;
    }
}
