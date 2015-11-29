// desc pong network manager
// maintainer hugoyu

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

class PongNetworkManager : NetworkManager {

    public Vector3[] m_startPoints;

    // game states buffer
    int m_curConnections = 0;
    List<Paddle> m_players = new List<Paddle>();

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
        AssertUtil.Assert(m_startPoints.Length != 0);
        var startPointIndex = m_curConnections % m_startPoints.Length;
        var player = (GameObject)GameObject.Instantiate(playerPrefab, m_startPoints[startPointIndex], Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        m_players.Add(player.GetComponent<Paddle>());
        ++m_curConnections;

        if (m_curConnections >= maxConnections) {
            StartNetworkGame();
        } 
    }

    // TODO implement better game control
    public override void OnServerDisconnect(NetworkConnection conn) {
        --m_curConnections;

        AssertUtil.Assert(conn.playerControllers.Count == 1);
        m_players.Remove(conn.playerControllers[0].gameObject.GetComponent<Paddle>());

        base.OnServerDisconnect(conn);
    }

    public override void OnStopHost() {
        m_curConnections = 0;
        m_players.Clear();

        base.OnStopHost();
    }

    // NOTE this callback can not be invoked !?
    // TODO figure out this ...
    /*
    public override void OnServerRemovePlayer(NetworkConnection conn, PlayerController player) {
        --m_curConnections;
        m_players.Remove(player.gameObject.GetComponent<Paddle>());

        // just stop host when any player is gone for simplicity
        StopHost();

        base.OnServerRemovePlayer(conn, player);
    }
    */

    void StartNetworkGame() {
        // first we enable "game" object
        // NOTE game logic is just run on host or server
        var game = Game.GetInstance();
        Game.GetInstance().gameObject.SetActive(true);

        // spawn ball
        // TODO improve
        var ball = (GameObject)GameObject.Instantiate(spawnPrefabs[0], game.m_ballOriPos, Quaternion.identity);
        NetworkServer.Spawn(ball);

        game.SetBall(ball.GetComponent<Ball>());
        game.SetPaddleLeft(m_players[0]);
        game.SetPaddleRight(m_players[1]);

        Game.GetInstance().Reset();
        game.SetGameState("GameStart");
    }

}
