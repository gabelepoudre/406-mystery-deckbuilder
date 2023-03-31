using System.Collections.Generic;
using UnityEngine;

/*
 * Static class containing a method to return an instance of a card with a given ID
 * Returned object must be cast to it's card type (Conversation/Preparation)
 */
public static class Cards
    {
    public static int totalCardCount = 36;

    public static object CreateCardWithID(int id, bool no_effect = false)
    {
        switch (id)
        {
            case 1:
                return new Bluster(no_effect);
            case 2:
                return new PartingShot(no_effect);
            case 3:
                return new BrowBeat(no_effect);
            case 4:
                return new BadCop(no_effect);
            case 5:
                return new Encourage(no_effect);
            case 6:
                return new Complement(no_effect);
            case 7:
                return new SobStory(no_effect);
            case 8:
                return new GoodCop(no_effect);
            case 9:
                return new Articulate(no_effect);
            case 10:
                return new Salutation(no_effect);
            case 11:
                return new Lecture(no_effect);
            case 12:
                return new MenacingPresence(no_effect);
            case 13:
                return new Tirade(no_effect);
            case 14:
                return new Empathize(no_effect);
            case 15:
                return new Reassure(no_effect);
            case 16:
                return new Eloquence(no_effect);
            case 17:
                return new Monologue(no_effect);
            case 18:
                return new Inquire(no_effect);
            case 19:
                return new EarlyBird(no_effect);
            case 20:
                return new Despair(no_effect);
            case 21:
                return new Accuse(no_effect);
            case 22:
                return new Whisper(no_effect);
            case 23:
                return new Yell(no_effect);
            case 24:
                return new Stare(no_effect);
            case 25:
                return new Sigh(no_effect);
            case 26:
                return new Shrug(no_effect);
            case 27:
                return new Live(no_effect);
            case 28:
                return new Laugh(no_effect);
            case 29:
                return new Love(no_effect);
            case 30:
                return new Lie(no_effect);
            case 31:
                return new Gaslight(no_effect);
            case 32:
                return new Reconsider(no_effect);
            case 33:
                return new Improvise(no_effect);
            case 34:
                return new Bluff(no_effect);
            case 35:
                return new Apologize(no_effect);
            case 36:
                return new Commit(no_effect);
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
    public Bluster(bool noEffect = false) : base(1)
    {
        this._metadata["element"] = "Intimidation";
        this._metadata["name"] = "Bluster";
        this._metadata["description"] = "";
        this._metadata["patience"] = "1";
        this._metadata["compliance"] = "10";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";
    }
}

public class PartingShot : Card
{
    public PartingShot(bool noEffect = false) : base(2)
    {
        this._metadata["element"] = "Intimidation";
        this._metadata["name"] = "Parting Shot";
        this._metadata["description"] = "Triple Compliance when Patience is less than 5.";
        this._metadata["patience"] = "2";
        this._metadata["compliance"] = "15";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";

        if (!noEffect)
        {
            this.__localEffects["Parting Shot!"] = new EPartingShot(this);
        }
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
        private string _desc_1 = "Tripled Compliance while remaining Patience is 5!";

        public EPartingShot(Card c) : base(99) { _parent = c; }
        public string GetDescription() { return _desc_1; }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            if (EncounterConditionals.PatienceEqualTo(5))
            {
                _parent.StackableComplianceMod += 2;
                _parent.DisplayEffect(this);
            }
        }
    }
}


public class BrowBeat : Card
{
    public BrowBeat(bool noEffect = false) : base(3)
    {
        this._metadata["element"] = "Intimidation";
        this._metadata["name"] = "Brow Beat";
        this._metadata["description"] = "Raise Compliance by 2 for every card played.";
        this._metadata["patience"] = "2";
        this._metadata["compliance"] = "10";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";

        if (!noEffect)
        {
            this.__localEffects["Brow Beat!"] = new EBrowBeat(this);
        }
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
        private string _desc_1 = "Raising Compliance by 2 for every card played! (";

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
                _parent.UnstackableComplianceMod += 2 * GameState.Meta.activeEncounter.Value.Statistics.NumberOfPlays;
                _parent.DisplayEffect(this);
            }
        }
    }
}

