﻿using Battle.UI.Utils;
using UnityEngine;

namespace Battle.UI.Jewel
{
  /// <summary>
  ///     Main components of an UI card.
  /// </summary>
  public interface IUiJewelComponents
  {
    Camera MainCamera { get; }
    MeshRenderer[] MRenderers { get; }
    MeshRenderer MRenderer { get; }
    SpriteRenderer[] Renderers { get; }
    SpriteRenderer Renderer { get; }
    Collider Collider { get; }
    Rigidbody Rigidbody { get; }
    IMouseInput Input { get; }
    MonoBehaviour MonoBehavior { get; }
    GameObject gameObject { get; }
    Transform transform { get; }
  }
}