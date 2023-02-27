using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Task;

namespace RPG.Creature {
    [CreateAssetMenu(fileName = "Character data Container", menuName = "RPG/Data/Character", order =0)]
    public class CharacterDataContainer : ScriptableObject, ITaskOptions {

        public Ability ability;

        public string TaskPath => "Container";

        public IEnumerable<ITask> GetTaskOptions(Character askingCharacter) {

            foreach (var task in ability.GetTaskOptions(askingCharacter)) {
                task.AddToPath(TaskPath);
                yield return task;
            }
        }
    }

}