public class BadCop : Card
{
    public BadCop(bool noEffect = false) : base(4)
    {
        this._metadata["element"] = "Intimidation";
        this._metadata["name"] = "Bad Cop";
        this._metadata["description"] = "Raise Compliance by 10 for every Sympathy card in hand";
        this._metadata["patience"] = "3";
        this._metadata["compliance"] = "10";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";

        if (!noEffect)
        {
            this.__localEffects["Bad Cop!"] = new EBadCop(this);
        }
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
    public Encourage(bool noEffect = false) : base(5)
    {
        this._metadata["element"] = "Sympathy";
        this._metadata["name"] = "Encourage";
        this._metadata["description"] = "";
        this._metadata["patience"] = "1";
        this._metadata["compliance"] = "10";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";
    }
}

public class Complement : Card
{
    public Complement(bool noEffect = false) : base(6)
    {
        this._metadata["element"] = "Sympathy";
        this._metadata["name"] = "Complement";
        this._metadata["description"] = "When no cards have been played, triple Compliance";
        this._metadata["patience"] = "2";
        this._metadata["compliance"] = "15";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";

        if (!noEffect)
        {
            this.__localEffects["Complement!"] = new EComplement(this);
        }
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
    public SobStory(bool noEffect = false) : base(7)
    {
        this._metadata["element"] = "Sympathy";
        this._metadata["name"] = "Sob Story";
        this._metadata["description"] = "Raise Compliance by 4 for every card drawn so far";
        this._metadata["patience"] = "4";
        this._metadata["compliance"] = "0";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";

        if (!noEffect)
        {
            this.__localEffects["Sob Story!"] = new ESobStory(this);
        }
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
        private string _desc_1 = "Raising Compliance by 4 for every card drawn so far! (";

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
                _parent.UnstackableComplianceMod += 4 * GameState.Meta.activeEncounter.Value.Statistics.NumberOfDraws;
                _parent.DisplayEffect(this);
            }
        }
    }
}

public class GoodCop : Card
{
    public GoodCop(bool noEffect = false) : base(8)
    {
        this._metadata["element"] = "Sympathy";
        this._metadata["name"] = "Good Cop";
        this._metadata["description"] = "Raise compliance by 10 for every Intimidation card in hand.";
        this._metadata["patience"] = "3";
        this._metadata["compliance"] = "10";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";

        if (!noEffect)
        {
            this.__localEffects["Good Cop!"] = new EGoodCop(this);
        }
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
    public Articulate(bool noEffect = false) : base(9)
    {
        this._metadata["element"] = "Persuasion";
        this._metadata["name"] = "Articulate";
        this._metadata["description"] = "";
        this._metadata["patience"] = "1";
        this._metadata["compliance"] = "10";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";
    }
}

public class Salutation : Card
{
    public Salutation(bool noEffect = false) : base(10)
    {
        this._metadata["element"] = "Persuasion";
        this._metadata["name"] = "Salutation";
        this._metadata["description"] = "When played as the first card, automatically draw a card";
        this._metadata["patience"] = "2";
        this._metadata["compliance"] = "15";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";

        if (!noEffect)
        {
            this.__localEffects["Salutation!"] = new ESalutation(this);
        }
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
        private string _desc_1 = "No cards have been played, so automatically draw a card on play!";

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
                Debug.Log("Triggered a draw card effect!");
                GameState.Meta.activeEncounter.Value.DrawCard(0);
            }
        }
    }

}

public class Lecture : Card
{
    public Lecture(bool noEffect = false) : base(11)
    {
        this._metadata["element"] = "Persuasion";
        this._metadata["name"] = "Lecture";
        this._metadata["description"] = "Raise Compliance by 2 for every Patience still remaining";
        this._metadata["patience"] = "3";
        this._metadata["compliance"] = "5";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";

        if (!noEffect) { this.__localEffects["Lecture!"] = new ELecture(this); }
        
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
        private string _desc_1 = "Raising Compliance by 2 for every Patience remaining! (";

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
                _parent.UnstackableComplianceMod += 2 * GameState.Meta.activeEncounter.Value.Statistics.Patience;
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
    public MenacingPresence(bool noEffect = false) : base(12)
    {
        this._metadata["element"] = "Preparation";
        this._metadata["name"] = "Menacing Presence";
        this._metadata["description"] = "+5 Compliance to Conversation cards for 3 plays";
        this._metadata["patience"] = "1";
        this._metadata["compliance"] = "0";
    }

    public override void OnPlay()
    {
        GameState.Meta.activeEncounter.Value.AddGlobal(new EMenacingPresence());
    }

    /* A local effect (as seen by E prefix) for MenacingPresence */
    public class EMenacingPresence : Effect, IExecutableEffect
    {
        private Color _color = new Color(200 / 255, 200 / 255, 200 / 255);

        private string _name = "Lecture!";
        private string _desc_1 = "+5 Compliance to Conversation cards for 3 plays (";

        public EMenacingPresence() : base(3) {}
        public string GetDescription()
        {
            return _desc_1 + this.GetRemainingDuration().ToString() + " remaining)";
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            List<Card> hand = GameState.Meta.activeEncounter.Value.GetHand();
            foreach(Card c in hand)
            {
                if (c.GetElement() != "Preparation")
                {
                    c.UnstackableComplianceMod += 5;
                    c.DisplayEffect(this);
                }
            }
        }
    }
}

public class Tirade : Card
{
    public Tirade(bool noEffect = false) : base(13)
    {
        this._metadata["element"] = "Preparation";
        this._metadata["name"] = "Tirade";
        this._metadata["description"] = "The next card you play has quadruple Compliance and Patience";
        this._metadata["patience"] = "2";
        this._metadata["compliance"] = "0";
    }

