using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderNPC : MonoBehaviour, IInteractive
{
    [SerializeField] DialogueListSO rootDialogues;
    [SerializeField] GameObject input;
    [Header("UI")]
    [SerializeField] GameObject shopPanel;
    [field: SerializeField] public GameObject Tip { get; set; }

    void Awake()
    {
        Tip.SetActive(false);
    }
    public void Interact(PlayerHand hand)
    {
        Tip.SetActive(false);
        shopPanel.SetActive(true);
        DialogueManager.instance.SetDefaultDialogue(rootDialogues);
        input.SetActive(false);
    }
}
