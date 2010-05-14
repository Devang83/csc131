using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public class LatLng
{
    public double Lat
    {
        get;
        set;
    }
    public double Lng
    {
        get;
        set;
    }

    public LatLng(double lat, double lng)
    {
        Lat = lat;
        Lng = lng;
    }

    public override string ToString()
    {
        return Lat.ToString() + ", " + Lng.ToString();
    }

    public override bool Equals(object obj)
    {
        if (!(obj is LatLng))
        {
            return false;
        }
        if (obj == null && this != null)
        {
            return false;
        }
        return Lat == ((LatLng)obj).Lat && Lng == ((LatLng)obj).Lng;
    }

    //public override int GetHashCode()
    //{
    //return (int)(Lat * 100000) + (int)(Lng * ;
    //}
}


public partial class Maps_MapControl : System.Web.UI.UserControl
{
    
    public double Lat
    {
        get;
        set;
    }
    public double Lng
    {
        get;
        set;
    }


    public int ZoomLevel
    {
        get;
        set;
    }

    /// <summary>
    /// List of tenants or a list of properties.
    /// </summary>
    public List<object> Places
    {
        get;
        set;
    }

    protected string apiKey = "";

    protected Dictionary<LatLng, List<object>> places = new Dictionary<LatLng, List<object>>();

    protected Dictionary<long, string> propertyAddresses = new Dictionary<long, string>();

	protected string GetHost() 
	{					
		if (Request.ServerVariables["HTTP_HOST"] == null || Request.ServerVariables["HTTP_HOST"].Trim() == "")
		{
			return "";
		}
		string[] url = Request.ServerVariables["HTTP_HOST"].Split(new char[] { ':' });
		string host = url[0];		
		if (host.Contains(".") && !host.Contains("127.0.0.1"))
		{
			string[] parts = host.Split(new char[] {'.'});
			host = parts[parts.Length - 2] + "." + parts[parts.Length - 1];
		}		    
		return host;
	}
	
	protected int GetPort() 
	{
		if (Request.ServerVariables["HTTP_HOST"] == null || Request.ServerVariables["HTTP_HOST"].Trim() == "")
		{
			return -1;
		}
		string[] url = Request.ServerVariables["HTTP_HOST"].Split(new char[] { ':' });
		if (url.Length < 2) 
		{
			return 80;
		}
		int portNumber = -1;
		if (int.TryParse(url[1], out portNumber))
		{
			return portNumber;
		}
		return -1;
	}
	
    protected void Page_Load(object sender, EventArgs e)
    {
        QuickPMWebsite.DatabaseSettings.UpdateDatabaseConnectionString(HttpContext.Current.Profile, Request);
		
        apiKey = QuickPMWebsite.AppCode.GoogleMapsFunctions.GetAPIKey(GetHost(), GetPort());
        PopulatePlaces();
        if (ZoomLevel == 0)
        {
            ZoomLevel = 5;
        }
        if (Lat == 0 && Lng == 0)
        {
            LatLng mean = ComputeMeanLatLng();
            if (mean != null)
            {
                Lat = mean.Lat;
                Lng = mean.Lng;
            }
        }
    }
	
	protected string GenerateJSHtmlCode()
	{
		string code = "";
		foreach(LatLng latlng in places.Keys) { 
        	foreach(object o in places[latlng]) {                 		
            	if(o is QuickPM.Tenant) {
            	QuickPM.Tenant t = (QuickPM.Tenant)o;
            	code += "html = \'" + "<a href=\"" + QuickPMWebsite.AppCode.Link.LinkTo(t, this, this.Context.Profile.IsAnonymous) + "\">" + t.Name.Replace("'", "") + "</a>" + "\';";
            	code += "html = html + \"<br/>\";";
				code += "html = html + \"<a href=\\\"\" + \"http://maps.google.com/maps?q=\" + \"" + Sanitize.Encode(t.GetFullAddress()) + "\" + \"\\\">View In Google</a>\";";            	
            	} else if (o is QuickPM.Property) {
            		QuickPM.Property p = (QuickPM.Property)o;
            		code += "html = \'" + "<a href=\"" + QuickPMWebsite.AppCode.Link.LinkTo(p, this, this.Context.Profile.IsAnonymous) + "\">" + p.Name.Replace("'", "") + "</a>" + "\';";
            		code += "html = html + \"<br/>\";";
					code += "html = html + \"<a href=\\\"\" + \"http://maps.google.com/maps?q=\" + \"" + Sanitize.Encode(propertyAddresses[p.Id]) + "\" + \"\\\">View In Google</a>\";";
            		
            	}
            	code += "map.addOverlay(createMarker(new GLatLng(" + latlng.Lat.ToString() + ", " + latlng.Lng.ToString() + "), html));";
            	      
                  
        	}
        }
		return code;
	}

