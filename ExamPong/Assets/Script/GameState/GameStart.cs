using UnityEngine;
using System.Collections;

public class GameStart : GameState {

    public float m_startInputSpeed;
    public float m_emitImpulse;

    Ball m_ball;
    Transform m_ballOriTransParent;
    Paddle m_paddle;

    public override string GetName() {
        return "GameStart";
    }

    public override void OnEnter() {
        m_ball = Game.GetInstance().GetBall();
        m_paddle = Game.GetInstance().GetPaddleLeft();

        // make ball as child of paddle
        var ballTransform = m_ball.GetComponent<Transform>();
        var paddleTransform = m_paddle.GetComponent<Transform>();
        m_ballOriTransParent = ballTransform.parent;
        ballTransform.parent = paddleTransform;
    }

    public override void OnUpdate() {
        if (m_paddle) {
            UpdateInput();
            // TODO other things here ...
        }
    }

    public override void OnLeave() {
        // rollback ball transform hierarchy
        var ballTransform = m_ball.GetComponent<Transform>();
        ballTransform.parent = m_ballOriTransParent;
    }

    // inner functions
 
    void OnMovePaddle(float inputValue) {
        float moveDist = inputValue * m_startInputSpeed;
        m_paddle.Move(moveDist);
    }

    void OnEmitBall() {
        // first set game state
        var game = Game.GetInstance();
        var ingameState = game.GetComponent<InGame>();
        game.SetGameState(ingameState);

        // then add impulse to ball
        m_ball.GetComponent<Rigidbody>().AddForce(m_emitImpulse, m_emitImpulse, 0, ForceMode.Impulse);
    }

    void UpdateInput() {
        var vertInput = Input.GetAxis("Vertical");
        if (!Mathf.Equals(vertInput, 0)) {
            OnMovePaddle(vertInput);
        }

        if (Input.GetButton("Fire1")) {
            OnEmitBall();
        }
    }
}