    public override void OnPlay()
    {
        GameState.Meta.activeEncounter.Value.AddGlobal(new ETirade());
    }

    /* A local effect (as seen by E prefix) for Tirade */
    public class ETirade : Effect, IExecutableEffect
    {
        private Color _color = new Color(200 / 255, 200 / 255, 200 / 255);

        private string _name = "Tirade!";
        private string _desc_1 = "For this play only, this card has quadruple Compliance and Patience!";

        public ETirade() : base(1) { }
        public string GetDescription()
        {
            return _desc_1;
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            List<Card> hand = GameState.Meta.activeEncounter.Value.GetHand();
            foreach (Card c in hand)
            {
                c.StackableComplianceMod += 4;
                c.StackablePatienceMod += 4;
                c.DisplayEffect(this);
            }
        }
    }
}

public class Empathize : Card
{
    public Empathize(bool noEffect = false) : base(14)
    {
        this._metadata["element"] = "Preparation";
        this._metadata["name"] = "Empathize";
        this._metadata["description"] = "The next card you play costs 0 Patience";
        this._metadata["patience"] = "0";
        this._metadata["compliance"] = "0";
    }

    public override void OnPlay()
    {
        GameState.Meta.activeEncounter.Value.AddGlobal(new EEmpathize());
    }

    /* A local effect (as seen by E prefix) for Empathize */
    public class EEmpathize : Effect, IExecutableEffect
    {
        private Color _color = new Color(200 / 255, 200 / 255, 200 / 255);

        private string _name = "Empathize!";
        private string _desc_1 = "For this play only, this card costs 0 Patience!";

        public EEmpathize() : base(1) { }
        public string GetDescription()
        {
            return _desc_1;
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            List<Card> hand = GameState.Meta.activeEncounter.Value.GetHand();
            foreach (Card c in hand)
            {
                c.PatienceOverridden = true;
                c.PatienceOverride = 0;
                c.DisplayEffect(this);
            }
        }
    }
}

public class Reassure : Card
{
    public Reassure(bool noEffect = false) : base(15)
    {
        this._metadata["element"] = "Preparation";
        this._metadata["name"] = "Reassure";
        this._metadata["description"] = "Gain a patience free play for each Conversation card in your hand";
        this._metadata["patience"] = "5";
        this._metadata["compliance"] = "0";
    }

    public override void OnPlay()
    {
        GameState.Meta.activeEncounter.Value.AddGlobal(new EReassure());
    }

    /* A local effect (as seen by E prefix) for Reassure */
    public class EReassure : Effect, IExecutableEffect
    {
        private Color _color = new Color(200 / 255, 200 / 255, 200 / 255);

        private string _name = "Reassure!";
        private string _desc_1 = "For the next ";
        private string _desc_2 = " plays only, this card costs 0 Patience!";

        public EReassure() : base(GameState.Meta.activeEncounter.Value.Statistics.ConversationCardsInHand) { }
        public string GetDescription()
        {
            return _desc_1 + this.GetRemainingDuration() + _desc_2;
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            List<Card> hand = GameState.Meta.activeEncounter.Value.GetHand();
            foreach (Card c in hand)
            {
                c.PatienceOverridden = true;
                c.PatienceOverride = 0;
                c.DisplayEffect(this);
            }
        }
    }
}

public class Eloquence : Card
{
    public Eloquence(bool noEffect = false) : base(16)
    {
        this._metadata["element"] = "Preparation";
        this._metadata["name"] = "Eloquence";
        this._metadata["description"] = "The next Conversation card you play gives +10 more Compliance";
        this._metadata["patience"] = "1";
        this._metadata["compliance"] = "0";
    }

    public override void OnPlay()
    {
        GameState.Meta.activeEncounter.Value.AddGlobal(new EEloquence());
    }

    /* A local effect (as seen by E prefix) for Empathize */
    public class EEloquence : Effect, IExecutableEffect
    {
        private Color _color = new Color(200 / 255, 200 / 255, 200 / 255);

        private int _conv_cards_played_on_execute = -1;
        private string _name = "Eloquence!";
        private string _desc_1 = "For this play only, this card gains +10 Compliance!";

        public EEloquence() : base(99) { }
        public string GetDescription()
        {
            return _desc_1;
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            if (_conv_cards_played_on_execute == -1)
            {
                _conv_cards_played_on_execute = GameState.Meta.activeEncounter.Value.Statistics.ConversationCardsPlayed;
            }
            else if (_conv_cards_played_on_execute < GameState.Meta.activeEncounter.Value.Statistics.ConversationCardsPlayed)
            {
                this.__forceTermination = true;
                return;
            }

            List<Card> hand = GameState.Meta.activeEncounter.Value.GetHand();
            foreach (Card c in hand)
            {
                if (c.GetElement() != "Preparation")
                {
                    c.UnstackableComplianceMod += 10;
                }
                c.DisplayEffect(this);
            }
        }
    }
}

public class Monologue : Card
{
    public Monologue(bool noEffect = false) : base(17)
    {
        this._metadata["element"] = "Preparation";
        this._metadata["name"] = "Monologue";
        this._metadata["description"] = "Automatically draw up to 2 cards without reducing Patience";
        this._metadata["patience"] = "0";
        this._metadata["compliance"] = "0";
    }

