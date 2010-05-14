<%@ Control Language="C#" AutoEventWireup="true" Inherits="BottomBar" Codebehind="BottomBar.ascx.cs" %>
<style type="text/css">
    div#bottombar 
    {
        border-top-style: solid;
        border-top-color: Blue;
        border-top-width: thin;
        
        
        border-bottom-style: solid;
        border-bottom-color: Blue;
        border-bottom-width: thin;
        margin: 0 auto;
        /*text-align: center;*/
        padding: 4px;
    }
</style>
<br />
<center>
<div id="bottombar">
<a class="button-link" href="<%= ResolveUrl("~/About.aspx") %>">About</a>
&nbsp;&nbsp;&nbsp; 
</div>
</center>