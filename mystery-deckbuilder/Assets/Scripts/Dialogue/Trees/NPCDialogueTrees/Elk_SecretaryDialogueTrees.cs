/*
 * Author(s): Ehsan Soltan
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The Class which holds all of the Elk Secretary's dialogue trees in a dictionary, the point being
 * that all of the dialogue will be built here and passed to the NPC class 
 */
public class Elk_SecretaryDialogueTrees : MonoBehaviour, IDialogueTreeCollection
{
    private Dictionary<string, DialogueTree> _dialogueTreeDict; //a dictionary of dialogue trees

    public Elk_SecretaryDialogueTrees()
    {
        _dialogueTreeDict = new();
        BuildTreeDictionary();
    }

    
    //populates the dictionary will Elk's dialogue trees
    private void BuildTreeDictionary()
    {
    
        _dialogueTreeDict.Add("Intro", BuildIntro());
        _dialogueTreeDict.Add("BuildDialogueWithAlan", BuildDialogueWithAlan());
        _dialogueTreeDict.Add("BuildDialogueWithNina", BuildDialogueWithNina());
        _dialogueTreeDict.Add("BuildDialogueWithAlanAndNina", BuildDialogueWithAlanAndNina());
        _dialogueTreeDict.Add("BuildDialogueWithCroutonAndNina", BuildDialogueWithCroutonAndNina());
        _dialogueTreeDict.Add("BuildDialogueWithAllThree", BuildDialogueWithAllThree());
        _dialogueTreeDict.Add("BuildAlanWithEvidence", BuildAlanWithEvidence());
        _dialogueTreeDict.Add("BuildAlanWithEvidenceAndNina", BuildAlanWithEvidenceAndNina());
        _dialogueTreeDict.Add("BuildAlanWithEvidenceAndNinaAndCrouton", BuildAlanWithEvidenceAndNinaAndCrouton());
        _dialogueTreeDict.Add("AfterEncounterWin", BuildAfterEncounterWin());
        _dialogueTreeDict.Add("AfterEncounterLoss", BuildAfterEncounterLoss());
    }

    /** Elk's intro **/
    private DialogueTree BuildIntro()
    {
        NPCNode greeting = new(new string[] { "Greetings Detective Glub, I am both the secretary of Small Pines and the personal advisor for Mayor Crouton.", 
            "How can I be of service--but do remember, I am quite a busy person so please no small talk." });

        OptionNode reply = new();

        PlayerNode askWhere = new(new string[] { "I know that you are very approving of me finding the berries, but I do not want to rule out any possibilities." ,
            "Be honest, what were you doing on the day before the berries disappeared?" });

        NPCNode whereAnswer = new(new string[] { "Do not worry detective, I understand. I will be truthful to you. Firstly, I woke up at 5:00 AM, drank coffee and did my daily morning jog.",
        "When I returned home I brushed my teeth and took a shower, both at the same time. Then I prepared myself breakfast while listening to my favorite radio station 108.4FM.", 
        "Once it was 6:00 AM, I did my meditation and yoga for 30 minutes. At 6:30 AM I prepared myself for work. I arrived at the Town Hall 5 minutes before 7:00 AM.",
        "At 7:00 AM I called Crouton to wake her up. By 7:17 AM she got herself up from bed and I reminded her of her morning duties such as:",
        "Brushing her teeth, remembering to take a shower, eating breakfast, deciding which clothes to wear, remembering her car keys, remembering to...", 
        "(Long Story short, he sounds very busy.) " });

        PlayerNode askLocations = new(new string[] { "Tell me secretary, you must know Small Pines very well and I'd like to know which areas to visit.",
            "Are there any areas in town that are large enough to fit all of the berries?" });

        NPCNode locations = new(new string[] { "It would've been troublesome if you told me you'd simply like to visit, remember detective I have no room for small talk.", 
        "In regards to the areas that are able to fit all of the berries,",
        "the easier ones would be the lake north of town, the forest surrounding the town and outside of town.",
        "The larger areas that are likely candidates are the Rat Mob's hideout, mainly the areas that very few eyes have laid upon and are rather secretive,",
        "The Lumberyard near the Beaver's Union where all manners of junk and inventions could be used to obstruct the berries,",
        "The Railyard since there are plenty of unused carts that could store the berries,",
        "and the Town Hall with its unused storage rooms that were accustomed to storing hay bales from various farms back in the day.",
        "The Town Hall storage room is not open to the public and that includes you detective, it has become a symbol of survival and perseverance for the people of Small Pines.",
        "It's a shrine, if you will, and I must simply ask you to respect tradition.",
        "However, I find the Town Hall idea very unlikely. If it is indeed being used to hide the berries, then I'll have more work on my hands.",
        "The creative locations would include under the river bridge, the Breakfast Palace if all of the berries are squeezed out for juice,",
        "or even the entire Rodent side of the town. However, if that was the case there would've been plenty of eyes who saw the berries,",
        "and I say creative because that would mean that all the rodents are in on the plan.",
        "Lastly, it could be a bear that might be preparing for hibernation, but it's not springtime so this would be unlikely. That was a joke, you are free to laugh or not detective I do not mind.", 
        "If you do not get the joke then allow me to explain.",
        "It is impossible for a singular bear to eat the entirety of the berries and humor comes from the thought of a singular bear trying to gorge down all of the berries..."});

        PlayerNode talksAlot = new(new string[] {"(He sure talks a lot, I guess there really is no room for small talk.)" });

        (string, IDialogueNode)[] replyOptionsList = {
        ("Ask about whereabouts", askWhere),
        ("Ask about likely hiding spots", askLocations)
        };

        greeting.SetNext(reply);
        reply.SetOptions(replyOptionsList);

        askWhere.SetNext(whereAnswer);
        whereAnswer.SetNext(reply);

        askLocations.SetNext(locations);
        locations.SetNext(talksAlot);
        talksAlot.SetNext(reply);

        return new DialogueTree(greeting);
    }

