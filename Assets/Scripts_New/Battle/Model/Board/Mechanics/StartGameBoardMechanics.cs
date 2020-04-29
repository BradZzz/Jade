﻿using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.Player;
using Battle.Model.RuntimeBoard;
using Patterns;
using UnityEngine;

namespace Battle.UI.RuntimeBoard.Mechanics
{
  public class StartGameBoardMechanics : BaseBoardMechanics, IListener, IStartGame
  {
    public StartGameBoardMechanics(IRuntimeBoard board) : base(board)
    {
      GameEvents.Instance.AddListener(this);
      runtimeboard = board;
    }

    private IRuntimeBoard runtimeboard;

    public void OnStartGame(IPlayer starter)
    {
      Execute();
    }

    /// <summary>
    ///     Execution of start game
    /// </summary>
    public void Execute()
    {
      IRuntimeJewel[,] jewelMap = runtimeboard.GetMap();
      int width = jewelMap.GetLength(0);
      int height = jewelMap.GetLength(1);

      //if (Game.IsGameStarted) return;

      //Game.IsGameStarted = true;

      ////calculus of the starting player
      //Game.TurnLogic.DecideStarterPlayer();

      //OnGameStarted(Game.TurnLogic.StarterPlayer);

      //This needs to be pulled in from another file
      JewelDatabase db = JewelDatabase.Instance;
      List<JewelData> jewels = db.GetFullList();

      // Draw the initial jewels the board needs here!
      // The position relates to the offset position from center
      Vector2 middle = new Vector2((int) width / 2, (int) height / 2);
      for (int x = 0; x < jewelMap.GetLength(0); x++)
      {
        for (int y = 0; y < jewelMap.GetLength(1); y++)
        {
          IRuntimeJewel thisJewel = new RuntimeJewel(jewels[Random.Range(0, jewels.Count)]);
          runtimeboard.SetJewel(thisJewel, x, y);
          OnDrawJewel(thisJewel, new Vector2(x - middle.x, y - middle.y));
        }
      }
    }

    /// <summary>
    ///     Dispatch start game event to the listeners.
    /// </summary>
    /// <param name="starterPlayer"></param>
    private void OnDrawJewel(IRuntimeJewel jewel, Vector2 pos)
    {
      GameEvents.Instance.Notify<IBoardDrawJewel>(i => i.OnDraw(jewel, pos));
    }
  }
}
