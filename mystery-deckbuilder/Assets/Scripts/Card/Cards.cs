using System.Collections.Generic;
using UnityEngine;

/*
 * Static class containing a method to return an instance of a card with a given ID
 * Returned object must be cast to it's card type (Conversation/Preparation)
 */
public static class Cards
    {
    public static object CreateCardWithID(int id)
    {
        switch (id)
        {
            case 1:
                return new Bluster();
            case 2:
                return new PartingShot();
            case 3:
                return new BrowBeat();
            case 4:
                return new BadCop();
            case 5:
                return new Encourage();
            case 6:
                return new Complement();
            case 7:
                return new SobStory();
            case 8:
                return new GoodCop();
            case 9:
                return new Articulate();
            case 10:
                return new Salutation();
            case 11:
                return new Lecture();
            case 12:
                return new MenacingPresence();
            case 13:
                return new Tirade();
            case 14:
                return new Empathize();
            case 15:
                return new Reassure();
            case 16:
                return new Eloquence();
            case 17:
                return new Monologue();
            default:
                return null;
        }
    }
}
/*
 * Derived basic classes for converstion cards, all base stats for the card are hardcoded
 * Stats can only be accessed and not changed in an instance of a card
 * Any special card effects are not implemented yet
 */
public class Bluster : Card
{
    public Bluster() : base(1)
    {
        this._metadata["element"] = "Intimidation";
        this._metadata["name"] = "Bluster";
        this._metadata["description"] = "No special effect";
        this._metadata["patience"] = "1";
        this._metadata["compliance"] = "10";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";
    }
}

public class PartingShot : Card
{
    public PartingShot() : base(2)
    {
        this._metadata["element"] = "Intimidation";
        this._metadata["name"] = "Parting Shot";
        this._metadata["description"] = "Double Compliance when Patience is less than 2.";
        this._metadata["patience"] = "2";
        this._metadata["compliance"] = "15";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";

        this.__localEffects["Parting Shot!"] = new EPartingShot(this);
    }

    public override void OnChange()
    {
        this.__localEffects["Parting Shot!"].Execute();
    }


    /* A local effect (as seen by E prefix) for parting shot */
    public class EPartingShot : Effect, IExecutableEffect
    {
        private Color _color = new Color(255 / 255, 255 / 255, 100 / 255);

        private Card _parent;
        private string _name = "Parting Shot!";
        private string _desc_1 = "Doubled Compliance while remaining Patience is 1!";

        public EPartingShot(Card c) : base(99) { _parent = c; }
        public string GetDescription() { return _desc_1; }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            if (EncounterConditionals.PatienceEqualTo(1))
            {
                _parent.StackableComplianceMod += 1;
                _parent.DisplayEffect(this);
            }
        }
    }
}


public class BrowBeat : Card
{
    public BrowBeat() : base(3)
    {
        this._metadata["element"] = "Intimidation";
        this._metadata["name"] = "Brow Beat";
        this._metadata["description"] = "Raise Compliance by 10 for every card played.";
        this._metadata["patience"] = "2";
        this._metadata["compliance"] = "10";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";

        this.__localEffects["Brow Beat!"] = new EBrowBeat(this);
    }

    public override void OnChange()
    {
        this.__localEffects["Brow Beat!"].Execute();
    }

    /* A local effect (as seen by E prefix) for Brow Beat */
    public class EBrowBeat : Effect, IExecutableEffect
    {
        private Color _color = new Color(255 / 255, 255 / 255, 100 / 255);

        private Card _parent;
        private string _name = "Brow Beat!";
        private string _desc_1 = "Raising Compliance by 10 for every card played! (";

        public EBrowBeat(Card c) : base(99) { _parent = c; }
        public string GetDescription() 
        {
            return _desc_1 + GameState.Meta.activeEncounter.Value.Statistics.NumberOfPlays + ")"; 
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            if (EncounterConditionals.NumberPlaysGreaterThan(0))
            {
                _parent.UnstackableComplianceMod += 10 * GameState.Meta.activeEncounter.Value.Statistics.NumberOfPlays;
                _parent.DisplayEffect(this);
            }
        }
    }
}

public class BadCop : Card
{
    public BadCop() : base(4)
    {
        this._metadata["element"] = "Intimidation";
        this._metadata["name"] = "Bad Cop";
        this._metadata["description"] = "Raise Compliance by 10 for every Sympathy card in hand";
        this._metadata["patience"] = "3";
        this._metadata["compliance"] = "10";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";

        this.__localEffects["Bad Cop!"] = new EBadCop(this);
    }

    public override void OnChange()
    {
        this.__localEffects["Bad Cop!"].Execute();
    }

    /* A local effect (as seen by E prefix) for Bad Cop */
    public class EBadCop : Effect, IExecutableEffect
    {
        private Color _color = new Color(255 / 255, 255 / 255, 100 / 255);

