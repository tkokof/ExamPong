// desc paddle input local class
// maintainer hugoyu

using UnityEngine;

class PaddleInputLocal : MonoBehaviour {

    public float m_inputSpeed;
    Paddle m_paddle;

    void Start() {
        if (m_paddle == null) {
            m_paddle = GetComponent<Paddle>();
        }
    }

    void Update() {
        if (m_paddle) {
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
    }

}