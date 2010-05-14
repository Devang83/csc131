<%@ Control Language="C#" AutoEventWireup="true" Inherits="Maps_MapControl" Codebehind="MapControl.ascx.cs" %>
  <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;sensor=false&amp;key=<%= apiKey %>" type="text/javascript"></script>
    <script type="text/javascript">
    function onbodyload(){
        initialize();
    }
    function onbodyunload(){
        GUnload();
    }
    
    function createMarker(point,html) {
        var myPano = new GStreetviewPanorama(document.getElementById("pano"));
        GEvent.addListener(myPano, "error", handleNoFlash);  

        var marker = new GMarker(point);        
        GEvent.addListener(marker, 'click', function() {
          myPano.setLocationAndPOV(point);
          marker.openInfoWindowHtml(html);          
        });
        return marker;
      }
    function handleNoFlash(errorCode) {
      if (errorCode == FLASH_UNAVAILABLE) {
        alert("Error: Flash doesn't appear to be supported by your browser");
        return;
      }
    }  

    function initialize() {
   
      if (GBrowserIsCompatible()) {
        

        var map = new GMap2(document.getElementById("map_canvas"));        
         map.addControl(new GLargeMapControl());
      	map.addControl(new GMapTypeControl());
        map.setCenter(new GLatLng(<%= Lat %>, <%= Lng %>), <%= ZoomLevel %>);                
        var html = '';
        <%= GenerateJSHtmlCode() %>
      }
    }

    </script> 
                
<div id="map_canvas" style="width: 800px; height: 600px"></div>
<br />
<br />
<div id="pano" style="width: 500px; height: 200px"></div>
					