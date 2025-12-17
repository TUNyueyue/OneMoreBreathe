using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class DialogueOption
{
    public string content;
    public DialogueListSO dialogues;
}

[CreateAssetMenu(fileName = "DialogueNodeSO", menuName = "Dialogue/DialogueNode")]
public class DialogueNodeSO : ScriptableObject
{
    public SpeakerSO speaker;
    [TextArea(3, 6)] public string content;
    public List<DialogueOption> options = new List<DialogueOption>(4);
    //最多四个选项
}
