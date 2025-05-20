using UnityEngine;
using System.Collections;

public class WeaponContoller : MonoBehaviour
{
    public GameObject Sword;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public bool IsAttacking = false;
    public int AttackDamage = 1;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();  
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack)
            {
                SwordAttack();
            }

        }




    }

    public void SwordAttack()
    {
        IsAttacking = true;
        CanAttack = false;
        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        audioManager.PlaySFX(audioManager.swing);
        StartCoroutine(ResetAttackCoolDown());

    }

    IEnumerator ResetAttackCoolDown()
    {
        StartCoroutine(ResetAttack());
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1.0f);
        IsAttacking = false;
    }

}
