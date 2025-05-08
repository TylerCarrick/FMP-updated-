using UnityEngine;

public class EnemyColiision : MonoBehaviour
{
    public EnemyWeaponController ewc;
    public WeaponContoller wc;
    public GameObject HitParticle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && ewc.IsAttacking)
        {
            print("hit");
            
                Debug.Log(other.name);
            other.GetComponent<Animator>().SetTrigger("Hit");
            Instantiate(HitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);


        }
        else
        {
            print("miss");
        }
    }


}
