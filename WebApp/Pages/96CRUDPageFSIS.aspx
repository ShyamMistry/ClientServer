<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="96CRUDPageFSIS.aspx.cs" Inherits="WebApp.Pages._96CRUDPageFSIS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Player Maintenance Page</h1>
    <div class="row">
        <div class="col-4 text-right">
                <asp:Label ID="Label1" runat="server" Text="Player's ID"
                     AssociatedControlID="ID">
                </asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:TextBox ID="ID" runat="server" ReadOnly="true">
                </asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-4 text-right">
                  <asp:Label ID="Label2" runat="server" Text="First Name"
                     AssociatedControlID="FName"></asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:TextBox ID="FName" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-4 text-right">
                  <asp:Label ID="Label3" runat="server" Text="Last Name"
                     AssociatedControlID="LName"></asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:TextBox ID="LName" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-4 text-right">
                <asp:Label ID="Label6" runat="server" Text="Age"
                     AssociatedControlID="Age">
                </asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:TextBox ID="Age" runat="server"></asp:TextBox> 
        </div>
    </div>
    <div class="row">
        <div class="col-4 text-right">
                <asp:Label ID="Label7" runat="server" Text="Gender"
                     AssociatedControlID="Gender">
                </asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:TextBox ID="Gender" runat="server">
                </asp:TextBox> 
        </div>
    </div>
    <div class="row">
        <div class="col-4 text-right">
                  <asp:Label ID="Label4" runat="server" Text="Health Card"
                     AssociatedControlID="HCard">
                  </asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:TextBox ID="HCard" runat="server"> 
                </asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-4 text-right">
                <asp:Label ID="Label5" runat="server" Text="Team"
                     AssociatedControlID="TeamList">
                </asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:DropDownList ID="TeamList" runat="server" Width="300px" >
                </asp:DropDownList> 
        </div>
    </div>
    <div class="row">
        <div class="col-4 text-right">
                <asp:Label ID="Label8" runat="server" Text="Gaurdian"
                     AssociatedControlID="GuardianList">
                </asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:DropDownList ID="GuardianList" runat="server" Width="300px" >
                </asp:DropDownList> 
        </div>
    </div>
    <div class="row">
        <div class="col-4 text-right">
                  <asp:Label ID="Label11" runat="server" Text="Medical Alert Details"
                     AssociatedControlID="MAlert">
                  </asp:Label>
        </div>
        <div class="col-8 text-left">
                <asp:TextBox ID="MAlert" runat="server">
                </asp:TextBox> 
        </div>
    </div>
    <br/>
    <div class="row">
        <div class="col-4"></div>
        <div class="col-8 text-left">
            <asp:Button ID="BackButton" runat="server" Text="Back" OnClick="Back_Click" />&nbsp;
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
