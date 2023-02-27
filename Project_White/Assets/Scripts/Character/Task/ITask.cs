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

        /// <summary>
        /// Check if character can get this task as an option
        /// </summary>
        /// <param name="requestingCharacter">  </param>
        /// <returns> True = Allow </returns>
        public bool Requirements(Character requestingCharacter);
        public void Cancel();

        public UI.DisplayInfo_Button GetDisplayInfo(Character requestingCharacter);

        public void AddToPath(string path);
    }
}