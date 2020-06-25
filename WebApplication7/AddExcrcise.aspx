<%@ Page Title="" Language="C#" MasterPageFile="~/Excrcise.Master" AutoEventWireup="true" CodeBehind="AddExcrcise.aspx.cs" Inherits="WebApplication7.AddExcrcise" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="NiceBacground Centerd" style="width:30%; height:100%"> 
            <h1>Add Excrcise</h1>
            <asp:Label ID="LblExcrciseName" runat="server" Text="שם תרגיל"></asp:Label>
            <br />
            <asp:TextBox ID="TxtExcrciseName" runat="server"></asp:TextBox>
            
            <asp:RadioButtonList ID="IsCompundOrIsolate" CssClass="radioButtonList ListItemCenterd" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="IsCompound">compound</asp:ListItem>
                <asp:ListItem Value="" Selected="True">isolate</asp:ListItem>
            </asp:RadioButtonList>
            
                <h3>Shoulders</h3>
                <asp:RadioButtonList ID="Shoulder" CssClass="radioButtonList ListItemCenterd" runat="server" RepeatDirection="Horizontal" >
                <asp:ListItem Value="1">true</asp:ListItem>
                <asp:ListItem Value="0" Selected="True">fale</asp:ListItem>
                </asp:RadioButtonList>
            <h3>Triceps</h3>
                <asp:RadioButtonList ID="Triceps" CssClass="radioButtonList ListItemCenterd" runat="server" RepeatDirection="Horizontal" >
                <asp:ListItem Value="1">true</asp:ListItem>
                <asp:ListItem Value="0" Selected="True">fale</asp:ListItem>
                </asp:RadioButtonList>
            <h3>Back</h3>
                <asp:RadioButtonList ID="Lats" CssClass="radioButtonList ListItemCenterd" runat="server" RepeatDirection="Horizontal" >
                <asp:ListItem Value="1">true</asp:ListItem>
                <asp:ListItem Value="0" Selected="True">fale</asp:ListItem>
                </asp:RadioButtonList>
            <h3>Chest</h3>
                <asp:RadioButtonList ID="Chest" CssClass="radioButtonList ListItemCenterd" runat="server" RepeatDirection="Horizontal" >
                <asp:ListItem Value="1">true</asp:ListItem>
                <asp:ListItem Value="0" Selected="True">fale</asp:ListItem>
                </asp:RadioButtonList>
            <h3>Biceps</h3>
                <asp:RadioButtonList ID="Biceps" CssClass="radioButtonList ListItemCenterd" runat="server" RepeatDirection="Horizontal" >
                <asp:ListItem Value="1">true</asp:ListItem>
                <asp:ListItem Value="0" Selected="True">fale</asp:ListItem>
                </asp:RadioButtonList>
            <h3>Abs</h3>
                <asp:RadioButtonList ID="Abs" CssClass="radioButtonList ListItemCenterd" runat="server" RepeatDirection="Horizontal" >
                <asp:ListItem Value="1">true</asp:ListItem>
                <asp:ListItem Value="0" Selected="True">fale</asp:ListItem>
                </asp:RadioButtonList>
            <h3>Legs</h3>
                <asp:RadioButtonList ID="Legs" CssClass="radioButtonList ListItemCenterd" runat="server" RepeatDirection="Horizontal" >
                <asp:ListItem Value="1">true</asp:ListItem>
                <asp:ListItem Value="0" Selected="True">fale</asp:ListItem>
                </asp:RadioButtonList>
            <h3>PowerLifting</h3>
                <asp:RadioButtonList ID="PowerLifting" CssClass="radioButtonList ListItemCenterd" runat="server" RepeatDirection="Horizontal" >
                <asp:ListItem Value="1">true</asp:ListItem>
                <asp:ListItem Value="0" Selected="True">fale</asp:ListItem>
                </asp:RadioButtonList>
            <h3>Street</h3>
                <asp:RadioButtonList ID="Street" CssClass="radioButtonList ListItemCenterd" runat="server" RepeatDirection="Horizontal" >
                <asp:ListItem Value="1">true</asp:ListItem>
                <asp:ListItem Value="0" Selected="True">fale</asp:ListItem>
                </asp:RadioButtonList>
            <h3>Difficult     </h3>
            <asp:TextBox ID="Difficult" runat="server"></asp:TextBox>
            <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="AddExcrciseToTable" />
        </div>
        

    </div>
</asp:Content>
