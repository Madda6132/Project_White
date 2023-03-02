using System.Collections.Generic;
using UnityEngine;

namespace RPG.World.Connection {
    [System.Serializable]
    public class AreaConnector {

        [SerializeField] List<AreaConnection> areaConnection = new();

        List<AbstractArea> _connectedAreas = new();

        public AbstractArea[] AreaConnections => _connectedAreas.ToArray();

        public void Awake(AbstractArea currentArea) {
            areaConnection.ForEach(x => x.ConnectAreas(currentArea));
        }

        public void AddAreaConnection(AbstractArea area) => _connectedAreas.Add(area);

        public void RemoveAreaConnection(AbstractArea area) => _connectedAreas.Remove(area);

        public void DisplayConnections(AbstractArea currentArea) {
            areaConnection.ForEach(x => x.DrawConnection(currentArea));
        }
    }

    [System.Serializable]
    public struct AreaConnection {

        [SerializeField] AbstractArea connectArea;
        [SerializeField] bool twoWayConnection;


        public AreaConnection(AbstractArea area, bool twoWayConnection) {
            this.connectArea = area;
            this.twoWayConnection = twoWayConnection;
        }

        public void ConnectAreas(AbstractArea currentArea) {
            currentArea.AreaConnector.AddAreaConnection(connectArea);
            if (twoWayConnection) {
                connectArea.AreaConnector.AddAreaConnection(currentArea);
            }
        }

        public void DrawConnection(AbstractArea currentArea) {
            Gizmos.color = twoWayConnection ? Color.green : Color.red;
            Gizmos.DrawLine(currentArea.transform.position, connectArea.transform.position);
        }
    }
}
