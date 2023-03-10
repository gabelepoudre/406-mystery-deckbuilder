# Style Guidelines
Not what you're looking for? Check [The Miscellaneous Guides](guides.md) or [The Practices Guide](practices.md)

Unless anyone feels strongly against it, we will be employing a relaxed* version of [Google's C# Style Guide](https://google.github.io/styleguide/csharp-style.html). The
most important takeaway from this style guide is the section on naming rules: PascalCase in most cases, camelCase for _privateVariables and parameters. I think as a 
small student coding team we can be quite lax on most non-naming rules presented in the linked guide. Do your best to write clean and readable code.


*A notable relaxation to their style guidelines is their section on "Whitespace", as most (if not all) IDEs will automatically handle spacing and indentation(tab) length.
Additionally, I find the Google style for if-statements ugly and less readable, personally:

** NOTE: We have opted to switch our bracket style to the third example here labelled "Unity". Classes will also follow this bracket structure **

```csharp
// Google
if (someBool) {

    if (someOtherBool) {
        // do something
    } else if (aThirdBool) {
        // do something else
    }
}

// "Normal"
if (someBool) {
    if (someOtherBool) {
        // do something
    }
    else if (aThirdBool) {
        // do something else 
    }
}

// Unity
if (someBool)
{
    if (someOtherBool) 
    {
        // do something
    }
    else if (aThirdBool)
    {
        // do something else
    }
}


```
