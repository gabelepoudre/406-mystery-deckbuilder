using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter 
{

    //Chapters holds numpages, title, and contain ID's. 
    public int currentPageID;
    public string title;
    public List<Page> pageList;
    public int numPages;
 
    //Construct a new Chapter
    public Chapter(string title)
    {
        this.title = title;
        this.numPages = 0;
        this.pageList = new List<Page>();
        this.currentPageID = 0;
    }

    //Set pages
    public void SetNumPages(int x)
    {
        this.numPages = x;
        
    }

    //Get pages
    //Returns as an int the number of pages in the chapter
    public int GetNumPages()
    {
        return this.numPages;
    }

    //Add a page to the chapter
    public void AddPage(Page p)
    {
        pageList.Add(p);
        numPages +=1;

    }

    //Set the title of the chapter
    public void SetTitle(string title)
    {
        this.title = title;
    }

    //Get the title of the chapter
    //Returns as a string the title
    public string GetTitle()
    {
        return this.title;
    }

    //Returns as a List of Pages the pageList associated with this chapter
    public List<Page> GetPageList()
    {
        return pageList;

    }


}
