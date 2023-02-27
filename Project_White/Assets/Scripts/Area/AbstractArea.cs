using System.Collections.Generic;
using UnityEngine;
using RPG.Creature;
using RPG.Task;

namespace RPG.World {
    
    public abstract class AbstractArea : MonoBehaviour, ITaskOptions {

        [SerializeField] string _AreaName = "";
        [SerializeField] Sprite _Background;

        public string AreaName {
            get => _AreaName;
            protected set => _AreaName = value; }
        public Sprite Background {
            get => _Background;
            protected set => _Background = value;
        }
        public float AreaScale {
            get => _AreaScale;
            protected set => _AreaScale = value;
        }
        public IEnumerable<AbstractArea> WorldLocation {
            get {
                AbstractArea parentArea = ParentArea;
                if (parentArea) {
                    foreach (var area in parentArea.WorldLocation) {
                        yield return area;
                    }
                }

                yield return this;
                }
        }
        public AbstractArea ParentArea {
            get {
                Transform parent = transform.parent;
                if (parent != null && parent.TryGetComponent(out AbstractArea area)) {
                    return area;
                }

                return null;
            }
        }
        public AbstractArea[] AreaConnections => conectedAreas.ToArray();
        public IEnumerable<Character> Characters => characters;

        public string TaskPath => _AreaName;

        protected float _AreaScale = 1f;
        protected List<AbstractArea> conectedAreas = new();
        protected List<Character> characters = new();

        //List<InteractEvents> interactEvents = new();

        public virtual IEnumerable<ITask> GetTaskOptions(Character requestingCharacter) {


            foreach (var connection in AreaConnections) {
                yield return Task.Actions.TaskActionLibrary.TravelTask(this, connection);
            }

            foreach (var character in characters) {
                foreach (var task in character.GetTaskOptions(requestingCharacter)) {
                    yield return task;
                }
            }
        }



        public int TravelTimeToArea(AbstractArea toArea) {
            return (int)_AreaScale;
        }



        public TaskMove CreateMoveTask(AbstractArea area) { 
            
            if(!conectedAreas.Contains(area)) return null;

            return new TaskMove(area, (int)_AreaScale); 
        }

        public virtual bool isAreaConnected(AbstractArea moveToArea) => conectedAreas.Contains(moveToArea);



        public void CharacterEntered(Character character) {

            if (!characters.Contains(character)) {
                
                characters.Add(character);
                Core.GameBroadcast.CharacterMoved.ListenToSpecific(CharacterLeft, character);
                Core.GameBroadcast.AreaUpdate.Broadcast(this);
            }
        
        }

        //Characters actions
        //Move Character here
        //Get Interactions
        //Get tags
        //Travel Time Multiplier

        //Areas
        //Connecting other Areas?

        /*---Protected---*/

        protected void CharacterLeft(Character character) { 

            if (character.Location != this && characters.Contains(character)) { 
                characters.Remove(character);
                Core.GameBroadcast.CharacterMoved.IgnoreSpecific(CharacterLeft, character);
            }
        }


        /*---Private---*/

        protected virtual void Awake() {

            //Get characters
            Character[] character = GetComponentsInChildren<Character>();
            foreach (var person in character) {

                characters.Add(person);

                //Listen to characters
                Core.GameBroadcast.CharacterMoved.ListenToSpecific(CharacterLeft, person);
            }

        }

    }

}
