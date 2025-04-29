using System.Collections;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    PlayerMovement pm;

    public float dashSpeed;
    public float dashTime;

    private void Start()
    {
       pm = GetComponent<PlayerMovement>();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(Dash());
        }
    }
    
    IEnumerator Dash()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
          transform.Translate(Vector3.forward * dashSpeed);

            yield return null;
        }
    }
}
