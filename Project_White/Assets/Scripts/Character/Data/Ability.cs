using RPG.Creature;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Task {
    [CreateAssetMenu(fileName = "Ability data", menuName = "RPG/Data/Ability", order = 0)]
    public class Ability : ScriptableObject, ITaskOptions {

        [SerializeField] List<Ability> nextAbility;
        [SerializeField] string taskPath = "";
        [SerializeField] int sleepCount = 1;
        public string TaskPath => taskPath;

        public IEnumerable<ITask> GetTaskOptions(Character requestingCharacter) {

            for (int i = 0; i < sleepCount; i++) { 
                ITask task = new TaskSleep();
                task.AddToPath(taskPath);
                yield return task;
            }
            foreach (var ability in nextAbility) { 
                foreach (var task in ability.GetTaskOptions(requestingCharacter)) {
                    task.AddToPath(taskPath);
                    yield return task;
                }
            }
        }
    }
}
