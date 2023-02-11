

/*
 * Derived basic classes for converstion cards, all base stats for the card are hardcoded
 * Stats can only be accessed and not changed in an instance of a card
 * Any special card effects are not implemented yet
 */
public class Bluster : ConversationCard
{
    public Bluster(int id) : base(id)
    {
        this._metadata["element"] = "Intimidation";
        this._metadata["name"] = "bluster";
        this._metadata["description"] = "none";
        this._metadata["patience"] = "-1";
        this._metadata["compliance"] = "10";
    }
}

public class PartingShot : ConversationCard
{
    public PartingShot(int id) : base(id)
    {
        this._metadata["element"] = "Intimidation";
        this._metadata["name"] = "Parting Shot";
        this._metadata["description"] = "Can only be used when patience is at 1(2?).";
        this._metadata["patience"] = "-2";
        this._metadata["compliance"] = "30";
    }
}

public class BrowBeat : ConversationCard
{
    public BrowBeat(int id) : base(id)
    {
        this._metadata["element"] = "Intimidation";
        this._metadata["name"] = "Brow Beat";
        this._metadata["description"] = "Raise compliance by 10 for every card discarded.";
        this._metadata["patience"] = "-2";
        this._metadata["compliance"] = "10";
    }
}

public class BadCop : ConversationCard
{
    public BadCop(int id) : base(id)
    {
        this._metadata["element"] = "Intimidation";
        this._metadata["name"] = "Bad Cop";
        this._metadata["description"] = "Raise compliance by 10 for every Sympathy card in hand";
        this._metadata["patience"] = "-3";
        this._metadata["compliance"] = "10";
    }
}

public class Encourage : ConversationCard
{
    public Encourage(int id) : base(id)
    {
        this._metadata["element"] = "Sympathy";
        this._metadata["name"] = "Encourage";
        this._metadata["description"] = "none";
        this._metadata["patience"] = "-1";
        this._metadata["compliance"] = "10";
    }
}

public class Complement : ConversationCard
{
    public Complement(int id) : base(id)
    {
        this._metadata["element"] = "Sympathy";
        this._metadata["name"] = "Complement";
        this._metadata["description"] = "When played as the first card, it raises compliance by 15 instead of 5.";
        this._metadata["patience"] = "-1";
    }

    public int GetCompliance(bool playedFirst)
    {
        if (playedFirst) { return 15; }
        else return 5;
    }
}

public class SobStory : ConversationCard
{
    public SobStory(int id) : base(id)
    {
        this._metadata["element"] = "Sympathy";
        this._metadata["name"] = "Sob Story";
        this._metadata["description"] = "Raise compliance by 8 for every card drawn so far.";
        this._metadata["patience"] = "-2";
        this._metadata["compliance"] = "8";
    }
}

public class GoodCop : ConversationCard
{
    public GoodCop(int id) : base(id)
    {
        this._metadata["element"] = "Sympathy";
        this._metadata["name"] = "Good Cop";
        this._metadata["description"] = "Raise compliance by 10 for every Intimidation card in hand.";
        this._metadata["patience"] = "-3";
        this._metadata["compliance"] = "10";
    }
}

public class Articulate : ConversationCard
{
    public Articulate(int id) : base(id)
    {
        this._metadata["element"] = "Persuasion";
        this._metadata["name"] = "Articulate";
        this._metadata["description"] = "None";
        this._metadata["patience"] = "-1";
        this._metadata["compliance"] = "10";
    }
}

public class Salutation : ConversationCard
{
    public Salutation(int id) : base(id)
    {
        this._metadata["element"] = "Persuasion";
        this._metadata["name"] = "Salutation";
        this._metadata["description"] = "When played as the first card, draw a card.";
        this._metadata["patience"] = "-1";
        this._metadata["compliance"] = "8";
    }
}

public class Lecture : ConversationCard
{
    public Lecture(int id) : base(id)
    {
        this._metadata["element"] = "Persuasion";
        this._metadata["name"] = "Lecture";
        this._metadata["description"] = "Raise compliance by 10 for every patience still remaining.";
        this._metadata["patience"] = "-3";
        this._metadata["compliance"] = "10";
    }
}

/*
 * Derived basic classes for Preparation cards, all base stats for the card are hardcoded
 */
public class MenacingPresence: PreparationCard
{
    public MenacingPresence(int id) : base(id)
    {
        this._metadata["element"] = "Preparation";
        this._metadata["name"] = "Menacing Presence";
        this._metadata["description"] = "+5 compliance to conversation cards for 3 plays.";
        this._metadata["patience"] = "-1";
    }
}

public class Tirade : PreparationCard
{
    public Tirade(int id) : base(id)
    {
        this._metadata["element"] = "Preparation";
        this._metadata["name"] = "Tirade";
        this._metadata["description"] = "+1 card for each persuasion card in hand.";
        this._metadata["patience"] = "-2";
    }
}

public class Empathize : PreparationCard
{
    public Empathize(int id) : base(id)
    {
        this._metadata["element"] = "Preparation";
        this._metadata["name"] = "Empathize";
        this._metadata["description"] = "The next card you play costs 1 less patience.";
        this._metadata["patience"] = "0";
    }
}

public class Reassure : PreparationCard
{
    public Reassure(int id) : base(id)
    {
        this._metadata["element"] = "Preparation";
        this._metadata["name"] = "Reassure";
        this._metadata["description"] = "+1 temporary patience for each card in hand.";
        this._metadata["patience"] = "-2";
    }
}

public class Eloquence : PreparationCard
{
    public Eloquence(int id) : base(id)
    {
        this._metadata["element"] = "Preparation";
        this._metadata["name"] = "Eloquence";
        this._metadata["description"] = "The next conversation card you play gives +10 more compliance";
        this._metadata["patience"] = "-1";
    }
}

public class Monologue : PreparationCard
{
    public Monologue(int id) : base(id)
    {
        this._metadata["element"] = "Preparation";
        this._metadata["name"] = "Monologue";
        this._metadata["description"] = "+1 card for each sympathy card in hand.";
        this._metadata["patience"] = "-2";
    }
}