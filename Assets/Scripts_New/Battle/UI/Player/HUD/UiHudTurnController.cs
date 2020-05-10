﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI.Player
{
  public class UiHudTurnController : MonoBehaviour
  {
    private void Awake()
    {
      UiPlayerHUD = GetComponentInParent<IUiPlayerHUD>();
      Image = GetComponent<Image>();
    }

    private IUiPlayerHUD UiPlayerHUD;
    private Image Image;

    [SerializeField]
    private Sprite IsTurnSprite;
    [SerializeField]
    private Sprite IsNotTurnSprite;

    private void Update()
    {
      if (UiPlayerHUD.IsPlayerTurn)
      {
        Image.sprite = IsTurnSprite;
      } else
      {
        Image.sprite = IsNotTurnSprite;
      }
    }
  }
}