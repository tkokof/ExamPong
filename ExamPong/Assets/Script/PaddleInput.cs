// desc paddle input handler class
// maintainer hugoyu

using UnityEngine;

// NOTE not so sure about this ...
public class PaddleInput : MonoBehaviour {

    public float m_inputSpeed;
    public Paddle m_paddleRef;

    void Start() {
        if (m_paddleRef == null) {
            m_paddleRef = GetComponent<Paddle>();
        }
    }

    void Update() {
        if (m_paddleRef) {
            var vertInput = Input.GetAxis("Vertical");
            float moveDist = vertInput * m_inputSpeed;

            m_paddleRef.Move(moveDist);
        }
    }

}
