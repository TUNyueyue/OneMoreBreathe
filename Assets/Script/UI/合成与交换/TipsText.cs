using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TipsText : MonoBehaviour
{
    TextMeshProUGUI text;
    [SerializeField] string defaultString;
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        text.text = defaultString;
    }
    private void OnDisable()
    {
        
    }

    public void SetText(string tipString)
    {
        text.text =tipString;
    }

}
