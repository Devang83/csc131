using System.Web;
public class Sanitize {
  public static string GetSanitized(string str){
    return str.Replace("'", "").Replace("\"", "").Replace("\n", "").Replace("\r", "");
  }

  public static string Encode(string str){
      return HttpUtility.HtmlEncode(HttpUtility.UrlEncode(GetSanitized(str))).Replace("&", "");
      //return HttpUtility.UrlEncode(GetSanitized(str));      
  }
}