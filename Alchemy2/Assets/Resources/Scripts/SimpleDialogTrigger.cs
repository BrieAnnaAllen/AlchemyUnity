using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDialogTrigger : MonoBehaviour
{
    [TextArea(4,10)]
    public string[] dialogLines;
  
    // Use this for initialization
    public void Interact()
    {
        DialogManager.Instance.CreateDialog(dialogLines);
    }

    
}
