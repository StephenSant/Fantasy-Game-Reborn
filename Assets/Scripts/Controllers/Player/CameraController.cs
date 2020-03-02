using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CameraMode cameraMode = CameraMode.gameplay;

    [Header("Gameplay Mode")]
    public Transform player;
    public float mouseSensitivity = 10;
    public float distFromPlayer = 2;
    public Vector2 pitchMinMax = new Vector2(-40, 85);
    float yaw;
    float pitch;
    bool runRay;
    public LayerMask playerLayer;
    public float rayRange;

    void LateUpdate()
    {
        if (cameraMode == CameraMode.gameplay)
        {
            PlayerCamera();
        }
    }
    public void PlayerCamera()
    {
        //Getting the pitch and yaw
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        //Rotating
        Vector3 targetRotation = new Vector3(pitch, yaw);
        transform.eulerAngles = targetRotation;

        //Pull back from player
        transform.position = player.position - transform.forward * distFromPlayer;
    }

    private void FixedUpdate()
    {
        //dont run the interacting ray if not in game mode
        runRay = cameraMode == CameraMode.gameplay;


    }

    public RaycastHit InteractRay()
    {
        if (runRay)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.forward, out hit, rayRange, ~playerLayer);
            return hit;
        }
        return new RaycastHit();
    }
}
public enum CameraMode
{
    gameplay,
    dialogue,
    cinematic
}