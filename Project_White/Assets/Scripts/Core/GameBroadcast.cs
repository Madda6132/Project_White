using UnityEngine;
using RPG.Utilities;

namespace RPG.Core {
    public class GameBroadcast : MonoBehaviour {

        BroadcastMessage<Creature.Character, World.AbstractArea> _characterMoved;
        BroadcastMessage<World.AbstractArea> _areaUpdate;
        BroadcastMessage<Time.TimeContainer> _timeChanged;

        private static GameBroadcast Instance {
            get {
                if (_instance == null) { 
                    Debug.LogError("GameBroadcast Instance is null. Instantiate an GameBroadcast. GameBroadcast should be in Core prefab"); 
                }
                return _instance;
            }
        }

        static GameBroadcast _instance;

        /// <summary>
        /// Broadcast when a character has moved from an area
        /// </summary>
        public static BroadcastMessage<Creature.Character, World.AbstractArea> CharacterMoved => Instance._characterMoved;
        /// <summary>
        /// Broadcast when a area changes. Such as character enters or leaves
        /// </summary>
        public static BroadcastMessage<World.AbstractArea> AreaUpdate => Instance._areaUpdate;
        /// <summary>
        /// Broadcast when the time changes. (Hours, Minutes, Minutes past)
        /// </summary>
        public static BroadcastMessage<Time.TimeContainer> TimeChanged => Instance._timeChanged;

        /*---Private---*/

        private void Awake() {

            if (_instance != null) {
                Destroy(this);
                Debug.LogError($"Cant have more than one instance of GameBroadcast");
                return;
            }
            _instance = this;
            _characterMoved = new();
            _areaUpdate = new();
            _timeChanged = new();
        }
    }
}
