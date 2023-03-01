using System.Collections.Generic;
using RPG.Creature;

namespace RPG.Task {
    public interface ITaskOptions {

        public string TaskPath { get; }
        public IEnumerable<ITask> GetTaskOptions(Character requestingCharacter);
    }
}