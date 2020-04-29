﻿using System.Collections;
using System.Collections.Generic;
using Battle.Model.Jewel;
using UnityEngine;

namespace Battle.UI.ModelJewel.Mechanics
{
  public class BaseJewelMechanics : MonoBehaviour
  {
    /// <summary>
    ///     Player reference.
    /// </summary>
    protected IRuntimeJewel Jewel { get; }

    protected BaseJewelMechanics(IRuntimeJewel jewel)
    {
      Jewel = jewel;
    }
  }
}