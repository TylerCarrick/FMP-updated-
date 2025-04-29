using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public EnemyPatrol ep;
    public WeaponContoller wc;
    public int currentHealth;
    public int maxHealth;
    public HealthBar healthBar;


    void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {

        if (currentHealth <= 0)
        {
            StartCoroutine (Death());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dagger" && wc.IsAttacking)
        {
            currentHealth--;
            healthBar.SetHealth(currentHealth);
        }

    } 

    public IEnumerator Death()
    {
        ep.enabled = false;
        GetComponent<Animator>().SetTrigger("Death");
        yield return new WaitForSeconds(7);
        Destroy(gameObject);


    }

}
