﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Model.MoheModel
{
  public class Mohe : IMohe
  {
    public Mohe (IMoheData Data, List<IAbility> Abilities, IMoheStats Stats)
    {
      data = Data;
      abilities = Abilities;
      stats = Stats;
    }

    public IMoheData Data => data;
    public List<IAbility> Abilities => abilities;
    public IMoheStats Stats => stats;

    private IMoheData data;
    private List<IAbility> abilities;
    private IMoheStats stats;
  }
}