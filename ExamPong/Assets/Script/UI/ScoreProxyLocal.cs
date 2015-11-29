// desc score net class
// maintainer hugoyu

using UnityEngine;

class ScoreProxyLocal : MonoBehaviour {

    public Score m_score;

    void Start() {
        Game.GetInstance().GameScoreChangeEvent += OnScoreChange;
    }

    void OnScoreChange(int leftScore, int rightScore) {
        m_score.SetLeftScore(leftScore);
        m_score.SetRightScore(rightScore);
    }

}