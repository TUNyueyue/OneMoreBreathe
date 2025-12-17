using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] DialogueListSO rootDialogues;
    [SerializeField] DialogueUI dialogueUI;
    [SerializeField] UnityEvent onDialogueStart;
    [SerializeField] UnityEvent onDialogueEnd;

    DialogueListSO currentDialogues;
    DialogueNodeSO currentDialogue;
    int dialogueIndex = 0;

    void Start()
    {
        StartDialogue();
    }

    public void StartDialogue(DialogueListSO startDialogues = null)
    {
        if (startDialogues == null)
            currentDialogues = rootDialogues;
        else
            currentDialogues = startDialogues;

        dialogueIndex = 0;


        //数据更新
        currentDialogue = currentDialogues.nodeList[dialogueIndex];
        dialogueUI.ShowDialogue();
        dialogueUI.UpdateDialogueUI(currentDialogue);
        onDialogueStart?.Invoke();
    
    }

    public void MoveToNextDialogue()
    { 
    dialogueIndex++;
        if (dialogueIndex >= currentDialogues.nodeList.Count)
        {
            CompleteDialogue();
            return;
        }
        currentDialogue = currentDialogues.nodeList[dialogueIndex];
        dialogueUI.UpdateDialogueUI(currentDialogue);
    }
    public void MoveToNextOptionDialogue(int optionIndex)
    {
        //处理分支关键逻辑，索引设为0，重新开始一段对话
        dialogueIndex = 0;
        currentDialogues = currentDialogue.options[optionIndex].dialogues;
        currentDialogue = currentDialogues.nodeList[dialogueIndex];
        dialogueUI.UpdateDialogueUI(currentDialogue);
        
    }
    public void CompleteDialogue()
    {

        dialogueUI.HideDialogue();        
        onDialogueEnd?.Invoke();
    }

   





}
