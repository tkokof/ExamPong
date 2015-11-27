// desc input manager
// maintainer hugoyu

using UnityEngine;

class InputManager : MonoBehaviour, ISingleton<InputManager> {

    public delegate void OnInput(InputData inputData);
    event OnInput InputEvent;

    static public InputManager GetInstance() {
        return SingletonUtil<InputManager>.Instance;
    }

    void Awake() {
        SingletonUtil<InputManager>.Instance = this;
    }

    public void RegisterListener(OnInput callback) {
        InputEvent += callback;
    }

    public void UnregisterListener(OnInput callback) {
        InputEvent -= callback;
    }

    public void DispatchInput(InputData inputData) {
        if (InputEvent != null) {
            InputEvent(inputData);
        }
    }

}