    private DialogueTree BuildDialogueWithNina()
    {
        NPCNode greeting = new(new string[] { "Greetings Detective Glub, I am both the secretary of Small Pines and the personal advisor for Mayor Crouton.", 
            "How can I be of service--but do remember, I am quite a busy person so please no small talk." });

        OptionNode reply = new();

        PlayerNode askWhere = new(new string[] { "I know that you are very approving of me finding the berries, but I do not want to rule out any possibilities." ,
            "Be honest, what were you doing on the day before the berries disappeared?" });

        NPCNode whereAnswer = new(new string[] { "Do not worry detective, I understand. I will be truthful to you. Firstly, I woke up at 5:00 AM, drank coffee and did my daily morning jog.",
        "When I returned home I brushed my teeth and took a shower, both at the same time. Then I prepared myself breakfast while listening to my favorite radio station 108.4FM.", 
        "Once it was 6:00 AM, I did my meditation and yoga for 30 minutes. At 6:30 AM I prepared myself for work. I arrived at the Town Hall 5 minutes before 7:00 AM.",
        "At 7:00 AM I called Crouton to wake her up. By 7:17 AM she got herself up from bed and I reminded her of her morning duties such as:",
        "Brushing her teeth, remembering to take a shower, eating breakfast, deciding which clothes to wear, remembering her car keys, remembering to...", 
        "(Long Story short, he sounds very busy.) " });

        PlayerNode askLocations = new(new string[] { "Tell me secretary, you must know Small Pines very well and I'd like to know which areas to visit.",
            "Are there any areas in town that are large enough to fit all of the berries?" });

        NPCNode locations = new(new string[] { "It would've been troublesome if you told me you'd simply like to visit, remember detective I have no room for small talk.", 
        "In regards to the areas that are able to fit all of the berries,",
        "the easier ones would be the lake north of town, the forest surrounding the town and outside of town.",
        "The larger areas that are likely candidates are the Rat Mob's hideout, mainly the areas that very few eyes have laid upon and are rather secretive,",
        "The Lumberyard near the Beaver's Union where all manners of junk and inventions could be used to obstruct the berries,",
        "The Railyard since there are plenty of unused carts that could store the berries,",
        "and the Town Hall with its unused storage rooms that were accustomed to storing hay bales from various farms back in the day.",
        "The Town Hall storage room is not open to the public and that includes you detective, it has become a symbol of survival and perseverance for the people of Small Pines.",
        "It's a shrine, if you will, and I must simply ask you to respect tradition.",
        "However, I find the Town Hall idea very unlikely. If it is indeed being used to hide the berries, then I'll have more work on my hands.",
        "The creative locations would include under the river bridge, the Breakfast Palace if all of the berries are squeezed out for juice,",
        "or even the entire Rodent side of the town. However, if that was the case there would've been plenty of eyes who saw the berries,",
        "and I say creative because that would mean that all the rodents are in on the plan.",
        "Lastly, it could be a bear that might be preparing for hibernation, but it's not springtime so this would be unlikely. That was a joke, you are free to laugh or not detective I do not mind.", 
        "If you do not get the joke then allow me to explain.",
        "It is impossible for a singular bear to eat the entirety of the berries and humor comes from the thought of a singular bear trying to gorge down all of the berries..."});

        PlayerNode askNina = new(new string[] { "I've met with the mayor's younger sister, and she's told me that all of Crouton's ideas are denied.",
            "Why is it that it seems like the mayor herself doesn't have her own voice in political matters?" });

        NPCNode ninaAnswer = new(new string[] { "Listen detective, Nina is a young disappointment to her ancestors...",
        "She does not understand the amount of procedures and care that must be done before an action is taken.",
        "Her older sister Crouton has a tendency of blurting out her thoughts and it is my job to correct her, nothing more than that.",
        "Is that all detective? Well then, I must remind you, I do not have time for small talk."});

        askNina.SetNext(ninaAnswer);
        ninaAnswer.SetNext(reply);

        (string, IDialogueNode)[] replyOptionsList = {
        ("Ask about whereabouts", askWhere),
        ("Ask about likely hiding spots", askLocations),
        ("Ask about Mayor's Agency", askNina)
        };

        greeting.SetNext(reply);
        reply.SetOptions(replyOptionsList);

        askWhere.SetNext(whereAnswer);
        whereAnswer.SetNext(reply);

        askLocations.SetNext(locations);
        locations.SetNext(reply);



        return new DialogueTree(greeting);
    }

