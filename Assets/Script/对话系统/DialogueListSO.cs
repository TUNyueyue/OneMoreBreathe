using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueListSO", menuName = "Dialogue/DialogueList")]
public class DialogueListSO : ScriptableObject
{
    public List<DialogueNodeSO> nodeList;
}
