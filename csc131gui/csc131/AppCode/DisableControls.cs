using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuickPMWebsite.AppCode
{
    public class DisableControls
    {		
        public static void DisableTextBoxControls(Control page)
        {			
            foreach (Control c in page.Controls)
            {
                if (c is TextBox)
                {					
                    ((TextBox)c).Enabled = false;
					((TextBox)c).BackColor = System.Drawing.Color.White;
                    ((TextBox)c).ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    if (c.HasControls())
                    {
                        DisableTextBoxControls(c);
                        
                    }
                }
                

            }
        }
    }
}
