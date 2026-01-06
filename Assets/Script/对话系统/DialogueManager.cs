using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class DialogueManager : MonoBehaviour
{
    DialogueListSO defaultDialogues;
    [SerializeField] DialogueUI dialogueUI;
    [SerializeField] UnityEvent onDialogueStart;
    [SerializeField] UnityEvent onDialogueEnd;

    DialogueListSO currentDialogues;
    DialogueNodeSO currentDialogue;
    int dialogueIndex = 0;

    public static DialogueManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void SetDefaultDialogue(DialogueListSO defaultDialogues)
    {
        this.defaultDialogues = defaultDialogues;
    }

    public void StartDefalutDialogue()
    {
        StartDialogue(defaultDialogues);
    }

    public void StartDialogue(DialogueListSO startDialogues)
    {
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
