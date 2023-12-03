using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

//the parent class of the enemy and the player
public class BaseCharacter : MonoBehaviour
{
  
    protected bool isPlayer;

    private float health;
    private float postureValue;
    public Slider healthSlider;
    public Slider postureSlider;

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

    //if the posturevalue reach to 100, the character would be still
    protected float PostureValue { get => postureValue; set 
        { postureValue = value;
            if (postureValue>=100)
            {
                postureValue = 100;
                Still();
            }
            else if (postureValue<=0)
            {
                postureValue = 0;
            }
          postureSlider.value = postureValue / 100; 
        }
    }

    protected virtual void Init()
    {
        Health = 100;
        PostureValue = 50;
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


    //if the character is stilled, it would become white, and the posturevalue reset to 50
    protected void Still()
    {
        GetComponent<MeshRenderer>().material.color = Color.white;
        PostureValue = 50;
        if (isPlayer)
        {
            GetComponent<PlayerInput>().enabled = false;
            Invoke("ResetAfterStill", 5);
        }
        else
        {
            GetComponent<Enemy>().canAttack = false;
            Invoke("AttackAction",5);
        }
    }

}
