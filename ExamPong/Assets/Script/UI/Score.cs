// desc simple score display
// maintainer hugoyu

using UnityEngine;

// now score is view & model
// TODO implement, like split model to separate class
public class Score : MonoBehaviour {

    public TextMesh m_leftScoreText;
    public TextMesh m_rightScoreText;

    int m_leftScore;
    int m_rightScore;

    void Awake() {
        AssertUtil.Assert(m_leftScoreText != null && m_rightScoreText != null);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void SetLeftScore(int score) {
        m_leftScore = score;
        m_leftScoreText.text = m_leftScore.ToString();
    }

    public int GetLeftScore() {
        return m_leftScore;
    }

    public void SetRightScore(int score) {
        m_rightScore = score;
        m_rightScoreText.text = m_rightScore.ToString();
    }

    public int GetRightScore() {
        return m_rightScore;
    }

    public void Reset() {
        SetLeftScore(0);
        SetRightScore(0);
    }

    // utility functions
    public void AddLeftScore(int addValue = 1) {
        m_leftScore += addValue;
        m_leftScoreText.text = m_leftScore.ToString();
    }

    public void AddRightScore(int addValue = 1) {
        m_rightScore += addValue;
        m_rightScoreText.text = m_rightScore.ToString();
    }

}
