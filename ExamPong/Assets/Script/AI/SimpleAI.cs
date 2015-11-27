// desc simple AI class
// maintainer hugoyu
// TODO improve

using System.Collections;
using UnityEngine;

class SimpleAI : MonoBehaviour {

    public float m_aiInputSpeed;

    public Paddle m_controlPaddle;
    Ball m_gameBall;

    public Vector2 m_aiInputRandomAttack;
    public Vector2 m_aiInputRandomDefend;

    float m_curRandomValAttack;
    float m_curRandomValDefend;

    void Awake() {
        AssertUtil.Assert(m_controlPaddle != null);
    }

    void Start() {
        m_gameBall = Game.GetInstance().GetBall();

        StartCoroutine(UpdateInputRandom());
    }

    IEnumerator UpdateInputRandom() {
        // TODO improve
        var intervalTime = 1;

        while (true) {
            m_curRandomValAttack = Random.Range(m_aiInputRandomAttack.x, m_aiInputRandomAttack.y);
            m_curRandomValDefend = Random.Range(m_aiInputRandomDefend.x, m_aiInputRandomDefend.y);
            yield return new WaitForSeconds(intervalTime);
        }
    }

    void Update() {
        // first calculate target position
        var ballPosition = m_gameBall.transform.position;
        var paddlePosition = m_controlPaddle.transform.position;

        bool isAttacking = true;
        var ballVelocity = m_gameBall.GetComponent<Rigidbody>().velocity;
        if (ballVelocity.x > 0) {
            isAttacking = false;
        }

        var targetHeight = ballPosition.y;
        if (isAttacking) {
            targetHeight = 0;
        }

        // then calculate delta
        var inputHeight = targetHeight - paddlePosition.y;
        inputHeight = Mathf.Clamp(inputHeight, -m_aiInputSpeed, m_aiInputSpeed);

        // random target input
        var random = isAttacking ? m_curRandomValAttack : m_curRandomValDefend;
        inputHeight *= random;

        // dispatch event here
        if (!Mathf.Abs(inputHeight).Equals(0)) {
            var inputData = InputData.GetInputData(InputData.InputType.Move,
                                                   m_controlPaddle,
                                                   inputHeight);
            InputManager.GetInstance().DispatchInput(inputData);
        }
    }

}

