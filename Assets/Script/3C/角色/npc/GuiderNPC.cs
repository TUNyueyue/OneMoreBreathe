using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiderNPC : MonoBehaviour,IInteractive
{
    [field: SerializeField] public GameObject Tip { get; set; }
    [SerializeField] DialogueListSO rootDialogues;
    void Awake()
    {
        Tip.SetActive(false);
    }

    public void Interact(PlayerHand hand)
    {
        Tip.SetActive(false);
        DialogueManager.instance.StartDialogue(rootDialogues);
    }
}
