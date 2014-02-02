"use strict";

//Area of global Array Declaration - used to save the EMBER Variables from HTML-Template to Javascript global arrays
var version = "version 1.80";
var iCount;
var DataSource = [];
var DataURL = [];
var DataFilename = [];
var DataBild = [];
var DataName = [];
var DataGenre = [];
var DataDate = [];
var DataFanart = [];
var DataYear = [];
var DataRating = [];
var DataActors = [];

var DataVidBitrate = [];
var DataAudBitrate = [];
var DataWatched = [];
var DataThreed = [];
var DataPlot = [];
var DataNow;
var DataSet = [];
var DataMoviesets;
var DataTVShows;

//Here are string fragments which will be fused together with function-variables to build the whole site navigation dynamically
var navilinks_full, navilinks, moviewall, moviewall_full;
var string_navigation_1 = '<li><a href="javascript:{}" onclick="func_ShowDetails(';
var string_navigation_1HD1 = '<li><a href="javascript:{}" style="color:#A6D3EA;" onclick="func_ShowDetails(';
var string_navigation_1DVD1 = '<li><a href="javascript:{}" style="color:#b8860b;" onclick="func_ShowDetails(';
var string_navigation_1Movieset = '<li><a href="javascript:{}" onclick="func_DisplayMovieset(';
var string_navigation_1TvShow = '<li><a href="javascript:{}" onclick="func_DisplayTvSeason(';
var string_navigation_2 = ')"';
var string_tabindexstart = ' tabindex="';
var string_tabindexend = '">';
var string_navigation_3 = '</a> ';
var string_navigation_3HD2 = ' <img style="border: 0px solid;  "alt="" src="images/hd.jpg"> ';
var string_navigation_3DVD2 = ' <img style="border: 0px solid;  position:  "alt="" src="images/dvd.jpg"> ';
var string_watched3 = ' <img style="border: 0px solid ; alt="" src="images/haken.png">  ';
var string_navigation_4 = '</li>';
var string_moviewalllink_1 = ' <a class="thumbs" href="javascript:func_ShowDetails(';
var string_moviewalllink_2 = ')"  onerror="func_DefaultFanart(';
var string_moviewalllink_3 = ')">';
var string_moviewallpic_1 = '<img class="img150" src="';
var string_moviewallpic_2 = '" width="133" height="200" alt="';
var string_moviewallpic_3 = '" title="';
var string_moviewallpic_4 = '"</a>';