    private DialogueTree BuildDialogueWithAlan()
    {
        NPCNode greeting = new(new string[] { "Greetings Detective Glub, I am both the secretary of Small Pines and the personal advisor for Mayor Crouton.", 
            "How can I be of service--but do remember, I am quite a busy person so please no small talk." });

        OptionNode reply = new();

        PlayerNode askWhere = new(new string[] { "I know that you are very approving of me finding the berries, but I do not want to rule out any possibilities." ,
            "Be honest, what were you doing on the day before the berries disappeared?" });

        NPCNode whereAnswer = new(new string[] { "Do not worry detective, I understand. I will be truthful to you. Firstly, I woke up at 5:00 AM, drank coffee and did my daily morning jog.",
        "When I returned home I brushed my teeth and took a shower, both at the same time. Then I prepared myself breakfast while listening to my favorite radio station 108.4FM.", 
        "Once it was 6:00 AM, I did my meditation and yoga for 30 minutes. At 6:30 AM I prepared myself for work. I arrived at the Town Hall 5 minutes before 7:00 AM.",
        "At 7:00 AM I called Crouton to wake her up. By 7:17 AM she got herself up from bed and I reminded her of her morning duties such as:",
        "Brushing her teeth, remembering to take a shower, eating breakfast, deciding which clothes to wear, remembering her car keys, remembering to...", 
        "(Long Story short, he sounds very busy.) " });

        PlayerNode askLocations = new(new string[] { "Tell me secretary, you must know Small Pines very well and I'd like to know which areas to visit.",
            "Are there any areas in town that are large enough to fit all of the berries?" });

        NPCNode locations = new(new string[] { "It would've been troublesome if you told me you'd simply like to visit, remember detective I have no room for small talk.", 
        "In regards to the areas that are able to fit all of the berries,",
        "the easier ones would be the lake north of town, the forest surrounding the town and outside of town.",
        "The larger areas that are likely candidates are the Rat Mob's hideout, mainly the areas that very few eyes have laid upon and are rather secretive,",
        "The Lumberyard near the Beaver's Union where all manners of junk and inventions could be used to obstruct the berries,",
        "The Railyard since there are plenty of unused carts that could store the berries,",
        "and the Town Hall with its unused storage rooms that were accustomed to storing hay bales from various farms back in the day.",
        "The Town Hall storage room is not open to the public and that includes you detective, it has become a symbol of survival and perseverance for the people of Small Pines.",
        "It's a shrine, if you will, and I must simply ask you to respect tradition.",
        "However, I find the Town Hall idea very unlikely. If it is indeed being used to hide the berries, then I'll have more work on my hands.",
        "The creative locations would include under the river bridge, the Breakfast Palace if all of the berries are squeezed out for juice,",
        "or even the entire Rodent side of the town. However, if that was the case there would've been plenty of eyes who saw the berries,",
        "and I say creative because that would mean that all the rodents are in on the plan.",
        "Lastly, it could be a bear that might be preparing for hibernation, but it's not springtime so this would be unlikely. That was a joke, you are free to laugh or not detective I do not mind.", 
        "If you do not get the joke then allow me to explain.",
        "It is impossible for a singular bear to eat the entirety of the berries and humor comes from the thought of a singular bear trying to gorge down all of the berries..."});        

        PlayerNode askAlan = new(new string[] { "Secretary, I heard that you have been receiving mysterious mail for the past week.",
            "Care to share the contents of that mail with me?" });

        NPCNode alanAnswer = new(new string[] { "Detective, accusations without evidence and witnesses is a false report. You could face both criminal or civil charges.", 
        "I am willing to let this one slide since you are vital to finding the missing berries, but do remember that listening to baseless rumors is quite troublesome.",
        "I recommend avoiding it." });

        askAlan.SetNext(alanAnswer);

        alanAnswer.SetNext(reply);

        (string, IDialogueNode)[] replyOptionsList = {
        ("Ask about whereabouts", askWhere),
        ("Ask about likely hiding spots", askLocations),
        ("Ask about the mail", askAlan)
        };

        greeting.SetNext(reply);
        reply.SetOptions(replyOptionsList);

        askWhere.SetNext(whereAnswer);
        whereAnswer.SetNext(reply);

        askLocations.SetNext(locations);
        locations.SetNext(reply);



        return new DialogueTree(greeting);
    }

