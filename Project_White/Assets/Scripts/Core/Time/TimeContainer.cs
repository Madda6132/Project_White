
namespace RPG.Core.Time {
    public readonly struct TimeContainer {

        readonly int _minutes;
        readonly int _hours;
        readonly int _minutesPast;
        
        public TimeContainer(int hours, int minutes, int minutesPast) {
            this._hours = hours;
            this._minutes = minutes;
            this._minutesPast = minutesPast;
        }

        public int TimePast => _minutesPast;

        public string Time => $"{_hours:00}:{_minutes:00}";

        public bool PastTime(int hours, int minutes) => (minutes + hours * 60) < (_minutes + _hours * 60);
    }

}
