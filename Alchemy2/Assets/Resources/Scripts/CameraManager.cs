using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour {

    private GameObject player;
    private CinemachineVirtualCamera currentCamera;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Changes the priority of the virtual camera attached to the the current game object
    /// </summary>
    /// <param name="ChangeCameraPriority"></param>
    public void ChangeCameraPriority(RaycastHit detectInteractableObjects)
    {
        CinemachineVirtualCamera interactableObjectCamera = detectInteractableObjects.collider.gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
        if (interactableObjectCamera != null)
        {
            currentCamera = interactableObjectCamera;
            currentCamera.Priority = 99;
        }
    }

    public void ResetCameraPriority()
    {
        currentCamera.Priority = 9;
    }
}
