// desc paddle input handler class
// maintainer hugoyu

using UnityEngine;
using UnityEngine.Networking;

// TODO improve, like merge code with PaddleInputLocal
public class PaddleInputNet : NetworkBehaviour {

    public float m_inputSpeed;
    Paddle m_paddle;

    void Start() {
        if (m_paddle == null) {
            m_paddle = GetComponent<Paddle>();
        }
    }

    void Update() {
        if (isLocalPlayer) {
            UpdateInput();
        }
    }

    void UpdateInput() {
        if (m_paddle) {
            if (isServer) {
                if (Input.GetButton("Fire1")) {
                    var inputData = InputData.GetInputData(InputData.InputType.Fire,
                                                           m_paddle);
                    InputManager.GetInstance().DispatchInput(inputData);
                }

                var vertInput = Input.GetAxis("Vertical");
                float moveDist = vertInput * m_inputSpeed;
                if (!Mathf.Abs(moveDist).Equals(0)) {
                    var inputData = InputData.GetInputData(InputData.InputType.Move,
                                                           m_paddle,
                                                           moveDist);
                    InputManager.GetInstance().DispatchInput(inputData);
                }
            }
            else {
                // send command to server
                // TODO implement

                // now just do raw move, then depends on network sync
                var vertInput = Input.GetAxis("Vertical");
                float moveDist = vertInput * m_inputSpeed;
                if (!Mathf.Abs(moveDist).Equals(0)) {
                    m_paddle.Move(moveDist);
                }
            }
        }
    }

}
