using RPG.Creature;
using RPG.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI.Library.Buttons;

namespace RPG.Task {
    public abstract class AbstractTaskProgression : ITask {

        public abstract string TaskDescription { get; }
        public int TaskDuration => _DurationMinutes + (_DurationHours * 60);

        public abstract string DisplayName { get; } 

        public abstract bool isBusy { get; }

        //Allow set for methods to categorize it
        public string Path { get; set; } = "";

        protected ButtonType _ButtonType = ButtonType.DEFAULT;
        protected UIButtonManager _UIButtonManager = null;

        int _DurationMinutes = 5;
        int _DurationHours = 0;

        int _Progression = 0;

        public AbstractTaskProgression(int _DurationMinutes, ButtonType buttonType = ButtonType.DEFAULT) {

            this._DurationMinutes = _DurationMinutes % 60;
            this._DurationHours = _DurationMinutes / 60;
            this._ButtonType = buttonType;
        }

        public AbstractTaskProgression(UIButtonManager uIButtonManager, int _DurationMinutes) {

            this._UIButtonManager = uIButtonManager;
            this._DurationMinutes = _DurationMinutes % 60;
            this._DurationHours = _DurationMinutes / 60;
        }


        public abstract void Cancel();

        public abstract void Perform(IController controler);

        public abstract bool FulfilledRequirements(Character requestingCharacter);

        public void AddToPath(string path) => Path = $"{path}/{Path}";

        public DisplayInfo_Button GetDisplayInfo(Character requestingCharacter) {

            if(_UIButtonManager != null) {

                return new DisplayInfo_Button(DisplayName, Path, TaskDescription, FulfilledRequirements(requestingCharacter), () => requestingCharacter.TaskHandler.StartTask(this), _UIButtonManager);
            } else {

                return new DisplayInfo_Button(DisplayName, Path, TaskDescription, FulfilledRequirements(requestingCharacter), () => requestingCharacter.TaskHandler.StartTask(this), _ButtonType);
            }

        }

        /*---Private---*/

    }

}