var string_tvshowwalllink_1 = ' <a class="thumbs" href="javascript:func_ShowDetailsTvSeason(';
var string_tvshowwalllink_2 = ')"  onerror="func_DefaultFanart(';
var string_tvshowwalllink_3 = ')">';
var string_tvshowwallpic_1 = '<img class="img150" src="';
var string_tvshowwallpic_2 = '" width="133" height="200" alt="';
var string_tvshowwallpic_3 = '" title="';
var string_tvshowwallpic_4 = '"</a>';

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
//---- Function for setting default wallpaper if movie has no fanart-----
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
function func_DefaultFanart(selectedmovie) {
var X;
X = document.getElementById("backgrounddiv" + selectedmovie);
X.innerHTML = '<img src="images/default.jpg" width="100%" height="100%" alt=""/>';
}

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
//---- Function: Choose fanart of selected movie and activate div -----
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
function func_ShowDetails(selectedmovie) {
var i, Control1, Control2;
for (i = 1;i <= iCount; i++) // deactivate div before open up new one!
{
Control1 = document.getElementById("movie" + i);
Control1.style.display = 'none';
}
Control2 = document.getElementById("backgrounddiv" + selectedmovie);
//display default background if there's no fanart for selected movie
Control2.innerHTML = '<img src="' + DataFanart[selectedmovie] + '" width="100%" height="100%" onerror="func_DefaultFanart(' + selectedmovie + ')"; alt="" />';
Control1 = document.getElementById("movie" + selectedmovie);
Control1.style.display = 'block';
}

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
//---- Function for displaying last modified date on template header ----- Erweiterung von User cimex Vielen Dank!!
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
function func_DisplayLastModified()
{
var T;
T = document.getElementById("div_displaylastmodified");
T.innerHTML = '<p><span style="font-family:Verdana;font-size:1.2em;">Update: </span> <span style="font-family:Verdana; color: white;font-size:1.5em;">' + DataNow + '</span></p>';
}

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
//---- Function for building left and right navigation of site-----
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
function func_BuildNavigation(selectedmovie)
{
var y, Control1, Control2, Control3, i, arr2str, result1, result2,result3,result4;
Control1 = document.getElementById("navigation");
Control2 = document.getElementById("div_moviewall");
Control3 = document.getElementById("div_moviecounter");
var myregexp1 = new RegExp('1080');
var myregexp2 = new RegExp('720');
var myregexp3 = new RegExp('576');
var myregexp4 = new RegExp('default');
var myregexp5 = new RegExp('1');
var bluraycounter = 0;
var dvdcounter = 0;
var othercounter = 0;
var moviecounter = 0;
var watchedcounter = 0;
var threedcounter = 0;

//execute only for buildung up navigation for ALL movies (PageLoad, Reset-Button)!
if (selectedmovie === 0) {
navilinks_full = "";  // deletes all entries of left sidebar
moviewall_full = ""; // deletes all entries of right sidebar
y = iCount; // this will make sure to add navigation of ALL movies!
func_ShowDetails(1);
}
else // only navigation elements of one specific movie will be added!
{
y = selectedmovie;
}
//Navigation builder for left and right sidebar
for (i = selectedmovie;i <= y; i++)
{
if (i === 0) {
i = 1; }
arr2str = DataSource[i].toString();
result1 = arr2str.search(myregexp1);
result2 = arr2str.search(myregexp2);
result3 = arr2str.search(myregexp3);
result4 = arr2str.search(myregexp4);

  if (DataThreed [i].toString() !== "") // Returns true if not empty("") or zero (0) -> MULTIVIEW<>"" or MULTIVIEW<>"0" --> Found: 3D Movie!
   {
   	threedcounter = threedcounter + 1
   }
   
if (result1 !== -1 || result2 !== -1) // Found something! Bluray
{
	if (DataWatched[i].toString() !== "") // Returns true if not empty("") or zero (0) -> PLAYCOUNT<>"" or PLAYCOUNT<>"0" --> Found: Watched Movie!
	{
	 //Navigation Build: Include Watched Movie/HD Icon!
   navilinks = string_navigation_1HD1 + i + string_navigation_2 + string_tabindexstart + i + string_tabindexend  + DataName[i] + string_navigation_3 + string_navigation_3HD2 + string_watched3 + string_navigation_4;
   watchedcounter = watchedcounter + 1;
   }

  else 
  	{
  	//Navigation Build: Include HD Icon!
  	navilinks = string_navigation_1HD1 + i + string_navigation_2 + string_tabindexstart + i + string_tabindexend  + DataName[i] + string_navigation_3 + string_navigation_3HD2 + string_navigation_4;
  }
bluraycounter = bluraycounter + 1;
}
else if(result3 !== -1 || result4 !== -1) // Found something! DVD
	{
		
	if (DataWatched[i].toString() !== "") // Returns true if not empty("") or zero (0) -> PLAYCOUNT<>"" or PLAYCOUNT<>"0" --> Found: Watched Movie!
	{
		 //Navigation Build: Include Watched Movie/DVD Icon!
   navilinks = string_navigation_1DVD1 + i + string_navigation_2 + string_tabindexstart + i + string_tabindexend  + DataName[i] + string_navigation_3 + string_navigation_3DVD2 + string_watched3 + string_navigation_4;
   watchedcounter = watchedcounter + 1;
   }
  else
  	{
  //Navigation Build: Include DVD Icon!
  navilinks = string_navigation_1DVD1 + i + string_navigation_2 + string_tabindexstart + i + string_tabindexend  + DataName[i] + string_navigation_3 + string_navigation_3DVD2 + string_navigation_4;
  }
dvdcounter = dvdcounter + 1;
}

else
{
	
	if (DataWatched[i].toString() !== "") // Returns true if not empty("") or zero (0) -> PLAYCOUNT<>"" or PLAYCOUNT<>"0" --> Found: Watched Movie!
	{
 navilinks = string_navigation_1 + i + string_navigation_2 + string_tabindexstart + i + string_tabindexend  + DataName[i] + string_navigation_3 + string_navigation_3 + string_watched3 + string_navigation_4;
   watchedcounter = watchedcounter + 1;
   }
  else
  	{
  		//Navigation Build: Movie is not DVD, not Bluray and also not watched --> no icons, basic string
   navilinks = string_navigation_1 + i + string_navigation_2 + string_tabindexstart + i + string_tabindexend  + DataName[i] + string_navigation_3 + string_navigation_3 + string_navigation_4;
  }

othercounter = othercounter + 1;
}
//Navigation builder to build whole page dynamically
navilinks_full = navilinks_full + navilinks;
moviewall = string_moviewalllink_1 + i + string_moviewalllink_2 + i + string_moviewalllink_3 + string_moviewallpic_1 + DataBild[i] + string_moviewallpic_2 + DataName[i] + string_moviewallpic_3 + DataName[i] + string_moviewallpic_4;
moviewall_full = moviewall_full + moviewall;
}
if (selectedmovie === 0) { // only needed when function not called from another function
selectedmovie = 1;
moviecounter = dvdcounter + bluraycounter + othercounter;
Control1.innerHTML = '<ol class="symbol" id="ol_navigation" style="padding-left: 9px">' + navilinks_full + '</ol><p style="color:yellow;font-size:0.6em;">' + version + '</p>';
Control2.innerHTML = moviewall_full;
Control3.innerHTML = '<p><span style="font-family:Verdana;">Total: </span> <span style="font-family:Verdana; color: white;">' + moviecounter + '</span> <span style="font-family:Verdana; color: #A6D3EA;">&nbsp;&nbsp;Blurays: ' + bluraycounter + '</span><span style="font-family:Verdana; color: #b8860b;">&nbsp;&nbsp;DVD: ' + dvdcounter + '</span><span style="font-family:Verdana; color: red;">&nbsp;&nbsp;3D: ' + threedcounter + '</span><span style="font-family:Verdana; color: Orange;">&nbsp;&nbsp;Rest: ' + othercounter + '</span><span style="font-family:Verdana; color: white;">&nbsp;&nbsp;Watched: ' + watchedcounter + '</span></p>';
}
}


