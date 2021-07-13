<%@ Page Language="C#" Inherits="Permutation_Services.Default" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Default</title>
          <link rel="stylesheet" href="styling.css">
        <script type="text/javascript" src="js_scripts.js"></script>
       
        <h1 class="h1_text">Permutation test</h1>
</head>
<body>
	<form id="form1" runat="server">
            <div class="main">    
            <div style="margin:20px">
                <div class="">
                    <div class="">
                        <input id="first_word" type="text" class="similar_css"/>        
                        <a href="#" class="similar_css" id="link_words" onclick="updateWordsQueryString()">Similar word</a>
                    </div>
                            
                </div>
                <div>
                    <div class="">
                        <a href="#" id="statsLink" onclick="updateWordsDictionaryQueryString()">See some Stats !</a>                                
                    </div>
                </div>
            </div>
        </div>
	</form>
</body>
</html>
