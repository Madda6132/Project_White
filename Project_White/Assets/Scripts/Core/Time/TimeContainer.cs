using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core.Time {
    public struct TimeContainer {

        int _Minutes;
        int _Hours;
        int _MinutesPast;
        public TimeContainer(int Hours, int Minutes, int MinutesPast) {
            this._Hours = Hours;
            this._Minutes = Minutes;
            this._MinutesPast = MinutesPast;
        }

        public int TimePast => _MinutesPast;

        public string Time => string.Format("{0:00}:{1:00}", _Hours, _Minutes);

        public bool PastTime(int Hours, int Minutes) => (Minutes + Hours * 60) < (_Minutes + _Hours * 60);
    }

}
