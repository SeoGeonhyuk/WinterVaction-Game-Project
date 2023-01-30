using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogueEvent;

    public Dialogue[] GetDialogue()
    {
        DialogueEvent t_dialogueEvent = new DialogueEvent();
        t_dialogueEvent.dialogues = DatabaseManager.instance.GetDialogue((int)dialogueEvent.line.x, (int)dialogueEvent.line.y);
        
        for(int i = 0; i < dialogueEvent.dialogues.Length; i++)
        {
            t_dialogueEvent.dialogues[i].tf_Target = dialogueEvent.dialogues[i].tf_Target;
            t_dialogueEvent.dialogues[i].cameraType = dialogueEvent.dialogues[i].cameraType;
        }
        dialogueEvent.dialogues = t_dialogueEvent.dialogues;

        return dialogueEvent.dialogues;

    }

    
}
