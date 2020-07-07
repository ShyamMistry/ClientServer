<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectCRUD.aspx.cs" Inherits="WebApp.Exercises.ProjectCRUD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Project CRUD By Shyam Mistry</h1>
    <div class="row">
        <div class="col-4"></div>
        <div class="col-8 text-left">
            <h3>Program Maintenance Page</h3>
        </div>
    </div>
     <div class="row">
        <div class="col-4 text-right">
                <asp:Label ID="Label1" runat="server" Text="Program ID"
                     AssociatedControlID="PID">
                </asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:TextBox ID="PID" runat="server" ReadOnly="true">
                </asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-4 text-right">
                  <asp:Label ID="Label2" runat="server" Text="Program Name"
                     AssociatedControlID="PName"></asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:TextBox ID="PName" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-4 text-right">
                  <asp:Label ID="Label3" runat="server" Text="Diploma Name"
                     AssociatedControlID="DName"></asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:TextBox ID="DName" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-4 text-right">
                <asp:Label ID="Label6" runat="server" Text="School Code"
                     AssociatedControlID="SchoolList">
                </asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:DropDownList ID="SchoolList" runat="server" Width="300px">
                </asp:DropDownList> 
        </div>
    </div>
    <div class="row">
        <div class="col-4 text-right">
                  <asp:Label ID="Label5" runat="server" Text="Tuition"
                     AssociatedControlID="Tuition"></asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:TextBox ID="Tuition" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-4 text-right">
                  <asp:Label ID="Label4" runat="server" Text="International Tuition"
                     AssociatedControlID="IntTuition"></asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:TextBox ID="IntTuition" runat="server"></asp:TextBox>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-4"></div>
        <div class="col-8 text-left">
            <asp:Button ID="BackButton" runat="server" Text="Back" OnClick="Back_Click" />&nbsp;
            <asp:Button ID="AddButton" runat="server" OnClick="Add_Click" Text="Add"/>&nbsp;
            <asp:Button ID="UpdateButton" runat="server" OnClick="Update_Click" Text="Update"/>&nbsp;
            <asp:Button ID="DeleteButton" runat="server" OnClick="Delete_Click" Text="Delete"
              OnClientClick="return CallFunction();"/>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-4"></div>
        <div class="col-8">
            <label ID="MessageLabel" runat="server" />
        </div>
    </div>
    <script type="text/javascript">
        function CallFunction() {
            return confirm("Are you sure you wish to delete this record?");
       }
   </script>
</asp:Content>
