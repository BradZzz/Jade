﻿using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.RuntimeBoard.Data;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Fsm
{
  /*
   * A successful swap has been made, and the board needs to remove the matching gems then move to the CascadeBoard state
   */

  public class RemoveSelectedBoardState : BaseBoardState
  {
    public RemoveSelectedBoardState(BoardBasedFsm Fsm, IBoardData BoardData) : base(Fsm, BoardData)
    {
      boardData = BoardData;
    }

    private IBoardData boardData;

    public override void OnEnterState()
    {
      // Look through all jewels
      IRuntimeJewel[,] jewels = boardData.GetMap();

      // Right here is where I need to look through all the jewels and see if two jewels are clicked
      List<IRuntimeJewel> jewelsClicked = new List<IRuntimeJewel>();
      for (int x = 0; x < jewels.GetLength(0); x++)
        for (int y = 0; y < jewels.GetLength(1); y++)
          if (jewels[x, y].IsSelected)
            jewels[x, y].DoUnselect();

      OnCleanBoardState();
    }

    private void OnCleanBoardState()
    {
      GameEvents.Instance.Notify<ICleanBoard>(i => i.OnBoardCleanCheck());
    }
  }
}
