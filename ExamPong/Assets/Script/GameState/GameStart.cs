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
        base.OnEnter();

        m_ball = Game.GetInstance().GetBall();
        m_paddle = Game.GetInstance().GetPaddleLeft();

        // make ball as child of paddle
        var ballTransform = m_ball.GetComponent<Transform>();
        var paddleTransform = m_paddle.GetComponent<Transform>();
        m_ballOriTransParent = ballTransform.parent;
        ballTransform.parent = paddleTransform;

        InputManager.GetInstance().RegisterListener(OnInput);
    }

    public override void OnUpdate() {
        // TODO implement
    }

    public override void OnLeave() {
        // rollback ball transform hierarchy
        var ballTransform = m_ball.GetComponent<Transform>();
        ballTransform.parent = m_ballOriTransParent;

        InputManager.GetInstance().UnregisterListener(OnInput);

        base.OnLeave();
    }

    // inner functions
 
    void OnMovePaddle(Paddle paddle, float inputValue) {
        float moveDist = inputValue * m_startInputSpeed;
        paddle.Move(moveDist);
    }

    void OnEmitBall() {
        // first set game state
        var game = Game.GetInstance();
        var ingameState = game.GetComponent<InGame>();
        game.SetGameState(ingameState);

        // then add impulse to ball
        m_ball.GetComponent<Rigidbody>().AddForce(m_emitImpulse, m_emitImpulse, 0, ForceMode.Impulse);
    }

    void OnInput(InputData inputData) {
        var inputType = inputData.GetType();
        switch (inputType) {
            case InputData.InputType.Fire:
                OnEmitBall();
                break;
            case InputData.InputType.Move:
                var target = inputData.GetTarget();
                var moveDist = inputData.GetParamByIndex(0);
                OnMovePaddle(target, moveDist);
                break;
        }
    }

}
