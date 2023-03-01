using RPG.Creature;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI.Library.Buttons;
using RPG.UI;

namespace RPG.Task {
    public abstract class AbstractTaskDirect : ITask {

        public abstract string TaskDescription { get; }
        public virtual int TaskDuration { get { 
                return _DurationMinutes + (_DurationHours * 60); 
            } 
        }

        public abstract string DisplayName { get; }

        public abstract bool isBusy { get; }
        public string Path { get; private set; } = "";


        protected ButtonType _ButtonType = ButtonType.DEFAULT;
        protected UI.UIButtonManager _UIButtonManager = null;

        int _DurationMinutes = 5;
        int _DurationHours = 0;

        protected AbstractTaskDirect(int _DurationMinutes, ButtonType buttonType = ButtonType.DEFAULT) {

            this._DurationMinutes = _DurationMinutes % 60;
            this._DurationHours = _DurationMinutes / 60;
            _ButtonType = buttonType;
        }

        protected AbstractTaskDirect(UIButtonManager uIButtonManager, int _DurationMinutes) {

            this._DurationMinutes = _DurationMinutes % 60;
            this._DurationHours = _DurationMinutes / 60; 
            _UIButtonManager = uIButtonManager;
        }

        public abstract void Cancel();

        public abstract void Perform(IController controler);

        public abstract bool FulfilledRequirements(Character requestingCharacter);

        public void AddToPath(string path) => Path = $"{path}/{Path}";

        public DisplayInfo_Button GetDisplayInfo(Character requestingCharacter) {

            if (_UIButtonManager != null) {

                return new DisplayInfo_Button(DisplayName, Path, TaskDescription, FulfilledRequirements(requestingCharacter), () => requestingCharacter.TaskHandler.StartTask(this), _UIButtonManager);
            } else {

                return new DisplayInfo_Button(DisplayName, Path, TaskDescription, FulfilledRequirements(requestingCharacter), () => requestingCharacter.TaskHandler.StartTask(this), _ButtonType);
            }

        }
    }

}
