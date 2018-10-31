using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogManager : MonoBehaviour {

    public static DialogManager Instance;
    private Text dialog;
    private GameObject dialogBox;

    private List<string> lines = new List<string>();
    private int lineIndex;
    private int maxIndex;
    private bool dialogActivated = false;
    // Use this for initialization
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        dialogBox = GameObject.FindGameObjectWithTag("DialogBox");
        dialog = dialogBox.GetComponentInChildren<Text>();
        dialogBox.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (dialogActivated == true)
        {
            CycleLines();
        }
    }

    public void CreateDialog(string[] linestemp)
    {
        lines = new List<string>(linestemp.Length);
        lines.AddRange(lines);
        lineIndex = 0;
        maxIndex = linestemp.Length - 1;
        dialogBox.SetActive(true);
        dialogActivated = true;
        SetDialog();
    }

    private void SetDialog()
    {
        dialog.text = lines[lineIndex];
    }

    private void CycleLines()
    {

        if (lineIndex < maxIndex)
        {
            if (Input.GetButtonDown("Select"))
            {
                lineIndex++;
                SetDialog();
            }
        }
        else
        {
            if (Input.GetButtonDown("Select") && (lineIndex >= maxIndex))
            {
                dialogBox.SetActive(false);
                dialogActivated = false;
            }
        }
    }

    public bool IsDialogBoxActive()
    {
        return dialogBox.active;
    }


}
