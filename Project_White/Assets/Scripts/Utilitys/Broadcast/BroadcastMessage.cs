using System.Collections.Generic;
using System;

namespace RPG.Utilities {
    public class BroadcastMessage {
        readonly Dictionary<string, List<Action>> _broadcastSpecific = new();
        readonly List<Action> _broadcastAny = new();

        public void Broadcast(string signal) {
            List<Action> list;
            if (_broadcastSpecific.ContainsKey(signal)) {
                list = new(_broadcastSpecific[signal]);
                list.ForEach(x => x.Invoke());
            }
            list = new(_broadcastAny);
            list.ForEach(x => x.Invoke());
        }

        /*---Any---*/
        /// <summary>
        /// Listen to a any message
        /// </summary>
        /// <param name="listener"> The action to call </param>
        public void ListenToAny(Action listener) {
            if (!_broadcastAny.Contains(listener)) 
                _broadcastAny.Add(listener); 
        }
        /// <summary>
        /// Ignore any messages
        /// </summary>
        /// <param name="listener"> The action to ignore </param>
        public void IgnoreAny(Action listener) {
            if(_broadcastAny.Contains(listener)) 
                _broadcastAny.Add(listener); 
        }

        /*---Specific---*/
        /// <summary>
        /// Listen to a specific message
        /// </summary>
        /// <param name="listener"> The action to call </param>
        /// <param name="signal"> Specific message </param>
        public void ListenToSpecific(Action listener, string signal) {
            if (!_broadcastSpecific.ContainsKey(signal)) {
                _broadcastSpecific.Add(signal, new());
            }
            if (!_broadcastSpecific[signal].Contains(listener)) { 
                _broadcastSpecific[signal].Add(listener);
            }
        }
        /// <summary>
        /// Ignore the specific message
        /// </summary>
        /// <param name="listener"> The action to ignore </param>
        /// <param name="signal"> Specific message </param>
        public void IgnoreSpecific(Action listener, string signal) {
            if (!_broadcastSpecific.ContainsKey(signal)) return;
            if (_broadcastSpecific[signal].Contains(listener)) {
                _broadcastSpecific[signal].Remove(listener);
                if(_broadcastSpecific[signal].Count == 0) {
                    _broadcastSpecific.Remove(signal);
                }
            }
        }
    }

    public class BroadcastMessage<T> {

        readonly Dictionary<T, List<Action<T>>> _broadcastSpecific = new();
        readonly List<Action<T>> _broadcastAny = new();

        public void Broadcast(T signal) {
            List<Action<T>> list;
            if (_broadcastSpecific.ContainsKey(signal)) {
                list = new(_broadcastSpecific[signal]);
                list.ForEach(x => x.Invoke(signal));
            }
            list = new(_broadcastAny);
            list.ForEach(x => x.Invoke(signal));
        }

        /*---Any---*/
        /// <summary>
        /// Listen to a any message
        /// </summary>
        /// <param name="listener"> The action to call </param>
        public void ListenToAny(Action<T> listener) {
            if (!_broadcastAny.Contains(listener))
                _broadcastAny.Add(listener);
        }
        /// <summary>
        /// Ignore any messages
        /// </summary>
        /// <param name="listener"> The action to ignore </param>
        public void IgnoreAny(Action<T> listener) {
            if (_broadcastAny.Contains(listener))
                _broadcastAny.Add(listener);
        }

        /*---Specific---*/
        /// <summary>
        /// Listen to a specific message
        /// </summary>
        /// <param name="listener"> The action to call </param>
        /// <param name="signal"> Specific message </param>
        public void ListenToSpecific(Action<T> listener, T signal) {
            if (!_broadcastSpecific.ContainsKey(signal)) {
                _broadcastSpecific.Add(signal, new());
            }
            if (!_broadcastSpecific[signal].Contains(listener)) {
                _broadcastSpecific[signal].Add(listener);
            }
        }
        /// <summary>
        /// Ignore the specific message
        /// </summary>
        /// <param name="listener"> The action to ignore </param>
        /// <param name="signal"> Specific message </param>
        public void IgnoreSpecific(Action<T> listener, T signal) {
            if (!_broadcastSpecific.ContainsKey(signal)) return;
            if (_broadcastSpecific[signal].Contains(listener)) {
                _broadcastSpecific[signal].Remove(listener);
                if (_broadcastSpecific[signal].Count == 0) {
                    _broadcastSpecific.Remove(signal);
                }
            }
        }
    }

    public class BroadcastMessage<T1, T2> {

        readonly Dictionary<T1, List<Action<T1, T2>>> _broadcastSpecific = new();
        readonly List<Action<T1, T2>> _broadcastAny = new();