    private DialogueTree BuildAlanWithEvidence()
    {
        NPCNode greeting = new(new string[] { "Greetings Detective Glub, I am both the secretary of Small Pines and the personal advisor for Mayor Crouton.", 
            "How can I be of service--but do remember, I am quite a busy person so please no small talk." });

        OptionNode reply = new();

        PlayerNode askWhere = new(new string[] { "I know that you are very approving of me finding the berries, but I do not want to rule out any possibilities." ,
            "Be honest, what were you doing on the day before the berries disappeared?" });

        NPCNode whereAnswer = new(new string[] { "Do not worry detective, I understand. I will be truthful to you. Firstly, I woke up at 5:00 AM, drank coffee and did my daily morning jog.",
        "When I returned home I brushed my teeth and took a shower, both at the same time. Then I prepared myself breakfast while listening to my favorite radio station 108.4FM.", 
        "Once it was 6:00 AM, I did my meditation and yoga for 30 minutes. At 6:30 AM I prepared myself for work. I arrived at the Town Hall 5 minutes before 7:00 AM.",
        "At 7:00 AM I called Crouton to wake her up. By 7:17 AM she got herself up from bed and I reminded her of her morning duties such as:",
        "Brushing her teeth, remembering to take a shower, eating breakfast, deciding which clothes to wear, remembering her car keys, remembering to...", 
        "(Long Story short, he sounds very busy.) " });

        PlayerNode askLocations = new(new string[] { "Tell me secretary, you must know Small Pines very well and I'd like to know which areas to visit.",
            "Are there any areas in town that are large enough to fit all of the berries?" });

        NPCNode locations = new(new string[] { "It would've been troublesome if you told me you'd simply like to visit, remember detective I have no room for small talk.", 
        "In regards to the areas that are able to fit all of the berries,",
        "the easier ones would be the lake north of town, the forest surrounding the town and outside of town.",
        "The larger areas that are likely candidates are the Rat Mob's hideout, mainly the areas that very few eyes have laid upon and are rather secretive,",
        "The Lumberyard near the Beaver's Union where all manners of junk and inventions could be used to obstruct the berries,",
        "The Railyard since there are plenty of unused carts that could store the berries,",
        "and the Town Hall with its unused storage rooms that were accustomed to storing hay bales from various farms back in the day.",
        "The Town Hall storage room is not open to the public and that includes you detective, it has become a symbol of survival and perseverance for the people of Small Pines.",
        "It's a shrine, if you will, and I must simply ask you to respect tradition.",
        "However, I find the Town Hall idea very unlikely. If it is indeed being used to hide the berries, then I'll have more work on my hands.",
        "The creative locations would include under the river bridge, the Breakfast Palace if all of the berries are squeezed out for juice,",
        "or even the entire Rodent side of the town. However, if that was the case there would've been plenty of eyes who saw the berries,",
        "and I say creative because that would mean that all the rodents are in on the plan.",
        "Lastly, it could be a bear that might be preparing for hibernation, but it's not springtime so this would be unlikely. That was a joke, you are free to laugh or not detective I do not mind.", 
        "If you do not get the joke then allow me to explain.",
        "It is impossible for a singular bear to eat the entirety of the berries and humor comes from the thought of a singular bear trying to gorge down all of the berries..."});

        PlayerNode askAlan = new(new string[] { "Secretary, these are not baseless rumors.",
        "Mayor Crouton herself told me that you have been receiving mysterious mail that you do not want to show her." });

        NPCNode alanAnswer = new(new string[] { "That mumbling furball... We receive mail everyday. However, they are always sent to me, so she doesn't see them.",
        "And since I'm the secretary, I deal with those myself.",
        "If the only lead you have right now is 'mysterious mail', then I suggest you quit this right now and find those berries." });

        askAlan.SetNext(alanAnswer);

        alanAnswer.SetNext(reply);

        (string, IDialogueNode)[] replyOptionsList = {
        ("Ask about whereabouts", askWhere),
        ("Ask about likely hiding spots", askLocations),
        ("Ask about the mail again", askAlan)
        };

        greeting.SetNext(reply);
        reply.SetOptions(replyOptionsList);

        askWhere.SetNext(whereAnswer);
        whereAnswer.SetNext(reply);

        askLocations.SetNext(locations);
        locations.SetNext(reply);

        return new DialogueTree(greeting);
    }
    private DialogueTree BuildDialogueWithCroutonAndNina()
    {
        NPCNode greeting = new(new string[] { "Greetings Detective Glub, I am both the secretary of Small Pines and the personal advisor for Mayor Crouton.", 
            "How can I be of service--but do remember, I am quite a busy person so please no small talk." });

        OptionNode reply = new();

        PlayerNode askWhere = new(new string[] { "I know that you are very approving of me finding the berries, but I do not want to rule out any possibilities." ,
            "Be honest, what were you doing on the day before the berries disappeared?" });

        NPCNode whereAnswer = new(new string[] { "Do not worry detective, I understand. I will be truthful to you. Firstly, I woke up at 5:00 AM, drank coffee and did my daily morning jog.",
        "When I returned home I brushed my teeth and took a shower, both at the same time. Then I prepared myself breakfast while listening to my favorite radio station 108.4FM.", 
        "Once it was 6:00 AM, I did my meditation and yoga for 30 minutes. At 6:30 AM I prepared myself for work. I arrived at the Town Hall 5 minutes before 7:00 AM.",
        "At 7:00 AM I called Crouton to wake her up. By 7:17 AM she got herself up from bed and I reminded her of her morning duties such as:",
        "Brushing her teeth, remembering to take a shower, eating breakfast, deciding which clothes to wear, remembering her car keys, remembering to...", 
        "(Long Story short, he sounds very busy.) " });

        PlayerNode askLocations = new(new string[] { "Tell me secretary, you must know Small Pines very well and I'd like to know which areas to visit.",
            "Are there any areas in town that are large enough to fit all of the berries?" });

        NPCNode locations = new(new string[] { "It would've been troublesome if you told me you'd simply like to visit, remember detective I have no room for small talk.", 
        "In regards to the areas that are able to fit all of the berries,",
        "the easier ones would be the lake north of town, the forest surrounding the town and outside of town.",
        "The larger areas that are likely candidates are the Rat Mob's hideout, mainly the areas that very few eyes have laid upon and are rather secretive,",
        "The Lumberyard near the Beaver's Union where all manners of junk and inventions could be used to obstruct the berries,",
        "The Railyard since there are plenty of unused carts that could store the berries,",
        "and the Town Hall with its unused storage rooms that were accustomed to storing hay bales from various farms back in the day.",
        "The Town Hall storage room is not open to the public and that includes you detective, it has become a symbol of survival and perseverance for the people of Small Pines.",
        "It's a shrine, if you will, and I must simply ask you to respect tradition.",
        "However, I find the Town Hall idea very unlikely. If it is indeed being used to hide the berries, then I'll have more work on my hands.",
        "The creative locations would include under the river bridge, the Breakfast Palace if all of the berries are squeezed out for juice,",
        "or even the entire Rodent side of the town. However, if that was the case there would've been plenty of eyes who saw the berries,",
        "and I say creative because that would mean that all the rodents are in on the plan.",
        "Lastly, it could be a bear that might be preparing for hibernation, but it's not springtime so this would be unlikely. That was a joke, you are free to laugh or not detective I do not mind.", 
        "If you do not get the joke then allow me to explain.",
        "It is impossible for a singular bear to eat the entirety of the berries and humor comes from the thought of a singular bear trying to gorge down all of the berries..."});

        NPCNode askCrouton = new(new string[] { "Your lies end here secretary, Mayor Crouton has revealed to me that she is a fake mayor.",
        "And that the one making the calls, using Crouton as a puppet and deceiving the people they are meant to serve, is you." });

        EncounterNode encounter = new();

        askCrouton.SetNext(encounter);

        PlayerNode askNina = new(new string[] { "I've met with the mayor's younger sister, and she's told me that all of Crouton's ideas are denied.",
            "Why is it that it seems like the mayor herself doesn't have her own voice in political matters?" });

        NPCNode ninaAnswer = new(new string[] { "Listen detective, Nina is a young disappointment to her ancestors...",
        "She does not understand the amount of procedures and care that must be done before an action is taken.",
        "Her older sister Crouton has a tendency of blurting out her thoughts and it is my job to correct her, nothing more than that.",
        "Is that all detective? Well then, I must remind you, I do not have time for small talk."});

        askNina.SetNext(ninaAnswer);
        ninaAnswer.SetNext(reply);

        (string, IDialogueNode)[] replyOptionsList = {
        ("Ask about whereabouts", askWhere),
        ("Ask about likely hiding spots", askLocations),
        ("Ask about Mayor's Agency", askNina),
        ("Confront secretary", askCrouton)
        };

        greeting.SetNext(reply);
        reply.SetOptions(replyOptionsList);

        askWhere.SetNext(whereAnswer);
        whereAnswer.SetNext(reply);

        askLocations.SetNext(locations);
        locations.SetNext(reply);

        return new DialogueTree(greeting);
    }