    public override void OnPlay()
    {
        IExecutableEffect e = new EMonologue();
        e.Execute();
    }

    /* A local effect (as seen by E prefix) for Salutation */
    public class EMonologue : Effect, IExecutableEffect
    {
        private Color _color = new Color(200 / 255, 200 / 255, 200 / 255);

        private string _name = "Monologue!";
        private string _desc_1 = "Automatically draw up to 2 cards without reducing Patience!";

        public EMonologue() : base(1) {}
        public string GetDescription()
        {
            return _desc_1;
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            Debug.Log("Triggered a draw card effect!");
            GameState.Meta.activeEncounter.Value.DrawCard(0);
            GameState.Meta.activeEncounter.Value.DrawCard(0);
        }
    }
}


public class Inquire : Card
{
    public Inquire(bool noEffect = false) : base(18)
    {
        this._metadata["element"] = "Persuasion";
        this._metadata["name"] = "Inquire";
        this._metadata["description"] = "Raise Compliance by 10 for every Persuasion card in hand";
        this._metadata["patience"] = "3";
        this._metadata["compliance"] = "10";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";

        if (!noEffect)
        {
            this.__localEffects["Inquire!"] = new EInquire(this);
        }
    }

    public override void OnChange()
    {
        this.__localEffects["Inquire!"].Execute();
    }

    /* A local effect (as seen by E prefix) for Inquire */
    public class EInquire : Effect, IExecutableEffect
    {
        private Color _color = new Color(255 / 255, 255 / 255, 100 / 255);

        private Card _parent;
        private string _name = "Inquire!";
        private string _desc_1 = "Raising Compliance by 10 for every Persuasion card in hand! (";

        public EInquire(Card c) : base(99) { _parent = c; }
        public string GetDescription()
        {
            return _desc_1 + GameState.Meta.activeEncounter.Value.Statistics.PersuasionCardsInHand + ")";
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            if (EncounterConditionals.CardsOfElementInHandGreaterThan("Persuasion", 0))
            {
                _parent.UnstackableComplianceMod += 10 * GameState.Meta.activeEncounter.Value.Statistics.PersuasionCardsInHand;
                _parent.DisplayEffect(this);
            }
        }
    }
}

public class EarlyBird : Card
    {
    public EarlyBird(bool noEffect = false) : base(19)
    {
        this._metadata["element"] = "Intimidation";
        this._metadata["name"] = "Early Bird";
        this._metadata["description"] = "Raise Compliance by 1 for every card left in the deck";
        this._metadata["patience"] = "3";
        this._metadata["compliance"] = "5";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";

        if (!noEffect)
        {
            this.__localEffects["Early Bird!"] = new EEarlyBird(this);
        }
    }

    public override void OnChange()
    {
        this.__localEffects["Early Bird!"].Execute();
    }

    /* A local effect (as seen by E prefix) for Early Bird */
    public class EEarlyBird : Effect, IExecutableEffect
    {
        private Color _color = new Color(255 / 255, 255 / 255, 100 / 255);

        private Card _parent;
        private string _name = "Early Bird!";
        private string _desc_1 = "Raising Compliance by 1 for every card in the deck (";

        public EEarlyBird(Card c) : base(99) { _parent = c; }
        public string GetDescription()
        {
            return _desc_1 + GameState.Player.dailyDeck.Value.Count + ")";
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {

            _parent.UnstackableComplianceMod += 1 * (GameState.Player.dailyDeck.Value.Count);
            _parent.DisplayEffect(this);
        }
    }
}

public class Despair : Card
{
    public Despair(bool noEffect = false) : base(20)
    {
        this._metadata["element"] = "Sympathy";
        this._metadata["name"] = "Despair";
        this._metadata["description"] = "Double Compliance when Patience is less than 50%";
        this._metadata["patience"] = "2";
        this._metadata["compliance"] = "10";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";

        if (!noEffect)
        {
            this.__localEffects["Despair!"] = new EDespair(this);
        }
    }

    public override void OnChange()
    {
        this.__localEffects["Despair!"].Execute();
    }


    /* A local effect (as seen by E prefix) for despair */
    public class EDespair : Effect, IExecutableEffect
    {
        private Color _color = new Color(255 / 255, 255 / 255, 100 / 255);

        private Card _parent;
        private string _name = "Despair!";
        private string _desc_1 = "Doubled Compliance while remaining Patience is less than 50%!";

