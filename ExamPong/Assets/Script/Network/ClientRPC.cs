// desc client RPC
// maintainer hugoyu

using UnityEngine;
using UnityEngine.Networking;

class ClientRPC : NetworkBehaviour, ISingleton<ClientRPC> {

    Score m_gameScore;

    public static ClientRPC GetInstance() {
        return SingletonUtil<ClientRPC>.Instance;
    }

    void Awake() {
        SingletonUtil<ClientRPC>.Instance = this;
    }

    void Start() {
        AssertUtil.Assert(m_gameScore != null);
    }

    [ClientRpc]
    void RpcSetScore(int leftScore, int rightScore) {
        m_gameScore.SetLeftScore(leftScore);
        m_gameScore.SetRightScore(rightScore);
    }

    // TODO implement

}