    private DialogueTree BuildDialogueWithAlanAndNina()
    {
        NPCNode greeting = new(new string[] { "Greetings Detective Glub, I am both the secretary of Small Pines and the personal advisor for Mayor Crouton.", 
            "How can I be of service--but do remember, I am quite a busy person so please no small talk." });

        OptionNode reply = new();

        PlayerNode askWhere = new(new string[] { "I know that you are very approving of me finding the berries, but I do not want to rule out any possibilities." ,
            "Be honest, what were you doing on the day before the berries disappeared?" });

        NPCNode whereAnswer = new(new string[] { "Do not worry detective, I understand. I will be truthful to you. Firstly, I woke up at 5:00 AM, drank coffee and did my daily morning jog.",
        "When I returned home I brushed my teeth and took a shower, both at the same time. Then I prepared myself breakfast while listening to my favorite radio station 108.4FM.", 
        "Once it was 6:00 AM, I did my meditation and yoga for 30 minutes. At 6:30 AM I prepared myself for work. I arrived at the Town Hall 5 minutes before 7:00 AM.",
        "At 7:00 AM I called Crouton to wake her up. By 7:17 AM she got herself up from bed and I reminded her of her morning duties such as:",
        "Brushing her teeth, remembering to take a shower, eating breakfast, deciding which clothes to wear, remembering her car keys, remembering to...", 
        "(Long Story short, he sounds very busy.) " });

        PlayerNode askLocations = new(new string[] { "Tell me secretary, you must know Small Pines very well and I'd like to know which areas to visit.",
            "Are there any areas in town that are large enough to fit all of the berries?" });

        NPCNode locations = new(new string[] { "It would've been troublesome if you told me you'd simply like to visit, remember detective I have no room for small talk.", 
        "In regards to the areas that are able to fit all of the berries,",
        "the easier ones would be the lake north of town, the forest surrounding the town and outside of town.",
        "The larger areas that are likely candidates are the Rat Mob's hideout, mainly the areas that very few eyes have laid upon and are rather secretive,",
        "The Lumberyard near the Beaver's Union where all manners of junk and inventions could be used to obstruct the berries,",
        "The Railyard since there are plenty of unused carts that could store the berries,",
        "and the Town Hall with its unused storage rooms that were accustomed to storing hay bales from various farms back in the day.",
        "The Town Hall storage room is not open to the public and that includes you detective, it has become a symbol of survival and perseverance for the people of Small Pines.",
        "It's a shrine, if you will, and I must simply ask you to respect tradition.",
        "However, I find the Town Hall idea very unlikely. If it is indeed being used to hide the berries, then I'll have more work on my hands.",
        "The creative locations would include under the river bridge, the Breakfast Palace if all of the berries are squeezed out for juice,",
        "or even the entire Rodent side of the town. However, if that was the case there would've been plenty of eyes who saw the berries,",
        "and I say creative because that would mean that all the rodents are in on the plan.",
        "Lastly, it could be a bear that might be preparing for hibernation, but it's not springtime so this would be unlikely. That was a joke, you are free to laugh or not detective I do not mind.", 
        "If you do not get the joke then allow me to explain.",
        "It is impossible for a singular bear to eat the entirety of the berries and humor comes from the thought of a singular bear trying to gorge down all of the berries..."});

        PlayerNode askAlan = new(new string[] { "Secretary, I heard that you have been receiving mysterious mail for the past week.",
            "Care to share the contents of that mail with me?" });

        NPCNode alanAnswer = new(new string[] { "Detective, accusations without evidence and witnesses is a false report. You could face both criminal and civil charges.",
        "I am willing to let this one slide since you are vital to finding the missing berries, but do remember that listening to baseless rumors is quite troublesome.",
        "I recommend avoiding it." });

        askAlan.SetNext(alanAnswer);

        alanAnswer.SetNext(reply);

        PlayerNode askNina = new(new string[] { "I've met with the mayor's younger sister, and she's told me that all of Crouton's ideas are denied.",
            "Why is it that it seems like the mayor herself doesn't have her own voice in political matters?" });

        NPCNode ninaAnswer = new(new string[] { "Listen detective, Nina is a young disappointment to her ancestors...",
        "She does not understand the amount of procedures and care that must be done before an action is taken.",
        "Her older sister Crouton has a tendency of blurting out her thoughts and it is my job to correct her, nothing more than that.",
        "Is that all detective? Well then, I must remind you, I do not have time for small talk."});

        askNina.SetNext(ninaAnswer);
        ninaAnswer.SetNext(reply);

        (string, IDialogueNode)[] replyOptionsList = {
        ("Ask about whereabouts", askWhere),
        ("Ask about likely hiding spots", askLocations),
        ("Ask about the mail", askAlan),
        ("Ask about Mayor's Agency", askNina)
        };

        greeting.SetNext(reply);
        reply.SetOptions(replyOptionsList);

        askWhere.SetNext(whereAnswer);
        whereAnswer.SetNext(reply);

        askLocations.SetNext(locations);
        locations.SetNext(reply);



        return new DialogueTree(greeting);
    }

