using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum AttackType
{
    YellowAttack,
    BlueAttack,
    RedAttack,
    BlackAttack,
    Null
}

public class Enemy : BaseCharacter
{
    public Player player;

    public GameObject PreAttackObject;
    public GameObject AttackShowObject;
    public AttackType attackType;
    private Color[] attackColorArray = new Color[] { Color.yellow, Color.blue, Color.red, Color.black };
    private int attackNum;


    protected override void Init()
    {
        blockValue = 50;
        health = 100;
        healthSlider.value = health / 100;
        blockValueSlider.value = blockValue / 100;
        attackType = AttackType.Null;
        InvokeRepeating("AttackAction", 2, 2);
    }

    private void AttackAction()
    {
        StartCoroutine("RealAttack");
    }

    private IEnumerator RealAttack()
    {
        player.pressBlockKey = false;
        attackNum = Random.Range(0, 4);
        PreAttackObject.SetActive(true);
        PreAttackObject.GetComponent<MeshRenderer>().material.color = attackColorArray[attackNum];
        yield return new WaitForSeconds(0.3f);
        PreAttackObject.SetActive(false);
        AttackShowObject.SetActive(true);
        AttackShowObject.GetComponent<MeshRenderer>().material.color = attackColorArray[attackNum];
        attackType = (AttackType)attackNum;
        yield return new WaitForSeconds(0.3f);
        attackType = AttackType.Null;
        AttackShowObject.SetActive(false);
        if (player.pressBlockKey == false)
        {
           player.Hurt();
        }
    }

    public void ResetAttack()
    {
        attackType = AttackType.Null;

    }

    public void BeBlocked()
    {
        blockValue += 10;
        if (blockValue >= 100)
        {
            blockValue = 100;
        }
        blockValueSlider.value = blockValue / 100;
    }

    protected override void Win()
    {
        base.Win();
        CancelInvoke();
    }
}
