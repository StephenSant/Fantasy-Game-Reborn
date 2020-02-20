using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float jumpHeight;

    private Vector2 input;

    [Header("References")]
    public Rigidbody rbody;
    public CameraController playerCam;

    void Start()
    {

    }

    void Update()
    {
        Walk();
    }

    public void Walk()
    {
        //Gets input
        input = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

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

        //I don't really know how this works
        transform.position += (camF*input.y + camR*input.x)*Time.deltaTime*walkSpeed;
    }
}
