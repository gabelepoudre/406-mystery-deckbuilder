# Note: Incomplete

# Practices Guide
Not what you're looking for? Check [The Miscellaneous Guides](guides.md) or [The Style Guide](style.md)


Provides a (somewhat) verbose guide as to the expected practices when developing. Do your best to follow everything here, unless explicitly labelled as optional. If you have a question, do not hesitate to ask in the #tech channel on discord or to DM me @ G_#5865

## Git

As a brief reminder, Git is a Version Control System (VCS) that allows for multiple *versions* of something to be *controlled* without storing entire backups of a previous version. This is done by keeping track of the differences or "diffs" that have modified a project throughout multiple "merges". These incremental changes create a "history" of the project's progress and allows for changes to be rolled back. More important for us is that this technology allows for easy team development as two individuals can work on seperate parts of a project and merge their changes into the same document.

Git itself only contains the version controlling utilities. Storing the "histories" remotely which enables team git use is done by different companies, like GitHub or GitLab. Git also operates on a command line, where changes are manipulated with a few verbs (note: this list is not at all exhaustive)-
- `git add --all` Would add all of your current changes
- `git commit -m "Update practices.md"` Would commit those changes with the message "Update practices.md"
- `git push` Would push your commits to wherever your changes for this project are being stored (configured earlier)

As you can imagine, there are lots of verbs and lots of arguments for those verbs to use command-line git. For that reason, I (note: windows user) use a GUI called "GitHub Desktop" which provides a clean UI for GitHub specifically. Another visual option you might like is TortoiseGit, which is a git UI that builds into a your standard right-click windows menu.

