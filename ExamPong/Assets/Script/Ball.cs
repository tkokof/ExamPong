// desc simple ball class
// maintainer hugoyu

using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public delegate void OnBallEnterCollision(Collision collision);
    public event OnBallEnterCollision EnterCollisionEvent;

    public delegate void OnBallEnterTrigger(Collider collider);
    public event OnBallEnterTrigger EnterTriggerEvent;

    void OnCollisionEnter(Collision collision) {
        if (EnterCollisionEvent != null) {
            EnterCollisionEvent(collision);
        }
    }

    void OnTriggerEnter(Collider collider) {
        if (EnterTriggerEvent != null) {
            EnterTriggerEvent(collider);
        }
    }
    
}
