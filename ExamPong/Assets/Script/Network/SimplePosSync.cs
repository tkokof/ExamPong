// desc simple network position sync
// maintainer hugoyu

using UnityEngine;
using UnityEngine.Networking;

class SimplePosSync : NetworkBehaviour {

    Transform m_transform;
    Vector3 m_targetPos;

    [SerializeField]
    float m_syncThreshold = 0.1f;
    [SerializeField]
    float m_snapThreshold = 5.0f;
    [SerializeField]
    float m_interpolateFactor = 0.1f;

    void Start() {
        m_transform = GetComponent<Transform>();
        AssertUtil.Assert(m_transform != null);

        m_targetPos = m_transform.position;
    }

    void Update() {
        // not so sure about this : "isLocalPlayer"
        if (hasAuthority) {
            var curPos = m_transform.position;
            var lastDist = (curPos - m_targetPos).magnitude;
            if (lastDist > m_syncThreshold) {
                CmdSyncPosition(curPos);
                m_targetPos = curPos;
            }
        }
        else {
            var curPos = m_transform.position;
            var lastDist = (curPos - m_targetPos).magnitude;
            if (lastDist > m_snapThreshold || lastDist <= m_syncThreshold) {
                curPos = m_targetPos;
            }
            else {
                curPos = Vector3.Lerp(curPos, m_targetPos, Mathf.Clamp01(m_interpolateFactor));
            }
            m_transform.position = curPos;
        }
    }

    [ClientRpc]
    void RpcSyncPosition(Vector3 targetPos) {
        if (!isLocalPlayer) {
            m_targetPos = targetPos;
        }
    }

    [Command]
    void CmdSyncPosition(Vector3 curPos) {
        RpcSyncPosition(curPos);
    }
}
