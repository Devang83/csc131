// CreateMenu.cs created with MonoDevelop
// User: bellb at 12:46 AM 6/5/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
// using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
// using System.Xml.Linq;

public class CreateMenu
{
    static string currentDirectoryClass = "current-directory";
    static string plainDirectoryClass = "plain-directory";


  public CreateMenu()
  {
  }
  
  public static Dictionary<string, string> CreateMenuItem(string path, string title, string filename,
							  string arguments, string displayName, int segmentNumber)
  {
      string id = "";
      if (path.Length >= 2)
      {
          id = path.Remove(0, 2) + "/";
      }

      return CreateMenuItem(id, title, path, filename, arguments, displayName, false, segmentNumber);
  }

  public static string GetCurrentPage(HttpRequest request, int segmentNumber)
  {
      string currentPage = request.Url.Segments[segmentNumber];      
      if (request.Url.Segments[1] == "quickpm.net/")
      {
          currentPage = request.Url.Segments[segmentNumber + 1];
      }
      return currentPage;
  }

  public static Dictionary<string, string> CreateMenuItem(string id, string title, string path, string filename,
                              string arguments, string displayName, bool externalLink, int segmentNumber)
  {
      Dictionary<string, string> menuItem = new Dictionary<string, string>();
      menuItem["Id"] = id;
      menuItem["Title"] = title;
      menuItem["DisplayName"] = displayName;
      menuItem["Path"] = path;
      menuItem["Filename"] = filename;
      menuItem["Arguments"] = arguments;
      menuItem["ExternalLink"] = externalLink.ToString();
      menuItem["SegmentNumber"] = segmentNumber.ToString();
      return menuItem;
  }

  
  public static string CreateMenuItemHtml(System.Web.UI.Page page, Dictionary<string, string> item, bool isDropdown)
  {
    return CreateMenuItemHtml(page, item, "", isDropdown);
  }
  
  public static string CreateMenuItemHtml(System.Web.UI.Page page, Dictionary<string, string> item, string className, bool isDropdown)
  {
    string currentPage = GetCurrentPage(page.Request, Convert.ToInt32(item["SegmentNumber"]));
    string path = item["Path"];        
    string filename = item["Filename"];
    string arguments = item["Arguments"];
    string displayName = item["DisplayName"];
    string id = item["Id"];
    string title = item["Title"];      
    string baseUrl = page.ResolveUrl(path + "/" + filename);
    bool externalLink = Boolean.Parse(item["ExternalLink"]);
    if (externalLink)
    {
        baseUrl = path + "/" + filename;
    }
    if (!externalLink && currentPage == id)
      {
          if (!isDropdown)
          {
              return "<a class=\"" + currentDirectoryClass + "\" id=\"" + id + "\" title=\"" + title + "\" href=\"" + baseUrl + arguments + "\">" + displayName + "</a> &nbsp; ";//displayName + " &nbsp;&nbsp;";       
          }
          else
          {
              return "<a id=\"" + id + "\" title=\"" + title + "\" href=\"" + baseUrl + arguments + "\">" + displayName + "</a>";//displayName + " &nbsp;&nbsp;";       
          }
      }
    else
    {

        if (!isDropdown)
        {
            return "<a id=\"" + id +  "\" title=\"" + title + "\" class=\"" + plainDirectoryClass + "\" " + " href=\"" + baseUrl + arguments + "\">" + displayName + "</a> &nbsp; ";
        }
        else
        {
            return "<a id=\"" + id + "\" title=\"" + title + "\"" + " href=\"" + baseUrl + arguments + "\">" + displayName + "</a> ";
        }
	    
    }    
  }
   
}