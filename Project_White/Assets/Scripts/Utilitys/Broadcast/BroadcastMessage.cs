using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG.Utilitys {
    public class BroadcastMessage {

        Dictionary<string, List<Action>> brodcastSpesific = new();
        List<Action> brodcastAny = new();

        public void Broadcast(string signal) {

            List<Action> list;
            if (brodcastSpesific.ContainsKey(signal)) {
                list = new(brodcastSpesific[signal]);
                list.ForEach(x => x.Invoke());
            }

            list = new(brodcastAny);
            brodcastAny.ForEach(x => x.Invoke());
        
        }

        /*---Any---*/
        /// <summary>
        /// Listen to a any message
        /// </summary>
        /// <param name="listener"> The action to call </param>
        public void ListenToAny(Action listener) { 
            
            if (!brodcastAny.Contains(listener)) 
                brodcastAny.Add(listener); 
        }
        /// <summary>
        /// Ignore any messages
        /// </summary>
        /// <param name="listener"> The action to ignore </param>
        public void IgnoreAny(Action listener) { 
            
            if(brodcastAny.Contains(listener)) 
                brodcastAny.Add(listener); 
        }

        /*---Specific---*/
        /// <summary>
        /// Listen to a specific message
        /// </summary>
        /// <param name="listener"> The action to call </param>
        /// <param name="signal"> Specific message </param>
        public void ListenToSpecific(Action listener, string signal) {

            if (!brodcastSpesific.ContainsKey(signal)) {
                brodcastSpesific.Add(signal, new());
            }

            if (!brodcastSpesific[signal].Contains(listener)) { 
                brodcastSpesific[signal].Add(listener);
            }
        }
        /// <summary>
        /// Ignore the specific message
        /// </summary>
        /// <param name="listener"> The action to ignore </param>
        /// <param name="signal"> Specific message </param>
        public void IgnoreSpecific(Action listener, string signal) {

            if (!brodcastSpesific.ContainsKey(signal)) return;

            if (brodcastSpesific[signal].Contains(listener)) {
                brodcastSpesific[signal].Remove(listener);

                if(brodcastSpesific[signal].Count == 0) {
                    brodcastSpesific.Remove(signal);
                }
            }
        }
    }

    public class BroadcastMessage<T> {

        Dictionary<T, List<Action<T>>> brodcastSpesific = new();
        List<Action<T>> brodcastAny = new();

        public void Broadcast(T signal) {

            List<Action<T>> list;
            if (brodcastSpesific.ContainsKey(signal)) {

                list = new(brodcastSpesific[signal]);
                list.ForEach(x => x.Invoke(signal));
            }

            list = new(brodcastAny);
            list.ForEach(x => x.Invoke(signal));

        }

        /*---Any---*/
        /// <summary>
        /// Listen to a any message
        /// </summary>
        /// <param name="listener"> The action to call </param>
        public void ListenToAny(Action<T> listener) {

            if (!brodcastAny.Contains(listener))
                brodcastAny.Add(listener);
        }
        /// <summary>
        /// Ignore any messages
        /// </summary>
        /// <param name="listener"> The action to ignore </param>
        public void IgnoreAny(Action<T> listener) {

            if (brodcastAny.Contains(listener))
                brodcastAny.Add(listener);
        }

        /*---Specific---*/
        /// <summary>
        /// Listen to a specific message
        /// </summary>
        /// <param name="listener"> The action to call </param>
        /// <param name="signal"> Specific message </param>
        public void ListenToSpecific(Action<T> listener, T signal) {

            if (!brodcastSpesific.ContainsKey(signal)) {
                brodcastSpesific.Add(signal, new());
            }

            if (!brodcastSpesific[signal].Contains(listener)) {
                brodcastSpesific[signal].Add(listener);
            }
        }
        /// <summary>
        /// Ignore the specific message
        /// </summary>
        /// <param name="listener"> The action to ignore </param>
        /// <param name="signal"> Specific message </param>
        public void IgnoreSpecific(Action<T> listener, T signal) {

            if (!brodcastSpesific.ContainsKey(signal)) return;

            if (brodcastSpesific[signal].Contains(listener)) {
                brodcastSpesific[signal].Remove(listener);

                if (brodcastSpesific[signal].Count == 0) {
                    brodcastSpesific.Remove(signal);
                }
            }
        }
    }

    public class BroadcastMessage<T1, T2> {

        Dictionary<T1, List<Action<T1, T2>>> brodcastSpesific = new();
        List<Action<T1, T2>> brodcastAny = new();

        public void Broadcast(T1 signal1, T2 signal2) {

            List<Action<T1, T2>> list;
            if (brodcastSpesific.ContainsKey(signal1)) {

                list = new(brodcastSpesific[signal1]);
                list.ForEach(x => x.Invoke(signal1, signal2));
            }

            list = new(brodcastAny);
            list.ForEach(x => x.Invoke(signal1, signal2));

        }

        /*---Any---*/
        /// <summary>
        /// Listen to a any message
        /// </summary>
        /// <param name="listener"> The action to call </param>
        public void ListenToAny(Action<T1, T2> listener) {

            if (!brodcastAny.Contains(listener))
                brodcastAny.Add(listener);
        }
        /// <summary>
        /// Ignore any messages
        /// </summary>
        /// <param name="listener"> The action to ignore </param>
        public void IgnoreAny(Action<T1, T2> listener) {

            if (brodcastAny.Contains(listener))
                brodcastAny.Add(listener);
        }

        /*---Specific---*/
        /// <summary>
        /// Listen to a specific message
        /// </summary>
        /// <param name="listener"> The action to call </param>
        /// <param name="signal"> Specific message </param>
        public void ListenToSpecific(Action<T1, T2> listener, T1 signal) {

            if (!brodcastSpesific.ContainsKey(signal)) {
                brodcastSpesific.Add(signal, new());
            }

            if (!brodcastSpesific[signal].Contains(listener)) {
                brodcastSpesific[signal].Add(listener);
            }
        }
        /// <summary>
        /// Ignore the specific message
        /// </summary>
        /// <param name="listener"> The action to ignore </param>
        /// <param name="signal"> Specific message </param>
        public void IgnoreSpecific(Action<T1, T2> listener, T1 signal) {

            if (!brodcastSpesific.ContainsKey(signal)) return;

            if (brodcastSpesific[signal].Contains(listener)) {
                brodcastSpesific[signal].Remove(listener);

                if (brodcastSpesific[signal].Count == 0) {
                    brodcastSpesific.Remove(signal);
                }
            }
        }
    }
    public class BroadcastMessage<T1, T2, T3> {

        Dictionary<T1, List<Action<T1, T2, T3>>> brodcastSpesific = new();
        List<Action<T1, T2, T3>> brodcastAny = new();

        public void Broadcast(T1 signal1, T2 signal2, T3 signal3) {

            List<Action<T1, T2, T3>> list;
            if (brodcastSpesific.ContainsKey(signal1)) {

                list = new(brodcastSpesific[signal1]);
                list.ForEach(x => x.Invoke(signal1, signal2, signal3));
            }

            list = new(brodcastAny);
            list.ForEach(x => x.Invoke(signal1, signal2, signal3));

        }

        /*---Any---*/
        /// <summary>
        /// Listen to a any message
        /// </summary>
        /// <param name="listener"> The action to call </param>
        public void ListenToAny(Action<T1, T2, T3> listener) {

            if (!brodcastAny.Contains(listener))
                brodcastAny.Add(listener);
        }
        /// <summary>
        /// Ignore any messages
        /// </summary>
        /// <param name="listener"> The action to ignore </param>
        public void IgnoreAny(Action<T1, T2, T3> listener) {

            if (brodcastAny.Contains(listener))
                brodcastAny.Add(listener);
        }

        /*---Specific---*/
        /// <summary>
        /// Listen to a specific message
        /// </summary>
        /// <param name="listener"> The action to call </param>
        /// <param name="signal"> Specific message </param>
        public void ListenToSpecific(Action<T1, T2, T3> listener, T1 signal) {

            if (!brodcastSpesific.ContainsKey(signal)) {
                brodcastSpesific.Add(signal, new());
            }

            if (!brodcastSpesific[signal].Contains(listener)) {
                brodcastSpesific[signal].Add(listener);
            }
        }
        /// <summary>
        /// Ignore the specific message
        /// </summary>
        /// <param name="listener"> The action to ignore </param>
        /// <param name="signal"> Specific message </param>
        public void IgnoreSpecific(Action<T1, T2, T3> listener, T1 signal) {

            if (!brodcastSpesific.ContainsKey(signal)) return;

            if (brodcastSpesific[signal].Contains(listener)) {
                brodcastSpesific[signal].Remove(listener);

                if (brodcastSpesific[signal].Count == 0) {
                    brodcastSpesific.Remove(signal);
                }
            }
        }
    }
}
