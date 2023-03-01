using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG.Core {
    public class TimeManager : MonoBehaviour {

        public static TimeManager Instance {
            get {
                if (_Instance == null) { Debug.LogError("TimeManager Instance is null. Instantiate an TimeManager. TimeManager should be in Core prefab"); }
                return _Instance;
            }
            private set {
                _Instance = value;
            }
        }

        static TimeManager _Instance;

        int Time_Hours = 10;
        int Time_Minutes = 30;
        float _SeconedCounter = 0f;

        private void PassMinutes(int min) {
            Time_Minutes += min;
            if(59 < Time_Minutes) {
                Time_Minutes = 0;
                PassHours(1);
            }
            GameBroadcast.TimeChanged.Broadcast(new(Time_Hours, Time_Minutes, min));
        }

        private void PassHours(int hour) {
            Time_Hours += hour;
            if (23 < Time_Hours) Time_Hours = Time_Hours % 24;
        }

        /*---Static---*/
        public static Time.TimeContainer GetTime => Instance ? new(Instance.Time_Hours, Instance.Time_Minutes, 0) : new(12, 0, 0);

        public static void PassTime(int min) => Instance.PassMinutes(min);

        /*---Private---*/

        private void Awake() {
            if (_Instance != null) {
                Debug.Log("Only one TimeManager instance can exist. Please remove the duplicate TimeManagers");
                Destroy(gameObject);
                return;
            }
            _Instance = this;
        }

        private void Update() {
            _SeconedCounter += UnityEngine.Time.deltaTime;
            if (60 < _SeconedCounter) {
                _SeconedCounter -= 60;
                PassMinutes(1);
            }
        }
    }
}
