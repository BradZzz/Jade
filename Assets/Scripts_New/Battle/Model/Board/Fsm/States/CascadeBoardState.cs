﻿using System.Collections;
using System.Collections.Generic;
using Battle.GameEvent;
using Battle.Model.Jewel;
using Battle.Model.RuntimeBoard.Data;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Fsm
{
  /*
   * The board needs to be filled up with missing gems
   */

  public class CascadeBoardState : BaseBoardState
  {
    public CascadeBoardState(BoardBasedFsm Fsm, IRuntimeBoard Board) : base(Fsm, Board)
    {
      board = Board;
    }

    private IRuntimeBoard board;

    private int Count { get; set; }

    public override void OnEnterState()
    {
      base.OnEnterState();

      //Debug.Log("CascadeBoardState");
      List<JewelData> jewels = JewelDatabase.Instance.GetFullList();
      List<IRuntimeJewel> readyJewels = new List<IRuntimeJewel>();
      IRuntimeJewel[,] jewelMap = board.GetBoardData().GetMap();
      int width = jewelMap.GetLength(0);
      int height = jewelMap.GetLength(1);


      for (int x = 0; x < width; x++)
      {
        for (int y = 0; y < height; y++)
        {
          Vector2 pos = new Vector2(x, y);
          IRuntimeJewel jewel = FindNextJewel(jewelMap, pos);
          GameObject UIJewel = jewel == null ? null : GameObject.Find(jewel.JewelID);
          if (jewel == null || UIJewel == null)
          {
            jewel = new RuntimeJewel(jewels[Random.Range(0, jewels.Count)], pos, "Jewel_" + Count++);
          } else
          {
            // Rotate gem's position
            jewel.RotatePos(pos);
            // Remove old jewel position from buffer
            jewelMap[(int)jewel.LastPos.x, (int)jewel.LastPos.y] = null;
          }
          // Place Jewel On Board
          SetJewelData(jewel, pos);
          // Update UI
          readyJewels.Add(jewel);
          // Update temp buffer
          jewelMap[x, y] = jewel;
        }
      }

      //board.GetBoardData().PrettyJewelMap();

      // Update UI
      foreach (IRuntimeJewel jwl in readyJewels)
      {
        OnCascadeJewel(jwl);
      }
    }

    private IRuntimeJewel FindNextJewel(IRuntimeJewel[,] jewelMap, Vector2 currentPos)
    {
      int height = jewelMap.GetLength(1);
      int row = (int) currentPos.y;
      while (row < height - 1 && jewelMap[(int)currentPos.x, row] == null)
      {
        row++;
      }
      return jewelMap[(int)currentPos.x, row];
    }

    // Update
    private void SetJewelData(IRuntimeJewel jewel, Vector2 pos)
    {
      board.GetBoardData().SetJewel(jewel, pos);
    }
    // Notify
    private void OnCascadeJewel(IRuntimeJewel jewel)
    {
      GameEvents.Instance.Notify<ICascadeJewel>(i => i.OnJewelFall(jewel));
    }
  }
}
