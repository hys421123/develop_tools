<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" 
    Inherits="EntityGenerator.Default" ValidateRequest="false" %>
<%@ Import namespace="System.Data" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>实体转换工具</title>
 
    <link href="css/newstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
         <div class="fix">
         <div class="sidebox">
         <div class="p20">
    	        <ul class="inputbox">
        	        <li><label>手机系统:</label>
                        <asp:DropDownList ID="ddlMobileSystem" runat="server" CssClass="t01" Height="20px" Width="160px"></asp:DropDownList>
                    </li>
                    <li>
                        <label id="lblVariable" runat="server"></label>
                        <input id="txtVariable" runat="server" type="text" class="t01" />
                    </li>
                    <li>
                        <label>Mobile API:</label>
                        <input id="txtMobileAPI" runat="server" type="text" class="t01" />
                    </li>
                </ul>
                <p class="mt10">JSON 字符串:</p>
                <p class="mt10"><asp:TextBox ID="txtJsonString" runat="server" CssClass="texta" TextMode="MultiLine" ></asp:TextBox></p>
                <p class="mt10">
                    <asp:Button ID="btnLoadMapping" runat="server" onclick="btnLoadMapping_Click" 
                        Text="生成映射关系" />
                </p>
                </div>
            </div>
            <!--sidebox end-->
 	        <div class="mainbox">
    	        <div class="p20">

                    <asp:GridView ID="gvMappingList" AutoGenerateColumns="false" runat="server" Height="100%" Width="359px">
                        <Columns>
                            <asp:TemplateField HeaderText="层级">
                                <ItemTemplate>
                                    <asp:Label ID="Level" runat="server" Text=<%# Eval("Level")%> Width="25px" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Json实体名">
                                <ItemTemplate>
                                    <asp:TextBox ID="EntityName" runat="server" Text=<%# Eval("EntityName")%>></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="转换前">
                                <ItemTemplate>
                                    <asp:TextBox ID="JsonFromField" runat="server" Text=<%# Eval("JsonFromField")%>></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="转换后">
                                <ItemTemplate>
                                    <asp:TextBox ID="JsonToField" runat="server" Text=<%# Eval("JsonToField")%>></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="类型">
                                <ItemTemplate>
                                    <asp:TextBox ID="JsonToType" runat="server" Text=<%# Eval("JsonToType")%>></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>


         <p class="mt10">
             <asp:Button ID="btnGenerate" runat="server" onclick="btnGenerate_Click" Text="生成实体" />
         </p>
                </div>
            </div>
    
         </div>
    </form>
</body>
</html>
