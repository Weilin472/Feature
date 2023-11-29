using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class Player : BaseCharacter
{
    public Enemy enemy;
    public GameObject attackEffect;

    public bool pressBlockKey;

    private float lastAttackTime;
    private float attackTimeGap=1.5f;

    public bool isDefensing = false;


    protected override void Init()
    {
        Health = 100;
        BlockValue = 50;
        isPlayer = true;
        lastAttackTime = -0.5f;
    }

    private void BlockSucessfully()
    {
        enemy.BeBlocked();
        BlockValue -= 10;
    }

    public void DefenseAttack()
    {
        BlockValue += 10;
    }

    private void ResetAfterStill()
    {
        GetComponent<MeshRenderer>().material.color = Color.gray;
        GetComponent<PlayerInput>().enabled = true;
    }

  
    private void JudgeBlock(AttackType attacktype)
    {
        if (enemy.attackType!=AttackType.Null&&!isDefensing)
        {
            pressBlockKey = true;
            if (enemy.attackType == attacktype)
            {
                BlockSucessfully();
            }
            else
            {
                Hurt(15);
            }
            enemy.ResetAttackType();
        }
    }
   

    public void PressQ(InputAction.CallbackContext callback)
    {
        if (callback.performed )
        {
            JudgeBlock(AttackType.YellowAttack);
        }
    }

    public void PressW(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            JudgeBlock(AttackType.BlueAttack);
        }
    }

    public void PressE(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            JudgeBlock(AttackType.RedAttack);
        }
    }

    public void PressR(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            JudgeBlock(AttackType.BlackAttack);
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
        }
        else if (callback.canceled)
        {
            isDefensing = false;
        }
    }

    protected override void Lost()
    {
        base.Lost();
        enemy.CancelInvoke();
        GetComponent<PlayerInput>().enabled = false;
    }


}
