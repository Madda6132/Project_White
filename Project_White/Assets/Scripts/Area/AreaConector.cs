using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RPG.World.Connection {
    [System.Serializable]
    public class AreaConnector {

        List<AbstractArea> conectedAreas = new();

        public AbstractArea[] AreaConnections => conectedAreas.ToArray();

        public void AddAreaConnection(AreaConnection areaConnection) => AddAreaConnection(areaConnection.ConnectArea);

        public void AddAreaConnection(AbstractArea area) => conectedAreas.Add(area);

        public void RemoveAreaConnection(AbstractArea area) => conectedAreas.Remove(area);


    }

    [System.Serializable]
    public struct AreaConnection {

        public AbstractArea ConnectArea { get; private set; }
        public bool TwoWayConnection { get; private set; }

        public AreaConnection(AbstractArea area, bool twoWayConnection) {
            this.ConnectArea = area;
            this.TwoWayConnection = twoWayConnection;
        }
    }
}
