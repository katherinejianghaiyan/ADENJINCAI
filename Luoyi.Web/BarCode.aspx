<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BarCode.aspx.cs" Inherits="Luoyi.Web.BarCode" %>

<%@ Import Namespace="Luoyi.Common" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
            <asp:Image ID="Image1" runat="server" />
            <%= Request.Url.Host %>
        </div>
    </form>
</body>
</html>
