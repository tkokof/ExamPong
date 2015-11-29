// desc score net class
// maintainer hugoyu

using UnityEngine;
using UnityEngine.Networking;

class ScoreProxyNet : NetworkBehaviour {

    public Score m_score;

    void Start() {
        Game.GetInstance().GameScoreChangeEvent += OnScoreChange;
    }

    void OnScoreChange(int leftScore, int rightScore) {
        //m_score.SetLeftScore(leftScore);
        //m_score.SetRightScore(rightScore);
        if (isServer) {
            RpcSetScore(leftScore, rightScore);
        }
    }

    [ClientRpc]
    void RpcSetScore(int leftScore, int rightScore) {
        m_score.SetLeftScore(leftScore);
        m_score.SetRightScore(rightScore);
    }

}