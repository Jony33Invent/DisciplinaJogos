using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateWave : GameState
{
    public GameStateWave(GameManager context, GameStateFactory factory) : base(context, factory) { }

    public override void StartState()
    {
        GameEvents.Instance.WaveBegin();

        Context.CurWave++;

        // TODO: Criar fórmula para quantidade de inimigos
        Context.RemainingEnemies = 10;
    }

    public override void UpdateState()
    {

        if (Context.RemainingEnemies <= 0)
        {
            Context.SwitchState(Factory.StateIdle);
        }
    }

    public override void ExitState()
    {
        GameEvents.Instance.WaveEnd();
    }

}
