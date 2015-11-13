// desc paddle input handler class
// maintainer hugoyu

using UnityEngine;

public class PaddleInput : MonoBehaviour {

    public float m_inputSpeed;
    public Paddle m_paddleRef;
    public Vector2 m_moveRange;

    private Transform m_transform;

    void Start() {
        m_transform = GetComponent<Transform>();
    }

    void Update() {
        if (m_paddleRef) {
            var vertInput = Input.GetAxis("Vertical");

            // method 1 - manually position calculation
            float moveDist = vertInput * m_inputSpeed;
            m_transform.Translate(0, moveDist, 0, Space.Self);
            var localPosition = m_transform.localPosition;
            localPosition.y = Mathf.Clamp(localPosition.y, m_moveRange.x, m_moveRange.y);
            m_transform.localPosition = localPosition;

            // method 2 - physics force
            // TODO implement
        }
    }
}