        public EDespair(Card c) : base(99) { _parent = c; }
        public string GetDescription() { return _desc_1; }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            if (GameState.Meta.activeEncounter.Value.Statistics.PatiencePercentOfTotal < 0.5)
            {
                _parent.StackableComplianceMod += 1;
                _parent.DisplayEffect(this);
            }
        }
    }
}


public class Accuse : Card
{
    public Accuse(bool noEffect = false) : base(21)
    {
        this._metadata["element"] = "Intimidation";
        this._metadata["name"] = "Accuse";
        this._metadata["description"] = "Double Compliance when Patience is more than 50%";
        this._metadata["patience"] = "2";
        this._metadata["compliance"] = "10";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";

        if (!noEffect)
        {
            this.__localEffects["Accuse!"] = new EAccuse(this);
        }
    }

    public override void OnChange()
    {
        this.__localEffects["Accuse!"].Execute();
    }


    /* A local effect (as seen by E prefix) for accuse */
    public class EAccuse : Effect, IExecutableEffect
    {
        private Color _color = new Color(255 / 255, 255 / 255, 100 / 255);

        private Card _parent;
        private string _name = "Accuse!";
        private string _desc_1 = "Doubled Compliance while remaining Patience is more than 50%!";

        public EAccuse(Card c) : base(99) { _parent = c; }
        public string GetDescription() { return _desc_1; }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            if (GameState.Meta.activeEncounter.Value.Statistics.PatiencePercentOfTotal > 0.5)
            {
                _parent.StackableComplianceMod += 1;
                _parent.DisplayEffect(this);
            }
        }
    }
}


public class Whisper : Card
{
    public Whisper(bool noEffect = false) : base(22)
    {
        this._metadata["element"] = "Sympathy";
        this._metadata["name"] = "Whisper";
        this._metadata["description"] = "The next three cards you play will have +10 compliance!";
        this._metadata["patience"] = "2";
        this._metadata["compliance"] = "5";

    }

    public override void OnPlay()
    {
        GameState.Meta.activeEncounter.Value.AddGlobal(new EWhisper());
    }



    /* A local effect (as seen by E prefix) for whisper */
    public class EWhisper : Effect, IExecutableEffect
    {
        private Color _color = new Color(50 / 255, 150 / 255, 255 / 255);

        private Card _parent;
        private string _name = "Whisper!";
        private string _desc_1 = "For the next ";

        private string _desc_2 = " plays only, this card has +10 compliance!";


        public EWhisper() : base(3) { }
        public string GetDescription() 
        { 
            return _desc_1 + this.GetRemainingDuration() + _desc_2; 
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            List<Card> hand = GameState.Meta.activeEncounter.Value.GetHand();
            foreach (Card c in hand)
            {
                c.UnstackableComplianceMod += 5;
                c.DisplayEffect(this);
             }
        }
    }
}


public class Yell : Card
{
    public Yell(bool noEffect = false) : base(23)
    {
        this._metadata["element"] = "Intimidation";
        this._metadata["name"] = "Yell";
        this._metadata["description"] = "The next card you play will have -10 Compliance!";
        this._metadata["patience"] = "5";
        this._metadata["compliance"] = "30";
    }

    public override void OnPlay()
    {
        GameState.Meta.activeEncounter.Value.AddGlobal(new EYell());
    }


    /* A local effect (as seen by E prefix) for yell */
    public class EYell : Effect, IExecutableEffect
    {
        private Color _color = new Color(255 / 255, 255 / 255, 100 / 255);

        private Card _parent;
        private string _name = "Yell!";
                private string _desc_1 = "For the next ";

        private string _desc_2 = " plays only, this card has -10 Compliance!";

        public EYell() : base(1) { }
        public string GetDescription() 
        { 
            return _desc_1 + this.GetRemainingDuration() + _desc_2; 
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            List<Card> hand = GameState.Meta.activeEncounter.Value.GetHand();
            foreach (Card c in hand)
            {
                c.UnstackableComplianceMod -= 10;
                c.DisplayEffect(this);
            }
        }
    }
}


public class Stare : Card
{
    public Stare(bool noEffect = false) : base(24)
    {
        this._metadata["element"] = "Intimidation";
        this._metadata["name"] = "Stare";
        this._metadata["description"] = "";
        this._metadata["patience"] = "0";
        this._metadata["compliance"] = "0";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";
    }
}


public class Sigh : Card
{
    public Sigh(bool noEffect = false) : base(25)
    {
        this._metadata["element"] = "Sympathy";
        this._metadata["name"] = "Sigh";
        this._metadata["description"] = "";
        this._metadata["patience"] = "0";
        this._metadata["compliance"] = "0";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";
    }
}


public class Shrug : Card
{
    public Shrug(bool noEffect = false) : base(26)
    {
        this._metadata["element"] = "Persuasion";
        this._metadata["name"] = "Shrug";
        this._metadata["description"] = "";
        this._metadata["patience"] = "0";
        this._metadata["compliance"] = "0";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";
    }
}


