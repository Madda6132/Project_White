using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.World;
using RPG.Task;

namespace RPG.Creature {
    [RequireComponent(typeof(Character))]
    public class PlayerController : MonoBehaviour, IController, ITaskOptions {

        [SerializeField] AbstractArea testTarget;

        public Character Character { get; private set; }
        public string TaskPath => Character.CharacterName;


        public IEnumerable<ITask> GetTaskOptions(Character askingCharacter) {
            //task.AddPath(Character.Name);
            if(false) yield return null;

        }

        /*---Private---*/

        private void Awake() {
            Character = GetComponent<Character>();
        }

        //Commented out sections are mostly for testing
        #region Test

        Queue<TaskMove> _test = new();
        float _time = 0;

        private void Start() {
            _test = new(AreaHandler.GetPathFromTo(Character.Location, testTarget));
        }


        private void Update() {

            //Add Task to move to target

            _time += Time.deltaTime;
            if (2f < _time && 0 < _test.Count) {
                _test.Dequeue().Perform(this);
                _time -= 2f;
            }
        }

        #endregion
    }

}
