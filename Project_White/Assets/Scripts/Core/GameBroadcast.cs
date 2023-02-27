using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Utilitys;

namespace RPG.Core {
    public class GameBroadcast : MonoBehaviour {
        //public static GameBroadcast PlayerVisualsInstance {
        //    get {
        //        if (_Instance == null) { Debug.LogError("PlayerVisuals Instance is null. Instantiate an PlayerVisuals. PlayerVisuals should be in Core prefab"); }
        //        return _Instance;
        //    }
        //    private set {
        //        _Instance = value;
        //    }
        //}

        //static GameBroadcast _Instance;

        /// <summary>
        /// Broadcast when a character moves area
        /// </summary>
        public static BroadcastMessage<Creature.Character> CharacterMoved { get; } = new();
        /// <summary>
        /// Broadcast when a area changes. Such as character enters or leaves
        /// </summary>
        public static BroadcastMessage<World.AbstractArea> AreaUpdate { get; } = new();
        /// <summary>
        /// Broadcast when the time changes. (Hours, Minutes, Minutes past)
        /// </summary>
        public static BroadcastMessage<int, int, int> TimeChanged { get; } = new();





        /*---Private---*/

        //private void Awake() {

        //    if (_Instance != null) {
        //        Destroy(this);
        //        Debug.LogError($"Cant have more than one instance of GameBroadcast");
        //        return;
        //    }
        //}

    }


}
