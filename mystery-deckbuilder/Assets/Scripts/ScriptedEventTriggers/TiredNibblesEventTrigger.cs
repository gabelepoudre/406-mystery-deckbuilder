using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiredNibblesEventTrigger : MonoBehaviour
{
    //literally any placeholder NPC will do
    public GameObject Glub;

    // Start is called before the first frame update
    void Start()
    {   
        TriggerTiredDialogue();
    }

    private void TriggerTiredDialogue()
    {
        if (GameState.Meta.currentDay.Value == 1
        && GameState.NPCs.Nibbles.met.Value
        && GameState.NPCs.Austin.met.Value
        && GameState.NPCs.Austyn.met.Value)
        {
            DialogueTree dialogue1 = new(new PlayerNode(new string[] {"*YAWWWWWWN* I sure am tired. Maybe it's time I head back to the motel?"}));
            DialogueTree dialogue2 = new(new PlayerNode(new string[] {"*YAWWWWWWN* Sheesh, I'm tired. Maybe I should start heading back to the motel?"}));
            DialogueTree dialogue3 = new(new PlayerNode(new string[] {"*YAWWWWWWN* All this walking has made my tail fins sore. Maybe I should start heading back to the motel"}));

            List<DialogueTree> trees = new() {dialogue1, dialogue2, dialogue3};
            var random = new System.Random();
            int index = random.Next(trees.Count);

            DialogueManager.Instance.StartDialogue(trees[index], Glub);
        }
        
    }
}
