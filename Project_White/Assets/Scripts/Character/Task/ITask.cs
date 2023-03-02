using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Creature;

namespace RPG.Task {
    public interface ITask {

        public string TaskDescription { get; }
        public string DisplayName { get; }
        public int TaskDuration { get;}
        public bool isBusy { get; }
        public string Path { get; }

        public void Perform(IController controler);
        public bool FulfilledRequirements(Character requestingCharacter);
        public void Cancel();
        public UI.DisplayInfoButton GetDisplayInfo(Character requestingCharacter);
        public void AddToPath(string path);
    }
}