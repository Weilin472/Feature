using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class BaseCharacter : MonoBehaviour
{
  
    protected bool isPlayer;

    private float health;
    private float blockValue;
    public Slider healthSlider;
    public Slider blockValueSlider;

    protected float Health { get => health; set
        { health = value;
            if (health >= 100)
            {
                health = 100;
            }
            else if (health <= 0)
            {
                health = 0;
                if (isPlayer)
                {
                    Lost();
                }
                else
                {
                    Win();
                }
            }
            healthSlider.value = health / 100; } 
    
    }

    protected float BlockValue { get => blockValue; set 
        { blockValue = value;
            if (blockValue>=100)
            {
                blockValue = 100;
                Still();
            }
            else if (blockValue<=0)
            {
                blockValue = 0;
            }
          blockValueSlider.value = blockValue / 100; 
        }
    }

    protected virtual void Init()
    {
        Health = 100;
        BlockValue = 50;
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

 

    public virtual void Hurt(float damage)
    {
        Health -= damage;
    }

    protected virtual void Lost()
    {
        UIManager.Instance.ShowLostUI();
    }

    protected virtual void Win()
    {
        UIManager.Instance.ShowWinUI();
    }

    protected void Still()
    {
        GetComponent<MeshRenderer>().material.color = Color.white;
        BlockValue = 50;
        if (isPlayer)
        {
            GetComponent<PlayerInput>().enabled = false;
            Invoke("ResetAfterStill", 5);
        }
        else
        {
            CancelInvoke();
            Invoke("AttackAction",5);
        }
    }

}
