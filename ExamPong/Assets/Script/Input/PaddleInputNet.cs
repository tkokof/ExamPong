// desc paddle input handler class
// maintainer hugoyu

using UnityEngine;
using UnityEngine.Networking;

// NOTE not so sure about this ...
public class PaddleInputNet : NetworkBehaviour {

    public float m_inputSpeed;
    Paddle m_paddle;

    void Start() {
        if (m_paddle == null) {
            m_paddle = GetComponent<Paddle>();
        }
    }

    void Update() {
        if (m_paddle) {
            if (isLocalPlayer) {
                var vertInput = Input.GetAxis("Vertical");
                float moveDist = vertInput * m_inputSpeed;

                m_paddle.Move(moveDist);
            }
        }
    }

}
