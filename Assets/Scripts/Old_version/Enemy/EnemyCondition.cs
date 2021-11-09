using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCondition : MonoBehaviour
{
    [SerializeField] GameObject healthBar;
   
    [SerializeField] Slider slider;

    [SerializeField] int health = 2;

    [SerializeField] EnemyPhaseEnum enemyPhase;
 

    internal int Health
    { get { return health; }
      set
        {
            health = value;

            if (health == 0)
            {
                healthBar.SetActive(false);
                EnemyIsDead();
            }

            else
            {
                slider.value = health == 1 ? 1 : 2;
            }
            
        }
    }


    void EnemyIsDead()
    {
        PhaseController.NextPhase(enemyPhase);
    }
}
