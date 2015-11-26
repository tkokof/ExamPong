// desc pong network manager
// maintainer hugoyu

using UnityEngine;
using UnityEngine.Networking;

class PongNetworkManager : NetworkManager {

    public Vector3[] m_startPoints;
    int m_curConnections = 0;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
        var player = (GameObject)GameObject.Instantiate(playerPrefab, m_startPoints[m_curConnections], Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        ++m_curConnections;
    }

}
