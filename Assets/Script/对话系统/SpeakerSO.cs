using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SpeakerSO", menuName = "Dialogue/Speaker")]
public class SpeakerSO : ScriptableObject
{
    public string _name;
    public Sprite icon;
    public float oneWordSpeakTime;
}
