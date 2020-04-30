﻿using System.Collections;
using System.Collections.Generic;
using Battle.Controller;
using Battle.GameEvent;
using Battle.Model.Player;
using Battle.Model.RuntimeBoard.Fsm;
using Patterns;
using UnityEngine;

namespace Battle.Model.RuntimeBoard.Controller
{
  public class BoardController : SingletonMB<BoardController>, IBoardController, IListener, IStartGame, IEvaluateBoard
  {
    /// <summary>
    ///     State machine that holds the board logic.
    /// </summary>
    private BoardBasedFsm BoardBasedLogic { get; set; }

    public MonoBehaviour MonoBehaviour => this;

    public string Name => gameObject.name;

    void Awake()
    {
      base.Awake();
      GameEvents.Instance.AddListener(this);
    }

    //private void Start()
    //{
    //  // Get config and board from gameobject
    //  BoardBasedLogic = new BoardBasedFsm(this, GameData.Instance.RuntimeGame.GameBoard.GetBoardData());
    //  OnTurnStart();
    //}

    public void OnStartGame(IPlayer starter)
    {
      Debug.Log("OnStartGame");
      BoardBasedLogic = new BoardBasedFsm(this, GameData.Instance.RuntimeGame.GameBoard.GetBoardData());
      OnTurnStart();
    }

    public void OnTurnStart()
    {
      /*
       * CleanBoardState
       * SelectedBoardState
       * SwapBoardState
       * EvaluateBoardState
       * RemoveMatchesBoardState
       * CasecadeBoardState
       * CleanBoardState
       */
      BoardBasedLogic.PopState();
      BoardBasedLogic.PushState<CascadeBoardState>();
    }

    public void OnBoardEvaluateCheck()
    {
      BoardBasedLogic.PopState();
      BoardBasedLogic.PushState<EvaluateBoardState>();
    }
  }
}
