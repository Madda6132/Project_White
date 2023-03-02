using System.Collections.Generic;
using UnityEngine;
using RPG.Creature;
using RPG.Task;

namespace RPG.World {
    
    public abstract class AbstractArea : MonoBehaviour, ITaskOptions {

        [SerializeField] protected Transform areasContainer;
        [SerializeField] protected Transform interactContainer;
        [SerializeField] Connection.AreaConnector areaConnector;
        [SerializeField] string areaName = "";
        [SerializeField] Sprite background;

        public string TaskPath => areaName;
        public string AreaName {
            get => areaName;
            protected set => areaName = value; }
        public Sprite Background {
            get => background;
            protected set => background = value;
        }
        public float TravelMultiplier {
            get => TravelMultiplierValue;
            protected set => TravelMultiplierValue = value;
        } 
        public IEnumerable<AbstractArea> WorldLocation {
            get {
                if (ParentArea) {
                    foreach (var area in ParentArea.WorldLocation) {
                        yield return area;
                    }
                }

                yield return this;
                }
        }
        public AbstractArea ParentArea => _parentAreaValue ? _parentAreaValue : FindAndSetParentArea();
        public Connection.AreaConnector AreaConnector => areaConnector;
        
        protected float TravelMultiplierValue = 1f;

        AbstractArea _parentAreaValue;
        Tasks.AreaTaskCollector _taskCollector;

        //Get tags. Such as public, Forest, Mountain...

        public virtual IEnumerable<ITask> GetTaskOptions(Character requestingCharacter) {
            foreach (var task in _taskCollector.GetTaskOptions(requestingCharacter)) {
                yield return task;
            }
        }

        public TComponent[] GetComponentsInInteract<TComponent>() where TComponent : class {
            return interactContainer.GetComponentsInChildren<TComponent>();
        }

        public int TravelTimeToArea(AbstractArea toArea) {
            return (int)TravelMultiplierValue;
        }

        public void AddObjectToInteract(Transform objectTransform) {
            objectTransform.transform.SetParent(interactContainer);
            Core.GameBroadcast.AreaUpdate.Broadcast(this);
        }


        /*---Protected---*/

        protected virtual void Awake() {
            _taskCollector = new(this, TaskPath);
            AreaConnector.Awake(this);
        }
        
        /*---Private---*/


        private AbstractArea FindAndSetParentArea() {
            Transform parentTransform = transform.parent;
            while (parentTransform != null) {
                if (parentTransform.TryGetComponent(out AbstractArea foundArea)) {
                    _parentAreaValue = foundArea;
                    break;
                }
                parentTransform = parentTransform.parent;
            }
            return _parentAreaValue;
        }
    }
}