public class Live : Card
{
    public Live(bool noEffect = false) : base(27)
    {
        this._metadata["element"] = "Intimidation";
        this._metadata["name"] = "Live";
        this._metadata["description"] = "3x the compliance of the card \"Laugh\" if played next";
        this._metadata["patience"] = "2";
        this._metadata["compliance"] = "10";
    }
}


public class Laugh : Card
{
    public Laugh(bool noEffect = false) : base(28)
    {
        this._metadata["element"] = "Persuasion";
        this._metadata["name"] = "Laugh";
        this._metadata["description"] = "3x the compliance of the card \"Love\" if played next";
        this._metadata["patience"] = "2";
        this._metadata["compliance"] = "10";

        if (!noEffect) { this.__localEffects["Laugh!"] = new ELaugh(this); }
    }

    public override void OnChange()
    {
        this.__localEffects["Laugh!"].Execute();

    }


    /* A local effect (as seen by E prefix) for laugh */
    public class ELaugh : Effect, IExecutableEffect
    {
        private Color _color = new Color(255 / 255, 255 / 255, 100 / 255);

        private Card _parent;
        private string _name = "Live Laugh!";
        private string _desc_1 = "3x compliance for this turn only!";


        public ELaugh(Card c) : base(2) { _parent = c; }
        public string GetDescription() 
        { 
            return _desc_1; 
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            int lastCardId = GameState.Meta.activeEncounter.Value.Statistics.ListOfPlayedCards.Count - 1;
            if (lastCardId < 0)
            {
                return;
            }

            Card lastCard = (Card)Cards.CreateCardWithID(GameState.Meta.activeEncounter.Value.Statistics.ListOfPlayedCards[lastCardId]);
            if (lastCard.GetName() == "Live")
            {
                _parent.StackableComplianceMod += 2;
                _parent.DisplayEffect(this);
            }
        }
    }
}


public class Love : Card
{
    public Love(bool noEffect = false) : base(29)
    {
        this._metadata["element"] = "Sympathy";
        this._metadata["name"] = "Love";
        this._metadata["description"] = "6x compliance if played sequentially after cards \"Live\" and \"Laugh\"";
        this._metadata["patience"] = "2";
        this._metadata["compliance"] = "10";

        if (!noEffect) { this.__localEffects["LoveMinor!"] = new ELoveMinor(this); }
        if (!noEffect) { this.__localEffects["LoveMajor!"] = new ELoveMajor(this); }
    }

    public override void OnChange()
    {
        this.__localEffects["LoveMinor!"].Execute();
        this.__localEffects["LoveMajor!"].Execute();
    }


    /* A local effect (as seen by E prefix) for love */
    public class ELoveMinor : Effect, IExecutableEffect
    {
        private Color _color = new Color(255 / 255, 255 / 255, 100 / 255);

        private Card _parent;
        private string _name = "Laugh!";
        private string _desc_1 = "3x compliance for this play only!";


        public ELoveMinor(Card c) : base(2) { _parent = c; }
        public string GetDescription() 
        { 
            return _desc_1; 
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            int lastCardId = GameState.Meta.activeEncounter.Value.Statistics.ListOfPlayedCards.Count - 1;
            int secondLastCardId = GameState.Meta.activeEncounter.Value.Statistics.ListOfPlayedCards.Count - 2;
            if (lastCardId < 0 || secondLastCardId < 0)
            {
                return;
            }

            Card lastCard = (Card)Cards.CreateCardWithID(GameState.Meta.activeEncounter.Value.Statistics.ListOfPlayedCards[lastCardId]);
            Card secondLastCard = (Card)Cards.CreateCardWithID(GameState.Meta.activeEncounter.Value.Statistics.ListOfPlayedCards[secondLastCardId]);

            if (lastCard.GetName() == "Laugh" && secondLastCard.GetName() != "Live")
            {
                _parent.StackableComplianceMod += 2;
                _parent.DisplayEffect(this);
            }
        }
    }

    /* A local effect (as seen by E prefix) for love */
    public class ELoveMajor : Effect, IExecutableEffect
    {
        private Color _color = new Color(255 / 255, 100 / 255, 255 / 255);

        private Card _parent;
        private string _name = "LIVE LAUGH LOVE!";
        private string _desc_1 = "6x Compliance for this play only!";


