using UnityEngine;
using System.Collections;

// desc game logic class
// maintainer hugoyu

public class Game : MonoBehaviour, ISingleton<Game> {

    public Ball m_ball;
    public Paddle m_paddleLeft;
    public Paddle m_paddleRight;

    GameState m_gameState;

    public static Game GetInstance() {
        return SingletonUtil<Game>.Instance;
    }

    void Awake() {
        // handle singleton pattern
        SingletonUtil<Game>.Instance = this;

        AssertUtil.Assert(m_ball != null, "Ball is null, please check ref setting!");
        AssertUtil.Assert(m_paddleLeft != null, "Paddle Left is null, please check ref setting!");
        AssertUtil.Assert(m_paddleRight != null, "Paddle Right is null, please check ref setting!");
    }

    public GameState GetGameState() {
        return m_gameState;
    }

    public void SetGameState(GameState gameState) {
        if (m_gameState != null) {
            m_gameState.OnLeave();
        }

        m_gameState = gameState;

        if (m_gameState != null) {
            m_gameState.OnEnter();
        }
    }

    void Start() {
        // test stuff
        SetGameState(GetComponent<GameStart>());
    }

    void Update() {
        if (m_gameState != null) {
            m_gameState.OnUpdate();
        }
    }

    void Reset() {
        if (m_gameState != null) {
            m_gameState.OnLeave();
        }

        // TODO implement
    }

    // utility functions
    public Ball GetBall() {
        return m_ball;
    }

    public Paddle GetPaddleLeft() {
        return m_paddleLeft;
    }

    public Paddle GetPaddleRight() {
        return m_paddleRight;
    }

}
