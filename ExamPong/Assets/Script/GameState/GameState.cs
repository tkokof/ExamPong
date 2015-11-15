using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

    public virtual string GetName() {
        return "Unimplement";
    }

    public virtual void OnEnter() {
    }

    public virtual void OnUpdate() {
    }

    public virtual void OnLeave() {
    }
	
}
