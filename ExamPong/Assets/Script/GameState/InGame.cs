using UnityEngine;
using System.Collections;

public class InGame : GameState {

    public float m_gameInputSpeed;

    Paddle m_paddle;

    public override string GetName() {
        return "InGame";
    }

    public override void OnEnter() {
        m_paddle = Game.GetInstance().GetPaddleLeft();
    }

    public override void OnUpdate() {
        if (m_paddle != null) {
            UpdateInput();
            // TODO other things here ...
        }
    }

    // inner functions

    void OnMovePaddle(float inputValue) {
        float moveDist = inputValue * m_gameInputSpeed;
        m_paddle.Move(moveDist);
    }

    void UpdateInput() {
        var vertInput = Input.GetAxis("Vertical");
        if (!Mathf.Equals(vertInput, 0)) {
            OnMovePaddle(vertInput);
        }
    }

}
