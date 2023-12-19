using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public int ID { get;}
    void TakeHit(int ammountDamage , int id);
}
