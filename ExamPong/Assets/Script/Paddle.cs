// desc paddle class
// maintainer hugoyu

using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

    private Transform m_transform;
    public Vector2 m_moveRange;

    void Awake() {
        m_transform = GetComponent<Transform>();
    }

    // positive means up, negative means down, just one dimensions here
    public void Move(float moveDist) {
        // method 1 - manually position calculation
        m_transform.Translate(0, moveDist, 0, Space.Self);
        var localPosition = m_transform.localPosition;
        localPosition.y = Mathf.Clamp(localPosition.y, m_moveRange.x, m_moveRange.y);
        m_transform.localPosition = localPosition;

        // method 2 - physics force
        // TODO implement
    }

}
