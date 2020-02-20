using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject player;
    public CameraController cameraInUse;

    private void Awake()
    {
        #region Singleton
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        #endregion
    }

    void Start()
    {
        cameraInUse = Camera.main.GetComponent<CameraController>();
        player.GetComponent<PlayerController>().playerCam = cameraInUse;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
