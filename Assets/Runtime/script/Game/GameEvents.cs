using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
  public static GameEvents Instance;
  private void Awake()=>Instance = this;

  public event Action <int, int> TakeHit;
  public event Action <int> PlayerTakeDamage;
  public event Action <int> PlayerHeal;

  public void TakeHitEvent(int ammountDamage, int id) => TakeHit?.Invoke(ammountDamage, id);
  public void PlayerTakeDamageEvent(int ammountDamage) => PlayerTakeDamage?.Invoke(ammountDamage);
  public void PlayerHealEvent(int ammountHeal) => PlayerHeal?.Invoke(ammountHeal);
  
}
