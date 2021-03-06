﻿using Battle.GameEvent;
using Battle.Model.Player;

namespace Battle.Model.Game.Mechanics
{
  /// <summary>
  ///     Tick time Logic Implementation
  /// </summary>
  public class TickTimeMechanics : BaseGameMechanics
  {
    public TickTimeMechanics(IPrimitiveGame game) : base(game)
    {
    }

    private float TimeOutTurn => Game.Configurations.TimeOutTurn;
    private float TimeStartTurn => Game.Configurations.TimeStartTurn;

    /// <summary>
    ///     Execution of the tick logic.
    /// </summary>
    public void Execute()
    {
      if (!Game.IsTurnInProgress)
        return;

      if (!Game.IsGameStarted)
        return;

      if (Game.IsGameFinished)
        return;

      //Game.TurnTime++;
      //Game.TotalTime++;
      //var reverseTime = (int) (TimeOutTurn - 1 - Game.TurnTime - TimeStartTurn);
      //OnTickTime(reverseTime, Game.TurnLogic.CurrentPlayer);
    }

    /// <summary>
    ///     Dispatch tick time to the listeners.
    /// </summary>
    /// <param name="time"></param>
    /// <param name="current"></param>
    private void OnTickTime(int time, IPlayer current)
    {
      GameEvents.Instance.Notify<IDoTick>(i => i.OnTickTime(time, current));
    }
  }
}