using System;
using System.Data;
using System.Configuration;
//using System.Linq;
using System.IO;
using System.Web;
using System.Net.Mail;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;

/// <summary>
/// Summary description for SendEmail
/// </summary>
public class SendEmail
{
	public SendEmail()
	{
		//
		// TODO: Add constructor logic here
		//
	}    
    private static string password = "11235813";
    private static string sender = "support@quickpm.net";
    public static void Send(string email, string subject, string message, string physicalApplicationPath)
    {
        Send(email, subject, "text/plain", message, physicalApplicationPath, null, null); 
    }


    public static void Send(string email, string subject, string message, string physicalApplicationPath, Stream attachementStream, string attachmentName)
    {
        Send(email, subject, "text/plain", message, physicalApplicationPath, attachementStream, attachmentName);
    }

    public static void Send(string email, string subject, string contentType, string message, string physicalApplicationPath, Stream attachemtnStream, string attachementName)
    {


        MailMessage msg = new MailMessage();
        msg.From = new MailAddress(sender);        
        msg.To.Add(email);        
        msg.Subject = subject;
        msg.Body = message;
		Console.WriteLine(message);
        msg.IsBodyHtml = false;
        msg.BodyEncoding = System.Text.Encoding.UTF8;
        if (attachementName != null && attachemtnStream != null)
        {
            msg.Attachments.Add(new Attachment(attachemtnStream, attachementName));
        }
        SmtpClient smtp = new SmtpClient("outbound.mailhop.org", 25);
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new System.Net.NetworkCredential("bjwbell", password);
        //smtp.EnableSsl = true;
        smtp.Send(msg);      
    }

    public static void Send(string email, string subject, string contentType, string message, string physicalApplicationPath)
    {


        /*string parentPath = physicalApplicationPath;
        string programName = parentPath + "App_Code\\" + "fckmail.bat";

        if (System.Environment.OSVersion.Platform == PlatformID.Unix)
        {
            programName = "sh " + parentPath + "App_Code/" + "fckmail.sh";

        }
        string filePath = parentPath + "App_Data" + System.IO.Path.DirectorySeparatorChar + "tmp.txt";
	    FileStream f = new FileStream(parentPath + "App_Data" + System.IO.Path.DirectorySeparatorChar + "tmp.txt", FileMode.Create);
        StreamWriter sw = new StreamWriter(f);
        string str = "";
        str = username + "\n";
        str += password + "\n";
        str += sender + "\n";
        str += email + "\n";
        str += subject + "\n";
        str += contentType + "\n";
        str += message;
        sw.Write(str);

        sw.Close();
        f.Close();
        
        string arguments = "";
        System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(programName, arguments);
        System.Diagnostics.Process proc = new System.Diagnostics.Process();
        psi.WorkingDirectory = parentPath + "App_Code";
        proc.StartInfo = psi;
        proc.Start();
        */

        Send(email, subject, message, contentType, physicalApplicationPath, null, null);
    }
}