    private DialogueTree BuildDialogueWithAllThree()
    {
        NPCNode greeting = new(new string[] { "Greetings Detective Glub, I am both the secretary of Small Pines and the personal advisor for Mayor Crouton.", 
            "How can I be of service--but do remember, I am quite a busy person so please no small talk." });

        OptionNode reply = new();

        PlayerNode askWhere = new(new string[] { "I know that you are very approving of me finding the berries, but I do not want to rule out any possibilities." ,
            "Be honest, what were you doing on the day before the berries disappeared?" });

        NPCNode whereAnswer = new(new string[] { "Do not worry detective, I understand. I will be truthful to you. Firstly, I woke up at 5:00 AM, drank coffee and did my daily morning jog.",
        "When I returned home I brushed my teeth and took a shower, both at the same time. Then I prepared myself breakfast while listening to my favorite radio station 108.4FM.", 
        "Once it was 6:00 AM, I did my meditation and yoga for 30 minutes. At 6:30 AM I prepared myself for work. I arrived at the Town Hall 5 minutes before 7:00 AM.",
        "At 7:00 AM I called Crouton to wake her up. By 7:17 AM she got herself up from bed and I reminded her of her morning duties such as:",
        "Brushing her teeth, remembering to take a shower, eating breakfast, deciding which clothes to wear, remembering her car keys, remembering to...", 
        "(Long Story short, he sounds very busy.) " });

        PlayerNode askLocations = new(new string[] { "Tell me secretary, you must know Small Pines very well and I'd like to know which areas to visit.",
            "Are there any areas in town that are large enough to fit all of the berries?" });

        NPCNode locations = new(new string[] { "It would've been troublesome if you told me you'd simply like to visit, remember detective I have no room for small talk.", 
        "In regards to the areas that are able to fit all of the berries,",
        "the easier ones would be the lake north of town, the forest surrounding the town and outside of town.",
        "The larger areas that are likely candidates are the Rat Mob's hideout, mainly the areas that very few eyes have laid upon and are rather secretive,",
        "The Lumberyard near the Beaver's Union where all manners of junk and inventions could be used to obstruct the berries,",
        "The Railyard since there are plenty of unused carts that could store the berries,",
        "and the Town Hall with its unused storage rooms that were accustomed to storing hay bales from various farms back in the day.",
        "The Town Hall storage room is not open to the public and that includes you detective, it has become a symbol of survival and perseverance for the people of Small Pines.",
        "It's a shrine, if you will, and I must simply ask you to respect tradition.",
        "However, I find the Town Hall idea very unlikely. If it is indeed being used to hide the berries, then I'll have more work on my hands.",
        "The creative locations would include under the river bridge, the Breakfast Palace if all of the berries are squeezed out for juice,",
        "or even the entire Rodent side of the town. However, if that was the case there would've been plenty of eyes who saw the berries,",
        "and I say creative because that would mean that all the rodents are in on the plan.",
        "Lastly, it could be a bear that might be preparing for hibernation, but it's not springtime so this would be unlikely. That was a joke, you are free to laugh or not detective I do not mind.", 
        "If you do not get the joke then allow me to explain.",
        "It is impossible for a singular bear to eat the entirety of the berries and humor comes from the thought of a singular bear trying to gorge down all of the berries..."});

        PlayerNode askAlan = new(new string[] { "Secretary, I heard that you have been receiving mysterious mail for the past week.",
            "Care to share the contents of that mail with me?" });

        NPCNode alanAnswer = new(new string[] { "Detective, accusations without evidence and witnesses is a false report. You could face both criminal and civil charges.",
        "I am willing to let this one slide since you are vital to finding the missing berries, but do remember that listening to baseless rumors is quite troublesome.",
        "I recommend avoiding it." });

        askAlan.SetNext(alanAnswer);

        alanAnswer.SetNext(reply);

        NPCNode askCrouton = new(new string[] { "Your lies end here secretary, Mayor Crouton has revealed to me that she is a fake mayor.",
        "And that the one making the calls, using Crouton as a puppet and deceiving the people they are meant to serve, is you." });

        EncounterNode encounter = new();

        askCrouton.SetNext(encounter);

        PlayerNode askNina = new(new string[] { "I've met with the mayor's younger sister, and she's told me that all of Crouton's ideas are denied.",
            "Why is it that it seems like the mayor herself doesn't have her own voice in political matters?" });

        NPCNode ninaAnswer = new(new string[] { "Listen detective, Nina is a young disappointment to her ancestors...",
        "She does not understand the amount of procedures and care that must be done before an action is taken.",
        "Her older sister Crouton has a tendency of blurting out her thoughts and it is my job to correct her, nothing more than that.",
        "Is that all detective? Well then, I must remind you, I do not have time for small talk."});

        askNina.SetNext(ninaAnswer);
        ninaAnswer.SetNext(reply);

        (string, IDialogueNode)[] replyOptionsList = {
        ("Ask about whereabouts", askWhere),
        ("Ask about likely hiding spots", askLocations),
        ("Ask about the mail", askAlan),
        ("Ask about Mayor's Agency", askNina),
        ("Confront secretary", askCrouton)
        };

        greeting.SetNext(reply);
        reply.SetOptions(replyOptionsList);

        askWhere.SetNext(whereAnswer);
        whereAnswer.SetNext(reply);

        askLocations.SetNext(locations);
        locations.SetNext(reply);



        return new DialogueTree(greeting);
    }

