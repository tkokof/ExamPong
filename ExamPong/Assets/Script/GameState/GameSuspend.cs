// desc game suspend state
// maintainer hugoyu
// TODO improve, like make a better name

using UnityEngine;
using System.Collections;

// NOTE now we do not use it ...
public class GameSuspend : GameState {

    public float m_suspendTime = 2.0f;

    public override string GetName() {
        return "GameSuspend";
    }

    public override void OnEnter() {
        base.OnEnter();
        StartCoroutine(WaitForGameStart(m_suspendTime));
    }

    public override void OnLeave() {
        base.OnLeave();
    }

    IEnumerator WaitForGameStart(float time) {
        yield return new WaitForSeconds(m_suspendTime);

        Game.GetInstance().SetGameState("GameStart");
    }
}
