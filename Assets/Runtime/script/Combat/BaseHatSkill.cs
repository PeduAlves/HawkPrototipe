using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHatSkill : MonoBehaviour
{
    public BaseRevolver gun;
    public PlayerInputs inputs;
    public float hatSkillCooldown = 10f;
    public float WaitForSeconds = 0.2f;
    private bool isHatSkill = false;


    public virtual void HatSkill(){
        
        if(inputs.GetHatSkillInput() && !isHatSkill){

            StartCoroutine(hatSkillCourotine());
        }
    }
    protected virtual IEnumerator hatSkillCourotine(){
        
        isHatSkill = true;
        gun.balasNoTambor = gun.qntBullet;
        for(int i = 0; i < gun.qntBullet; i++){

            yield return new WaitForSeconds(WaitForSeconds);
            gun.PlayerShoot();
        }
        gun.balasNoTambor = gun.qntBullet;
        yield return new WaitForSeconds(hatSkillCooldown);
        isHatSkill = false;
    }
}
