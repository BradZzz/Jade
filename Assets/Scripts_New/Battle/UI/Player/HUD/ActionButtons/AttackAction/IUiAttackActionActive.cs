﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI.Player
{
  public interface IUiAttackActionActive
  {
    Outline Outline { get; }
    bool Active { get; }

    void SetActive(bool active);
  }
}