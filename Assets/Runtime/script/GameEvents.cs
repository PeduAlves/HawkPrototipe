using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
  public static GameEvents Instance;
  private void Awake()=>Instance = this;

  public event Action <int, int> EnemyTakeDamage;
  
  public void EnemyTakeDamageEvent(int ammountDamage, int id) => EnemyTakeDamage?.Invoke(ammountDamage, id);
}
