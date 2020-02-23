﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody body;
    public CameraController playerCam;

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

    void Start()
    {
        health = maxHealth;
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
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) && IsGrounded())
        {
            Run();
        }
        else
        {
            Walk();
        }

        if (Input.GetButton("Jump"))
        {
            Jump();
        }
    }

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

        Vector3 newInput = (camF * input.y + camR * input.x) * Time.deltaTime * (walkSpeed * 100);

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

        Vector3 newInput = (camF * input.y + camR * input.x) * Time.deltaTime * (runSpeed * 100);

        //Slap these numbers in to change its velocity
        body.velocity = new Vector3(newInput.x, body.velocity.y, newInput.z);
    }

    void Jump()
    {
        if (IsGrounded())
        {
            body.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }
    bool IsGrounded()
    {
        bool grounded = Physics.Raycast(transform.position, Vector3.down * groundCheck, whatIsGround);
        return grounded;
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        //GroundCheck
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * groundCheck);
    }
}
