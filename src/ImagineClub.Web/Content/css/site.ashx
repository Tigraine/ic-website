﻿* {
    margin: 0;
}

html, body 
{
    background: url(../images/background-tile.jpg);
    padding: 0;
    height: 100%;
    font-family: Tahoma;
    font-size: 12px;
}

img
{
    border: 0px;
}

a
{
    color: #005b7f;
}

.hidden
{
    display: none;
}

#password
{
    display: none;
}

@width: 745px;

#wrapper
{
    width: @width;
    background: url(../images/background.png);
    background-repeat: no-repeat;
   
    /* Sticky Footer */
    min-height: 100%;
    height: auto !important;
    height: 100%;
    margin: 0 auto -168px;
}

#footer
{
    position: relative;
    height: 168px;
    width: @width;
    background: url(../images/footer-bg.png);
    background-position: 23px 0px;
    margin: 0 auto;
    background-repeat: no-repeat;
}

#header
{
    padding-top: 22px;
    padding-left: 30px;
    padding-right: 30px;
    height: 74px;
    margin: 0;
    font-family: "Trebuchet MS";
    font-size: 16px;
    font-weight: bold;
}

#header img
{
    margin-top: 11px;
    margin-left: 19px;
    float: left;
}

#header ol
{
    margin: 0px 0px 0px 135px;
    padding:0;
    list-style:none;
}

@header_height: 72px;
#header ol li
{
    float: left;
    padding: 0;
    margin: 0px 10px 0px 0px;
    height: @header_height;
}

#header ol li:hover, li.active
{
    background: url(../images/active-nav-bg.png) no-repeat right top;
}

#header ol li:hover a, li.active a
{
    background:url(../images/active-nav-left.png)
      no-repeat left top;
    color: #fff;
}

#header ol li a
{
    display: block;
    padding-top: 20px;
    padding-left: 22px;
    padding-right: 22px;
    height: @header_height;
    text-decoration: none;
    color: Black;
}


.push 
{
    height: 168px;
}

#footer h6, #footer p
{
    margin-left: 230px;
    color: #d0d0d0;
    width: 300px;
}

#footer h6
{
    font-family: Trebuchet MS;
    font-size: 24px;
    padding-top: 25px;
    margin-bottom: 20px;
}

#footer .imprint
{
    margin-left: 55px;
    color: #9a9a9a;
    width: 145px;
    text-align: right;
    float: left;
}

#footer .imprint a, #footer .imprint a:active, #footer .imprint a:visited
{
    color: #9a9a9a;
}

#footer .imprint a:hover
{
    color: #d0d0d0;
}


/* Tiny Date */
@dateColor: #959595;
div.date
{
    -moz-border-radius: 7px;
    -webkit-border-radius: 7px;
    width: 47px;
    height: 32px;
    background: white;
    color: @dateColor;
    padding: 0;
    float: left;
    padding-left: 4px;
    margin-right: 4px;
}

div.date span.day
{
    font-size: 25px;
    float: left;
    margin-right: 2px;
}

div.date hr
{
    height: 1px;
    width: 11px;
    border: none;
    border-top: 1px solid #e1dfdf;
    background: @dateColor;
    margin: 0;
    padding: 0;
}

div.date span.year, div.date span.month
{
    font-size: 9px;
}

/* body-wrap */
#body-wrap
{
    padding-top: 0px;
    margin: 0;
    margin-left: 45px;
    margin-top: 50px;
    margin-right: 60px;
    overflow: hidden;
}

div#content
{
    margin-bottom: 30px;
    width: 414px;
    margin-right: 26px;
    float: left;
}

h1 
{
    margin-bottom: 14px;
}

@title-fontsize: 22px;
h2, h1, h3
{
    font-size: @title-fontsize;
    font-weight: bold;
    font-family: "Trebuchet MS";
}

#body-wrap div.post h2 a
{
    color: Black;
    text-decoration: none;
}
#body-wrap div.post h2 a:hover
{
    text-decoration: underline;
}

#body-wrap ul, #body-warp ol
{
    padding-bottom: 10px;
}

div.post > p
{
    margin-top: 10px;
}

#body-wrap p
{
    margin-left: 14px;
    padding-bottom: 10px;
    text-align: justify;
}

#body-wrap div.post img
{
    background: #fff;
    padding: 4px;
    -moz-border-radius: 2px;
    -webkit-border-radius: 2px;
    float: right;
    margin: 6px;
}

#sidebar
{
    width: 196px;
    float: right;
}

#sidebar > div
{
    margin-bottom: 15px;
}

#login
{
    width: 196px;
}

#login fieldset
{
    margin: 0;
}

#login input
{
    width: 144px;
}

#login input[type=submit]
{
    width: 160px;
}

#login ol a
{
    display: inline-block;
    color: White;
    text-align: right;
    width: 160px;
    font-weight: bold;
    text-decoration: none;
}

div#services
{
    margin-bottom: 20px;
}

div#services > img
{
    float: left;
    margin-left: 15px;
    margin-right: 7px;
    margin-top: 2px;
}

div#services h3 
{
    color: #6f6f6f;
    font-size: 18px;
    font-weight: bold;
    font-family: "Trebuchet MS";
    padding: 0;
    margin: 0;
}