//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
//---- Function: Custom Search [Date]-----
// Displays latest added movies  - result also considers watched/unwatched state of checkboxes!
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
function func_SearchDate() {
var todaydate  = new Date(); //Date Object
var tt   = todaydate.getDate(); //today: day
var mm   = todaydate.getMonth() + 1; //today: month
var jjjj = todaydate.getFullYear(); //today: year
var timestamp_now = Date.UTC(jjjj, mm, tt); // today: timestamp
var timestamp_movie = new Array(iCount); 
timestamp_movie[0] = new Array(2);
var doOnce = 0;
var Control1 = document.getElementById("navigation");
var Control2 = document.getElementById("div_moviewall");
var i;
navilinks_full = "";  // deletes all entries of left sidebar
moviewall_full = ""; // deletes all entries of right sidebar

// Loop through all entries and search for dates lower than "selectedday"
for (i = 1;i <= iCount; i++){
var moviedate = DataDate[i].split("."); // i.e. moviedate[0]=24; moviedate[1]=12; moviedate[2]=2003;
timestamp_movie[i-1] = [i,Date.UTC(moviedate[2], moviedate[1], moviedate[0])]; // Timestamp in ms
}

// Array sorted with Quicksort function/ ascending --> Newest Movies first!
timestamp_movie.quicksortCol(0, (timestamp_movie.length-1), 1);

//Loop through all movies and build navigation with new sorted moviearray!
for (i = iCount;i > 0; i--){

	var z = timestamp_movie[i-1][0];
// Also consider watched/unwatched state of checkboxes and display accordingly !	
		// if watched checkbox is checked -> show watched movies
	if(document.getElementById('unwatched').checked == false && document.getElementById('watched').checked == true)
	{
		  if (DataWatched[z].toString() !== "") // Returns true if not empty("") or zero (0) -> PLAYCOUNT<>"" or PLAYCOUNT<>"0"
		  {
			func_BuildNavigation(z); // Calls LoadPage Function and add match to left and right navigation string variable!
		  } 
	}
		// if unwatched checkbox is checked -> show unwatched movies
	else if(document.getElementById('unwatched').checked == true && document.getElementById('watched').checked == false)
	{
	    if (DataWatched[z].toString() == "") // Returns true if empty("") and zero (0) -> PLAYCOUNT="" or PLAYCOUNT="0"
		  {
			func_BuildNavigation(z); // Calls LoadPage Function and add match to left and right navigation string variable!
		  }
	}
	// in other case if both checkboxes unchecked/checked show all movies unfiltered...
	else
	{
			func_BuildNavigation(z); // Calls LoadPage Function and add match to left and right navigation string variable!
	}
}

Control1.innerHTML = '<ol id="ol_navigation">' + navilinks_full + '</ol>';
Control2.innerHTML = moviewall_full;
}

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
//---- Function: Custom Search [Year]-----
// Displays all movies sorted, beginning with newest  - result also considers watched/unwatched state of checkboxes!
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
function func_SearchYear() {
var doOnce = 0;
var Control1 = document.getElementById("navigation");
var Control2 = document.getElementById("div_moviewall");
var i;
var year_movie = new Array(iCount); 
year_movie[0] = new Array(2);
navilinks_full = "";  // deletes all entries of left sidebar
moviewall_full = ""; // deletes all entries of right sidebar


// Populate array with timestamps of all movies
for (i = 1;i <= iCount; i++){
year_movie[i-1] = [i,DataYear[i]]; // Timestamp in ms
}

// Array sorted with Quicksort function/ ascending
year_movie.quicksortCol(0, (year_movie.length-1), 1);

//Loop through all movies and build navigation with new sorted moviearray!
for (i = iCount;i > 0; i--){

	var z = year_movie[i-1][0];
	
  // Also consider watched/unwatched state of checkboxes and display accordingly !	
		// if watched checkbox is checked -> show watched movies
	if(document.getElementById('unwatched').checked == false && document.getElementById('watched').checked == true)
	{
		  if (DataWatched[z].toString() !== "") // Returns true if not empty("") or zero (0) -> PLAYCOUNT<>"" or PLAYCOUNT<>"0"
		  {
			func_BuildNavigation(z); // Calls LoadPage Function and add match to left and right navigation string variable!
		  } 
	}
		// if unwatched checkbox is checked -> show unwatched movies
	else if(document.getElementById('unwatched').checked == true && document.getElementById('watched').checked == false)
	{
	    if (DataWatched[z].toString() == "") // Returns true if empty("") and zero (0) -> PLAYCOUNT="" or PLAYCOUNT="0"
		  {
			func_BuildNavigation(z); // Calls LoadPage Function and add match to left and right navigation string variable!
		  }
	}
	// in other case if both checkboxes unchecked/checked show all movies unfiltered...
	else
	{
			func_BuildNavigation(z); // Calls LoadPage Function and add match to left and right navigation string variable!
	}
}

Control1.innerHTML = '<ol id="ol_navigation">' + navilinks_full + '</ol>';
Control2.innerHTML = moviewall_full;
}

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
//---- Function: Custom Search [Genre]-----
// Displays all movies containing the selected genre  - result also considers watched/unwatched state of checkboxes!
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
function func_SearchGenre(selectedgenre) {

var myregexp = new RegExp(selectedgenre); //  NEW RegeExp Object
var doOnce = 0;
var Control1 = document.getElementById("navigation");
var Control2 = document.getElementById("div_moviewall");
var i;
navilinks_full = "";  // deletes all entries of left sidebar
moviewall_full = ""; // deletes all entries of right sidebar

// Loop through all entries and check if criteria matches
for (i = 1;i <= iCount; i++)
{
var arr2str = DataGenre[i].toString();
var result = arr2str.search(myregexp);

// in case of ALL display all movies by setting result = 1
if(selectedgenre == 'ALLE')
{
	result = 1;
}

if (result !== -1) // Match!
{
// Also consider watched/unwatched state of checkboxes and display accordingly !	
		// if watched checkbox is checked -> show watched movies
	if(document.getElementById('unwatched').checked == false && document.getElementById('watched').checked == true)
	{
		  if (DataWatched[i].toString() !== "") // Returns true if not empty("") or zero (0) -> PLAYCOUNT<>"" or PLAYCOUNT<>"0"
		  {
			func_BuildNavigation(i); // Calls LoadPage Function and add match to left and right navigation string variable!
		  } 
	}
		// if unwatched checkbox is checked -> show unwatched movies
	else if(document.getElementById('unwatched').checked == true && document.getElementById('watched').checked == false)
	{
	    if (DataWatched[i].toString() == "") // Returns true if empty("") and zero (0) -> PLAYCOUNT="" or PLAYCOUNT="0"
		  {
			func_BuildNavigation(i); // Calls LoadPage Function and add match to left and right navigation string variable!
		  }
	}
	// in other case if both checkboxes unchecked/checked show all movies unfiltered...
	else
	{
			func_BuildNavigation(i); // Calls LoadPage Function and add match to left and right navigation string variable!
	}	
}
}

Control1.innerHTML = '<ol id="ol_navigation">' + navilinks_full + '</ol>';
Control2.innerHTML = moviewall_full;

}
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
//---- Function: Custom Search [Titel]-----
// Displays all movies beginning with selected letter  - result also considers watched/unwatched state of checkboxes!
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
function func_SearchTitel(selectedLetter) {

var myregexp = new RegExp(selectedLetter); //  NEW RegeExp Object
var doOnce = 0;
var Control1 = document.getElementById("navigation");
var Control2 = document.getElementById("div_moviewall");
var i;
navilinks_full = "";  // deletes all entries of left sidebar
moviewall_full = ""; // deletes all entries of right sidebar

// Loop through all entries and check if movie genre contains "selectedgenre"
for (i = 1;i <= iCount; i++)
{
var arr2str = DataName[i].toString();
var result = arr2str.search(myregexp);

if (result !== -1) // Found movie with selected letter
{

// Also consider watched/unwatched state of checkboxes and display accordingly !	
		// if watched checkbox is checked -> show watched movies
	if(document.getElementById('unwatched').checked == false && document.getElementById('watched').checked == true)
	{
		  if (DataWatched[i].toString() !== "") // Returns true if not empty("") or zero (0) -> PLAYCOUNT<>"" or PLAYCOUNT<>"0"
		  {
			func_BuildNavigation(i); // Calls LoadPage Function and add match to left and right navigation string variable!
		  } 
	}
		// if unwatched checkbox is checked -> show unwatched movies
	else if(document.getElementById('unwatched').checked == true && document.getElementById('watched').checked == false)
	{
	    if (DataWatched[i].toString() == "") // Returns true if empty("") and zero (0) -> PLAYCOUNT="" or PLAYCOUNT="0"
		  {
			func_BuildNavigation(i); // Calls LoadPage Function and add match to left and right navigation string variable!
		  }
	}
	// in other case if both checkboxes unchecked/checked show all movies unfiltered...
	else
	{
			func_BuildNavigation(i); // Calls LoadPage Function and add match to left and right navigation string variable!
	}	
}
}

Control1.innerHTML = '<ol id="ol_navigation">' + navilinks_full + '</ol>';
Control2.innerHTML = moviewall_full;

}
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
//---- Function: Custom Search [Wertung]----- Erweiterung von User furryhamster Control2ielen Dank!!
// Sorting of movies, beginning with highest rated movies - result also considers watched/unwatched state of checkboxes!
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
function func_SearchWertung() {
var doOnce = 0;
var Control1 = document.getElementById("navigation");
var Control2 = document.getElementById("div_moviewall");
var i;
var rating_movie = new Array(iCount); 
rating_movie[0] = new Array(2);
navilinks_full = "";  // deletes all entries of left sidebar
moviewall_full = ""; // deletes all entries of right sidebar

for (i = 1;i <= iCount; i++){
rating_movie[i-1] = [i,DataRating[i]]; // Timestamp in ms
}

// Array sorted with Quicksort function/ ascending
rating_movie.quicksortCol(0, (rating_movie.length-1), 1);

//Loop through all movies and build navigation with new sorted moviearray!
for (i = iCount;i > 0; i--){
//get one specific movie from sorted array and hand over to Buildnavigation-Function!
	var z = rating_movie[i-1][0];
	
// Also consider watched/unwatched state of checkboxes and display accordingly !	
		// if watched checkbox is checked -> show watched movies
	if(document.getElementById('unwatched').checked == false && document.getElementById('watched').checked == true)
	{
		  if (DataWatched[z].toString() !== "") // Returns true if not empty("") or zero (0) -> PLAYCOUNT<>"" or PLAYCOUNT<>"0"
		  {
			func_BuildNavigation(z); // Calls LoadPage Function and add match to left and right navigation string variable!
		  } 
	}
		// if unwatched checkbox is checked -> show unwatched movies
	else if(document.getElementById('unwatched').checked == true && document.getElementById('watched').checked == false)
	{
	    if (DataWatched[z].toString() == "") // Returns true if empty("") and zero (0) -> PLAYCOUNT="" or PLAYCOUNT="0"
		  {
			func_BuildNavigation(z); // Calls LoadPage Function and add match to left and right navigation string variable!
		  }
	}
	// in other case if both checkboxes unchecked/checked show all movies unfiltered...
	else
	{
			func_BuildNavigation(z); // Calls LoadPage Function and add match to left and right navigation string variable!
	}
	
}

Control1.innerHTML = '<ol id="ol_navigation">' + navilinks_full + '</ol>';
Control2.innerHTML = moviewall_full;
}

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
//---- Function: [VideoBITRATE-Liste]
// Sorting of movies, beginning with highest Videobitrate movies - result also considers watched/unwatched state of checkboxes!
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
function func_SearchQualiVideo() {
var doOnce = 0;
var Control1 = document.getElementById("navigation");
var Control2 = document.getElementById("div_moviewall");
var i;
var vidbitrate_movie = new Array(iCount); 
vidbitrate_movie[0] = new Array(2);
navilinks_full = "";  // deletes all entries of left sidebar
moviewall_full = ""; // deletes all entries of right sidebar

for (i = 1;i <= iCount; i++){
vidbitrate_movie[i-1] = [i,DataVidBitrate[i]]; // Control2ideoBitrate
}

// Sort the array (numeric sorting) ascending
vidbitrate_movie.sort(function(x,y){return x[1]-y[1]});

//Loop through all movies and build navigation with new sorted moviearray!
for (i = iCount;i > 0; i--){
//get one specific movie from sorted array and hand over to Buildnavigation-Function!
	var z = vidbitrate_movie[i-1][0];
	
// Also consider watched/unwatched state of checkboxes and display accordingly !	
		// if watched checkbox is checked -> show watched movies
	if(document.getElementById('unwatched').checked == false && document.getElementById('watched').checked == true)
	{
		  if (DataWatched[z].toString() !== "") // Returns true if not empty("") or zero (0) -> PLAYCOUNT<>"" or PLAYCOUNT<>"0"
		  {
			func_BuildNavigation(z); // Calls LoadPage Function and add match to left and right navigation string variable!
		  } 
	}
		// if unwatched checkbox is checked -> show unwatched movies
	else if(document.getElementById('unwatched').checked == true && document.getElementById('watched').checked == false)
	{
	    if (DataWatched[z].toString() == "") // Returns true if empty("") and zero (0) -> PLAYCOUNT="" or PLAYCOUNT="0"
		  {
			func_BuildNavigation(z); // Calls LoadPage Function and add match to left and right navigation string variable!
		  }
	}
	// in other case if both checkboxes unchecked/checked show all movies unfiltered...
	else
	{
			func_BuildNavigation(z); // Calls LoadPage Function and add match to left and right navigation string variable!
	}
	
}

Control1.innerHTML = '<ol id="ol_navigation">' + navilinks_full + '</ol>';
Control2.innerHTML = moviewall_full;
}

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
//---- Function: [AudioBITRATE-Liste]
// Sorting of movies, beginning with highest Audiobitrate movies - result also considers watched/unwatched state of checkboxes!
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
function func_SearchQualiAudio() {
var doOnce = 0;
var Control1 = document.getElementById("navigation");
var Control2 = document.getElementById("div_moviewall");
var i;
var audiobitrate_movie = new Array(iCount); 
audiobitrate_movie[0] = new Array(2);
navilinks_full = "";  // deletes all entries of left sidebar
moviewall_full = ""; // deletes all entries of right sidebar

for (i = 1;i <= iCount; i++){
audiobitrate_movie[i-1] = [i,DataAudBitrate[i]]; // AudioBitrate
}

//Sort the array (numeric sorting) ascending
audiobitrate_movie.sort(function(x,y){return x[1]-y[1]});

//Loop through all movies and build navigation with new sorted moviearray!
for (i = iCount;i > 0; i--){
//get one specific movie from sorted array and hand over to Buildnavigation-Function!
	var z = audiobitrate_movie[i-1][0];
	
// Also consider watched/unwatched state of checkboxes and display accordingly !	
		// if watched checkbox is checked -> show watched movies
	if(document.getElementById('unwatched').checked == false && document.getElementById('watched').checked == true)
	{
		  if (DataWatched[z].toString() !== "") // Returns true if not empty("") or zero (0) -> PLAYCOUNT<>"" or PLAYCOUNT<>"0"
		  {
			func_BuildNavigation(z); // Calls LoadPage Function and add match to left and right navigation string variable!
		  } 
	}
		// if unwatched checkbox is checked -> show unwatched movies
	else if(document.getElementById('unwatched').checked == true && document.getElementById('watched').checked == false)
	{
	    if (DataWatched[z].toString() == "") // Returns true if empty("") and zero (0) -> PLAYCOUNT="" or PLAYCOUNT="0"
		  {
			func_BuildNavigation(z); // Calls LoadPage Function and add match to left and right navigation string variable!
		  }
	}
	// in other case if both checkboxes unchecked/checked show all movies unfiltered...
	else
	{
			func_BuildNavigation(z); // Calls LoadPage Function and add match to left and right navigation string variable!
	}
	
}

Control1.innerHTML = '<ol id="ol_navigation">' + navilinks_full + '</ol>';
Control2.innerHTML = moviewall_full;
}

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
//---- Function: Un/Watched Movies -----
// Depending on checked/unchecked Checkboxes show only filtered selection of movies
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
function func_WatchedMovies() {

var Control1 = document.getElementById("navigation");
var Control2 = document.getElementById("div_moviewall");
var i;
navilinks_full = "";  // deletes all entries of left sidebar
moviewall_full = ""; // deletes all entries of right sidebar
        
// Loop through all entries and check movie PLAYCOUNT(i) (DataWatched[i])

		// if watched checkbox is checked -> show watched movies
	if(document.getElementById('unwatched').checked == false && document.getElementById('watched').checked == true)
	{
		for (i = 1;i <= iCount; i++)
    {

		 if (DataWatched[i].toString() !== "") // Returns true if not empty("") or zero (0) -> PLAYCOUNT<>"" or PLAYCOUNT<>"0"
		  {
			func_BuildNavigation(i); // Calls LoadPage Function and add match to left and right navigation string variable!
		  }
	  }
	}
		// if unwatched checkbox is checked -> show unwatched movies
	else if(document.getElementById('unwatched').checked == true && document.getElementById('watched').checked == false)
	{
		for (i = 1;i <= iCount; i++)
    {

	 if (DataWatched[i].toString() == "") // Returns true if empty("") and zero (0) -> PLAYCOUNT="" or PLAYCOUNT="0"
		  {
			func_BuildNavigation(i); // Calls LoadPage Function and add match to left and right navigation string variable!
		  }
	  }
	}
	// in other case if both checkboxes unchecked/checked show all movies unfiltered...
	else
	{
		for (i = 1;i <= iCount; i++)
    {

			func_BuildNavigation(i); // Calls LoadPage Function and add match to left and right navigation string variable!
		
	  }
	}


Control1.innerHTML = '<ol id="ol_navigation">' + navilinks_full + '</ol>';
Control2.innerHTML = moviewall_full;

}

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
//---- Function: Custom Search [Schauspieler]----- Erweiterung von User furryhamster Vielen Dank!!
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
function func_SearchActor(actor) {
var myregexp = new RegExp(actor); //  NEW RegeExp Object
var doOnce = 0;
var Control1 = document.getElementById("navigation");
var Control2 = document.getElementById("div_moviewall");
var i;
navilinks_full = "";  // deletes all entries of left sidebar
moviewall_full = ""; // deletes all entries of right sidebar

// Loop through all entries and check if movie actors contains "actor"
for (i = 1;i <= iCount; i++)
{
var arr2str = DataActors[i].toString();
var result = arr2str.search(myregexp);

var arr2strLower = arr2str.toLowerCase();
var result2 = arr2strLower.search(myregexp);

if (result !== -1 || result2 !== -1) // Found something!
{
func_BuildNavigation(i); // Calls LoadPage Function and add match to left and right navigation string variable!
}
}

Control1.innerHTML = '<ol id="ol_navigation">' + navilinks_full + '</ol>';
Control2.innerHTML = moviewall_full;
}

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
//---- Function: Build Moviesetnavigation-----
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
function func_BuildMoviesets() {
var Control1 = document.getElementById("navigation");
var i;
var arrmoviesets=DataMoviesets.split("|"); 
navilinks_full = "";  // deletes all entries of left sidebar
moviewall_full = ""; // deletes all entries of right sidebar


// Loop through all entries and check if movie contains "movieset"
//Navigation builder to build whole page dynamically
for (var i=0; i < arrmoviesets.length; i++)
{
navilinks = string_navigation_1Movieset  + i + string_navigation_2 + string_tabindexstart + i + string_tabindexend  + arrmoviesets[i] + string_navigation_3 + string_navigation_4;
navilinks_full = navilinks_full + navilinks;
}

Control1.innerHTML = '<ol id="ol_navigation">' + navilinks_full + '</ol>';
}
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
//---- Function: Show Moviesets-----
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
function func_DisplayMovieset(moviesetindex) {
var arrmoviesets=DataMoviesets.split("|"); 
var movieset=arrmoviesets[moviesetindex];
var myregexp = new RegExp(movieset); //  NEW RegeExp Object
var doOnce = 0;
var Control2 = document.getElementById("div_moviewall");
var i;

moviewall_full = ""; // deletes all entries of right sidebar

// Loop through all entries and check if movie contains "movieset"
for (i = 1;i <= iCount; i++)
{
var arr2str = DataSet[i].toString();
var result = arr2str.search(myregexp);

var arr2strLower = arr2str.toLowerCase();
var result2 = arr2strLower.search(myregexp);

if (result !== -1 || result2 !== -1) // Found something!
{
func_BuildNavigationMoviesets(i); // Calls LoadPage Function and add match to left and right navigation string variable!
}
}

Control2.innerHTML = moviewall_full;
}
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
//---- Function for building left and right navigation of site-----
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
function func_BuildNavigationMoviesets(selectedmovie)
{
var y, Control1, Control2, Control3, i, arr2str, result1, result2,result3,result4;
Control1 = document.getElementById("navigation");
Control2 = document.getElementById("div_moviewall");
Control3 = document.getElementById("div_moviecounter");
var myregexp1 = new RegExp('1080');
var myregexp2 = new RegExp('720');
var myregexp3 = new RegExp('576');
var myregexp4 = new RegExp('default');
var myregexp5 = new RegExp('1');
var bluraycounter = 0;
var dvdcounter = 0;
var othercounter = 0;
var moviecounter = 0;
var watchedcounter = 0;
var threedcounter = 0;

//execute only for buildung up navigation for ALL movies (PageLoad, Reset-Button)!
if (selectedmovie === 0) {
navilinks_full = "";  // deletes all entries of left sidebar
moviewall_full = ""; // deletes all entries of right sidebar
y = iCount; // this will make sure to add navigation of ALL movies!
func_ShowDetails(1);
}
else // only navigation elements of one specific movie will be added!
{
y = selectedmovie;
}
//Navigation builder for left and right sidebar
for (i = selectedmovie;i <= y; i++)
{
if (i === 0) {
i = 1; }
arr2str = DataSource[i].toString();
result1 = arr2str.search(myregexp1);
result2 = arr2str.search(myregexp2);
result3 = arr2str.search(myregexp3);
result4 = arr2str.search(myregexp4);


  
//Navigation builder to build whole page dynamically
//navilinks_full = navilinks_full + navilinks;
moviewall = string_moviewalllink_1 + i + string_moviewalllink_2 + i + string_moviewalllink_3 + string_moviewallpic_1 + DataBild[i] + string_moviewallpic_2 + DataName[i] + string_moviewallpic_3 + DataName[i] + string_moviewallpic_4;
moviewall_full = moviewall_full + moviewall;
}
if (selectedmovie === 0) { // only needed when function not called from another function
selectedmovie = 1;
moviecounter = dvdcounter + bluraycounter + othercounter;
Control1.innerHTML = '<ol class="symbol" id="ol_navigation" style="padding-left: 9px">' + navilinks_full + '</ol><p style="color:yellow;font-size:0.6em;">' + version + '</p>';
Control2.innerHTML = moviewall_full;
Control3.innerHTML = '<p><span style="font-family:Verdana;">Total: </span> <span style="font-family:Verdana; color: white;">' + moviecounter + '</span> <span style="font-family:Verdana; color: #A6D3EA;">&nbsp;&nbsp;Blurays: ' + bluraycounter + '</span><span style="font-family:Verdana; color: #b8860b;">&nbsp;&nbsp;DVD: ' + dvdcounter + '</span><span style="font-family:Verdana; color: red;">&nbsp;&nbsp;3D: ' + threedcounter + '</span><span style="font-family:Verdana; color: Orange;">&nbsp;&nbsp;Rest: ' + othercounter + '</span><span style="font-family:Verdana; color: white;">&nbsp;&nbsp;Watched: ' + watchedcounter + '</span></p>';
}
}

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
//---- Function: Build TVSHOWnavigation-----
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
function func_BuildTvShows() {
var Control1 = document.getElementById("navigation");
var i;
var arrtvshowpackage=DataTVShows.split("|"); 
navilinks_full = "";  // deletes all entries of left sidebar
moviewall_full = ""; // deletes all entries of right sidebar


// Loop through all entries and check if movie contains "movieset"
//Navigation builder to build whole page dynamically
for (var i=0; i < arrtvshowpackage.length; i++)
{
	
var arrtvshow=arrtvshowpackage[i].split("*"); 
var seasonposters = "";
	for (var z=2; z < arrtvshow.length; z++)
	{
		seasonposters = seasonposters + "|" + arrtvshow[z]
	}
seasonposters =	"'" + seasonposters  + "'"
escape(seasonposters)
navilinks = string_navigation_1TvShow +  seasonposters  + string_navigation_2 + string_tabindexstart + i + string_tabindexend  + arrtvshow[1] + string_navigation_3 + string_navigation_4;
navilinks_full = navilinks_full + navilinks;
}

Control1.innerHTML = '<ol id="ol_navigation">' + navilinks_full + '</ol>';
}
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
//---- Function: Show TvSeasonPosters-----
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
function func_DisplayTvSeason(seasonposters) {
var Control2 = document.getElementById("div_moviewall");
var i;

var arrseasonposter=seasonposters.split("|"); 

moviewall_full = ""; // deletes all entries of right sidebar
	for (var i=1; i < arrseasonposter.length; i++)
	{
  func_BuildNavigationTVSeason(arrseasonposter[i]); // Calls LoadPage Function and add match to left and right navigation string variable!
  }
Control2.innerHTML = moviewall_full;
}
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
//---- Function: Show TVShowInfo-----
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
function func_ShowDetailsTvSeason(tvshowID) {
//---- TODO - NOT  USED at moment, maybe later show TV Season Details...-----
}
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
//---- Function for building left and right navigation of site-----
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
function func_BuildNavigationTVSeason(seasonposter)
{
//Navigation builder to build whole page dynamically
//navilinks_full = navilinks_full + navilinks;  
moviewall = string_tvshowwalllink_1 + "0" + string_tvshowwalllink_2 + "0" + string_tvshowwalllink_3 + string_tvshowwallpic_1 + "export/" + seasonposter + ".jpg" + string_tvshowwallpic_2 + "-" + string_tvshowwallpic_3 + "-" + string_tvshowwallpic_4;
moviewall_full = moviewall_full + moviewall;

}