    private DialogueTree BuildAlanWithEvidenceAndNina()
    {
        NPCNode greeting = new(new string[] { "Greetings Detective Glub, I am both the secretary of Small Pines and the personal advisor for Mayor Crouton.", 
            "How can I be of service--but do remember, I am quite a busy person so please no small talk." });

        OptionNode reply = new();

        PlayerNode askWhere = new(new string[] { "I know that you are very approving of me finding the berries, but I do not want to rule out any possibilities." ,
            "Be honest, what were you doing on the day before the berries disappeared?" });

        NPCNode whereAnswer = new(new string[] { "Do not worry detective, I understand. I will be truthful to you. Firstly, I woke up at 5:00 AM, drank coffee and did my daily morning jog.",
        "When I returned home I brushed my teeth and took a shower, both at the same time. Then I prepared myself breakfast while listening to my favorite radio station 108.4FM.", 
        "Once it was 6:00 AM, I did my meditation and yoga for 30 minutes. At 6:30 AM I prepared myself for work. I arrived at the Town Hall 5 minutes before 7:00 AM.",
        "At 7:00 AM I called Crouton to wake her up. By 7:17 AM she got herself up from bed and I reminded her of her morning duties such as:",
        "Brushing her teeth, remembering to take a shower, eating breakfast, deciding which clothes to wear, remembering her car keys, remembering to...", 
        "(Long Story short, he sounds very busy.) " });

        PlayerNode askLocations = new(new string[] { "Tell me secretary, you must know Small Pines very well and I'd like to know which areas to visit.",
            "Are there any areas in town that are large enough to fit all of the berries?" });

        NPCNode locations = new(new string[] { "It would've been troublesome if you told me you'd simply like to visit, remember detective I have no room for small talk.", 
        "In regards to the areas that are able to fit all of the berries,",
        "the easier ones would be the lake north of town, the forest surrounding the town and outside of town.",
        "The larger areas that are likely candidates are the Rat Mob's hideout, mainly the areas that very few eyes have laid upon and are rather secretive,",
        "The Lumberyard near the Beaver's Union where all manners of junk and inventions could be used to obstruct the berries,",
        "The Railyard since there are plenty of unused carts that could store the berries,",
        "and the Town Hall with its unused storage rooms that were accustomed to storing hay bales from various farms back in the day.",
        "The Town Hall storage room is not open to the public and that includes you detective, it has become a symbol of survival and perseverance for the people of Small Pines.",
        "It's a shrine, if you will, and I must simply ask you to respect tradition.",
        "However, I find the Town Hall idea very unlikely. If it is indeed being used to hide the berries, then I'll have more work on my hands.",
        "The creative locations would include under the river bridge, the Breakfast Palace if all of the berries are squeezed out for juice,",
        "or even the entire Rodent side of the town. However, if that was the case there would've been plenty of eyes who saw the berries,",
        "and I say creative because that would mean that all the rodents are in on the plan.",
        "Lastly, it could be a bear that might be preparing for hibernation, but it's not springtime so this would be unlikely. That was a joke, you are free to laugh or not detective I do not mind.", 
        "If you do not get the joke then allow me to explain.",
        "It is impossible for a singular bear to eat the entirety of the berries and humor comes from the thought of a singular bear trying to gorge down all of the berries..."});

        PlayerNode askAlan = new(new string[] { "Secretary, these are not baseless rumors.",
        "Mayor Crouton herself told me that you have been receiving mysterious mail that you do not want to show her" });

        NPCNode alanAnswer = new(new string[] { "That mumbling furball... We receive mail everyday. However, they are always sent to me, so she doesn't see them.",
        "And since I'm the secretary, I deal with those myself.",
        "If the only lead you have right now is 'mysterious mail', then I suggest you quit this right now and find those berries." });

        askAlan.SetNext(alanAnswer);

        alanAnswer.SetNext(reply);

        PlayerNode askNina = new(new string[] { "I've met with the mayor's younger sister, and she's told me that all of Crouton's ideas are denied.",
            "Why is it that it seems like the mayor herself doesn't have her own voice in political matters?" });

        NPCNode ninaAnswer = new(new string[] { "Listen detective, Nina is a young disappointment to her ancestors...",
        "She does not understand the amount of procedures and care that must be done before an action is taken.",
        "Her older sister Crouton has a tendency of blurting out her thoughts and it is my job to correct her, nothing more than that.",
        "Is that all detective? Well then, I must remind you, I do not have time for small talk."});

        askNina.SetNext(ninaAnswer);
        ninaAnswer.SetNext(reply);

        (string, IDialogueNode)[] replyOptionsList = {
        ("Ask about whereabouts", askWhere),
        ("Ask about likely hiding spots", askLocations),
        ("Ask about the mail again", askAlan),
        ("Ask about Mayor's Agency", askNina)
        };

        greeting.SetNext(reply);
        reply.SetOptions(replyOptionsList);

        askWhere.SetNext(whereAnswer);
        whereAnswer.SetNext(reply);

        askLocations.SetNext(locations);
        locations.SetNext(reply);

        return new DialogueTree(greeting);
    }

