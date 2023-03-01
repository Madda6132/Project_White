using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Utilitys;

namespace RPG.Core {
    public class GameBroadcast : MonoBehaviour {

        BroadcastMessage<Creature.Character, World.AbstractArea> _CharacterMoved;
        BroadcastMessage<World.AbstractArea> _AreaUpdate;
        BroadcastMessage<Time.TimeContainer> _TimeChanged;

        public static GameBroadcast Instance {
            get {
                if (_Instance == null) { 
                    Debug.LogError("GameBroadcast Instance is null. Instantiate an GameBroadcast. GameBroadcast should be in Core prefab"); 
                }
                return _Instance;
            }
            private set {
                _Instance = value;
            }
        }

        static GameBroadcast _Instance;

        /// <summary>
        /// Broadcast when a character has moved from an area
        /// </summary>
        public static BroadcastMessage<Creature.Character, World.AbstractArea> CharacterMoved => Instance._CharacterMoved;
        /// <summary>
        /// Broadcast when a area changes. Such as character enters or leaves
        /// </summary>
        public static BroadcastMessage<World.AbstractArea> AreaUpdate => Instance._AreaUpdate;
        /// <summary>
        /// Broadcast when the time changes. (Hours, Minutes, Minutes past)
        /// </summary>
        public static BroadcastMessage<Time.TimeContainer> TimeChanged => Instance._TimeChanged;

        /*---Private---*/

        private void Awake() {

            if (_Instance != null) {
                Destroy(this);
                Debug.LogError($"Cant have more than one instance of GameBroadcast");
                return;
            }
            _Instance = this;
            _CharacterMoved = new();
            _AreaUpdate = new();
            _TimeChanged = new();
        }
    }
}
