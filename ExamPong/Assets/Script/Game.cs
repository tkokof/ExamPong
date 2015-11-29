// desc game logic class
// maintainer hugoyu

using UnityEngine;

public class Game : MonoBehaviour, ISingleton<Game> {

    public Collider m_colliderLeft;
    public Collider m_colliderRight;

    // game data buffer
    public Vector3 m_ballOriPos;
    public Vector3 m_paddleLeftOriPos;
    public Vector3 m_paddleRightOriPos;

    // game score data
    public int m_scoreThreshold;
    public int m_scoreLeft;
    public int m_scoreRight;

    public delegate void OnGameScoreChange(int leftScore, int rightScore);
    public event OnGameScoreChange GameScoreChangeEvent;

    Ball m_ball;
    Paddle m_paddleLeft;
    Paddle m_paddleRight;

    GameState m_gameState;

    public enum GameResult {
        LeftWin,
        RightWin,
        NoResult,
    }

    public static Game GetInstance() {
        return SingletonUtil<Game>.Instance;
    }

    void Awake() {
        // handle singleton pattern
        SingletonUtil<Game>.Instance = this;
    }

    public GameState GetGameState() {
        return m_gameState;
    }

    void SetGameState(GameState gameState) {
        if (m_gameState != null) {
            m_gameState.OnLeave();
        }

        m_gameState = gameState;

        if (m_gameState != null) {
            m_gameState.OnEnter();
        }
    }

    public void SetGameState(string stateName) {
        var states = GetComponents<GameState>();
        for (int i = 1; i < states.Length; ++i) {
            if (states[i].GetName() == stateName) {
                SetGameState(states[i]);
                return;
            }
        }
        AssertUtil.Assert(false, "Unknown state name : " + stateName);
    }

    public void Reset() {
        ResetAll();
    }

    void OnBallExitTrigger(Collider collider) {
        if (collider == m_colliderLeft) {
            ++m_scoreRight;
        }
        else if (collider == m_colliderRight) {
            ++m_scoreLeft;
        }

        var gameResult = CheckResult();
        if (gameResult == GameResult.NoResult) {
            ResetBallAndPaddle();
            SetGameState(GetComponent<GameStart>());
        }
        else {
            ResetAll();
            SetGameState(GetComponent<GameStart>());
        }

        if (GameScoreChangeEvent != null) {
            GameScoreChangeEvent(m_scoreLeft, m_scoreRight);
        }
    }

    GameResult CheckResult() {
        if (m_scoreLeft > m_scoreThreshold) {
            Debug.Log("Left Win !!!");
            return GameResult.LeftWin;
        }
        else if (m_scoreRight > m_scoreThreshold) {
            Debug.Log("Right Win !!!");
            return GameResult.RightWin;
        }

        return GameResult.NoResult;
    }

    void Update() {
        if (m_gameState != null) {
            m_gameState.OnUpdate();
        }
    }

    void ResetBallAndPaddle() {
        m_ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        m_ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        m_ball.transform.position = m_ballOriPos;
        m_paddleLeft.transform.position = m_paddleLeftOriPos;
        m_paddleRight.transform.position = m_paddleRightOriPos;
    }

    void ResetScore() {
        m_scoreLeft = 0;
        m_scoreRight = 0;

        if (GameScoreChangeEvent != null) {
            GameScoreChangeEvent(m_scoreLeft, m_scoreRight);
        }
    }

    void ResetAll() {
        // reset ball and paddle
        ResetBallAndPaddle();

        // reset score
        ResetScore();
    }

    public Ball GetBall() {
        return m_ball;
    }

    public void SetBall(Ball ball) {
        if (m_ball != null) {
            m_ball.ExitTriggerEvent -= OnBallExitTrigger;
        }

        m_ball = ball;
        m_ball.ExitTriggerEvent += OnBallExitTrigger;
    }

    public Paddle GetPaddleLeft() {
        return m_paddleLeft;
    }

    public void SetPaddleLeft(Paddle paddleLeft) {
        m_paddleLeft = paddleLeft;
    }

    public Paddle GetPaddleRight() {
        return m_paddleRight;
    }

    public void SetPaddleRight(Paddle paddleRight) {
        m_paddleRight = paddleRight;
    }

    public int GetScoreThreshold() {
        return m_scoreThreshold;
    }

}
