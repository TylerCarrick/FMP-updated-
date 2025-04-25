using UnityEngine;

public class EnemyColiision : MonoBehaviour
{
    public WeaponContoller wc;
    public GameObject HitParticle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && wc.IsAttacking)
        {
            Debug.Log(other.name);
            other.GetComponent<Animator>().SetTrigger("Hit");
            Instantiate(HitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);


        }

    }


}
