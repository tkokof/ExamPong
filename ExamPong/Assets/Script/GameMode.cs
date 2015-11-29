// desc game mode class
// maintainer hugoyu

using UnityEngine;

class GameMode : MonoBehaviour, ISingleton<GameMode> {

    public enum Mode {
        Local,
        Network,
    }

    public Mode m_gameMode = Mode.Local;

    public Ball m_ball;
    public Paddle m_paddleLeft;
    public Paddle m_paddleRight;

    public static GameMode GetInstance() {
        return SingletonUtil<GameMode>.Instance;
    }

    void Awake() {
        SingletonUtil<GameMode>.Instance = this;
    }

    void Start() {
        switch (m_gameMode) {
            case Mode.Local:
                StartLocalGame();
                break;
            case Mode.Network:
                StartNetworkGame();
                break;
        }
	}

    public Mode GetMode() {
        return m_gameMode;
    }

    void StartLocalGame() {
        var game = Game.GetInstance();
        if (game != null) {
            Game.GetInstance().SetBall(m_ball);
            Game.GetInstance().SetPaddleLeft(m_paddleLeft);
            Game.GetInstance().SetPaddleRight(m_paddleRight);
            Game.GetInstance().SetGameState("GameStart");
        }
    }

    void StartNetworkGame() {
        // do nothing here ...
    }

}