        public void Broadcast(T1 signal1, T2 signal2) {
            List<Action<T1, T2>> list;
            if (_broadcastSpecific.ContainsKey(signal1)) {
                list = new(_broadcastSpecific[signal1]);
                list.ForEach(x => x.Invoke(signal1, signal2));
            }
            list = new(_broadcastAny);
            list.ForEach(x => x.Invoke(signal1, signal2));
        }

        /*---Any---*/
        /// <summary>
        /// Listen to a any message
        /// </summary>
        /// <param name="listener"> The action to call </param>
        public void ListenToAny(Action<T1, T2> listener) {
            if (!_broadcastAny.Contains(listener))
                _broadcastAny.Add(listener);
        }
        /// <summary>
        /// Ignore any messages
        /// </summary>
        /// <param name="listener"> The action to ignore </param>
        public void IgnoreAny(Action<T1, T2> listener) {
            if (_broadcastAny.Contains(listener))
                _broadcastAny.Add(listener);
        }

        /*---Specific---*/
        /// <summary>
        /// Listen to a specific message
        /// </summary>
        /// <param name="listener"> The action to call </param>
        /// <param name="signal"> Specific message </param>
        public void ListenToSpecific(Action<T1, T2> listener, T1 signal) {
            if (!_broadcastSpecific.ContainsKey(signal)) {
                _broadcastSpecific.Add(signal, new());
            }
            if (!_broadcastSpecific[signal].Contains(listener)) {
                _broadcastSpecific[signal].Add(listener);
            }
        }
        /// <summary>
        /// Ignore the specific message
        /// </summary>
        /// <param name="listener"> The action to ignore </param>
        /// <param name="signal"> Specific message </param>
        public void IgnoreSpecific(Action<T1, T2> listener, T1 signal) {
            if (!_broadcastSpecific.ContainsKey(signal)) return;
            if (_broadcastSpecific[signal].Contains(listener)) {
                _broadcastSpecific[signal].Remove(listener);
                if (_broadcastSpecific[signal].Count == 0) {
                    _broadcastSpecific.Remove(signal);
                }
            }
        }
    }
    public class BroadcastMessage<T1, T2, T3> {

        readonly Dictionary<T1, List<Action<T1, T2, T3>>> _broadcastSpecific = new();
        readonly List<Action<T1, T2, T3>> _broadcastAny = new();

        public void Broadcast(T1 signal1, T2 signal2, T3 signal3) {
            List<Action<T1, T2, T3>> list;
            if (_broadcastSpecific.ContainsKey(signal1)) {
                list = new(_broadcastSpecific[signal1]);
                list.ForEach(x => x.Invoke(signal1, signal2, signal3));
            }
            list = new(_broadcastAny);
            list.ForEach(x => x.Invoke(signal1, signal2, signal3));
        }

        /*---Any---*/
        /// <summary>
        /// Listen to a any message
        /// </summary>
        /// <param name="listener"> The action to call </param>
        public void ListenToAny(Action<T1, T2, T3> listener) {
            if (!_broadcastAny.Contains(listener))
                _broadcastAny.Add(listener);
        }
        /// <summary>
        /// Ignore any messages
        /// </summary>
        /// <param name="listener"> The action to ignore </param>
        public void IgnoreAny(Action<T1, T2, T3> listener) {
            if (_broadcastAny.Contains(listener))
                _broadcastAny.Add(listener);
        }

        /*---Specific---*/
        /// <summary>
        /// Listen to a specific message
        /// </summary>
        /// <param name="listener"> The action to call </param>
        /// <param name="signal"> Specific message </param>
        public void ListenToSpecific(Action<T1, T2, T3> listener, T1 signal) {
            if (!_broadcastSpecific.ContainsKey(signal)) {
                _broadcastSpecific.Add(signal, new());
            }
            if (!_broadcastSpecific[signal].Contains(listener)) {
                _broadcastSpecific[signal].Add(listener);
            }
        }
        /// <summary>
        /// Ignore the specific message
        /// </summary>
        /// <param name="listener"> The action to ignore </param>
        /// <param name="signal"> Specific message </param>
        public void IgnoreSpecific(Action<T1, T2, T3> listener, T1 signal) {
            if (!_broadcastSpecific.ContainsKey(signal)) return;
            if (_broadcastSpecific[signal].Contains(listener)) {
                _broadcastSpecific[signal].Remove(listener);
                if (_broadcastSpecific[signal].Count == 0) {
                    _broadcastSpecific.Remove(signal);
                }
            }
        }
    }
}
