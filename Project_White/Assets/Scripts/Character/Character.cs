using System.Collections.Generic;
using UnityEngine;
using RPG.World;
using RPG.Task;


namespace RPG.Creature {
    public class Character : MonoBehaviour, ITaskOptions {

        [SerializeField] string characterName;
        [SerializeField] CharacterDataContainer dataContainer;

        public AbstractArea Location => FindParentArea();
        public string CharacterName { 
            get => characterName; 
            private set => characterName = value; }
        public string TaskPath => "Character";
        public TaskHandler TaskHandler { 
            get; 
            private set; 
        }


        //Home
        //Inventory
        //Stats -> Skills
        //Job - Shop or other
        //

        public IEnumerable<ITask> GetTaskOptions(Character requestingCharacter) {
            List<ITaskOptions> otherTaskOptions = new(GetComponents<ITaskOptions>());
            otherTaskOptions.Add(dataContainer);
            otherTaskOptions.Remove(this);
            foreach (var taskOption in otherTaskOptions) {
                foreach (var task in taskOption.GetTaskOptions(requestingCharacter)) {
                    task.AddToPath(TaskPath);
                    yield return task;
                }
            }
            //A character can choose these options
            //Branch out more options if there are a lot
            //Character
            //          -Talk
            //          -Shop
        }

        /*---Private---*/

        private void Awake() {
            TaskHandler = new(GetComponent<IController>());
        }

        private AbstractArea FindParentArea() {
            Transform parentTransform = transform.parent;
            AbstractArea parentArea = null;
            while (parentTransform != null) {
                if (parentTransform.TryGetComponent(out AbstractArea foundArea)) {
                    parentArea = foundArea;
                    break;
                }
                parentTransform = parentTransform.parent;
            }
            return parentArea;
        }
    }
}
