using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DetectItems : MonoBehaviour {

    private GameObject player;
    private Vector3 playerCenter;
    private GameObject cameraManagerGameObject;
    private CameraManager cameraManagerScript;
    private RaycastHit detectInteractableObjects;
    private InventoryManager inventory;
    private DisableManager disable;
    private AudioSource soundEffects;
    [SerializeField] float raycastLength;
    private bool Reading = false;
    [SerializeField] private AudioClip pickup;
    // Use this for initialization
    void Start()
    {
        Reading = false;
        player = this.gameObject;
        playerCenter = player.transform.position;
        cameraManagerGameObject = GameObject.FindGameObjectWithTag("CameraManager");
        cameraManagerScript = cameraManagerGameObject.GetComponent<CameraManager>();
        GameObject InventoryManager = GameObject.FindGameObjectWithTag("InventoryManager");
        GameObject disableObject = GameObject.FindGameObjectWithTag("Disable");
        disable = disableObject.GetComponent<DisableManager>();
        inventory = InventoryManager.GetComponent<InventoryManager>();
        GameObject soundFX = GameObject.FindGameObjectWithTag("SoundEffects");
        soundEffects = soundFX.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        Detect();
        StopReading();
    }

    private void Detect()
    {
        float rayDistance;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * raycastLength;
        Ray detectTouchable = new Ray(transform.position, forward);
        Debug.DrawRay(transform.position, forward, Color.blue);

        if (Physics.Raycast(transform.position, (forward), out detectInteractableObjects, raycastLength))
        {
            rayDistance = detectInteractableObjects.distance;

            if ((detectInteractableObjects.collider.gameObject.tag == "Readable") && (detectInteractableObjects.collider.isTrigger))
            {
                Readable();
            }
            else if ((detectInteractableObjects.collider.gameObject.tag == "Pickupable") && (detectInteractableObjects.collider.isTrigger))
            {
                Debug.Log("Raycast working");
                Pickupable();

            }

        }
    }

    private void OnTriggerStay(Collider other)
    {

      
    }

    private void Readable()
    {
        if ((Input.GetButtonDown("Select")) && !Reading)
        {
            cameraManagerScript.ChangeCameraPriority(detectInteractableObjects);
            if (detectInteractableObjects.collider.gameObject.GetComponentInChildren<SimpleDialogTrigger>() != null)
            {
                SimpleDialogTrigger book = detectInteractableObjects.collider.gameObject.GetComponentInChildren<SimpleDialogTrigger>();
                book.Interact();
            }
            Reading = true;
            disable.DisablePlayerMovement();
        }
    }

    private void Pickupable()
    {
        if(Input.GetButtonDown("Select"))
        {
            soundEffects.clip = pickup;
            soundEffects.Play();
            GameObject thisItemGameObject = detectInteractableObjects.collider.gameObject;
            Item thisItem = thisItemGameObject.transform.GetChild(0).gameObject.GetComponent<Item>(); 
            inventory.CallAddItem(thisItem);
            Destroy(thisItemGameObject);
        }
    }
   private void StopReading()
    {
        bool dialogBoxIsActive = DialogManager.Instance.IsDialogBoxActive();
        if (Input.GetButtonDown("Select") && Reading && !dialogBoxIsActive)
        {
            Reading = false;
            cameraManagerScript.ResetCameraPriority();
            disable.EnablePlayerMovement();
        }
    }
    
}
