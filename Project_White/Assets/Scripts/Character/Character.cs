using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.World;
using RPG.Core;
using RPG.Task;
using System;


namespace RPG.Creature {
    public class Character : MonoBehaviour, ITaskOptions {

        /// <summary>
        /// Get the area the character is standing in
        /// </summary>
        public AbstractArea Location {
            get {
                Transform parent = transform.parent;
                if (parent != null && parent.TryGetComponent(out AbstractArea area)) {
                    return area;
                }

                return null;
            }
        }
        public string Name { get => _Name; private set => _Name = value; }

        public string TaskPath => "Character";


        [SerializeField] string _Name;
        [SerializeField] CharacterDataContainer dataContainer;

        public TaskHandler TaskHandler { get; private set; }


        //Home
        //Inventory
        //Stats -> Skills
        //Job - Shop or other
        //

        public void MoveCharacter(AbstractArea area) {

            transform.SetParent(area.transform);
            area.CharacterEntered(this);
            GameBroadcast.CharacterMoved.Broadcast(this);
        }

        public IEnumerable<ITask> GetTaskOptions(Character requestingCharacter) {

            //A character can choose these options
            //Branch out more options if there are a lot
            //Character
            //          -Talk
            //          -Shop

            List<ITaskOptions> otherTaskOptions = new(GetComponents<ITaskOptions>());
            otherTaskOptions.Add(dataContainer);
            otherTaskOptions.Remove(this);

            foreach (var taskOption in otherTaskOptions) {
                foreach (var task in taskOption.GetTaskOptions(requestingCharacter)) {

                    task.AddToPath(TaskPath);
                    yield return task;
                }
            }
        }

        /*---Private---*/

        private void Awake() {

            TaskHandler = new(GetComponent<IController>());
        }
    }

}
