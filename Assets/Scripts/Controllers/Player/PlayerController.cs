using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public CharacterController charController;
    public CameraController playerCam;
    public InventoryController inventory;

    [Header("Health")]
    public int health;
    public int maxHealth = 100;

    [Header("Physics")]
    Vector3 velocity;
    public float gravity = -9.81f;

    [Header("Movement")]
    public float walkSpeed;
    public float runSpeed;
    float moveSpeed;
    public float jumpForce;
    public LayerMask whatIsGround;
    public float groundCheck;
    private Vector2 inputDir;

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
        if (health <= 0) { Die(); }
    }

    bool showInv = false;
    void Update()
    {
        inputDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKey(KeyCode.LeftShift) && IsGrounded())
        {
            //Run
            moveSpeed = runSpeed;
        }
        else
        {
            //Walk
            moveSpeed = walkSpeed;

            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }
            else if (Input.GetKeyDown(KeyCode.LeftControl) && inputDir != Vector2.zero)
            {
                //Dodge
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
    }

    private void FixedUpdate()
    {
        Move(moveSpeed);
    }

    #region Movement
    void Move(float moveSpeed)
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
        Vector3 newInput = (camF * inputDir.y + camR * inputDir.x) * Time.deltaTime * (moveSpeed * 100);//the multiply by 100 is so its not so slow

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
            //if the collider is an enemy 
            //and
            //if the collider is the view collider
            if (collider.GetComponent<EnemyController>())
            {
                collider.GetComponent<EnemyController>().TakeDamage(inventory.weapon.damage);//deal damage
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
