<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-8">
            <asp:Label ID="lab_name" runat="server" Text="lab_name">姓名:</asp:Label>
            <asp:TextBox ID="txt_name" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-8">
            <asp:Label ID="lab_old" runat="server" Text="lab_old">年齡:</asp:Label>
            <asp:TextBox ID="txt_old" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-8">
            <asp:Label ID="lab_birthday" runat="server" Text="lab_birthday">生日:</asp:Label>
            <asp:TextBox ID="txt_birthday" runat="server"></asp:TextBox>
        </div>      
        <div class="col-md-8">
            <asp:Button ID="_btn" runat="server" Text="建立帳號" OnClick="_btn_Click"/>
        </div>     
         <div class="col-md-8">
        <asp:GridView ID="GridView1" runat="server" DataKeyNames="name"  OnRowCommand="GridView1_OnRowCommand" AutoGenerateColumns="False">
         <Columns>
        <asp:BoundField DataField="name" HeaderText="姓名" />
        <asp:BoundField DataField="old" HeaderText="年齡" />
        <asp:BoundField DataField="birthday" HeaderText="生日" />
        <asp:TemplateField ShowHeader="true">
            <ItemTemplate>
               <asp:Button ID="btn_Upd" Text="修改" runat="server" CommandName="Upd" CommandArgument='<%# Eval("name")+ "," + Eval("old")+ "," + Eval("birthday")   %>'/>
               <asp:Button ID="btn_Del" Text="刪除" runat="server" CommandName="Del" CommandArgument='<%# Eval("name")+ "," + Eval("old")+ "," + Eval("birthday")   %>'/>
            </ItemTemplate>
        </asp:TemplateField>
       </Columns>
        </asp:GridView>
            <asp:TextBox ID="txtSelectRow" runat="server" style="display:none;"></asp:TextBox>
        </div>    
    </div>
</asp:Content>
