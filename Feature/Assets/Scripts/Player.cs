using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player : BaseCharacter
{
    public Enemy enemy;
    public bool pressBlockKey;



    protected override void Init()
    {
        health = 100;
        blockValue = 50;
        healthSlider.value = health / 100;
        blockValueSlider.value = blockValue / 100;
        isPlayer = true;
    }

    private void BlockSucessfully()
    {
        enemy.BeBlocked();
    }

  

   

    public void PressQ(InputAction.CallbackContext callback)
    {
        if (callback.performed && enemy.attackType != AttackType.Null)
        {
            pressBlockKey = true;
            if (enemy.attackType == AttackType.YellowAttack)
            {
                BlockSucessfully();
            }
            else
            {
                Hurt();
            }
           enemy.ResetAttack();
        }
    }

    public void PressW(InputAction.CallbackContext callback)
    {
        if (callback.performed && enemy.attackType != AttackType.Null)
        {
            pressBlockKey = true;
            if (enemy.attackType == AttackType.BlueAttack)
            {
                BlockSucessfully();
            }
            else
            {
                Hurt();
            }
            enemy.ResetAttack();
        }
    }

    public void PressE(InputAction.CallbackContext callback)
    {
        if (callback.performed && enemy.attackType != AttackType.Null)
        {
            pressBlockKey = true;
            if (enemy.attackType == AttackType.RedAttack)
            {
                BlockSucessfully();
            }
            else
            {
                Hurt();
            }
            enemy.ResetAttack();
        }
    }

    public void PressR(InputAction.CallbackContext callback)
    {
        if (callback.performed && enemy.attackType != AttackType.Null)
        {
            pressBlockKey = true;
            if (enemy.attackType == AttackType.BlackAttack)
            {
                BlockSucessfully();
            }
            else
            {
                Hurt();
            }
            enemy.ResetAttack();
        }

    }

    protected override void Lost()
    {
        base.Lost();
        enemy.CancelInvoke();
    }
}
