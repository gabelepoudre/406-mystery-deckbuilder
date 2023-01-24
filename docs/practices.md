# Note: Incomplete

# Practices Guide
Provides a (somewhat) verbose guide as to the expected practices when developing. Do your best to follow everything here, unless explicitly labelled as optional. If you have a question, do not hesitate to ask in the #tech channel on discord or to DM me @ G_#5865

## Git

### Workflow
We will be using a VCS workflow called "Trunk-based" development. Specifically- we will have a "low-trust" TBD workflow where pushing changes directly to main is heavily discouraged, and we instead use low-depth branches and pull-requests for any major change. For those of you with experience with GitFlow (feature branches), be aware
that this is not the same. Where GitFlow creates deep branch patterns such as main->dev->feature_XxXx->..., the focus of Trunk-based is 
to have a single "main" branch and to only create single short lived branches off of the main branch (main->some_code).

Visually, the goal is for the merge history to look like the top of the following image, as opposed to the bottom.

![Visual Representation of TBDev vs GitFlow](figs/TBD_vs_flow.png)

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
Note that a guide will be pinned to the bottom of this document detailing how to create a pull request. Additionally, I (Gabriel) plan to do a live PR demo at the earliest convenience over discord. 

Once you have a branch filled with changes, first you should (do your best to) ensure that it is bug-free and has been manually tested. Once this is done, navigate to [our GitHub repo](https://github.com/gabelepoudre/406-mystery-deckbuilder) and ensure that you have selected your branch (it defaults to main) while on the code tab. Once this is done, you should see a green banner that says something along the lines of "Start Pull Request". **WARNING: Ensure that you are merging your_branch into main (main<-your_branch) and not some other branch**. To gain the full benefit of the PR UI for yourself, scroll to the bottom of the PR creation to (something along the lines of) "Open Pull Request". There will be a dropdown menu that allows you to select "Draft Pull Request". Once this has been selected, you can modify your draft and publish at your leisure for review. This also allows you to navigate to the (something along the lines of) "Review Changes" tab, where you can view the changes you have made and *manually review them *which is a mandatory step of the PR process*.

#### Reviewing
Reviewing someone's 
