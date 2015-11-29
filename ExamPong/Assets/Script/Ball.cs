// desc simple ball class
// maintainer hugoyu

using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public delegate void CollisionDelegate(Collision collision);
    public event CollisionDelegate EnterCollisionEvent;
    public event CollisionDelegate StayCollisionEvent;
    public event CollisionDelegate ExitCollisionEvent;

    public delegate void TriggerDelegate(Collider collider);
    public event TriggerDelegate EnterTriggerEvent;
    public event TriggerDelegate StayTriggerEvent;
    public event TriggerDelegate ExitTriggerEvent;

    void OnCollisionEnter(Collision collision) {
        if (EnterCollisionEvent != null) {
            EnterCollisionEvent(collision);
        }
    }

    void OnCollisionStay(Collision collision) {
        if (StayCollisionEvent != null) {
            StayCollisionEvent(collision);
        }
    }

    void OnCollisionExit(Collision collision) {
        if (ExitCollisionEvent != null) {
            ExitCollisionEvent(collision);
        }
    }

    void OnTriggerEnter(Collider collider) {
        if (EnterTriggerEvent != null) {
            EnterTriggerEvent(collider);
        }
    }

    void OnTriggerStay(Collider collider) {
        if (StayTriggerEvent != null) {
            StayTriggerEvent(collider);
        }
    }

    void OnTriggerExit(Collider collider) {
        if (ExitTriggerEvent != null) {
            ExitTriggerEvent(collider);
        }
    }
    
}