        private Card _parent;
        private string _name = "Bad Cop!";
        private string _desc_1 = "Raising Compliance by 10 for every Sympathy card in hand! (";

        public EBadCop(Card c) : base(99) { _parent = c; }
        public string GetDescription()
        {
            return _desc_1 + GameState.Meta.activeEncounter.Value.Statistics.SympathyCardsInHand + ")";
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            if (EncounterConditionals.CardsOfElementInHandGreaterThan("Sympathy", 0))
            {
                _parent.UnstackableComplianceMod += 10 * GameState.Meta.activeEncounter.Value.Statistics.SympathyCardsInHand;
                _parent.DisplayEffect(this);
            }
        }
    }
}

public class Encourage : Card
{
    public Encourage() : base(5)
    {
        this._metadata["element"] = "Sympathy";
        this._metadata["name"] = "Encourage";
        this._metadata["description"] = "No special effect";
        this._metadata["patience"] = "1";
        this._metadata["compliance"] = "10";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";
    }
}

public class Complement : Card
{
    public Complement() : base(6)
    {
        this._metadata["element"] = "Sympathy";
        this._metadata["name"] = "Complement";
        this._metadata["description"] = "When no cards have been played, triple Compliance";
        this._metadata["patience"] = "1";
        this._metadata["compliance"] = "5";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";

        this.__localEffects["Complement!"] = new EComplement(this);
    }

    public override void OnChange()
    {
        this.__localEffects["Complement!"].Execute();
    }

    /* A local effect (as seen by E prefix) for Complement */
    public class EComplement : Effect, IExecutableEffect
    {
        private Color _color = new Color(255 / 255, 255 / 255, 100 / 255);

        private Card _parent;
        private string _name = "Complement!";
        private string _desc_1 = "No cards have been played, so Complement's Compliance is tripled!";

        public EComplement(Card c) : base(99) { _parent = c; }
        public string GetDescription()
        {
            return _desc_1;
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            if (EncounterConditionals.NumberPlaysLessThan(1))
            {
                _parent.StackableComplianceMod += 2;
                _parent.DisplayEffect(this);
            }
        }
    }
}

public class SobStory : Card
{
    public SobStory() : base(7)
    {
        this._metadata["element"] = "Sympathy";
        this._metadata["name"] = "Sob Story";
        this._metadata["description"] = "Raise Compliance by 8 for every card drawn so far";
        this._metadata["patience"] = "2";
        this._metadata["compliance"] = "8";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";

        this.__localEffects["Sob Story!"] = new ESobStory(this);
    }

    public override void OnChange()
    {
        this.__localEffects["Sob Story!"].Execute();
    }

    /* A local effect (as seen by E prefix) for Sob Story */
    public class ESobStory : Effect, IExecutableEffect
    {
        private Color _color = new Color(255 / 255, 255 / 255, 100 / 255);

        private Card _parent;
        private string _name = "Sob Story!";
        private string _desc_1 = "Raising Compliance by 8 for every card drawn so far! (";

        public ESobStory(Card c) : base(99) { _parent = c; }
        public string GetDescription()
        {
            return _desc_1 + GameState.Meta.activeEncounter.Value.Statistics.NumberOfDraws + ")";
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            if (EncounterConditionals.NumberDrawsGreaterThan(1))
            {
                _parent.UnstackableComplianceMod += 8 * GameState.Meta.activeEncounter.Value.Statistics.NumberOfDraws;
                _parent.DisplayEffect(this);
            }
        }
    }
}

public class GoodCop : Card
{
    public GoodCop() : base(8)
    {
        this._metadata["element"] = "Sympathy";
        this._metadata["name"] = "Good Cop";
        this._metadata["description"] = "Raise compliance by 10 for every Intimidation card in hand.";
        this._metadata["patience"] = "3";
        this._metadata["compliance"] = "10";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";

        this.__localEffects["Good Cop!"] = new EGoodCop(this);
    }

    public override void OnChange()
    {
        this.__localEffects["Good Cop!"].Execute();
    }

    /* A local effect (as seen by E prefix) for Good Cop */
    public class EGoodCop : Effect, IExecutableEffect
    {
        private Color _color = new Color(255 / 255, 255 / 255, 100 / 255);

        private Card _parent;
        private string _name = "Good Cop!";
        private string _desc_1 = "Raising Compliance by 10 for every Intimidation card in hand! (";

        public EGoodCop(Card c) : base(99) { _parent = c; }
        public string GetDescription()
        {
            return _desc_1 + GameState.Meta.activeEncounter.Value.Statistics.IntimidationCardsInHand + ")";
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            if (EncounterConditionals.CardsOfElementInHandGreaterThan("Intimidation", 0))
            {
                _parent.UnstackableComplianceMod += 10 * GameState.Meta.activeEncounter.Value.Statistics.IntimidationCardsInHand;
                _parent.DisplayEffect(this);
            }
        }
    }
}

