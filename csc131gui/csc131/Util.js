﻿function Progress() {
    var e = document.getElementById("LoadingPage");
    e.innerHTML = "<h2>Processing, Please Wait...</h2>";
    e = document.getElementById("ErrorMessage");
    e.innerHTML = "";
    return true;
}

function Progress(elementId) {
    var e = document.getElementById(elementId);
    e.innerHTML = "<h2>Processing, Please Wait...</h2>";
    e = document.getElementById("ErrorMessage");
    e.innerHTML = "";
    return true;
}
    
    
    function IsNumber(elementId, message)    
    {
        var ele = document.getElementById(elementId);
        var txt = ele.value;
        var re = new RegExp("^[0-9]+$");
        if (txt.match(re)) 
        {
            return true;
        } 
        else 
        {
            alert(message);
            return false;
        }        
    }
    



  // this function is need to work around 
  // a bug in IE related to element attributes
  function hasClass(obj) {
     var result = false;
     if (obj.getAttributeNode("class") != null) {
         result = obj.getAttributeNode("class").value;
     }
     return result;
  }   

 function stripe(id) {    
    // the flag we'll use to keep track of 
    // whether the current row is odd or even
    var even = false;
  
    // if arguments are provided to specify the colours
    // of the even & odd rows, then use the them;
    // otherwise use the following defaults:
    var evenColor = arguments[1] ? arguments[1] : "#fff";
    var oddColor = arguments[2] ? arguments[2] : "#ddd";
  
    // obtain a reference to the desired table
    // if no such table exists, abort
    var table = document.getElementById(id);
    if (! table) { return; }
    
    // by definition, tables can have more than one tbody
    // element, so we'll have to get the list of child
    // &lt;tbody&gt;s 
    var tbodies = table.getElementsByTagName("tbody");

    // and iterate through them...
    for (var h = 0; h < tbodies.length; h++) {
    
     // find all the &lt;tr&gt; elements... 
      var trs = tbodies[h].getElementsByTagName("tr");
      
      // ... and iterate through them
      for (var i = 0; i < trs.length; i++) {
        
	    // avoid rows that have a class attribute
        // or backgroundColor style
	    if (!hasClass(trs[i]) && ! trs[i].style.backgroundColor) {
	        even = !even;
         // get all the cells in this row...
          var tds = trs[i].getElementsByTagName("td");
        
          // and iterate through them...
          for (var j = 0; j < tds.length; j++) {
        
            var mytd = tds[j];

            // avoid cells that have a class attribute
            // or backgroundColor style
	        //if (! hasClass(mytd) && ! mytd.style.backgroundColor) {
        
		      mytd.style.backgroundColor = even ? evenColor : oddColor;              
              //mytd.style.backgroundColor = oddColor;
            //}
            // flip from odd to even, or vice-versa
            
          }
          
        }
        
      }
    }
  }

function addLoadEvent(func){    
    var oldonload = window.onload;
        if(typeof window.onload != 'function'){
            window.onload = func;
        }else{
            window.onload = function() {
                if(oldonload){
                    oldonload();
                }
                func();
            }
        }
}