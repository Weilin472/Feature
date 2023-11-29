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

    private List<List<AttackType>> attackComboList = new List<List<AttackType>>();

    protected override void Init()
    {
        BlockValue = 50;
        Health = 100;
        attackType = AttackType.Null;
        Invoke("AttackAction", 2);
        InitCombo();
    }

    private void InitCombo()
    {
        List<AttackType> combo1 = new List<AttackType>();
        combo1.Add(AttackType.YellowAttack);
        combo1.Add(AttackType.YellowAttack);
        combo1.Add(AttackType.YellowAttack);
        combo1.Add(AttackType.BlueAttack);

        attackComboList.Add(combo1);

    }

    private void AttackAction()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
        StartCoroutine("RealAttack");
    }

    private IEnumerator RealAttack()
    {
        int attackNum = Random.Range(0, attackComboList.Count);
        List<AttackType> attackList = attackComboList[attackNum];
        //foreach (var item in attackDic)
        //{
        //    PreAttackObject.SetActive(true);
        //    PreAttackObject.GetComponent<MeshRenderer>().material.color = attackColorArray[(int)item.Key];
        //    yield return new WaitForSeconds(0.3f);
        //    PreAttackObject.SetActive(false);
        //    AttackShowObject.SetActive(true);
        //    AttackShowObject.GetComponent<MeshRenderer>().material.color = attackColorArray[(int)item.Key];
        //    attackType = item.Key;
        //    yield return new WaitForSeconds(item.Value);
        //    attackType = AttackType.Null;
        //    AttackShowObject.SetActive(false);
        //    if (!player.isDefensing && !player.pressBlockKey)
        //    {
        //        player.Hurt(15);
        //    }
        //    else if (player.isDefensing)
        //    {
        //        player.DefenseAttack();
        //    }
        //}

        for (int i = 0; i < attackList.Count; i++)
        {
            PreAttackObject.SetActive(true);
            PreAttackObject.GetComponent<MeshRenderer>().material.color = attackColorArray[(int)attackList[i]];
            yield return new WaitForSeconds(0.3f);
            PreAttackObject.SetActive(false);
            AttackShowObject.SetActive(true);
            AttackShowObject.GetComponent<MeshRenderer>().material.color = attackColorArray[(int)attackList[i]];
            attackType = attackList[i];
            yield return new WaitForSeconds(0.3f);
            attackType = AttackType.Null;
            AttackShowObject.SetActive(false);
            if (!player.isDefensing && !player.pressBlockKey)
            {
                player.Hurt(15);
            }
            else if (player.isDefensing)
            {
                player.DefenseAttack();
            }
        }

        //player.pressBlockKey = false;
        //attackNum = Random.Range(0, 4);
        //PreAttackObject.SetActive(true);
        //PreAttackObject.GetComponent<MeshRenderer>().material.color = attackColorArray[attackNum];
        //yield return new WaitForSeconds(0.3f);
        //PreAttackObject.SetActive(false);
        //AttackShowObject.SetActive(true);
        //AttackShowObject.GetComponent<MeshRenderer>().material.color = attackColorArray[attackNum];
        //attackType = (AttackType)attackNum;
        //yield return new WaitForSeconds(0.3f);
        //attackType = AttackType.Null;
        //AttackShowObject.SetActive(false);

        //if (!player.isDefensing && !player.pressBlockKey)
        //{
        //    player.Hurt(15);
        //}
        //else if (player.isDefensing)
        //{
        //    player.DefenseAttack();
        //}
    }

    public void ResetAttackType()
    {
        attackType = AttackType.Null;

    }

    public void BeBlocked()
    {
        BlockValue += 10;
        //if (BlockValue >= 100)
        //{
        //    InvokeRepeating("AttackAction", 7, 2);
        //}
    }

   

    public override void Hurt(float damage)
    {
        Health -= damage;
    }

    protected override void Win()
    {
        base.Win();
        CancelInvoke();
        player.GetComponent<PlayerInput>().enabled = false;
    }
}