div#services ol li a
{
    text-decoration: none;
    font-weight: bold;
    vertical-align: top;
}

div#services span
{
    display: block;
    margin-left: 24px;
}

div#services ol li a img
{
    margin-right: 8px;
}

div#services ol
{
    margin-top: 4px;
    margin-left: 6px;
    margin-right: 6px;
    border-top: 1px solid #a5a5a5;
    list-style: none;
    padding-left: 4px;
    padding-right: 4px;
    padding-top: 8px;
}

div#services ol li
{
    margin-top: 4px;
}

div#services ol li.seperated
{
    margin-top: 8px;
}

#body-wrap div.exception
{
    width: 414px;
    margin-bottom: 30px;
}

#body-wrap div.exception h1
{
    color: Red;
}

.exception pre
{
    overflow: scroll;
    background: #ffffcc;
    border: 1px solid black;
    margin-top: 4px;
}

sup
{
    padding-left: 15px;
    margin-bottom: 10px;
}

#content form
{
    margin-top: 30px;
}

fieldset
{
    padding: 4px 6px 6px 6px;
    background-color: #a4a4a4;
    background-image: url(../images/fieldset-top-bg.png);
    background-repeat: repeat-x;
    border: 0;
    position: relative;
    -moz-border-radius: 6px;
    -webkit-border-radius: 6px;
    margin: 0px 0px 10px 20px;
}

fieldset h3
{
    font-weight: bold;
    font-family: "Trebuchet MS";
    font-size: 18px;
    text-transform: lowercase;
    color: White;
    /*padding-top: 4px;*/
    vertical-align: top;
    margin-bottom: 10px;
}

fieldset > img
{
    float: left;
    margin-left: 6px;
    margin-right: 10px;
    margin-top: 5px;
}

fieldset legend
{
    display: none;
}

fieldset ol
{
    list-style: none;
    margin:0;
    padding:0;
}
fieldset li
{
    padding: 5px;
    text-align: center;
}

fieldset label
{
    width: 100px;
    line-height: 1.8;
    display: inline-block;
    vertical-align: top;
    text-align: left;
}

fieldset input, fieldset textarea
{
    width: 244px;
}

fieldset select
{
    width: 260px;
    height: 27px;
}

input, textarea
{
    border: 1px solid #595959;
    height: 27px;
    vertical-align: middle;
    color: #cecece;
    padding-left: 8px;
    padding-right: 8px;
}

.visited
{
    color: black;
}

textarea
{
    height: 180px;
}

input:focus
{
    color: Black;
}

input[type=submit]
{
    width: 144px;
    height: 31px;
    border: 1px solid white;
    -moz-border-radius: 3px;
    -webkit-border-radius: 3px;
    color: Black;
    background: url(../images/button-bg.png);
    text-transform: lowercase;
    font-size: 18px;
    font-family: "Trebuchet MS";
}
div.pagination
{
    width: 100%;
    background:Red;
}
div.pagination .next
{
    float: right;
}

div.pagination .previous
{
    float: left;
}

div#quickdownloads h3
{
    color: #6f6f6f;
    font-size: 18px;
    font-weight: bold;
    font-family: "Trebuchet MS";
    padding: 0;
    margin: 0;
}

div#quickdownloads > img
{
    float: left;
    margin-left: 15px;
    margin-right: 7px;
    margin-top: 2px;
}

div#quickdownloads ol
{
    margin-top: 4px;
    margin-left: 6px;
    margin-right: 6px;
    border-top: 1px solid #a5a5a5;
    list-style: none;
    padding-left: 4px;
    padding-right: 4px;
    padding-top: 8px;
}

div#quickdownloads li
{
    margin-bottom: 8px;
}

div#quickdownloads ol li a
{
    font-size: 12px;
    font-weight: bold;
    text-decoration: none;
    color: Black;
}

div#quickdownloads ol li span
{
    display: block;
}

div#quickdownloads ol li a img
{
    float: left;
    padding-left: 4px;
    padding-right: 5px;
    padding-top: 4px;
    padding-bottom: 5px;
    background: #4b4b4b;
    margin-right: 8px;
    
    -moz-border-radius: 5px;
    -webkit-border-radius: 5px;
}

/* FileDownload Badge */
div#filedownload
{
    margin-top: 30px;
    background: #4b4b4b;
    -moz-border-radius: 7px;
    -webkit-border-radius: 7px;
    height: 120px;
    color: White;
    padding: 4px 4px 6px 20px;
    text-align: center;
}

div#filedownload img#download-symbol
{
    float: left;
    position: relative;
    top: -30px;
    left: -18px;
}

div#filedownload h3
{
    margin-top: 10px;
}

div#filedownload button
{
    margin-top: 20px;
    width: 60%;
    height: 30px;
}

div#filedownload span
{
    display: block;
    margin-top: 10px;
}


div#team
{
    width: 414px;
    p
    {
        float: right;
        width: 300px;
    }
    h2
    {
        clear: right;
        font-size: @title-fontsize - 4;
    }
    img
    {  
        width: 100px;
        margin-bottom: 4px;
    }
}