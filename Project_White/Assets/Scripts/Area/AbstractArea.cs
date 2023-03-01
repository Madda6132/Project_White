using System.Collections.Generic;
using UnityEngine;
using RPG.Creature;
using RPG.Task;

namespace RPG.World {
    
    public abstract class AbstractArea : MonoBehaviour, ITaskOptions {

        [SerializeField] protected Transform AreasContainer;
        [SerializeField] protected Transform InteractContainer;
        [SerializeField] string _AreaName = "";
        [SerializeField] Sprite _Background;

        public string TaskPath => _AreaName;
        public string AreaName {
            get => _AreaName;
            protected set => _AreaName = value; }
        public Sprite Background {
            get => _Background;
            protected set => _Background = value;
        }
        public float TravelMultiplier {
            get => _TravelMultiplier;
            protected set => _TravelMultiplier = value;
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
        public AbstractArea ParentArea => _ParentArea ?? FindAndSetParentArea();
        public AbstractArea[] AreaConnections => conectedAreas.ToArray();
        
        protected float _TravelMultiplier = 1f;

        List<AbstractArea> conectedAreas = new();
        AbstractArea _ParentArea;
        Tasks.AreaTaskCollector _TaskCollector;

        //Get tags. Such as public, Forest, Mountain...

        public virtual IEnumerable<ITask> GetTaskOptions(Character requestingCharacter) {
            foreach (var task in _TaskCollector.GetTaskOptions(requestingCharacter)) {
                yield return task;
            }
        }

        public TComponent[] GetComponentsInInteract<TComponent>() where TComponent : class {
            return InteractContainer.GetComponentsInChildren<TComponent>();
        }

        public int TravelTimeToArea(AbstractArea toArea) {
            return (int)_TravelMultiplier;
        }

        public void AddObjectToInteract(Transform objectTransform) {
            objectTransform.transform.SetParent(InteractContainer);
            Core.GameBroadcast.AreaUpdate.Broadcast(this);
        }

        public void AddAreaConnection(AbstractArea area) {
            conectedAreas.Add(area);
        }

        /*---Protected---*/

        /*---Private---*/

        protected virtual void Awake() {
            _TaskCollector = new(this, TaskPath);
        }

        private AbstractArea FindAndSetParentArea() {
            Transform parentTransform = transform.parent;
            while (parentTransform != null) {
                if (parentTransform.TryGetComponent(out AbstractArea foundArea)) {
                    _ParentArea = foundArea;
                    break;
                }
                parentTransform = parentTransform.parent;
            }
            return _ParentArea;
        }
    }
}
