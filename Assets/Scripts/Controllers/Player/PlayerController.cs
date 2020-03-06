using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public CharacterController charController;
    //public Rigidbody body;
    public CameraController playerCam;
    public InventoryController inventory;

    [Header("Health")]
    public int health;
    public int maxHealth = 100;
    public Material material;
    float colTime = 1;

    [Header("Physics")]
    Vector3 velocity;
    public float gravity = -9.81f;

    [Header("Movement")]
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    public LayerMask whatIsGround;
    public float groundCheck;
    private Vector2 input;

    [Header("Combat")]
    public float attackArea = 1;

    void Start()
    {
        health = maxHealth;
        inventory = GetComponent<InventoryController>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        //Flashing red timer
        material.color = Color.red;
        colTime = .15f;
        if (colTime <= 0) { colTime = 0; material.color = Color.blue; }
        else { colTime -= Time.deltaTime; }

        if (health <= 0) { Die(); }
    }

    bool showInv = false;
    void Update()
    {
        //Stops gravity pulling the player down
        if (IsGrounded() && velocity.y < 0)
        {
            velocity.y = -2;
        }

        if (Input.GetButton("Jump") && IsGrounded())
        {
            Jump();
        }

        Gravity();

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKey(KeyCode.LeftShift) && IsGrounded())
        {
            Run();
        }
        else
        {
            Walk();

            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            try
            {
                playerCam.InteractRay().collider.GetComponent<IInteractable>().Interact(gameObject);
            }
            catch
            { return; }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            UIManager.instance.ShowInventory(!showInv);
            showInv = !showInv;
        }
    }

    #region Movement
    void Walk()
    {
        //Player faces forwards away from the camera
        transform.eulerAngles = Vector3.up * playerCam.transform.rotation.eulerAngles.y;

        //uhh...
        Vector3 camF = playerCam.transform.forward;
        Vector3 camR = playerCam.transform.right;

        //umm...
        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;

        //something, something multiply by Time.deltaTime and walkspeed
        Vector3 newInput = (camF * input.y + camR * input.x) * Time.deltaTime * (walkSpeed * 100);//the multiply by 100 is so its not so slow

        //Slap these numbers in to change its velocity
        velocity.x = newInput.x;
        velocity.z = newInput.z;

    }
    void Run()
    {
        //Player faces forwards away from the camera
        transform.eulerAngles = Vector3.up * playerCam.transform.rotation.eulerAngles.y;

        //uhh...
        Vector3 camF = playerCam.transform.forward;
        Vector3 camR = playerCam.transform.right;

        //umm...
        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;

        //something, something multiply by Time.deltaTime and runSpeed
        Vector3 newInput = (camF * input.y + camR * input.x) * Time.deltaTime * (runSpeed * 100);//the multiply by 100 is so its not so slow

        //Slap these numbers in to change its velocity
        velocity.x = newInput.x;
        velocity.z = newInput.z;
    }
    void Jump()
    {
        //v = sqrt(h * -2 * g)
        velocity.y = Mathf.Sqrt(jumpForce * -2 * gravity);
    }
    #endregion

    #region Physics
    bool IsGrounded()
    {
        bool grounded = Physics.Raycast(transform.position, Vector3.down, groundCheck, whatIsGround);
        return grounded;
    }
    void Gravity()
    {
        // Vy = 1/2g * t^2
        velocity.y += gravity * Time.deltaTime;
        charController.Move(velocity * Time.deltaTime);
    }
    #endregion

    #region Combat
    void Attack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + playerCam.transform.forward, attackArea, gameObject.layer);
        foreach (Collider collider in hitColliders)
        {
            if (collider.GetComponent<EnemyController>())
            {
                collider.GetComponent<EnemyController>().TakeDamage(inventory.weapon.damage);
            }
        }

    }
    #endregion

    void Die()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        //Ground Check
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector3.down * groundCheck);

        //Attack Area
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward, attackArea);
    }
}
