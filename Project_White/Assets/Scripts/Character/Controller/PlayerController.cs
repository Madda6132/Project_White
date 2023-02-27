using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.World;
using RPG.Task;

namespace RPG.Creature {
    [RequireComponent(typeof(Character))]
    public class PlayerController : MonoBehaviour, IController, ITaskOptions {

        public Character Character { get; private set; }

        public string TaskPath => Character.Name;

        [SerializeField] AbstractArea testTarget;


        public void MoveToArea(AbstractArea area) {

            if(!Character.Location.isAreaConnected(area)) return;
            Character.MoveCharacter(area);

        }


        
        public IEnumerable<ITask> GetTaskOptions(Character askingCharacter) {

            
            //task.AddPath(Character.Name);
            if(false) yield return null;

        }

        /*---Private---*/



        private void Awake() {

            Character = GetComponent<Character>();
            
        }

        //Commented out sections are mostly for testing

        Queue<TaskMove> test = new();
        float _Time = 0;

        private void Start() {
            test = new(AreaHandler.GetPathFromTo(Character.Location, testTarget));
        }


        private void Update() {

            //Add Task to move to target

            _Time += Time.deltaTime;
            if (2f < _Time && 0 < test.Count) {
                test.Dequeue().Perform(this);
                _Time -= 2f;
            }
        }

    }

}