        public ELoveMajor(Card c) : base(2) { _parent = c; }
        public string GetDescription()
        {
            return _desc_1;
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            int lastCardId = GameState.Meta.activeEncounter.Value.Statistics.ListOfPlayedCards.Count - 1;
            int secondLastCardId = GameState.Meta.activeEncounter.Value.Statistics.ListOfPlayedCards.Count - 2;
            if (lastCardId < 0 || secondLastCardId < 0)
            {
                return;
            }

            Card lastCard = (Card)Cards.CreateCardWithID(GameState.Meta.activeEncounter.Value.Statistics.ListOfPlayedCards[lastCardId]);
            Card secondLastCard = (Card)Cards.CreateCardWithID(GameState.Meta.activeEncounter.Value.Statistics.ListOfPlayedCards[secondLastCardId]);

            if (lastCard.GetName() == "Laugh" && secondLastCard.GetName() == "Live")
            {
                _parent.StackableComplianceMod += 5;
                _parent.DisplayEffect(this);
            }
        }
    }
}


public class Lie : Card
{
    public Lie(bool noEffect = false) : base(30)
    {
        this._metadata["element"] = "Persuasion";
        this._metadata["name"] = "Lie";
        this._metadata["description"] = "It's unpredictable! Compliance may skyrocket when you draw or play";
        this._metadata["patience"] = "3";
        this._metadata["compliance"] = "0";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";

        if (!noEffect)
        {
            this.__localEffects["Lie!"] = new EWildAccusation(this);
        }
    }

    public override void OnChange()
    {
        this.__localEffects["Lie!"].Execute();
    }

    /* A local effect (as seen by E prefix) for wild accusation */
    public class EWildAccusation : Effect, IExecutableEffect
    {
        private Color _color = new Color(255 / 255, 255 / 255, 100 / 255);

        private Card _parent;
        private string _name = "Lie!";
        private string _desc_1 = "Lucky you! If you play this card right now, +30 compliance!";

        public EWildAccusation(Card c) : base(99) { _parent = c; }
        public string GetDescription()
        {
            return _desc_1;
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            float r = Random.value;
            if (r > 0.9f)
            {
                _parent.UnstackableComplianceMod += 30;
                 _parent.DisplayEffect(this);
            }
        }
    }
}


public class Gaslight : Card
{
    public Gaslight(bool noEffect = false) : base(31)
    {
        this._metadata["element"] = "Persuasion";
        this._metadata["name"] = "Gaslight";
        this._metadata["description"] = "Undo your last play. It never happened!";
        this._metadata["patience"] = "0";
        this._metadata["compliance"] = "0";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";

        if (!noEffect)
        {
            this.__localEffects["Gaslight!"] = new EGaslight(this);
        }
    }

    public override void OnChange()
    {
        this.__localEffects["Gaslight!"].Execute();
    }

    /* A local effect (as seen by E prefix) for gaslight */
    public class EGaslight : Effect, IExecutableEffect
    {
        private Color _color = new Color(255 / 255, 255 / 255, 100 / 255);

        private Card _parent;
        private string _name = "Gaslight!";
        private string _desc_1 = "Undo your last play's compliance and patience, it never happened!";

        public EGaslight(Card c) : base(99) { _parent = c; }
        public string GetDescription()
        {
            return _desc_1;
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            _parent.ComplianceOverridden = true;
            _parent.PatienceOverridden = true;
            _parent.ComplianceOverride = -GameState.Meta.activeEncounter.Value.Statistics.LastComplianceDamage;
            _parent.PatienceOverride = -GameState.Meta.activeEncounter.Value.Statistics.LastPatienceDamage;
        }
    }
}


public class Reconsider : Card
{
    public Reconsider(bool noEffect = false) : base(32)
    {
        this._metadata["element"] = "Preparation";
        this._metadata["name"] = "Reconsider";
        this._metadata["description"] = "Clears your hand for a fresh start";
        this._metadata["patience"] = "3";
        this._metadata["compliance"] = "0";
    }

    public override void OnPlay()
    {
        IExecutableEffect e = new EReconsider();
        e.Execute();
    }

    /* A local effect (as seen by E prefix) for reconsider */
    public class EReconsider : Effect, IExecutableEffect
    {
        private Color _color = new Color(200 / 255, 200 / 255, 200 / 255);

        private string _name = "Reconsider!";
        private string _desc_1 = "Clears your hand for a fresh start!";

        public EReconsider() : base(1) { }
        public string GetDescription()
        {
            return _desc_1;
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            Debug.Log("Triggered clear hand effect!");
            foreach (Card c in GameState.Meta.activeEncounter.Value.GetHand())
            {
                GameState.Meta.activeEncounter.Value.GetEncounterController().RemoveCard(c);
                GameState.Player.dailyDeck.Value.Add(c.GetId());
            }
            GameState.Meta.activeEncounter.Value.GetHand().Clear();
            GameState.Player.dailyDeck.Raise();
            GameState.Meta.activeEncounter.Value.RecalculateHandStatistics();
        }
    }
}


public class Improvise : Card
{
    public Improvise(bool noEffect = false) : base(33)
    {
        this._metadata["element"] = "Preparation";
        this._metadata["name"] = "Improvise";
        this._metadata["description"] = "Transforms into a random card";
        this._metadata["patience"] = "1";
        this._metadata["compliance"] = "0";
    }

