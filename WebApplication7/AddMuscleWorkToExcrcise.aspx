<%@ Page Title="" Language="C#" MasterPageFile="~/Excrcise.Master" AutoEventWireup="true" CodeBehind="AddMuscleWorkToExcrcise.aspx.cs" Inherits="WebApplication7.AddMuscleWorkToExcrcise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="Centerd NiceBacground">
        <asp:DropDownList ID="DDL_ExcNames" runat="server"></asp:DropDownList>
        <h1>Shoulder  front deltoid</h1>
        <asp:DropDownList ID='DropDownList1' runat='server'></asp:DropDownList>
        <h1>Shoulder  rear deltoid</h1>
        <asp:DropDownList ID='DropDownList2' runat='server'></asp:DropDownList>
        <h1>Shoulder lateral deltoid</h1>
        <asp:DropDownList ID='DropDownList3' runat='server'></asp:DropDownList>
        <h1>Bicep short</h1>
        <asp:DropDownList ID='DropDownList4' runat='server'></asp:DropDownList>
        <h1>Bicep long</h1>
        <asp:DropDownList ID='DropDownList5' runat='server'></asp:DropDownList>
        <h1>Bicep brachialis</h1>
        <asp:DropDownList ID='DropDownList6' runat='server'></asp:DropDownList>
        <h1>Tricep long</h1>
        <asp:DropDownList ID='DropDownList7' runat='server'></asp:DropDownList>
        <h1>Tricep medial</h1>
        <asp:DropDownList ID='DropDownList8' runat='server'></asp:DropDownList>
        <h1>Back traps</h1>
        <asp:DropDownList ID='DropDownList9' runat='server'></asp:DropDownList>
        <h1>Back lats</h1>
        <asp:DropDownList ID='DropDownList10' runat='server'></asp:DropDownList>
        <h1>Back teres major</h1>
        <asp:DropDownList ID='DropDownList11' runat='server'></asp:DropDownList>
        <h1>Chest upper</h1>
        <asp:DropDownList ID='DropDownList12' runat='server'></asp:DropDownList>
        <h1>Chest lower</h1>
        <asp:DropDownList ID='DropDownList13' runat='server'></asp:DropDownList>
        <h1>Chest middle</h1>
        <asp:DropDownList ID='DropDownList14' runat='server'></asp:DropDownList>
        <h1>Abs oblique</h1>
        <asp:DropDownList ID='DropDownList15' runat='server'></asp:DropDownList>
        <h1>Abs abdominal</h1>
        <asp:DropDownList ID='DropDownList16' runat='server'></asp:DropDownList>
        <h1>Leg glutes</h1>
        <asp:DropDownList ID='DropDownList17' runat='server'></asp:DropDownList>
        <h1>Leg quads</h1>
        <asp:DropDownList ID='DropDownList18' runat='server'></asp:DropDownList>
        <h1>Leg Hamstring</h1>
        <asp:DropDownList ID='DropDownList19' runat='server'></asp:DropDownList>
        <h1>Leg Calves</h1>
        <asp:DropDownList ID='DropDownList20' runat='server'></asp:DropDownList>
        <br />
        <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="AddExcrciseToTable" />
    </div>
</asp:Content>