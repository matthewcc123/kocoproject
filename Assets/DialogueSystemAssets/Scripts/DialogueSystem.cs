using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [System.Serializable]
    public class CharImage
    {
        public Image image;
        public CanvasGroup cg;
    }

    enum DialogueState { waiting, talking }

    public DialogueData dialogueData;
    public CharImage[] charImages;
    public Image continueImage;
    public TextMeshProUGUI textBox;
    public TextMeshProUGUI charName;

    private Dialogue[] dialouges;
    private int dialougeIndex;
    private int currentCharacter;
    private DialogueState currentState = DialogueState.waiting;

    private void Awake()
    {
        //Continue Animation
        continueImage.transform.LeanMoveLocalY(continueImage.transform.localPosition.y - 10f, .5f).setEaseInOutQuad().setLoopPingPong();

        //Characters Setup
        currentCharacter = -1;
        for (int i = 0; i < charImages.Length; i++)
        {
            charImages[i].image.gameObject.SetActive(false);

            if (i < dialogueData.characters.Length)
            {
                if (dialogueData.characters[i] == null)
                    return;

                charImages[i].image.gameObject.SetActive(true);
                charImages[i].image.sprite = dialogueData.characters[i].sprite;
                charImages[i].image.rectTransform.sizeDelta = dialogueData.characters[i].sprite.rect.size;

                //Hide Characters
                charImages[i].cg.alpha = .25f;
            }
            
        }

        //Dialogue Setup
        textBox.text = "Press Space to Start.";
        dialouges = dialogueData.Dialogues;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentState == DialogueState.waiting)
        {
            StartCoroutine(StartDialouge());
        }
    }

    IEnumerator StartDialouge()
    {
        if (dialougeIndex == dialouges.Length)
            yield break;

        //Setup
        Dialogue dialogue = dialouges[dialougeIndex];

        currentState = DialogueState.talking;
        continueImage.gameObject.SetActive(false);
        textBox.text = "";

        int textLength = dialogue.text.Length;
        int currentLength = 0;

        //Change Character
        if (currentCharacter != dialogue.charIndex)
        {
            //Hide Character
            if (currentCharacter >= 0 || dialogue.charIndex < 0)
                charImages[currentCharacter].cg.LeanAlpha(.25f, .25f).setEaseInCubic();

            //If no characters
            if (dialogue.charIndex < 0)
            {
                currentCharacter = dialogue.charIndex;
                charName.text = "System";
            }
            else
            {
                //Show Character
                currentCharacter = dialogue.charIndex;
                charImages[currentCharacter].cg.LeanAlpha(1f, .25f).setEaseInCubic();

                //Change Name
                charName.text = dialogueData.characters[currentCharacter].name;
            }

        }

        //Text Animation
        while (currentLength < textLength)
        {

            textBox.text += dialogue.text.Substring(currentLength, 1);
            
            currentLength++;

            yield return new WaitForSeconds(dialogue.textSpeed);
        }

        //Next Dialogue
        dialougeIndex++;
        currentState = DialogueState.waiting;
        continueImage.gameObject.SetActive(true);

        yield return null;
    }

}
