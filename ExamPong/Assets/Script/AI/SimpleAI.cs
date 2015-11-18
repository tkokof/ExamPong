// desc simple AI class
// maintainer hugoyu

using UnityEngine;

class SimpleAI : MonoBehaviour {

    public float m_aiInputSpeed;
    public Vector2 m_aiInputRandom;

    public Paddle m_controlPaddle;

    Ball m_gameBall;

    void Awake() {
        AssertUtil.Assert(m_controlPaddle != null);
    }

    void Start() {
        m_gameBall = Game.GetInstance().GetBall();
    }

    void Update() {
        var ballPosition = m_gameBall.transform.position;
        var paddlePosition = m_controlPaddle.transform.position;

        var targetInput = ballPosition.y - paddlePosition.y;
        targetInput = Mathf.Clamp(targetInput, -m_aiInputSpeed, m_aiInputSpeed);

        // random target input
        var random = Random.Range(m_aiInputRandom.x, m_aiInputRandom.y);
        targetInput *= random;

        m_controlPaddle.Move(targetInput);
    }

}

