using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Task;
using RPG.Creature;

namespace RPG.World.Tasks {
    public class AreaTaskCollector : ITaskOptions{

        public string TaskPath => _Path;

        string _Path;
        AbstractArea _Area;

        public AreaTaskCollector(AbstractArea area, string path) { 
        
            this._Area = area;
            this._Path = path;
        }

        public IEnumerable<ITask> GetTaskOptions(Character requestingCharacter) {

            foreach (var connection in _Area.AreaConnections) {

                yield return Task.Actions.TaskActionLibrary.TravelTask(_Area, connection);
            }

            foreach (var taskOptions in _Area.GetComponentsInInteract<ITaskOptions>()) {

                foreach (var task in taskOptions.GetTaskOptions(requestingCharacter)) {

                    yield return task;
                }
            }
        }
    }
}
