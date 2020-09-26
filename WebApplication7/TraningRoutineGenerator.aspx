<%@ Page Title="" Language="C#" MasterPageFile="~/Excrcise.Master" AutoEventWireup="true" CodeBehind="TraningRoutineGenerator.aspx.cs" Inherits="WebApplication7.TraningRoutineGenerator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .DiffHelpImg {
        }

        .DiffHelpTxt {
            visibility: hidden;
        }

        img.DiffHelpImg:hover + .DiffHelpTxt {
            visibility: visible;
        }

        .KindHelpImg {
        }

        .KindHelpTxt {
            visibility: hidden;
        }

        img.KindHelpImg:hover + .KindHelpTxt {
            visibility: visible;
        }

        .auto-style4 {
            width: 114px;
            height: 230px;
        }

        .MuscleLight :hover {
            background-color: lawngreen;
        }

        .MuscleGroups {
        }

        .auto-style5 {
            width: 46px;
            height: 276px;
        }

        .auto-style6 {
            width: 125px;
            height: 306px;
        }

        .auto-style7 {
            width: 53px;
            height: 271px;
        }

        .auto-style8 {
            width: 80px;
            height: 109px;
        }
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="RegesterDIv" class="Centerd NiceBacground" style="width: 50%;">
        <h3>How much time the workout should be?</h3>
        <asp:RadioButtonList ID="WorkoutsLengthRBL" runat="server" CssClass="radioButtonList ListItemCenterd" RepeatDirection="Horizontal">
            <asp:ListItem Value="0">45 min</asp:ListItem>
            <asp:ListItem Value="1" Selected="True">60-90 min</asp:ListItem>
            <asp:ListItem Value="2">90+ min</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <br />
        <h3>What the difficulty of the workout should be?</h3>
        <asp:DropDownList ID="difficultyDDL" runat="server">
            <asp:ListItem Value="1">Beginner</asp:ListItem>
            <asp:ListItem Value="2" Selected="True">experienced</asp:ListItem>
        </asp:DropDownList>
        <img src="https://img.icons8.com/android/24/000000/info.png" id="DiffHelpImg" class="DiffHelpImg" />
        <p id="HelpTxt" class="DiffHelpTxt">
            Beginner- for people who don't want complex movements in their program like dead-lifts and squats
            experienced- for people who want complex movements in their program my also include more advanced tactics like super sets and drop sets
        </p>
        <br />
        <br />
        <br />
        <br />
        <h3>What Kind of split you would like?</h3>
        <asp:RadioButtonList ID="WorkoutKindRBL" runat="server" CssClass="radioButtonList ListItemCenterd" RepeatDirection="Horizontal">
            <asp:ListItem Value="0" Selected="True">full body</asp:ListItem>
            <asp:ListItem Value="1">ab</asp:ListItem>
            <asp:ListItem Value="2">abc</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <img src="https://img.icons8.com/android/24/000000/info.png" id="KindHelpImg" class="KindHelpImg" />
        <p class="KindHelpTxt">
            full body- each workout will work your whole body
            <br />
            ab- two different workouts will work on different kind of muscle groups
            <br />
            abc-three different workouts will work on different kind of muscle groups
        </p>
        <h3>What the focus of your workouts:</h3>
        <asp:RadioButtonList ID="WorkoutsFoucusRBL" runat="server" CssClass="radioButtonList ListItemCenterd" RepeatDirection="Horizontal">
            <asp:ListItem Value="MG" Selected="True">muscle building</asp:ListItem>
            <asp:ListItem Value="SG">strength</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Button ID="CreateRoutine" runat="server" Text="Create me a workout" OnClientClick="showLoading()" OnClick="CreateWotkout" />
    </div>
    <div style="margin-left: auto; margin-right: auto; text-align: center;">
        <asp:Label ID="LblTable" runat="server" Text="" ClientIDMode="Static" CssClass="Centerd"></asp:Label>
    </div>
    <script type="text/javascript">
        var w;
        function startLoadingAnumationWorker(table) {
            var periodNum = 0;
            if (typeof (Worker) !== "undefined") {
                if (typeof (w) == "undefined") {
                    w = new Worker("animationTiming.js");
                }
                w.onmessage = function (event) {
                    if (periodNum > 6) {
                        table.innerHTML = "LOADING";
                    } else {
                        table.innerHTML += ".";
                    }
                    periodNum += 1;
                };
            }
            else {
                document.getElementById("result").innerHTML = "Sorry, your browser does not support Web Workers...";
            }
        }
        function showLoading() {
            var table = document.getElementById("LblTable");
            table.style.fontSize = "70px";
            table.style.textAlign = "center";
            table.style.margin = "auto";
            table.innerHTML = "LOADING";
            startLoadingAnumationWorker(table);
        }

        function stopAnimation() {
            w.terminate();
            w = undefined;
        }
    </script>
</asp:Content>