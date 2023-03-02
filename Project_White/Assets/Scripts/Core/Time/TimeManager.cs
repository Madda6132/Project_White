using UnityEngine;

namespace RPG.Core {
    public class TimeManager : MonoBehaviour {
        private static TimeManager Instance {
            get {
                if (_instance == null) { Debug.LogError("TimeManager Instance is null. Instantiate an TimeManager. TimeManager should be in Core prefab"); }
                return _instance;
            }
        }

        static TimeManager _instance;

        int _timeHours = 10;
        int _timeMinutes = 30;
        float _secondCounter = 0f;

        private void PassMinutes(int min) {
            _timeMinutes += min;
            if(59 < _timeMinutes) {
                _timeMinutes = 0;
                PassHours(1);
            }
            GameBroadcast.TimeChanged.Broadcast(new(_timeHours, _timeMinutes, min));
        }

        private void PassHours(int hour) {
            _timeHours += hour;
            _timeHours %= 24;
        }

        /*---Static---*/
        public static Time.TimeContainer GetTime => Instance ? new(Instance._timeHours, Instance._timeMinutes, 0) : new(12, 0, 0);

        public static void PassTime(int min) => Instance.PassMinutes(min);

        /*---Private---*/

        private void Awake() {
            if (_instance != null) {
                Debug.Log("Only one TimeManager instance can exist. Please remove the duplicate TimeManagers");
                Destroy(gameObject);
                return;
            }
            _instance = this;
        }

        private void Update() {
            _secondCounter += UnityEngine.Time.deltaTime;
            if (59 < _secondCounter) {
                _secondCounter = 0;
                PassMinutes(1);
            }
        }
    }
}
