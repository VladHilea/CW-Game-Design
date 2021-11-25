using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public Text playerHealthUI;
    public GameObject shield;          
    //public GameObject m_ExplosionPrefab;
    //public AudioSource m_ExplosionAudio;          
    private float currentHealth;  
    private bool dead;            

    void Start() {
        playerHealthUI.text = gameObject.name + ": " + startingHealth.ToString() + "%";
    }

    private void OnEnable()
    {
        currentHealth = startingHealth;
        dead = false;
    }

    public void TakeDamage(int amount)
    {
        if(shield.activeInHierarchy) {
            amount = 0;
        }

        currentHealth -= amount;
        playerHealthUI.text = gameObject.name + ": " + currentHealth.ToString() + "%";

        if(currentHealth < 0)
            playerHealthUI.text = gameObject.name + ": 0%";

        if(currentHealth <= 0 && !dead)
            OnDeath();
        
        Debug.Log(amount + " damage dealt to " + gameObject.name);
    }

    private void OnDeath()
    {
        dead = true;
        //gameObject.SetActive(false);
         StartCoroutine(Text());

            IEnumerator Text()  //  <-  its a standalone method
            {
                yield return new WaitForSeconds(1f);
               
                SceneManager.LoadScene("Menu Scene");

                
            }
        
    }
}