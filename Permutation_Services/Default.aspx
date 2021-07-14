<%@ Page Language="C#" Debug="true" Inherits="Permutation_Services.Default" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Permutation Home Test</title>
    <meta content="text/html;charset=utf-8" http-equiv="Content-Type"/>
    <meta content="utf-8" http-equiv="encoding"/>
    <link rel="stylesheet" href="styling.css"/>
    <script type="text/javascript" src="js_scripts.js"></script>
</head>
<body style="font-family:Arial;">
	<form id="form1" runat="server">
        
        <table><tr><td width="40%"></td><td><h1 class="h1_text">Permutation test</h1></td><td></td></tr>
                <tr><td></td><td>
        <div class="main">    
            <div style="margin:20px;border:1px solid red;padding:10px">            
                <div class="">
                    <input id="first_word" type="text" class="similar_css"/>        
                    <a href="#" class="similar_css" id="link_words" onclick="updateWordsQueryString()">Similar word</a>
                </div>       
                <div class="">
                    <a href="#" id="statsLink" onclick="updateWordsDictionaryQueryString()">See some Stats !</a>                                
                </div>                 
            </div>        
            <label id="messageDiv">When tranfered to the results page, press the "Back" button to come back here.</label>    
        </div>                
        </td><td></td></tr></table>
	</form>
</body>
</html>