//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
//---- Function: Custom Search [3D MOVIE]-----
// Displays all movies which have MULTIVIEW Scanned <> zero  - result also considers watched/unwatched state of checkboxes!
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
function func_SearchThreeD() {

var doOnce = 0;
var Control1 = document.getElementById("navigation");
var Control2 = document.getElementById("div_moviewall");
var i;
navilinks_full = "";  // deletes all entries of left sidebar
moviewall_full = ""; // deletes all entries of right sidebar

// Loop through all entries and check if movie genre contains "selectedgenre"
for (i = 1;i <= iCount; i++)
{

//Now look if 3Dmovie
if (DataThreed[i].toString() !== "") // Returns true if not empty("") or zero (0) -> MULTIVIEW<>"" or MULTIVIEW<>"0" --> Found: 3D Movie!
{

// Also consider watched/unwatched state of checkboxes and display accordingly !	
		// if watched checkbox is checked -> show watched movies
	if(document.getElementById('unwatched').checked == false && document.getElementById('watched').checked == true)
	{
		  if (DataWatched[i].toString() !== "") // Returns true if not empty("") or zero (0) -> PLAYCOUNT<>"" or PLAYCOUNT<>"0"
		  {
			func_BuildNavigation(i); // Calls LoadPage Function and add match to left and right navigation string variable!
		  } 
	}
		// if unwatched checkbox is checked -> show unwatched movies
	else if(document.getElementById('unwatched').checked == true && document.getElementById('watched').checked == false)
	{
	    if (DataWatched[i].toString() == "") // Returns true if empty("") and zero (0) -> PLAYCOUNT="" or PLAYCOUNT="0"
		  {
			func_BuildNavigation(i); // Calls LoadPage Function and add match to left and right navigation string variable!
		  }
	}
	// in other case if both checkboxes unchecked/checked show all movies unfiltered...
	else
	{
			func_BuildNavigation(i); // Calls LoadPage Function and add match to left and right navigation string variable!
	}	
}
}

Control1.innerHTML = '<ol id="ol_navigation">' + navilinks_full + '</ol>';
Control2.innerHTML = moviewall_full;

}

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
//---- Function: Open Movie Folder -----
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
function func_OpenFolder(Counter) {

window.open ('file:///' + DataURL[Counter] + '/' + DataFilename[Counter], '_blank');

}


