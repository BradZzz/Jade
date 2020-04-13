﻿using System;
using Battle.Model.Player;
using UnityEngine;

namespace Battle.Model.Game.Mechanics
{
  /// <summary>
  ///     Finish Game Step Implementation.
  /// </summary>
  public class FinishGameMechanics : BaseGameMechanics
  {
    public FinishGameMechanics(IPrimitiveGame game) : base(game)
    {
    }

    public void Execute(IPlayer winner)
    {
      if (!Game.IsGameStarted)
        return;
      if (Game.IsGameFinished)
        return;

      Game.IsGameFinished = true;

      OnGameFinished(winner);
    }

    public void CheckWinCondition()
    {
      var playerLeft = Game.TurnLogic.GetPlayer(PlayerSeat.Left);
      var playerRight = Game.TurnLogic.GetPlayer(PlayerSeat.Right);

      //var captainLeft = playerLeft.Team.Captain?.Attributes?.IsDead;
      //var captainRight = playerRight.Team.Captain?.Attributes?.IsDead;

      ////player has privilege when TIE
      //if (playerRight.Team.IsEmpty || (captainRight.HasValue && captainRight.Value))
      //  OnGameFinished(playerLeft);
      //else
      //if (playerLeft.Team.IsEmpty || (captainLeft.HasValue && captainLeft.Value))
      //  OnGameFinished(playerRight);

      //Left player wins always
      OnGameFinished(playerLeft);
    }


    /// <summary>
    ///     Dispatch end game to the listeners.
    /// </summary>
    /// <param name="winner"></param>
    private void OnGameFinished(IPlayer winner)
    {
      //Save game shit here

      //var gameState = BaseSaver.GetGameData();
      //var playerLeft = Game.TurnLogic.GetPlayer(PlayerSeat.Left);
      //gameState.player.hp = playerLeft.Team.Captain.Attributes.Health;
      //BaseSaver.PutGame(gameState);
      //GameEvents.Instance.Notify<IFinishGame>(i => i.OnFinishGame(winner));
    }
  }
}