    private DialogueTree BuildAlanWithEvidenceAndNinaAndCrouton()
    {
        NPCNode greeting = new(new string[] { "Greetings Detective Glub, I am both the secretary of Small Pines and the personal advisor for Mayor Crouton.", 
            "How can I be of service--but do remember, I am quite a busy person so please no small talk." });

        OptionNode reply = new();

        PlayerNode askWhere = new(new string[] { "I know that you are very approving of me finding the berries, but I do not want to rule out any possibilities." ,
            "Be honest, what were you doing on the day before the berries disappeared?" });

        NPCNode whereAnswer = new(new string[] { "Do not worry detective, I understand. I will be truthful to you. Firstly, I woke up at 5:00 AM, drank coffee and did my daily morning jog.",
        "When I returned home I brushed my teeth and took a shower, both at the same time. Then I prepared myself breakfast while listening to my favorite radio station 108.4FM.", 
        "Once it was 6:00 AM, I did my meditation and yoga for 30 minutes. At 6:30 AM I prepared myself for work. I arrived at the Town Hall 5 minutes before 7:00 AM.",
        "At 7:00 AM I called Crouton to wake her up. By 7:17 AM she got herself up from bed and I reminded her of her morning duties such as:",
        "Brushing her teeth, remembering to take a shower, eating breakfast, deciding which clothes to wear, remembering her car keys, remembering to...", 
        "(Long Story short, he sounds very busy.) " });

        PlayerNode askLocations = new(new string[] { "Tell me secretary, you must know Small Pines very well and I'd like to know which areas to visit.",
            "Are there any areas in town that are large enough to fit all of the berries?" });

        NPCNode locations = new(new string[] { "It would've been troublesome if you told me you'd simply like to visit, remember detective I have no room for small talk.", 
        "In regards to the areas that are able to fit all of the berries,",
        "the easier ones would be the lake north of town, the forest surrounding the town and outside of town.",
        "The larger areas that are likely candidates are the Rat Mob's hideout, mainly the areas that very few eyes have laid upon and are rather secretive,",
        "The Lumberyard near the Beaver's Union where all manners of junk and inventions could be used to obstruct the berries,",
        "The Railyard since there are plenty of unused carts that could store the berries,",
        "and the Town Hall with its unused storage rooms that were accustomed to storing hay bales from various farms back in the day.",
        "The Town Hall storage room is not open to the public and that includes you detective, it has become a symbol of survival and perseverance for the people of Small Pines.",
        "It's a shrine, if you will, and I must simply ask you to respect tradition.",
        "However, I find the Town Hall idea very unlikely. If it is indeed being used to hide the berries, then I'll have more work on my hands.",
        "The creative locations would include under the river bridge, the Breakfast Palace if all of the berries are squeezed out for juice,",
        "or even the entire Rodent side of the town. However, if that was the case there would've been plenty of eyes who saw the berries,",
        "and I say creative because that would mean that all the rodents are in on the plan.",
        "Lastly, it could be a bear that might be preparing for hibernation, but it's not springtime so this would be unlikely. That was a joke, you are free to laugh or not detective I do not mind.", 
        "If you do not get the joke then allow me to explain.",
        "It is impossible for a singular bear to eat the entirety of the berries and humor comes from the thought of a singular bear trying to gorge down all of the berries..."});

        PlayerNode askAlan = new(new string[] { "Secretary, these are not baseless rumors.",
        "Mayor Crouton herself told me that you have been receiving mysterious mail that you do not want to show her." });

        NPCNode alanAnswer = new(new string[] { "That mumbling furball... We receive mail everyday. However, they are always sent to me, so she doesn't see them.",
        "And since I'm the secretary, I deal with those myself.",
        "If the only lead you have right now is 'mysterious mail', then I suggest you quit this right now and find those berries." });

        askAlan.SetNext(alanAnswer);

        alanAnswer.SetNext(reply);

        PlayerNode askNina = new(new string[] { "I've met with the mayor's younger sister, and she's told me that all of Crouton's ideas are denied.",
            "Why is it that it seems like the mayor herself doesn't have her own voice in political matters?" });

        NPCNode ninaAnswer = new(new string[] { "Listen detective, Nina is a young disappointment to her ancestors...",
        "She does not understand the amount of procedures and care that must be done before an action is taken.",
        "Her older sister Crouton has a tendency of blurting out her thoughts and it is my job to correct her, nothing more than that.",
        "Is that all detective? Well then, I must remind you, I do not have time for small talk."});

        askNina.SetNext(ninaAnswer);
        ninaAnswer.SetNext(reply);


        NPCNode askCrouton = new(new string[] { "Your lies end here secretary, Mayor Crouton has revealed to me that she is a fake mayor.",
        "And that the one making the calls, using Crouton as a puppet and deceiving the people they are meant to serve, is you." });

        EncounterNode encounter = new();

        askCrouton.SetNext(encounter);

        (string, IDialogueNode)[] replyOptionsList = {
        ("Ask about whereabouts", askWhere),
        ("Ask about likely hiding spots", askLocations),
        ("Ask about the mail again", askAlan),
        ("Ask about Mayor's Agency", askNina),
        ("Confront secretary", askCrouton)
        };
        greeting.SetNext(reply);
        reply.SetOptions(replyOptionsList);

        askWhere.SetNext(whereAnswer);
        whereAnswer.SetNext(reply);

        askLocations.SetNext(locations);
        locations.SetNext(reply);

        return new DialogueTree(greeting);
    }

        /** Elk's dialogue after you beat him **/
    private DialogueTree BuildAfterEncounterWin()
    {
        return new DialogueTree(new NPCNode(new string[] { "What have I done? I've lost my way. I've deceived and used my friends as tools to achieve my dreams.",
        "I've abused my power to conform this town to my views without caring about the input of the townsfolk.",
        "Detective, I have something to tell you. My friend Crouton knows how to reach the people's hearts.",
        "I told her to run for mayor and I used her popularity to put myself in a position of power.",
        "To me she was simply a tool I used to achieve my goals. I am despicable for believing this is the correct path.",
        "I will reveal to everyone about my doings, apologize to all affected, and quit my position as Small Pines secretary, on that you have my word.",
        "Detective, on a final note, I have been told that the Rat Mob knows something about the berries.",
        "Investigate that old bossy rat and his brat son.",
        "All I can ask of you is to seek out the criminals and bring justice to this case once and for all, just as you have with me..."}));
    }

    private DialogueTree BuildAfterEncounterLoss()
    {
        return new DialogueTree(new NPCNode(new string[] { "Detective... You water breather, enough of this. I do not like baseless accusations and I DO NOT LIKE SMALL TALK.",
            "Never approach me again and do your job. Find those berries." }));
    }
    public Dictionary<string, DialogueTree> GetDialogueTrees()
    {
        return _dialogueTreeDict;
    }


}
