using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody body;
    public CameraController playerCam;
    public InventoryController inventory;

    [Header("Health")]
    public int health;
    public int maxHealth = 100;
    public Material material;
    float colTime = 1;

    [Header("Movement")]
    public float walkSpeed;
    public float runSpeed;
    public float jumpHeight;
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

    void Update()
    {

    }

    void FixedUpdate()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetButton("Jump") && IsGrounded())
        {
            Jump();
        }
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
        body.velocity = new Vector3(newInput.x, body.velocity.y, newInput.z);
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
        body.velocity = new Vector3(newInput.x, body.velocity.y, newInput.z);
    }

    void Jump()
    {
        body.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }

    bool IsGrounded()
    {
        bool grounded = Physics.Raycast(transform.position, Vector3.down, groundCheck, whatIsGround);
        return grounded;
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
