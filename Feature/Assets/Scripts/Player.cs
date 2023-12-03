using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class Player : BaseCharacter
{
    public Enemy enemy;
    public GameObject attackEffect;
    public GameObject blockEffect;
    public GameObject defendEffect;

    //this variable would turn into true if the player press the block keys, the player would hurt if this variable is false in each attack from the enemy
    public bool pressBlockKey;

    //the player can not keep attacking, the player can only attack 1 second after the last attack
    private float lastAttackTime;
    private float attackTimeGap=1f;


    //prevent the player keep pressing the keys of block to block the attack, the player can only use block  0.3 second after the last block
    private float lastBlockTime;
    private float blockTimeGap = 0.3f;

    public bool isDefensing = false;


    protected override void Init()
    {
        Health = 100;
        PostureValue = 50;
        isPlayer = true;
        lastAttackTime = -0.5f;
    }

    //if the player block the attacks successfully, the posturevalue of the player would decrease, and the posturevalue of the enemy would increase
    private void BlockSucessfully()
    {
        enemy.BeBlocked();
        PostureValue -= 10;
    }

    //defense can defend the attacks, but the posturevalue would increase
    public void DefenseAttack()
    {
        PostureValue += 10;
    }

    private void ResetAfterStill()
    {
        GetComponent<MeshRenderer>().material.color = Color.gray;
        GetComponent<PlayerInput>().enabled = true;
    }

  
    
    private void JudgeBlock(AttackType attacktype)
    {
        if (enemy.attackType!=AttackType.Null&&!isDefensing&&Time.time-lastBlockTime>=blockTimeGap)
        {
            blockEffect.SetActive(true);
            pressBlockKey = true;//if the pressblockkey variable is false and he is not defending after the attack of the enemy, the player would get hurt
            if (enemy.attackType == attacktype)
            {
                BlockSucessfully();
            }
            else
            {
                Hurt(15);
            }
            enemy.ResetAttackType();//reset the attacktype of the enemy to NUll, so the player can only block once in each attack of the enemy
        }
        lastBlockTime = Time.time;
    }


    public void PressQ(InputAction.CallbackContext callback)
    {
        if (callback.performed )
        {
            JudgeBlock(AttackType.YellowAttack);
        }
        else if (callback.canceled)
        {
            blockEffect.SetActive(false);
        }
    }

    public void PressW(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            JudgeBlock(AttackType.BlueAttack);
        }
        else if (callback.canceled)
        {
            blockEffect.SetActive(false);
        }
    }

    public void PressE(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            JudgeBlock(AttackType.RedAttack);
        }
        else if (callback.canceled)
        {
            blockEffect.SetActive(false);
        }
    }

    public void PressR(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            JudgeBlock(AttackType.BlackAttack);
        }
        else if (callback.canceled)
        {
            blockEffect.SetActive(false);
        }

    }

    public void HandleAttack(InputAction.CallbackContext callback)
    {
        if (callback.performed&&Time.time-lastAttackTime>=attackTimeGap&&enemy.attackType==AttackType.Null)
        {
            enemy.Hurt(5);
            lastAttackTime = Time.time;
            attackEffect.SetActive(true);
        }
        if (callback.canceled)
        {
            attackEffect.SetActive(false);
        }
    }

    public void HandleDefense(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            isDefensing = true;
            defendEffect.SetActive(true);
        }
        else if (callback.canceled)
        {
            isDefensing = false;
            defendEffect.SetActive(false);
        }
    }

    protected override void Lost()
    {
        base.Lost();
        enemy.canAttack=false;
        enemy.StopAllCoroutines();
        GetComponent<PlayerInput>().enabled = false;
    }


}
