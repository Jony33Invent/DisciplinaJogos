using MWP.Misc;

namespace MWP.GameStates
{
    public class GameStateWave : GameState
    {
        public const int WaveMultiplier = 10;

        public GameStateWave(GameManager context, GameStateFactory factory) : base(context, factory) { }

        public override void StartState()
        {
            Context.CurWave++;

            Context.RemainingEnemies = WaveMultiplier * Context.CurWave;

            GameEvents.Instance.WaveBegin();
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
}