    public override void OnPlay()
    {
        IExecutableEffect e = new EImprovise();
        e.Execute();
    }

    /* A local effect (as seen by E prefix) for improvise */
    public class EImprovise : Effect, IExecutableEffect
    {
        private Color _color = new Color(200 / 255, 200 / 255, 200 / 255);

        private string _name = "Improvise!";
        private string _desc_1 = "Spawns any card from the game!";

        public EImprovise() : base(1) { }
        public string GetDescription()
        {
            return _desc_1;
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            int card = Random.Range(1, Cards.totalCardCount + 1);
            GameState.Meta.activeEncounter.Value.ForceCardInHand(card);
        }
    }
}

public class Bluff : Card
{
    public Bluff(bool noEffect = false) : base(34)
    {
        this._metadata["element"] = "Persuasion";
        this._metadata["name"] = "Bluff";
        this._metadata["description"] = "It's unpredictable! Compliance randomly changes on draw or play";
        this._metadata["patience"] = "4";
        this._metadata["compliance"] = "15";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";

        if (!noEffect)
        {
            this.__localEffects["Bluff!"] = new EBluff(this);
        }
    }

    public override void OnChange()
    {
        this.__localEffects["Bluff!"].Execute();
    }

    /* A local effect (as seen by E prefix) for wild accusation */
    public class EBluff : Effect, IExecutableEffect
    {
        private Color _color = new Color(255 / 255, 255 / 255, 100 / 255);

        private Card _parent;
        private string _name = "Bluff!";
        private string _desc_1 = "Right now, bluffing will do ";
        private string _desc_2 = ", but that could change at any time!";

        public EBluff(Card c) : base(99) { _parent = c; }
        public string GetDescription()
        {
            return _desc_1 + _parent.GetTotalCompliance() + _desc_2;
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            int r = Random.Range(-10, 41);
            _parent.ComplianceOverridden = true;
            _parent.ComplianceOverride = r;
            _parent.DisplayEffect(this);
        }
    }
}

public class Apologize : Card
{
    public Apologize(bool noEffect = false) : base(35)
    {
        this._metadata["element"] = "Sympathy";
        this._metadata["name"] = "Apologize";
        this._metadata["description"] = "3x Compliance if played immedietely after an Intimidation card";
        this._metadata["patience"] = "3";
        this._metadata["compliance"] = "10";

        if (!noEffect) { this.__localEffects["Apologize!"] = new EApologize(this); }
    }

    public override void OnChange()
    {
        this.__localEffects["Apologize!"].Execute();

    }


    /* A local effect (as seen by E prefix) for apologize */
    public class EApologize : Effect, IExecutableEffect
    {
        private Color _color = new Color(255 / 255, 255 / 255, 100 / 255);

        private Card _parent;
        private string _name = "Apologize!";
        private string _desc_1 = "3x Compliance for this turn only!";


        public EApologize(Card c) : base(1) { _parent = c; }
        public string GetDescription()
        {
            return _desc_1;
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            if (GameState.Meta.activeEncounter.Value.Statistics.LastPlayElement == "Intimidation")
            {
                _parent.StackableComplianceMod += 2;
                _parent.DisplayEffect(this);
            }
        }
    }
}

public class Commit : Card
{
    public Commit(bool noEffect = false) : base(36)
    {
        this._metadata["element"] = "Preparation";
        this._metadata["name"] = "Commit";
        this._metadata["description"] = "For 1 play, card Elements mimic the last played Conversation card's Element";
        this._metadata["patience"] = "0";
        this._metadata["compliance"] = "0";
    }

    public override void OnPlay()
    {
        GameState.Meta.activeEncounter.Value.InsertGlobal(new ECommit(), 0);
    }

    /* A local effect (as seen by E prefix) for Empathize */
    public class ECommit : Effect, IExecutableEffect
    {
        private Color _color = new Color(200 / 255, 200 / 255, 200 / 255);

        private string _name = "Commit!";
        private string _desc_1 = "For this play only, this card is treated as a ";
        private string _desc_2 = " Card!";

        public ECommit() : base(1) { }
        public string GetDescription()
        {
            return _desc_1 + GameState.Meta.activeEncounter.Value.Statistics.LastPlayElement + _desc_2; 
        }
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        /* Executes the effect. A conditional may be called within */
        public void Execute()
        {
            List<Card> hand = GameState.Meta.activeEncounter.Value.GetHand();
            foreach (Card c in hand)
            {
                if (c.GetElement() != "Preparation" && GameState.Meta.activeEncounter.Value.Statistics.LastPlayElement != "")
                {
                    c.ElementOverridden = true;
                    c.ElementOverride = GameState.Meta.activeEncounter.Value.Statistics.LastPlayElement;
                    c.DisplayEffect(this);
                }
            }
            GameState.Meta.activeEncounter.Value.RecalculateHandStatistics();
        }
    }
}