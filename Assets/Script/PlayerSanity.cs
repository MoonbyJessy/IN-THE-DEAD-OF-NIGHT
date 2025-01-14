using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSanity : MonoBehaviour
{
    public int maxSanity = 1500;
    public int currentSanity;
   // public SanityBar sanityBar;
   // public EnemyAIControl enemyAI;

    public Slider slider;
    private bool isDead;


    void Start()
    {
        currentSanity = maxSanity;
      //  sanityBar.SetMaxSanity(maxSanity);
    }


    void Update()
    {
        slider.value = currentSanity;

        if (currentSanity <= 0 && !isDead)
        {
            isDead = true;
            SceneManager.LoadSceneAsync(2);
            Debug.Log("Dead");
        }
    }
    public void IncreaseSanity(int increase)
    {
        currentSanity += increase;

    }
    public void TakeDamage(int damage)
    {
        currentSanity -= damage;
      //  sanityBar.SetSanity(currentSanity);

    }

}
