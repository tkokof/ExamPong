// desc simple ball class
// maintainer hugoyu

using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    void OnCollisionEnter(Collision collision) {
        Debug.Log("Collision Enter");
    }

    void OnTriggerEnter(Collider collider) {
        Debug.Log("Trigger Enter");
    }
    
}
