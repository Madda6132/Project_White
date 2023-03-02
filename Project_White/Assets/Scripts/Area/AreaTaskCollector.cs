using System.Collections.Generic;
using RPG.Task;
using RPG.Creature;

namespace RPG.World.Tasks {
    public class AreaTaskCollector : ITaskOptions{

        public string TaskPath { get; }

        readonly AbstractArea _area;

        public AreaTaskCollector(AbstractArea area, string path) {
            this._area = area;
            this.TaskPath = path;
        }

        public IEnumerable<ITask> GetTaskOptions(Character requestingCharacter) {
            foreach (var connection in _area.AreaConnector.AreaConnections) {
                yield return Task.Actions.TaskActionLibrary.TravelTask(_area, connection);
            }

            foreach (var taskOptions in _area.GetComponentsInInteract<ITaskOptions>()) {
                foreach (var task in taskOptions.GetTaskOptions(requestingCharacter)) {
                    yield return task;
                }
            }
        }
    }
}
