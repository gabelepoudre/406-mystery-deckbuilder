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

The largest takeaway from this is that <u>SHORT LIVED and NARROW SCOPE</u> branches will be created to add code, and once that code is completely functioning (free of bugs, manually tested) you submit a Pull Request to have the branch code merged into main. 

#### Branch Naming
In my experience, ....