If you you need a more in depth guide on using git, check the [Misc Guides](guides.md#command-line-git-basics) where I have added one on Git Fundamentals

### Workflow
We will be using a VCS workflow called ["Trunk-based" development](guides.md#trunk-based-development-with-pull-requests-blog). Specifically- we will have a "low-trust" TBD workflow where pushing changes directly to main is heavily discouraged, and we instead use low-depth branches and pull-requests for any major change. For those of you with experience with GitFlow (feature branches), be aware
that this is not the same. Where GitFlow creates deep branch patterns such as main->dev->feature_XxXx->..., the focus of Trunk-based is 
to have a single "main" branch and to only create single short lived branches off of the main branch (main->some_code).

Visually, the goal is for the merge history to look like the top of the following image, as opposed to the bottom.

![Visual Representation of TBD vs GitFlow](figs/TBD_vs_flow.png)

The largest takeaway from this is that <u>SHORT LIVED and NARROW SCOPE</u> branches will be created to add code, and once that code is completely functioning (free of bugs, manually tested) you submit a Pull Request to have the branch code merged into main. Whenever you wish to add a non-trivial change to the repo, you should create a branch and start working. A branch should not live more than a few days. If a branch is more than a week old, I will contact you to see if we can break up the additions you are making.

#### Branch Naming
In my experience, it is helpful to tie an owner's name to a branch in small teams. This is **not** to encourage any kind of code ownership, as working on other people's branches is encouraged. It is instead to give an indicator to who needs to be contacted if a branch is long living, or has issues, etc. For this reason, branches should be named [nsid]/[what_the_branch_is_for]. Some examples:

- gjl774/adding_documentation
- xyz123/dialogue_writing_milestone_1
- abc987/typos

Note that while is is *highly* suggested to go no more than a single branch in depth (always branch off of main), it is not prohibited to branch off of your own branches. A common example of when this may be acceptable is if a PR you have created has not been reviewed (see "Pull Requests and Reviews" below for more details) and you wish to work on a feature that depends on the PR'd code. This process can get messy quickly and is no better than GitFlow, so please do your best to avoid it.

### Pull Requests and Reviews

A "Pull Request" is largely what it sounds like- it is a "Request" to "Pull" the changes of a branch into another. It is not a Git implemented feature, but a GitHub feature. It gives a nice UI interface for other members of the team to review changes you have made, comment on them, and request changes **BEFORE** the code is added to the main branch. 

We will be implementing a "1 or more" approval policy for Pull Request, which means *at least* one other member of the team will need to review your changes and approve of them. It is highly encouraged to regularily check for live Pull Requests on GitHub and review them. While everyone on the team is considered a valid reviewer, reviewing Pull Requests will be a high priority for the Tech Lead and Test Lead.

#### Creating a Pull Request
For the docs, consider checking the [Misc Guides](guides.md#pull-request-docs). Additionally, I (Gabriel) plan to do a live PR demo at the earliest convenience over discord. 

Once you have a branch filled with changes, first you should (do your best to) ensure that it is bug-free and has been manually tested. Once this is done, navigate to [our GitHub repo](https://github.com/gabelepoudre/406-mystery-deckbuilder) and ensure that you have selected your branch (it defaults to main) while on the code tab. Once this is done, you should see a green banner that says something along the lines of "Start Pull Request". **WARNING: Ensure that you are merging your_branch into main (main<-your_branch) and not some other branch**. To gain the full benefit of the PR UI for yourself, scroll to the bottom of the PR creation to (something along the lines of) "Open Pull Request". There will be a dropdown menu that allows you to select "Draft Pull Request". Once this has been selected, you can modify your draft and publish at your leisure for review. This also allows you to navigate to the (something along the lines of) "Review Changes" tab, where you can view the changes you have made and *manually review them *which is a mandatory step of the PR process*. After you believe you have completed your PR, you can publish it for review. You should request reviewers using the right menu. It is at your discretion to choose who you wish to request, but I recommend including everyone who may have good insight on the work you have done.

**WARNING: ALWAYS ENSURE THAT YOUR BRANCH HAS BEEN UPDATED TO THE MOST RECENT main BRANCH!!!**

Once your PR has been approved, please:
- *Really* make sure it is up to date with main. You can reverse other people's work if you are not careful here
- Merge into main
- Delete your branch (it will prompt you to do this)

#### Reviewing
Reviewing someone's PR is quite easy. Navigate to the (something along the lines of) "Review Changes" tab. A list of file diffs will be displayed to show you the changes. Read through the individuals code. It is possible on any diff to add a comment refering to 1 or more lines of code. Click and slide over the relevant lines of code and you will be prompted to start a review and add a note. Once a diff has been reviewed, it can be closed to easily keep track of what has been seen. Files labelled as seen will be remembered, so a review can be started at one time and finished another without losing progress. As a highly general rule, all comments that do not need to be answered before approval should be started with "NIT: ..." to imply that the comment is "Nit Picking".

Some things to keep in mind while reviewing the PR:
- Have they adhered to the PR Template?
- Is their PR checklist filled?
- Is a given change out of scope for the PR? This is not a dealbreaker, but commonly a NIT to ensure a change was not kept/commited accidentally
- Are they adhering (somewhat) to the [Style Guide](style.md)
- Are they adhering to this practices document?
- Is the code readable? Is complicated code well documented?
- Is the code free of logical errors (hard to see just reading, do your best)
- Are all additions free of typos (be generous here)?
- Are the number of changed files reasonable? A high file-change count may imply some temp/autogenerated files were added accidentally, or that the branch has not been updated to main
- Others?

Once your review is complete, you can submit it in one of 3 ways:
- Approval, which implies the code is good to be merged. 
- Comment without approval, which implies that you personally do not wish to sign off on the PR, but you do not wish to prevent other reviews from approving the work. Mostly for when there are changes you would *like* done (typos) but don't wish to slow development
- Request Changes, which implies some issue *must* be rectified and the PR cannot be merged until it is fixed and you have okay'd it. Mostly for when there is a bug, logic error, or an egregious violation of the [Style Guide](style.md) or this practices document

## Structure
This one is quite short. Please ensure that code you have added is in some way nicely organized into subfolders. Use common sense:
- Pasting a prefab into the project root is not okay
- Putting a prefab into root/Prefabs/ is okay but one more level of folder (root/Prefabs/Environment) is recommended.
- Placing an asset in the root is also not okay, follow same rules as prefabs
- Placing scripts in the root is really not okay. Scripts should be highly structured (root/Scripts/ClickInteractions/hover_text.cs)

## Code Standards
### Code structure
Generally, the [Style Guide](style.md) is best guide for struture. Only a few "high-level" ideas should be outlined:
- Try to use good OOP principals
- - Script files (and the objects within) should have a single job
- - Strive for low coupling (difficult in the unity environment)
- - Composition is better than inheritance
- Use standard naming for common methods like setters and getters (ex: GetName() is good, RetrieveName() is not so good)
- Use descriptive variable names. "checkIfPlayerKnowsStevenTheCat" is better than "checkSteven"

### Commenting
#### Doc Comments
While it can be tedious, I am asking that all **custom** classes and **non-trivial** methods are multi-line doc commented. Unity built-ins (Start(), Update(), MonoBehaviour etc) do not need to be multi-line commented. Specifically, "trivial" methods include things like constructors, getters, setters, incrementers, or anything painfully obvious. While this is somewhat subjective, use your best judgement and lean towards adding a comment.

Optional, but suggested- Also include a multi-line comment at the top of your scripts that add you as an author and detail the inner workings of the script. 

**NOTE: authors are included ONLY for convenience. Code "ownership" leads to slow and bad development. Whatever you commit to the repo belongs to the group, not you**

Long form example (excuse any errors, I don't have an IDE here):

TestScript.py
```csharp
/*
 * author(s): Gabriel LePoudre, Jane Doe
 * 
 * This script exists as an example of what a "nicely" commented file may look like.
 * 
 * Contains the exhaustive list of cards, as well as an abstract base class they extend.
 * Contains the Attribute enum, which is used to organize card attributes while keeping them serializable
 * 
 * Note: This may not be the best way to create cards as someone needs to manually set all the ids as unique... Perhaps an enum?
 */

// we don't even use these here...
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* the Attribute enum is used to store all attributes that a card can have */
public enum Attribute {
    Intimidation,
    Sympathy,
    Confusion,
}

/*
 * Card is an abstract class that all cards override/inherit from
 * 
 * Note: There is currently no abstract methods defined in this class...
 */
public abstract class Card {

    private readonly int _id;
    private readonly Attribute _attribute;
    private readonly string _name;

    public Card(int id, int attribute, string name) {
        this._id = id;
        this._attribute = (Attribute)attribute;
        this._name = name;
    }

    // some getters
    public int GetId() { return this._id; }
    public Attribute GetAttribute() { return this._attribute; }
    public int GetAttributeAsInt() { return (int)this._attribute; }
    public string GetName() { return this._name; }

    /*
     * This is an example of a "non-trivial" method. You should probably explain what's going on here
     */
    public int AddIdToTheAttributeIntForNoReason() {
        return this._id + this.GetAttributeAsInt();
    }

}

/*
 * #1 SomeNewCard (attribute: Confusion) 
 * 
 * Note: While it may seem redundant at times, try to *always* comment your custom classes 
 */
public class SomeNewCard : Card {
    public SomeNewCard() : base(1, (int)Attribute.Confusion, "SomeNewCard") { }
}

/* #2 AnotherNewCard (attribute: Intimidation) */
public class AnotherNewCard : Card {
    public AnotherNewCard() : base(2, (int)Attribute.Intimidation, "AnotherNewCard") { }
}

/* #3 AThirdNewCard (attribute: Sympathy) */
public class AThirdNewCard : Card {
    public AThirdNewCard() : base(3, (int)Attribute.Sympathy, "AThirdCard") { }
}

```
 

