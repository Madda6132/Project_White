using System.Collections.Generic;
using UnityEngine;
using RPG.Creature;
using System.Linq;

namespace RPG.Core {
    public class GamePlayManager : MonoBehaviour {

        PlayerController _playerController;


        //Game state - Depending on the player
        //States
        //- Dialog 
        //  -Pause Auto time pass
        //  -Player visual focused on the character, dialog window, and potential choice buttons
        //  -Disable Player controls (Most of them)
        //  -
        //- Idle
        //- Busy
        //- Game Menu
        //  -Pause Auto time
        //  -Disable choice options
        //  -Disable Player controls
        //- GameOver?

        //Listen to
        //  - Player move/Character leave and enter to update choices
        //  - Display/Hide Character

        //Save and load game

        //Fix startup method
        // - Choices
        // - Player Visual
        //      - Background image
        // - TimeManager
        //      - To Pause and stuff
        // - Listeners for this
        //      -Player move

        /*---Private---*/
        private void Start() {
            _playerController = FindObjectOfType<PlayerController>();
            GameBroadcast.CharacterMoved.ListenToSpecific(UpdatePlayerOutput_OnMoved, _playerController.Character);
            GameBroadcast.AreaUpdate.ListenToSpecific(UpdatePlayerOutput_OnAreaUpdate, _playerController.Character.Location);
            UpdatePlayerOutput();
        }

        private void UpdatePlayerOutput_OnMoved(Character character, World.AbstractArea area) {
            GameBroadcast.AreaUpdate.IgnoreSpecific(UpdatePlayerOutput_OnAreaUpdate, area);
            GameBroadcast.AreaUpdate.ListenToSpecific(UpdatePlayerOutput_OnAreaUpdate, _playerController.Character.Location);
            UpdatePlayerOutput(); 
        }

        private void UpdatePlayerOutput_OnAreaUpdate(World.AbstractArea area) {
            UpdatePlayerOutput();
        }
        
        private void UpdatePlayerOutput() {
            UpdatePlayerVisuals();
            UpdatePlayerChoices();
        }
        private void UpdatePlayerVisuals() {
            //Background
            PlayerVisuals.UpdatePlayerVisuals(_playerController.Character);
        }

        private void UpdatePlayerChoices() {
            List<Task.ITask> tasks = new(Task.TaskHandler.GetTasks(_playerController.Character.Location, _playerController.Character));
            PlayerVisuals.DisplayChoices(_playerController, tasks.Select(x => x.GetDisplayInfo(_playerController.Character)));
        }

    }
}
