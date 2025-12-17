using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour//该类只负责UI及其行为
{
    [SerializeField] DialogueManager mgr;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI contentText;
    [SerializeField] Image image;
    [SerializeField] Button clickToNextButton;
    [SerializeField] Button[] optionButtons;

    bool isTyping;

    void Awake()
    {

        clickToNextButton.onClick.AddListener(() => { mgr.MoveToNextDialogue(); });
        for (int i = 0; i < 4; i++)
        {
            int index = i;//由于闭包，这里必须声明i副本index，不然都是4
            Button optionButton = optionButtons[index];
            optionButton.onClick.RemoveAllListeners();
            optionButton.onClick.AddListener(() => { mgr.MoveToNextOptionDialogue(index); });
        }
    }
    public void ShowDialogue()
    {
        dialoguePanel.SetActive(true);

    }
    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);

    }

    public void UpdateDialogueUI(DialogueNodeSO dialogueNode)
    {
        //读取对话节点
        image.sprite = dialogueNode.speaker.icon;
        //contentText.text = dialogueNode.content;
        //改协程

        StartCoroutine(TypeContent(dialogueNode));
        foreach (Button optionButton in optionButtons)
        {
            optionButton.gameObject.SetActive(false);
        }
        clickToNextButton.gameObject.SetActive(false);
    }

    IEnumerator TypeContent(DialogueNodeSO dialogueNode)
    {
        contentText.text = "";

        foreach (char letter in dialogueNode.content.ToCharArray())
        {
            contentText.text += letter;
            yield return new WaitForSeconds(dialogueNode.speaker.oneWordSpeakTime);
        }
        Debug.Log("TypeOver！");
        CheckNextNode(dialogueNode);
    }

    void CheckNextNode(DialogueNodeSO dialogueNode)
    {
        if (dialogueNode.options.Count <= 0)
        {
            clickToNextButton.gameObject.SetActive(true);
        }
        else
        {
            for (int i = 0; i < dialogueNode.options.Count; i++)
            {
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = dialogueNode.options[i].content;
                //这里补个小知识点，getINChildren和parent都是该对象+子/父对象，而且该对象的优先级更大
                optionButtons[i].gameObject.SetActive(true);

            }
        }
        //第二个循环不能用foreach的原因，存在双列表遍历 optionButtons[i] dialogueNode.options[i]
    }

}
