using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            takeDamage(50);
        }
    }

    public void takeDamage(float damageRatio)
    {
        healthAmount -= damageRatio;
        healthBar.fillAmount = healthAmount / 100f;
    }
}
