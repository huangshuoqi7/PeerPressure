using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

/*
 * An asset object that user can pre-written dialogue line and connect
 * with other narrative line. These object can be player on the 
 * Dialogue_Player
*/

[CreateAssetMenu(menuName ="Dialogue Node", fileName ="Node", order = 3)]
public class DialogueNode : ScriptableObject
{
    //Public Variables

    //Start variables
    //Dialogue and Question will be last index
    public static int LineSize = 1;
    [TextArea(3,10000)][SerializeField] public string[] LineOfDialogue = new string[LineSize]; //List of Dialogues Line for node
    public bool IsPlaying = false;
    public float[] LineDisplay = new float[LineSize]; //List of dialogues second translating.
    public bool IsAQueation = false;


    //Answers
    public static int ResponseSize = 4;
    public DialogueNode[] AnswerResponse = new DialogueNode[ResponseSize];
    [TextArea(3, 10000)][SerializeField] public string answer;
    public bool IsItCorrect = false;

    //Frame Functions
    public float beginFrame = 0;
    public float endFrame = 1;

    //Check if the Dialogue is with animation
    public bool IsPlay()
    {
        return IsPlaying;
    }
    //Check if the Dialogue will need a question
    public bool IsQuestion()
    {
        return IsAQueation;
    }
    //Return the dialogue Lines
    public string GetDialogueLine(int index)
    {
        if(index < LineOfDialogue.Length)
        {
            return "None Text Exist";
        }
        return LineOfDialogue[index];
    }
    //Return time dialogue will translating to the next dialogue
    public float GetDialogueSecond(int index)
    {
        if (IsPlay())
        {
            if(index < LineDisplay.Length)
            {
                return 0;
            }
            return LineDisplay[index];
        }
        return 0;
    }
    /*
    DialogueLine              Dialogue 1                Dialogue 2
                      |-------------------------|--------------------------------|
    DialogueSecond    0                        [0] = 1.2s                       [1] = 3.4s
     */
    
    //Return the node with the answer
    public DialogueNode GetAnswerLine(int index)
    {
        if(index < AnswerResponse.Length)
        {
            return null;
        }
        return AnswerResponse[index];
    }

    //Check if the answer of this node is correct
    public bool IsCorrect()
    {
        return IsItCorrect;
    }

    //Return the answer
    public string GetAnswer()
    {
        return answer;
    }

    //Return the length of the Arrays of the ListofDialogue, LineDisplay, and AnswerResponse
    public int DialogueLineSize()
    {
        return LineOfDialogue.Length;
    }
    public int DialogueSecondSize()
    {
        return LineDisplay.Length;
    }
    public int AnswerResponseSize()
    {
        return AnswerResponse.Length;
    }
}
