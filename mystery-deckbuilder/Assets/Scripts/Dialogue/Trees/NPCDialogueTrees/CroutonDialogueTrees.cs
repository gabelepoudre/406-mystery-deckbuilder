/*
 * Author(s): Ehsan Soltan
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The Class which holds all of the Crouton's dialogue trees in a dictionary, the point being
 * that all of the dialogue will be built here and passed to the NPC class 
 */
public class CroutonDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
    private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public CroutonDialogueTrees()
    {
        _dialogueTreeDict = new();
        BuildTreeDictionary();
    }

    
    
    private void BuildTreeDictionary()
    {
    
        _dialogueTreeDict.Add("Intro", BuildIntro());
        _dialogueTreeDict.Add("IntroAfterEncounter", BuildIntroAfterEncounter());

        _dialogueTreeDict.Add("Day2Intro", BuildDay2Intro());
        _dialogueTreeDict.Add("BerryCommotion", BuildBerryCommotion());
    }


    private DialogueTree BuildIntro()
    {
        
        return new DialogueTree(null);
    }

    /** Nibbles' intro after you beat him **/
    private DialogueTree BuildIntroAfterEncounter()
    {
         return new DialogueTree(null);
    }

    private DialogueTree BuildDay2Intro()
    {
        PlayerNode root = new(new string[] {"Why is there yelling in the morning? I thought Small Pines was supposed to be as quiet as the deep sea.", 
        "This is supposed to be my special vacation, my luscious scales might get dirty if I get involved.", 
        "But if this is a big deal, then maybe I should check it out"});

        return new DialogueTree(root);
    }

    private DialogueTree BuildBerryCommotion()
    {
        NPCNode berriesGone = new(new string[] {"Oh great haymaker, please help us!", 
        "Did a tornado pass by and we didn't notice?", 
        "what about the festival?! What's gonna happen now?!", 
        "Nah man, I drank too many berries yesterday and passed out on the highway, a tornado would've taken me.", 
        "Who would've done something like this?!", 
        "Yeah right, but you're too ugly for the tornado.", 
        "BERRIES!!"}, name:"Crowd");
        PlayerNode excuseMe = new(new string[] {"Excuse me, but what happened here?"});
        NPCNode fishBrains = new(new string[] {"Look around you fish brains, the berries are gone"}, name:"Crowd");
        PlayerNode woah = new(new string[] {"Woah all the berries are gone just like that, also that was rude."});
        NPCNode festival = new(new string[] {"Just before the festival! These people are mean!", 
        "THE BERRIES!!!", "Dude that was uncalled for. Tornadoes don't pick and choose.", 
        "Mommy, when are the berries gonna come back?", "NOOOO, I've waited all hibernation for those berries!"}, name:"Crowd");
        NPCNode cooperation = new(new string[] {"Everyone please...", 
        "I know how much the Berry Festival means to all of you, but for us to find the berries we need your cooperation and patience!"});
        NPCNode precious = new(new string[] {"Oh Crouton dear is just too precious, I'm so glad she's our mayor!", 
        "MS. CROUTON HELP THE BERRIES!", "Yup the tornado sure as dirt didn't pick and choose you.", 
        "I can't believe this, just like that all gone...", "What the dog doin'?", "Ms. Crouton, when are the berries gonna come back"}, name:"Crowd");
        NPCNode comingBack = new(new string[] {"The're going to come back, don't worry! Everyone please remain calm!", 
        "We are all going to require everyone's help if we are to find the berries before the Berry festival!"});
        NPCNode quietDown = new(new string[] {"Everyone let's quiet down for Mayor Crouton!", 
        "Oh sweet sweet precious summer berries.", "AAARGHH BERRIES! ME WANT!", "Whatever man, led's drop the berries and tornadoes topic", 
        "I already miss those berries....", "NO NO NO NO NO", "Yup, sounds good. Let's dip now and get Mike's, on you."}, name:"Crowd");
        PlayerNode bad = new(new string[] {"*This is bad, the crowd is nowhere close to calming down*"});
        NPCNode calm = new(new string[] {"Ummm.. Miss Crouton said to remain calm...", "I'M PANICKING CAN YOU TELL!?", 
        "This can't be... My dreams!", "What are we gonna do now?", 
        "What has happened here will be remembered for all history..."});
        NPCNode surprise = new(new string[] {"*Visible panic and a surprised Pikachu face* Uhhh please?"});
        NPCNode silence = new(new string[] {"SILENCE!!!"}, name:"?");
        PlayerNode loud = new(new string[] {"Woah what a loud voice. where did that come from?"});
        NPCNode bear = new(new string[] {"*Gasps* It's the Black Bear!"}, name:"Crowd");

        berriesGone.SetNext(excuseMe);
        excuseMe.SetNext(fishBrains);
        fishBrains.SetNext(woah);
        woah.SetNext(festival);
        festival.SetNext(cooperation);
        cooperation.SetNext(precious);
        precious.SetNext(comingBack);
        comingBack.SetNext(quietDown);
        quietDown.SetNext(bad);
        bad.SetNext(calm);
        calm.SetNext(surprise);
        surprise.SetNext(silence);
        silence.SetNext(loud);
        loud.SetNext(bear);

        return new DialogueTree(berriesGone);


    }

    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }


}
