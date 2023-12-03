using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum AttackType
{
    YellowAttack,//can press Q to block
    BlueAttack,//can press w to block
    RedAttack,//can press e to block
    BlackAttack,//can press r to block
    Null
}

public class Enemy : BaseCharacter
{
    public Player player;

    public GameObject PreAttackObject;//indicate what the attacktype it will be
    public GameObject AttackShowObject;
    public AttackType attackType;
    private Color[] attackColorArray = new Color[] { Color.yellow, Color.blue, Color.red, Color.black };
    private int attackNum;

    public bool canAttack;


    private List<AttackComboList<AttackType, float>> attackComboLists = new List<AttackComboList<AttackType, float>>();//store different combo

    protected override void Init()
    {
        canAttack = true;
        PostureValue = 50;
        Health = 100;
        attackType = AttackType.Null;
        InitCombo();
        Invoke("AttackAction", 2);
    }

    private void InitCombo()
    { 

        AttackComboList<AttackType, float> combo1 = new AttackComboList<AttackType, float>();
        combo1.Add(AttackType.YellowAttack, 0.3f);//the float is the time that between the preattack and the real attack, so some attacks are fast, and some are slow
        combo1.Add(AttackType.YellowAttack, 0.3f);
        combo1.Add(AttackType.YellowAttack, 0.3f);
        combo1.Add(AttackType.BlueAttack, 0.6f);

        AttackComboList<AttackType, float> combo2 = new AttackComboList<AttackType, float>();
        combo2.Add(AttackType.YellowAttack, 0.6f);
        combo2.Add(AttackType.BlueAttack, 0.6f);
        combo2.Add(AttackType.RedAttack, 0.6f);
        combo2.Add(AttackType.BlackAttack, 0.6f);

        AttackComboList<AttackType, float> combo3 = new AttackComboList<AttackType, float>();
        combo3.Add(AttackType.YellowAttack, 0.2f);
        combo3.Add(AttackType.BlueAttack, 0.2f);
        combo3.Add(AttackType.RedAttack, 0.2f);
        combo3.Add(AttackType.BlackAttack, 0.2f);

        //there are three types of combo rn
        attackComboLists.Add(combo1);
        attackComboLists.Add(combo2);
        attackComboLists.Add(combo3);
    }

    private void AttackAction()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
        canAttack = true;
        StartCoroutine("RealAttack");

    }

    private IEnumerator RealAttack()
    {    
        while (canAttack)
        {
                int attackNum = Random.Range(0, attackComboLists.Count);//decide which combo is going to use
                AttackComboList<AttackType, float> currentCombo = attackComboLists[attackNum];
                for (int i = 0; i < currentCombo.Count; i++)
                {
                    if (!canAttack)
                    {
                        break;
                    }
                    AttackType currentAttackType = currentCombo.GetFirstValue(i);
                    float timeBeforeAttack = currentCombo.GetSecondValue(i);
                    PreAttackObject.SetActive(true);//the preattackobject is used to indicate what the attacktype it is.
                    PreAttackObject.GetComponent<MeshRenderer>().material.color = attackColorArray[(int)currentAttackType];
                    yield return new WaitForSeconds(timeBeforeAttack);
                    player.pressBlockKey = false;
                    PreAttackObject.SetActive(false);
                    AttackShowObject.SetActive(true);
                    AttackShowObject.GetComponent<MeshRenderer>().material.color = attackColorArray[(int)currentAttackType];
                    attackType = currentAttackType;
                    yield return new WaitForSeconds(0.3f);//the player can block the attacks in this 0.3f
                    attackType = AttackType.Null;//the player can not block the attacks if the attacktype is reset to Null
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
                float nextAttackTime = Random.Range(3, 5);
                yield return new WaitForSeconds(nextAttackTime);
                     
        }   
    }

    public void ResetAttackType()
    {
        attackType = AttackType.Null;

    }

    public void BeBlocked()
    {
        PostureValue += 10;
    }

   

    public override void Hurt(float damage)
    {
        Health -= damage;
    }

    protected override void Win()
    {
        base.Win();
        canAttack = false;
        StopAllCoroutines();
        player.GetComponent<PlayerInput>().enabled = false;
    }
}
