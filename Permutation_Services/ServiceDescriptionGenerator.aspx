<%@ Page language="c#" Codebehind="ServiceDescriptionGenerator.aspx.cs" AutoEventWireup="false" Inherits="com.esendex.webservicedocumentation.ServiceDescriptionGenerator" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Xml.Serialization" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.Xml.Schema" %>
<%@ Import Namespace="System.Web.Services.Description" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="System.Resources" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="System.Net" %>
<HTML>
  <head>

  <head>

    <link rel="alternate" type="text/xml" href="<%#FileName%>?disco"/>
	<LINK href="WebServiceDocumentation.css" type="text/css" rel="stylesheet">

    <title><%#ServiceName + " " + GetLocalizedText("WebService")%></title>

  </head>

  <body>

    <div id="content">

      <p class="heading1"><%#ServiceName%></p><br>

      <span visible='<%#ShowingMethodList && ServiceDocumentation.Length > 0%>' runat=server>
          <p class="intro"><%#ServiceDocumentation%></p>
      </span>

      <span visible='<%#ShowingMethodList%>' runat=server>

          <p class="intro"><%#GetLocalizedText("OperationsIntro", new object[] { EscapedFileName + "?WSDL" })%></p>

          <asp:repeater id="MethodList" runat=server>
      
            <headertemplate name="headertemplate">
              <ul>
            </headertemplate>
      
            <itemtemplate name="itemtemplate">
              <li>
                <a href="<%#EscapedFileName%>?op=<%#EscapeParam(DataBinder.Eval(Container.DataItem, "Key").ToString())%>"><%#DataBinder.Eval(Container.DataItem, "Key")%></a>
                <span visible='<%#((string)DataBinder.Eval(Container.DataItem, "Value.Documentation")).Length>0%>' runat=server>
                  <br><%#DataBinder.Eval(Container.DataItem, "Value.Documentation")%>
                </span>
              </li>
              <p>
            </itemtemplate>
      
            <footertemplate name="footertemplate">
              </ul>
            </footertemplate>
      
          </asp:repeater>
      </span>

      <span visible='<%#!ShowingMethodList && OperationExists%>' runat=server>
          <p class="intro"><%#GetLocalizedText("LinkBack", new object[] { EscapedFileName })%></p>
          <h2><%#OperationName%></h2>
          <p class="intro"><%#SoapOperationBinding == null ? "" : SoapOperation.Documentation%></p>

          <h3><%#GetLocalizedText("TestHeader")%></h3>
          
          <% if (!showPost) { 
                 if (!ShowingHttpGet) { %>
                     <%#GetLocalizedText("NoHttpGetTest")%>
              <% }
                 else {
                     if (!ShowGetTestForm) { %>
                        <%#GetLocalizedText("NoTestNonPrimitive")%>
                  <% }
                     else { %>

                      <%#GetLocalizedText("TestText")%>

                      <form target="_blank" action='<%#TryGetUrl == null ? "" : TryGetUrl.AbsoluteUri%>' method="GET">
                        <asp:repeater datasource='<%#TryGetMessageParts%>' runat=server>

                        <headertemplate name="HeaderTemplate">
                           <table cellspacing="0" cellpadding="4" frame="box" bordercolor="#dcdcdc" rules="none" style="border-collapse: collapse;">
  						   <tr visible='<%# TryGetMessageParts.Length > 0%>' runat=server>
                            <td class="frmHeader" background="#dcdcdc" style="border-right: 2px solid white;"><%#GetLocalizedText("Parameter")%></td>
                            <td class="frmHeader" background="#dcdcdc"><%#GetLocalizedText("Value")%></td>
                          </tr>
                        </headertemplate>

                        <itemtemplate name="ItemTemplate">
                          <tr>
                            <td class="frmText" style="color: #000000; font-weight:normal;"><%# ((MessagePart)Container.DataItem).Name %>:</td>
                            <td><input class="frmInput" type="text" size="50" name="<%# ((MessagePart)Container.DataItem).Name %>"></td>
                          </tr>
                        </itemtemplate>

                        <footertemplate name="FooterTemplate">
                          <tr>
                            <td></td>
                            <td align="right"> <input type="submit" value="<%#GetLocalizedText("InvokeButton")%>" class="button"></td>
                          </tr>
                          </table>
                        </footertemplate>
                    </asp:repeater>

                      </form>
                  <% } 
                 }
             }
             else { // showPost
                 if (!ShowingHttpPost) { 
                    if (requestIsLocal) { %>
                        <%#GetLocalizedText("NoTestNonPrimitive")%>
                 <% }
                    else { %>
                        <%#GetLocalizedText("NoTestFormRemote")%>
                 <% }
                 }
                 else {
                     if (!ShowPostTestForm) { %>
                        <%#GetLocalizedText("NoTestNonPrimitive")%>
                     
                  <% }
                     else { %>                      

                    
                      <%#GetLocalizedText("TestText")%>



                      <form target="_blank" action='<%#TryPostUrl == null ? "" : TryPostUrl.AbsoluteUri%>' method="POST">                      
                        <asp:repeater datasource='<%#TryPostMessageParts%>' runat=server>

                        <headertemplate name="HeaderTemplate">
                          <table cellspacing="0" cellpadding="4" frame="box" bordercolor="#dcdcdc" rules="none" style="border-collapse: collapse;">
                          <tr visible='<%# TryPostMessageParts.Length > 0%>' runat=server>
                            <td class="frmHeader" background="#dcdcdc" style="border-right: 2px solid white;"><%#GetLocalizedText("Parameter")%></td>
                            <td class="frmHeader" background="#dcdcdc"><%#GetLocalizedText("Value")%></td>
                          </tr>
                        </headertemplate>

                        <itemtemplate name="ItemTemplate">
                          <tr>
                            <td class="frmText" style="color: #000000; font-weight: normal;"><%# ((MessagePart)Container.DataItem).Name %>:</td>
                            <td><input class="frmInput" type="text" size="50" name="<%# ((MessagePart)Container.DataItem).Name %>"></td>
                          </tr>
                        </itemtemplate>                        
						
                        <footertemplate name="FooterTemplate">
                        <tr>
                          <td></td>
                          <td align="right"> <input type="submit" value="<%#GetLocalizedText("InvokeButton")%>" class="button"></td>
                        </tr>
                        </table>
                      </footertemplate>
                    </asp:repeater>

                    </form>
                  <% }
                 }
             } %>
          <span visible='<%#ShowingSoap%>' runat=server>
              <h3><%#GetLocalizedText("SoapTitle")%></h3>
              <p><%#GetLocalizedText("SoapText")%></p>

              <pre><%#SoapOperationInput%></pre>

              <pre><%#SoapOperationOutput%></pre>
          </span>

          <span visible='<%#ShowingHttpGet%>' runat=server>
              <h3><%#GetLocalizedText("HttpGetTitle")%></h3>
              <p><%#GetLocalizedText("HttpGetText")%></p>

              <pre><%#HttpGetOperationInput%></pre>

              <pre><%#HttpGetOperationOutput%></pre>
          </span>

          <span visible='<%#ShowingHttpPost%>' runat=server>
              <h3><%#GetLocalizedText("HttpPostTitle")%></h3>
              <p><%#GetLocalizedText("HttpPostText")%></p>

              <pre><%#HttpPostOperationInput%></pre>

              <pre><%#HttpPostOperationOutput%></pre>
          </span>

      </span>
      <span visible='<%#ShowingMethodList && ServiceNamespace == "http://tempuri.org/"%>' runat=server>
          <hr>
          <h3><%#GetLocalizedText("DefaultNamespaceWarning1")%></h3>
          <h3><%#GetLocalizedText("DefaultNamespaceWarning2")%></h3>
          <p class="intro"><%#GetLocalizedText("DefaultNamespaceHelp1")%></p>
          <p class="intro"><%#GetLocalizedText("DefaultNamespaceHelp2")%></p>
          <p class="intro"><%#GetLocalizedText("DefaultNamespaceHelp3")%></p>
          <p class="intro">C#</p>
          <pre>[WebService(Namespace="http://microsoft.com/webservices/")]
public class MyWebService {
    // <%#GetLocalizedText("Implementation")%>
}</pre>
          <p class="intro">Visual Basic.NET</p>
          <pre>&lt;WebService(Namespace:="http://microsoft.com/webservices/")&gt; Public Class MyWebService
    ' <%#GetLocalizedText("Implementation")%>
End Class</pre>
          <p class="intro"><%#GetLocalizedText("DefaultNamespaceHelp4")%></p>
          <p class="intro"><%#GetLocalizedText("DefaultNamespaceHelp5")%></p>
          <p class="intro"><%#GetLocalizedText("DefaultNamespaceHelp6")%></p>
      </span>

      <span visible='<%#!ShowingMethodList && !OperationExists%>' runat=server>
          <%#GetLocalizedText("LinkBack", new object[] { EscapedFileName })%>
          <h2><%#GetLocalizedText("MethodNotFound")%></h2>
          <%#GetLocalizedText("MethodNotFoundText", new object[] { Server.HtmlEncode(OperationName), ServiceName })%>
      </span>


  </body>
</HTML>
