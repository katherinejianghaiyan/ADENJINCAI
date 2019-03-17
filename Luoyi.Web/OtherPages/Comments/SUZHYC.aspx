<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Controls/OtherPages/SUZHYC.Master" CodeBehind="SUZHYC.aspx.cs" Inherits="Luoyi.Web.OtherPages.Comments.SUZHYC" %>

<%@ MasterType VirtualPath="~/Controls/OtherPages/SUZHYC.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/css/SUZHYC.css?<%= Guid.NewGuid() %>" rel="stylesheet" />
    <style>
        body {
            background: white;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <p>意见建议</p>
    </div>
    <div style="text-align: center;">    
        <asp:TextBox ID="comments" cssClass="multitext" runat="server"  TextMode="MultiLine"></asp:TextBox>
            <br />
            <br />
        <asp:LinkButton ID="lbtnSubmit" runat="server" CssClass="linkbutton" OnClick="lbtnSubmit_Click" >
            发送
        </asp:LinkButton>
    </div>  
    <div class="divtext">
        <asp:Repeater ID="rptList" runat="server">
            <ItemTemplate>
                <div class="myList">
                    <div class="commentDetail">
                        <div style ="text-align:right">
                           <%# Eval("CommentTime") %>
                        </div>
                        <div> <%# Eval("Content") %></div>
                       
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>