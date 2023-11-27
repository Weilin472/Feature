using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseCharacter : MonoBehaviour
{
  
    protected bool isPlayer;

    protected float health;
    protected float blockValue;
    public Slider healthSlider;
    public Slider blockValueSlider;

    protected virtual void Init()
    {
        health = 100;
        blockValue = 50;
        healthSlider.value = health / 100;
        blockValueSlider.value = blockValue / 100;
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Hurt()
    {
        blockValue += 10;
        if (blockValue >= 100)
        {
            blockValue = 100;
        }
        health -= 10;
        if (health <= 0)
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
        healthSlider.value = health / 100;
        blockValueSlider.value = blockValue / 100;
    }

    protected virtual void Lost()
    {
        UIManager.Instance.ShowLostUI();
    }

    protected virtual void Win()
    {
        UIManager.Instance.ShowWinUI();
    }

}