public class Articulate : Card
{
    public Articulate() : base(9)
    {
        this._metadata["element"] = "Persuasion";
        this._metadata["name"] = "Articulate";
        this._metadata["description"] = "No special effect";
        this._metadata["patience"] = "1";
        this._metadata["compliance"] = "10";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";
    }
}

public class Salutation : Card
{
    public Salutation() : base(10)
    {
        this._metadata["element"] = "Persuasion";
        this._metadata["name"] = "Salutation";
        this._metadata["description"] = "When played as the first card, draw a card";
        this._metadata["patience"] = "1";
        this._metadata["compliance"] = "8";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";

        this.__localEffects["Salutation!"] = new ESalutation(this);
    }

    public override void OnPlay()
    {
        this.__localEffects["Salutation!"].Execute();
    }

    /* A local effect (as seen by E prefix) for Salutation */
    public class ESalutation : Effect, IExecutableEffect
    {
        private Color _color = new Color(255 / 255, 255 / 255, 100 / 255);

        private Card _parent;
        private string _name = "Salutation!";
        private string _desc_1 = "When played as the first card, draw a card!";

        public ESalutation  (Card c) : base(99) { _parent = c; }
        public string GetDescription()
        {
            return _desc_1;
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            if (EncounterConditionals.NumberPlaysEqualTo(1))  // this one is weird because it is counted after the play
            {
                GameState.Meta.activeEncounter.Value.DrawCard(0);
            }
        }
    }

}

public class Lecture : Card
{
    public Lecture() : base(11)
    {
        this._metadata["element"] = "Persuasion";
        this._metadata["name"] = "Lecture";
        this._metadata["description"] = "Raise Compliance by 10 for every Patience still remaining";
        this._metadata["patience"] = "3";
        this._metadata["compliance"] = "10";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";

        this.__localEffects["Lecture!"] = new ELecture(this);
    }

    public override void OnChange()
    {
        this.__localEffects["Lecture!"].Execute();
    }

    /* A local effect (as seen by E prefix) for Lecture */
    public class ELecture : Effect, IExecutableEffect
    {
        private Color _color = new Color(255 / 255, 255 / 255, 100 / 255);

        private Card _parent;
        private string _name = "Lecture!";
        private string _desc_1 = "Raising Compliance by 10 for every Patience remaining! (";

        public ELecture(Card c) : base(99) { _parent = c; }
        public string GetDescription()
        {
            return _desc_1 + GameState.Meta.activeEncounter.Value.Statistics.Patience + ")";
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            if (EncounterConditionals.PatienceGreaterThan(0))
            {
                _parent.UnstackableComplianceMod += 10 * GameState.Meta.activeEncounter.Value.Statistics.Patience;
                _parent.DisplayEffect(this);
            }
        }
    }
}

/*
 * Derived basic classes for Preparation cards, all base stats for the card are hardcoded
 */
public class MenacingPresence: Card
{
    public MenacingPresence() : base(12)
    {
        this._metadata["element"] = "Preparation";
        this._metadata["name"] = "Menacing Presence";
        this._metadata["description"] = "+5 compliance to conversation cards for 3 plays.";
        this._metadata["patience"] = "1";
    }
}

public class Tirade : Card
{
    public Tirade() : base(13)
    {
        this._metadata["element"] = "Preparation";
        this._metadata["name"] = "Tirade";
        this._metadata["description"] = "+1 card for each persuasion card in hand.";
        this._metadata["patience"] = "2";
    }
}

public class Empathize : Card
{
    public Empathize() : base(14)
    {
        this._metadata["element"] = "Preparation";
        this._metadata["name"] = "Empathize";
        this._metadata["description"] = "The next card you play costs 1 less patience.";
        this._metadata["patience"] = "0";
    }
}

public class Reassure : Card
{
    public Reassure() : base(15)
    {
        this._metadata["element"] = "Preparation";
        this._metadata["name"] = "Reassure";
        this._metadata["description"] = "+1 temporary patience for each card in hand.";
        this._metadata["patience"] = "2";
    }
}

public class Eloquence : Card
{
    public Eloquence() : base(16)
    {
        this._metadata["element"] = "Preparation";
        this._metadata["name"] = "Eloquence";
        this._metadata["description"] = "The next conversation card you play gives +10 more compliance";
        this._metadata["patience"] = "1";
    }
}

public class Monologue : Card
{
    public Monologue() : base(17)
    {
        this._metadata["element"] = "Preparation";
        this._metadata["name"] = "Monologue";
        this._metadata["description"] = "+1 card for each sympathy card in hand.";
        this._metadata["patience"] = "2";
    }
}