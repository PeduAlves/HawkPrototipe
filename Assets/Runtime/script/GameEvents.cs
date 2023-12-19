using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
  public static GameEvents Instance;
  private void Awake()=>Instance = this;

  public event Action <int, int> TakeHit;

  public void TakeHitEvent(int ammountDamage, int id) => TakeHit?.Invoke(ammountDamage, id);
}