/* ****************************************************************************** * // CREDIT: Quaese, Quelle: http://www.tutorials.de/javascript-ajax/283260-javascript-zweidimensionales-array-sortieren.html
 * Arrayobjekt um Methode erweitern - Array mit Quicksort nach Spalten sortieren
 *
 * Parameter: intLower - Untergrenze des Teilbereichs (beim Start i.A. 0)
 *            intUpper - Obergrenze des Teilbereichs (beim Start i.A. Array.length-1)
 *            intCol   - Spalte, nach der sortiert werden soll (beginnend bei 0)
 * ****************************************************************************** */
Array.prototype.quicksortCol = function(intLower, intUpper, intCol){
  var i = intLower, j = intUpper;
  var varHelp = new Array();
  // Teilen des Bereiches und Vergleichswert ermitteln
  var varX = this[parseInt(Math.floor(intLower+intUpper)/2)][intCol];
 
  // Teilbereiche bearbeiten bis:
  // - "linker" Bereich enthält alle "kleineren" Werte
  // - "rechter" Bereich enthält alle "grösseren" Werte
  do{
    // Solange Wert im linken Teil kleiner ist -> Grenzeindex inkrementieren
    while(this[i][intCol] < varX) i++;
    // Solange Wert im rechten Teil grösser ist -> Grenzindex dekrementieren
    while(varX < this[j][intCol]) j--;
 
    // Untergrenze kleiner als Obergrenze -> Tausch notwendig
    if(i<=j){
      var varHelp = this[i];
      this[i] = this[j];
      this[j] = varHelp;
      i++;
      j--;
    }
  }while(i<j);
 
  // Quicksort rekursiv aufrufen
  if(intLower < j) this.quicksortCol(intLower, j, intCol);
  if(i < intUpper)  this.quicksortCol(i, intUpper, intCol);
}