// desc game logic class
// maintainer hugoyu

using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour, ISingleton<Game> {

    public Ball m_ball;
    public Paddle m_paddleLeft;
    public Paddle m_paddleRight;
    public Collider m_colliderLeft;
    public Collider m_colliderRight;

    // game data buffer
    Vector3 m_ballOriPos;
    Vector3 m_paddleLeftOriPos;
    Vector3 m_paddleRightOriPos;

    // game score
    public int m_scoreThreshold;
    public Score m_gameScore;

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

        // check game refs
        AssertUtil.Assert(m_ball != null, "Ball is null, please check ref setting!");
        AssertUtil.Assert(m_paddleLeft != null, "Paddle Left is null, please check ref setting!");
        AssertUtil.Assert(m_paddleRight != null, "Paddle Right is null, please check ref setting!");
        AssertUtil.Assert(m_colliderLeft != null, "Collider Left is null, please check ref setting");
        AssertUtil.Assert(m_colliderRight != null, "Collider Right is null, please check ref setting");

        // buffer game data
        m_ballOriPos = m_ball.transform.position;
        m_paddleLeftOriPos = m_paddleLeft.transform.position;
        m_paddleRightOriPos = m_paddleRight.transform.position;
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

    void OnBallEnterTrigger(Collider collider) {
        if (collider == m_colliderLeft) {
            m_gameScore.AddRightScore();
        }
        else if (collider == m_colliderRight) {
            m_gameScore.AddLeftScore();
        }

        var gameResult = Check();
        if (gameResult == GameResult.NoResult) {
            ResetBallAndPaddle();
            SetGameState(GetComponent<GameStart>());
        }
        else {
            ResetAll();
            SetGameState(GetComponent<GameStart>());
        }
    }

    GameResult Check() {
        var leftScore = m_gameScore.GetLeftScore();
        var rightScore = m_gameScore.GetRightScore();

        if (leftScore > m_scoreThreshold) {
            Debug.Log("Left Win !!!");
            return GameResult.LeftWin;
        }
        else if (rightScore > m_scoreThreshold) {
            Debug.Log("Right Win !!!");
            return GameResult.RightWin;
        }

        return GameResult.NoResult;
    }

    void Start() {
        // init ball event
        m_ball.EnterTriggerEvent += OnBallEnterTrigger;

        // set game state
        SetGameState(GetComponent<GameStart>());
    }

    void Update() {
        if (m_gameState != null) {
            m_gameState.OnUpdate();
        }
    }

    void ResetBallAndPaddle() {
        m_ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        m_ball.transform.position = m_ballOriPos;
        m_paddleLeft.transform.position = m_paddleLeftOriPos;
        m_paddleRight.transform.position = m_paddleRightOriPos;
    }

    void ResetScore() {
        m_gameScore.Reset();
    }

    void ResetAll() {
        // reset ball and paddle
        ResetBallAndPaddle();

        // reset score
        ResetScore();
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

    public int GetScoreThreshold() {
        return m_scoreThreshold;
    }

    public Score GetScore() {
        return m_gameScore;
    }

}
