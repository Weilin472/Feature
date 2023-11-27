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

public class Enemy : MonoBehaviour
{
    public GameObject PreAttackObject;
    public GameObject AttackShowObject;
    public Slider slider;
    private AttackType attackType;
    private Color[] attackColorArray = new Color[] { Color.yellow, Color.blue, Color.red, Color.black };
    private int attackNum;
    private bool pressBlockKey;
    private int blockValue;

    // Start is called before the first frame update
    void Start()
    {
        attackType = AttackType.Null;
        blockValue = 0;
        InvokeRepeating("AttackAction", 2, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AttackAction()
    {
        StartCoroutine("RealAttack");
    }

    private IEnumerator RealAttack()
    {
        pressBlockKey = false;
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
        if (pressBlockKey==false)
        {
            Hurt();
        }
    }

    private void Hurt()
    {
        blockValue -= 10;
        if (blockValue<=0)
        {
            blockValue = 0;
        }
        slider.value = (float)blockValue / 100;
        Debug.Log(blockValue);
    }

    private void BlockSucessfully()
    {
        blockValue += 10;
        if (blockValue >= 100)
        {
            Debug.Log("win!");
            CancelInvoke();   
        }
        slider.value =(float)blockValue / 100;
        Debug.Log(blockValue);
    }

    private void ResetAttack()
    {
        attackType = AttackType.Null;

    }

    public void PressQ(InputAction.CallbackContext callback)
    {
        if (callback.performed&& attackType!=AttackType.Null)
        {
            pressBlockKey = true;
            if (attackType==AttackType.YellowAttack)
            {
                BlockSucessfully();
            }
            else
            {
                Hurt();
            }
            ResetAttack();
        }
    }

    public void PressW(InputAction.CallbackContext callback)
    {
        if (callback.performed && attackType != AttackType.Null)
        {
            pressBlockKey = true;
            if (attackType == AttackType.BlueAttack)
            {
                BlockSucessfully();
            }
            else
            {
                Hurt();
            }
            ResetAttack();
        }
    }

    public void PressE(InputAction.CallbackContext callback)
    {
        if (callback.performed && attackType != AttackType.Null)
        {
            pressBlockKey = true;
            if (attackType == AttackType.RedAttack )
            {
                BlockSucessfully();
            }
            else
            {
                Hurt();
            }
            ResetAttack();
        }
    }

    public void PressR(InputAction.CallbackContext callback)
    {
        if (callback.performed && attackType != AttackType.Null)
        {
            pressBlockKey = true;
            if (attackType == AttackType.BlackAttack)
            {
                BlockSucessfully();
            }
            else
            {
                Hurt();
            }
            ResetAttack();
        }

    }
}
