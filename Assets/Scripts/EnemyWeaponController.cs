using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class EnemyWeaponController : MonoBehaviour
{
    public GameObject Sword;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public bool IsAttacking = false;
    public Animator anim;

    void Start()
    {

    }

    // Update is called once per frame
   
    public void SwordAttack()
    {
        IsAttacking = true;
        CanAttack = false;
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


