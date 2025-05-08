using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public WeaponContoller wc;
    public EnemyWeaponController ewc;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public int currentHealth;
    public int maxHealth;
    public HealthBar healthBar;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
   
        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        
   
    
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (currentHealth <= 0)
        {
           Death();
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemySword" && ewc.IsAttacking)
        {
            print("Hit");
            currentHealth--;
            healthBar.SetHealth(currentHealth);
        }

    }

    public void Death()
    {
        SceneManager.LoadScene(2);
        Cursor.lockState = CursorLockMode.None;
    }
    


    

}
