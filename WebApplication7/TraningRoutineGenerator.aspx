<%@ Page Title="" Language="C#" MasterPageFile="~/Excrcise.Master" AutoEventWireup="true" CodeBehind="TraningRoutineGenerator.aspx.cs" Inherits="WebApplication7.TraningRoutineGenerator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .DiffHelpImg{

        }
        .DiffHelpTxt{
            visibility:hidden;
        }
        img.DiffHelpImg:hover + .DiffHelpTxt {
            visibility: visible;
        }
        .KindHelpImg{

        }
        .KindHelpTxt{
            visibility:hidden;
        }
        img.KindHelpImg:hover + .KindHelpTxt {
            visibility: visible;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="RegesterDIv" class="Centerd NiceBacground" style="width:50%;">
        <h3>How much time the workout should be?</h3>
        <asp:RadioButtonList ID="WorkoutsLengthRBL" runat="server" CssClass="radioButtonList ListItemCenterd" RepeatDirection="Horizontal">
            <asp:ListItem Value="1">45 min</asp:ListItem>
            <asp:ListItem Value="2" selected="True" >60-90 min</asp:ListItem>
            <asp:ListItem Value="3" >90+ min</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <br />
        <h3>What the diffuclty of the workout should be?</h3>
        <asp:DropDownList ID="difficultyDDL" runat="server">            
        </asp:DropDownList>
        <img src="https://img.icons8.com/android/24/000000/info.png" id="DiffHelpImg" class="DiffHelpImg"/>
        <p id="HelpTxt" class="DiffHelpTxt">
            for example:
            <br />
            1-wall pushup
            <br />
            2-knee pushup
            <br />
            3-regular pushups
            <br />
            4-decline pushups
            <br />
            5-one arm pushup
            <br />
            6-full planch pushups
            <br />
            *mostly releveant to body workouts
        </p>
        <h3>Type of workout:</h3>
        <asp:RadioButtonList ID="TypeOfWorkoutRBL" runat="server" CssClass="radioButtonList ListItemCenterd" RepeatDirection="Horizontal">
            <asp:ListItem Value="0" selected="True">gym assisted</asp:ListItem>
            <asp:ListItem Value="1" >body weight</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <br />
        <br />
        <br />
        <h3>What Kind of split you would like?</h3>
        <asp:RadioButtonList ID="WorkoutKindRBL" runat="server" CssClass="radioButtonList ListItemCenterd" RepeatDirection="Horizontal">
            <asp:ListItem Value="1" selected="True">full body</asp:ListItem>
            <asp:ListItem Value="2" >ab</asp:ListItem>
            <asp:ListItem Value="3" >abc</asp:ListItem>
        </asp:RadioButtonList>  
        <br />
        <img src="https://img.icons8.com/android/24/000000/info.png" id="KindHelpImg" class="KindHelpImg"/>
        <p class="KindHelpTxt">
            full body- each workout will work your whole body
            <br />
            ab- two diffrent workouts will work on diffrent kind of muscle groups
            <br />
            abc-three diffrent workouts will work on diffrent kind of muscle groups
        </p>
        <h3>What the focus of your workouts:</h3>
        <asp:RadioButtonList ID="WorkoutsFoucusRBL" runat="server" CssClass="radioButtonList ListItemCenterd" RepeatDirection="Horizontal">
            <asp:ListItem Value="MG" selected="True">muscle building</asp:ListItem>
            <asp:ListItem Value="SG" >strength</asp:ListItem>
            
        </asp:RadioButtonList>
        <asp:Button ID="CreateRoutine" runat="server" Text="Create me a workout" OnClick="CreateWotkout" />
    </div>
    
    <asp:Label ID="LblTable" runat="server" Text="Label"></asp:Label>
    Icons made by <a href="https://www.flaticon.com/authors/pixelmeetup" title="Pixelmeetup">Pixelmeetup</a> from <a href="https://www.flaticon.com/" title="Flaticon"> www.flaticon.com</a>
</asp:Content>
