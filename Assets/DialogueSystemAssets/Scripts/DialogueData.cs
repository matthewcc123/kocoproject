using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character
{
    public string name;
    public Sprite sprite;
}

[System.Serializable]
public class Dialogue
{
    public int charIndex;
    public string text;
    public float textSpeed = .05f;
}

[CreateAssetMenu(menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{

    public Character[] characters;
    public Dialogue[] Dialogues;

    private void OnValidate()
    {
        if (characters.Length > 2)
        {
            Debug.LogWarning("Max 2 Characters");
            System.Array.Resize(ref characters, 2);
        }
    }

}
