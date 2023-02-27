using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Creature;

namespace RPG.Task {
    public class TaskHandler {

        IController Controller;
        public TaskHandler(IController Controller) {
            this.Controller = Controller;
        }

        //Player pass time
        //Npc wait for time to pass

        //Tasks
        //Move
        //Talk
        //Give Item to Character
        //Take Item from Character

        //Force Task on to character?
        //Talk Task
            //Move Task
            //Get Path Queue<AbstractArea>
                //If move to character Listen to move to recalculate path
            //Move next -> Player - Pass time || NPC - Wait for time
            //Once Path is empty done.
        //Talk to player

        public void StartTask(ITask task) {

            Debug.Log("WOw a task! It was " + task.DisplayName);
            task.Perform(Controller);
        }

        public static IEnumerable<ITask> GetTasks(World.AbstractArea area, Creature.Character requestingCharacter) {

            //Get tasks from Area
            foreach (var task in area.GetTaskOptions(requestingCharacter)) {
                yield return task;
            }

            //Get Tasks from creatures
            foreach (var character in area.Characters) {
                foreach (var task in character.GetTaskOptions(requestingCharacter)) {
                    yield return task;
                }
            }

        }

    }
}
