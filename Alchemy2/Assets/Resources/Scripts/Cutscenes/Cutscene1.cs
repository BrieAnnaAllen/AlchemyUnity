using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Cutscene1 : MonoBehaviour {

    GameObject inventoryManager;
    private List<Item> Inventory = new List<Item>();
    [SerializeField] GameObject Player;
    private bool cutscene1Activated = false;
    private GameObject Wardrobe;
    private BoxCollider wardrobeCollider; // new
    [SerializeField] private CinemachineVirtualCamera cutsceneCamDoor;
    private bool trisTalking = false;
    private float raycastLength = 120f;
    private bool afterPolice = false;
    private bool policeCutsceneActivated = false;
    private bool wardrobeCutsceneActivated = false;
    [SerializeField] private Animator animator;
    [SerializeField] private Animator anim;
    SimpleDialogTrigger trigDialog;
    private AudioSource soundEffects;
    
    [SerializeField] private AudioClip doorKnocking;
    [SerializeField] private AudioClip breakingGlass;

    private DisableManager disable;
    // Use this for initialization
    private void Start () {

        inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager");
        GameObject disableObject = GameObject.FindGameObjectWithTag("Disable");
        disable = disableObject.GetComponent<DisableManager>();
        trigDialog = GetComponent<SimpleDialogTrigger>();
        Wardrobe = GameObject.FindGameObjectWithTag("Wardrobe");
        GameObject soundFX = GameObject.FindGameObjectWithTag("SoundEffects");
        soundEffects = soundFX.GetComponent<AudioSource>();
        soundEffects.clip = breakingGlass;
        soundEffects.Play();


        StartCoroutine(OpeningScene());
    }
    private IEnumerator OpeningScene()
    {

        yield return new WaitForSeconds(.5f);

        trigDialog.dialogLines = OpeningDialog();
        trigDialog.Interact();
        trisTalking = true;
    }

    void FixedUpdate()
    {

        if (cutscene1Activated == false)
        {
            if (hasAllCutsceneItems() == true)
            {
                PoliceTrigger();
            }
        }
        isTrisTalking();
        
        if (afterPolice == true)
        {
            Detect();

        }
        if (Input.GetButtonDown("Quit"))
        {
            Application.Quit();
        }
    }

    private void isTrisTalking()
    {
        if(DialogManager.Instance.IsDialogBoxActive() == false)
        {
            trisTalking = false;
            EnablePlayerMovement();
            if (policeCutsceneActivated == true)
            {
                returnCameraToTris();
                policeCutsceneActivated = false;
            }
        }
        else
        {
            trisTalking = true;
            DisablePlayerMovement();
            

        }
    }

    private string[] PoliceDialog()
    {
        string[] policeEncounter = new string[3];
        policeEncounter[0] = "Police: Open up, it's the police!";
        policeEncounter[1] = "Shit, it looks like the Magestry is a lot faster than I originally thought.";
        policeEncounter[2] = "I'm going to need to find a place to hide.  If I try to leave from where I came they'll capture me for sure.";
        return policeEncounter;
    }
    private string[] OpeningDialog()
    {
        string[] openingEncounter = new string[6];
        openingEncounter[0] = "Ow! Not one of my more graceful entrances . . .";
        openingEncounter[1] = "I don't really love breaking into homes, but I really need some supplies if I want to continue my studies.";
        openingEncounter[2] = "Most people in the bazaar know I'm an Alchemist and won't even sell to me, and those that do charge an arm and a leg!";
        openingEncounter[3] = "I swear if I have to hear one more 'just turn some lead into gold!' joke. . . ";
        openingEncounter[4] = "Alright, focus Tris! You need to find some money if you don't want to starve on the street. Two bags of coins should be enough to get you by.";
        openingEncounter[5] = "Might as well see if they also have any supplies. I've been running low on test tubes, hopefully the owner of this place has some.";
        return openingEncounter;
    }
    private void WardrobeTrigger()
    {
        Debug.Log("Debug Cutscene part 2");
        trigDialog.dialogLines = WardrobeDialog();
        trigDialog.Interact();
        afterPolice = false;
        trisTalking = true;
        wardrobeCutsceneActivated = true;
    }

    private string[] WardrobeDialog()
    {

        Debug.Log("Debug Cutscene part 3");
        string[] wardrobeEncounter = new string[2];
        wardrobeEncounter[0] = "This is a perfect spot to hide!";
        wardrobeEncounter[1] = "Let's hope they aren't looking for any new clothes. . .";
        return wardrobeEncounter;
    }

    
    private void PoliceTrigger()
    {

        soundEffects.clip = doorKnocking;
        soundEffects.Play();
        cutscene1Activated = true;
        cutsceneCamDoor.Priority = 99;

        trigDialog.dialogLines = PoliceDialog();
        trigDialog.Interact();
        trisTalking = true;
        policeCutsceneActivated = true;
        anim.SetTrigger("OpenDoor");

    }

    private void returnCameraToTris()
    {
        cutsceneCamDoor.Priority = 9;
        afterPolice = true;
    }



    private void UpdateInventory()
    {
        InventoryManager inventoryScript = inventoryManager.GetComponent<InventoryManager>();
        Inventory = inventoryScript.GetInvetory();
    }

    private bool hasAllCutsceneItems()
    {
        int currentCoins = 0;
        int currentTestTubes = 0;

        UpdateInventory();

        foreach (Item item in Inventory)
        {
            if(item.GetItemID() == "BagWithGold")
            {
                currentCoins = item.GetQuantity();
            }

            if (item.GetItemID() == "TestTube")
            {
                currentTestTubes = item.GetQuantity();
            }
        }

        if ((currentTestTubes >= 2) && (currentCoins >= 2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void EnablePlayerMovement()
    {
        disable.EnablePlayerMovement();
        if(wardrobeCutsceneActivated == true)
        {
            animator.SetTrigger("FadeOut");
            wardrobeCutsceneActivated = false;
            
        }
        
    }
    private void DisablePlayerMovement()
    {
        disable.DisablePlayerMovement();
    }
    private void Detect()
    {
        Debug.Log("Being called");
        float rayDistance;
        RaycastHit detectInteractableObjects;
        Vector3 forward = Player.transform.TransformDirection(Vector3.forward) * raycastLength;
        Ray detectTouchable = new Ray(transform.position, forward);
        Debug.DrawRay(Player.transform.position, forward, Color.red);

        if (Physics.Raycast(Player.transform.position, (forward), out detectInteractableObjects, raycastLength))
        {
            rayDistance = detectInteractableObjects.distance;

            if ((detectInteractableObjects.collider.gameObject == Wardrobe) && (detectInteractableObjects.collider.isTrigger))
            {
                if (Input.GetButtonDown("Select"))
                {
                    Debug.Log("Debug Cutscene part 1");
                    WardrobeTrigger();
                }
            }


        }
    }
}