    public LatLng ComputeMeanLatLng()
    {
        double distance = 0.1;
        if (places == null)
        {
            PopulatePlaces();
        }
        if (places == null || places.Count == 0)
        {
            return null;
        }

        List<LatLng> groupings = new List<LatLng>();
        List<long> numGrouping = new List<long>();
        foreach (LatLng latlng in places.Keys)
        {
            if (groupings.Count == 0)
            {
                groupings.Add(latlng);
                numGrouping.Add(1);
                continue;
            }
            bool newGrouping = true;
            foreach (LatLng ll in groupings)
            {
                if (Math.Abs(ll.Lat - latlng.Lat) + Math.Abs(ll.Lng - latlng.Lng) < distance)
                {
                    int index = groupings.IndexOf(ll);
                    numGrouping[index] += 1;
                    newGrouping = false;
                }
            }
            if (newGrouping)
            {
                groupings.Add(latlng);
                numGrouping.Add(1);
            }
        }
        if (groupings.Count == 0)
        {
            return null;
        }

        LatLng maxGrouping = new LatLng(0, 0);
        long max = 0;
        foreach (LatLng grouping in groupings)
        {
            if (numGrouping[groupings.IndexOf(grouping)] > max)
            {
                max = numGrouping[groupings.IndexOf(grouping)];
                maxGrouping = grouping;
            }
        }
        if (max == 0)
        {
            return null;
        }
        int num = 0;
        double totalLat = 0;
        double totalLng = 0;
        foreach (LatLng latlng in places.Keys)
        {
            if (Math.Abs(latlng.Lat - maxGrouping.Lat) + Math.Abs(latlng.Lng - maxGrouping.Lng) < distance)
            {
                num += places[latlng].Count;
                totalLat += places[latlng].Count * latlng.Lat;
                totalLng += places[latlng].Count * latlng.Lng;    
            }
            
        }
        if (num == 0)
        {
            return null;
        }
        

        double meanLat = totalLat / num;
        double meanLng = totalLng / num;
        return new LatLng(meanLat, meanLng);
		//return new LatLng(0, 0);
    }

    private void PopulatePlaces()
    {
        if (Places == null)
        {
            places = new Dictionary<LatLng, List<object>>();
            return;
        }
        foreach (object place in Places)
        {
            AddPlace(place);
        }

    }

    private void AddPlace(object place)
    {
        LatLng latlng = null;
        if (place is QuickPM.Tenant)
        {
            latlng = GetTenantLatLng((QuickPM.Tenant)place);
            
        }
        else if (place is QuickPM.Property)
        {
            latlng = GetPropertyLatLng((QuickPM.Property)place);
        }
        else
        {
            return;
        }

        if (latlng != null)
        {
            if (places.ContainsKey(latlng))
            {
                places[latlng].Add(place);
            }
            else
            {
                places[latlng] = new List<object>(new object[] { place });
            }
        }
    }


    protected LatLng GetTenantLatLng(QuickPM.Tenant tenant)
    {
        double lat, lng;
        if (tenant == null)
        {
            return null;
        }        
        string address = tenant.Address + "," + tenant.City + "," + tenant.State + "," + "USA";
        string requestUrl = "http://maps.google.com/maps/geo?q=" + Server.UrlEncode(address) +
                        "&output=csv&oe=utf8&sensor=false&key=" + apiKey;
        WebRequest webRequest = WebRequest.Create(requestUrl);
        WebResponse response = webRequest.GetResponse();
        System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
        string line = reader.ReadLine();
        if (line.Trim().Length == 0)
        {
            return null;
        }
        string[] tmp = line.Split(new char[] { ',' });
        if (tmp.Length != 4)
        {
            return null;
        }
        //int statusCode = int.Parse(tmp[0]);
        //int accuracy = int.Parse(tmp[1]);
        lat = double.Parse(tmp[2]);
        lng = double.Parse(tmp[3]);
        if (lat == 0 || lng == 0)
        {

            return null;
        }        
        return new LatLng(lat, lng);		
    }


    protected LatLng GetPropertyLatLng(QuickPM.Property p)
    {
        List<string> tenantIds = p.GetTenantIds();
        foreach (string tenantId in tenantIds)
        {
            QuickPM.Tenant tenant = new QuickPM.Tenant(tenantId);            
            string address = tenant.Address + "," + tenant.City + "," + tenant.State + "," + "USA";
            string requestUrl = "http://maps.google.com/maps/geo?q=" + Server.UrlEncode(address) +
                            "&output=csv&oe=utf8&sensor=false&key=" + apiKey;
            WebRequest webRequest = WebRequest.Create(requestUrl);
            WebResponse response = webRequest.GetResponse();
            System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
            string line = reader.ReadLine();
            if (line.Trim().Length == 0)
            {
                continue;
            }
            string[] tmp = line.Split(new char[] { ',' });
            if (tmp.Length != 4)
            {
                continue;
            }
            //int statusCode = int.Parse(tmp[0]);
            //int accuracy = int.Parse(tmp[1]);
            double lat = double.Parse(tmp[2]);
            double lng = double.Parse(tmp[3]);
            if (lat == 0 || lng == 0)
            {

                continue;
            }            
            propertyAddresses[p.Id] = address;
            return new LatLng(lat, lng);
        }
        return null;		
    }

    protected string GetPropertyAddress(QuickPM.Property p)
    {
        List<string> tenantIds = p.GetTenantIds();
        foreach (string tenantId in tenantIds)
        {
            QuickPM.Tenant t = new QuickPM.Tenant(tenantId);
            if (t.Address != "")
            {
                return t.GetFullAddress();
            }
        }
        return "";
    }
}
