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
        
        <table><tr><td width="40%"></td><td><h1 class="h1_text"><u>Permutation test</u></h1></td><td></td></tr>
            <tr><td></td><td>
                <div class="main">
                    <div class="mainContainer">
                        <div style="text-align:center;">
                            <p>Search examples: <i><b>"apple"</b></i>, <i><b>"stressed".</b></i>
                            <br/><br/>WAIT for the results to show on the next page.
                            <br/><br/>There is no progress indicator !</p>
                        </div>     
                        <div style="text-align:center;">
                            <input id="first_word" type="text" class="similar_css"/>        
                            <a href="#" class="similar_css" id="link_words" onclick="QueryPermutationDB()">Similar word</a>
                        </div>       
                        <div style="text-align:center;">
                            <a href="#" id="statsLink" onclick="OpenStats()">See some Stats !</a>                                
                        </div>                 
                    </div>        
                    <br/><br/>
                    <label id="messageDiv" style="font-size:40px">When transfered to the results page, press the "Back" button to come back here.</label>
                </div>                
            </td><td></td></tr>
        </table>
	</form>
</body>
</html>
