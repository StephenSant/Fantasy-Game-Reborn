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
    public Vector2 pitchMinMax = new Vector2 (-40,85);
    float yaw;
    float pitch;

    void Start()
    {

    }

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

        //Pulled back from player
        transform.position = player.position - transform.forward * distFromPlayer;
    }
}
public enum CameraMode
{
    gameplay,
    dialogue,
    cinematic
}