using UnityEngine;
using System.Collections;

public class InGame : GameState {

    public float m_gameInputSpeed;

    public override string GetName() {
        return "InGame";
    }

    public override void OnEnter() {
        base.OnEnter();
        InputManager.GetInstance().RegisterListener(OnInput);
    }

    public override void OnUpdate() {
        // TODO implement
    }

    public override void OnLeave() {
        InputManager.GetInstance().UnregisterListener(OnInput);
        base.OnLeave();
    }

    // inner functions

    void OnMovePaddle(Paddle paddle, float inputValue) {
        float moveDist = inputValue * m_gameInputSpeed;
        paddle.Move(moveDist);
    }

    void OnInput(InputData inputData) {
        var inputType = inputData.GetType();
        switch (inputType) {
            case InputData.InputType.Move:
                var target = inputData.GetTarget();
                var moveDist = inputData.GetParamByIndex(0);
                OnMovePaddle(target, moveDist);
                break;
        }
    }

}
