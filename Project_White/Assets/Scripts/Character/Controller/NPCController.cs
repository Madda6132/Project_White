using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.World;

namespace RPG.Creature {
    [RequireComponent(typeof(Character))]
    public class NPCController : MonoBehaviour, IController {

        public Character Character { get; private set; }

        //Memory of Characters
        Dictionary<Character, AbstractArea> memoryOfCharacters = new();


        public void MoveToArea(AbstractArea area) {
            throw new System.NotImplementedException();
        }

        //Relationship Meter (Player)?
        //  Stranger / Acquaintance / Friends / Close Friends / Couple / Married?

        //Daily Tasks
        //Talk to player -> Task to Seek out and speak to player?

        //Personality
        //  Extroverted -Mind-      Introverted
        //  Intuitive   -Energy-    Observant
        //  Thinking    -Nature-    Feeling
        //  Judging     -Tactics-   Prospecting
        //  Assertive   -Identity-  Turbulent



        /*---Private---*/

        private void Awake() {

            Character = GetComponent<Character>();
        }

    }
}
