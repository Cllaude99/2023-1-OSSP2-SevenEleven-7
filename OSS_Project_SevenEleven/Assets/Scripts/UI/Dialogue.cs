using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [TextArea(4,4)]
    public string[] sentences;
    //public Sprite[] sprites;
    public Sprite[] dialogueWindows;
}
