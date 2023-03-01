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
        this._metadata["name"] = "bluster";
        this._metadata["description"] = "none";
        this._metadata["patience"] = "-1";
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
        this._metadata["description"] = "Can only be used when patience is at 1(2?).";
        this._metadata["patience"] = "-2";
        this._metadata["compliance"] = "30";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";
    }
}

public class BrowBeat : Card
{
    public BrowBeat() : base(3)
    {
        this._metadata["element"] = "Intimidation";
        this._metadata["name"] = "Brow Beat";
        this._metadata["description"] = "Raise compliance by 10 for every card discarded.";
        this._metadata["patience"] = "-2";
        this._metadata["compliance"] = "10";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";
    }
}

public class BadCop : Card
{
    public BadCop() : base(4)
    {
        this._metadata["element"] = "Intimidation";
        this._metadata["name"] = "Bad Cop";
        this._metadata["description"] = "Raise compliance by 10 for every Sympathy card in hand";
        this._metadata["patience"] = "-3";
        this._metadata["compliance"] = "10";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";
    }
}

public class Encourage : Card
{
    public Encourage() : base(5)
    {
        this._metadata["element"] = "Sympathy";
        this._metadata["name"] = "Encourage";
        this._metadata["description"] = "none";
        this._metadata["patience"] = "-1";
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
        this._metadata["description"] = "When played as the first card, it raises compliance by 15 instead of 5.";
        this._metadata["patience"] = "-1";
        this._metadata["compliance"] = "5";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";
    }
}

public class SobStory : Card
{
    public SobStory() : base(7)
    {
        this._metadata["element"] = "Sympathy";
        this._metadata["name"] = "Sob Story";
        this._metadata["description"] = "Raise compliance by 8 for every card drawn so far.";
        this._metadata["patience"] = "-2";
        this._metadata["compliance"] = "8";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";
    }
}

public class GoodCop : Card
{
    public GoodCop() : base(8)
    {
        this._metadata["element"] = "Sympathy";
        this._metadata["name"] = "Good Cop";
        this._metadata["description"] = "Raise compliance by 10 for every Intimidation card in hand.";
        this._metadata["patience"] = "-3";
        this._metadata["compliance"] = "10";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";
    }
}

public class Articulate : Card
{
    public Articulate() : base(9)
    {
        this._metadata["element"] = "Persuasion";
        this._metadata["name"] = "Articulate";
        this._metadata["description"] = "None";
        this._metadata["patience"] = "-1";
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
        this._metadata["description"] = "When played as the first card, draw a card.";
        this._metadata["patience"] = "-1";
        this._metadata["compliance"] = "8";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";
    }
}

public class Lecture : Card
{
    public Lecture() : base(11)
    {
        this._metadata["element"] = "Persuasion";
        this._metadata["name"] = "Lecture";
        this._metadata["description"] = "Raise compliance by 10 for every patience still remaining.";
        this._metadata["patience"] = "-3";
        this._metadata["compliance"] = "10";
        this._metadata["duration"] = "0";
        this._metadata["filterId"] = "0";
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
        this._metadata["patience"] = "-1";
    }
}

public class Tirade : Card
{
    public Tirade() : base(13)
    {
        this._metadata["element"] = "Preparation";
        this._metadata["name"] = "Tirade";
        this._metadata["description"] = "+1 card for each persuasion card in hand.";
        this._metadata["patience"] = "-2";
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
        this._metadata["patience"] = "-2";
    }
}

public class Eloquence : Card
{
    public Eloquence() : base(16)
    {
        this._metadata["element"] = "Preparation";
        this._metadata["name"] = "Eloquence";
        this._metadata["description"] = "The next conversation card you play gives +10 more compliance";
        this._metadata["patience"] = "-1";
    }
}

public class Monologue : Card
{
    public Monologue() : base(17)
    {
        this._metadata["element"] = "Preparation";
        this._metadata["name"] = "Monologue";
        this._metadata["description"] = "+1 card for each sympathy card in hand.";
        this._metadata["patience"] = "-2";
    }
}