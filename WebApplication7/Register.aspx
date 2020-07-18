<%@ Page Title="" Language="C#" MasterPageFile="~/Excrcise.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebApplication7.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <style>
        td{
            background-color:transparent;

        }
        .passwordError
        {
           font-size:x-small;
           text-align:right;
           direction:rtl;
           text-decoration:double;
        }
    .requirment-bad {
        font-size:20px;
  color: darkred;
  font-weight:bold;
}

.requirment-ok {
    font-size:20px;
  color: green;
  font-weight:bold;
}
.Invisible{
    visibility:hidden;
}
.visible{
    visibility:visible;
}

</style>
<script src="Scripts/jquery-2.1.1.min.js" type="text/javascript"></script>
<script src="Scripts/jquery.maskedinput.js" type="text/javascript"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
<script type="text/javascript">
    function ValidatePassword() {
              var rules = [{
      Pattern: "[A-Z]",
      Name: "UpperCase"
    },
    {
      Pattern: "[a-z]",
      Name: "LowerCase"
    },
    {
      Pattern: "[0-9]",
      Name: "Numbers"
    },
    {
      Pattern: "[!@@#$%^&*]",
      Name: "Symbols"
    }
        ];
        var strength = 0; 
        var password = $("#<%=TxtPassword.ClientID %>").val();
        if (password.length > 4) {
            $("#Length").removeClass("requirment-bad");
            $("#Length").addClass("requirment-ok");
            strength = strength + 1;
        }
        else {
        $("#Length").addClass("requirment-bad");
        $("#Length").removeClass("requirment-ok");
        }
        
        for (var i = 0; i < rules.length; i++) {
            var pattern = rules[i].Pattern;
            var NamePattern = rules[i].Name;
            if (new RegExp(pattern).test(password)) {
                $("#" + NamePattern).removeClass("requirment-bad");
                $("#" + NamePattern).addClass("requirment-ok");
                strength = strength + 1;
            }

            else {
                $("#" + NamePattern).addClass("requirment-bad");
                $("#" + NamePattern).removeClass("requirment-ok");
            }
        }
        if (strength > 2) {
            $("#strength").removeClass("requirment-bad");
            $("#strength").addClass("requirment-ok");
            $("#<%=BtnRegister.ClientID %>").addClass("visible");
            $("#<%=BtnRegister.ClientID %>").removeClass("Invisible");
        }
        else {
            $("#strength").addClass("requirment-bad");
            $("#strength").removeClass("requirment-ok");
            $("#<%=BtnRegister.ClientID %>").removeClass("visible");
            $("#<%=BtnRegister.ClientID %>").addClass("Invisible");
        }
              
    }
$(document).ready(function(){
	$("#<%=TxtPassword.ClientID %>").on({
  	        keyup: function(){
    		ValidatePassword();
            }
     });
    });
    function check()
        {
            var username = document.getElementById("<%=TxtUsername.ClientID%>").value;
            var password = document.getElementById("<%=TxtPassword.ClientID%>").value;
        var email = document.getElementById("<%=TxtEmail.ClientID%>").value;
            if (username == null || username == "")
            {
                alert("you must enter username");
                return false;
            }
            if (username.length <4 || username.length > 16)
            {
                alert( "the username length has to be longer then 4 chars and shorter then 16");
                return false;
            }
            if (email == null || email == "")
            {
                alert("You must enter email");
                return false;
        }
        if (email.value[0] == '@' || pass.value[-1] == '@') {
                alert('email cant start or end with @');
                return false;
        }
            return true;
        }

</script>
<div style="font-size:medium;text-align:center; color:black; width:100%;height:35%; margin:auto;" dir="rtl" onload="checkPassword()">
    <div>
	<h1 style="color:white">הרשמה</h1>
	<table style="margin:auto;text-align:center;" class="NiceBacground">
		<tr>
			<td>
				<asp:Label ID="LblUsername" runat="server" Text="שם משתמש:" style="font-size:30px; text-align:right; color:black; "></asp:Label>
			</td>
			<td style=" direction:rtl;">
				<asp:TextBox ID="TxtUsername" runat="server"></asp:TextBox>
			</td>
			
		</tr>
		<tr>
			<td>
				<asp:Label ID="LblPassword" runat="server" Text="סיסמא:" style="font-size:30px; text-align:right; color:black; "></asp:Label>
			</td>
			<td style=" direction:rtl;">
				<asp:TextBox ID="TxtPassword" runat="server" TextMode="Password"></asp:TextBox>

                <br />
                <label id="LblNotGoodPass" class="passwordError" onload="checkPassword()" ></label>
                <asp:Label ID="LblNotGoodPass1" runat="server" Text="" CssClass="passwordError"></asp:Label>
			</td>
			 <td>
                <div id="Length" class="requirment-bad">Must be at least 5 charcters</div>
                    <div id="UpperCase" class="requirment-bad">have atleast 1 upper case character</div>
                    <div id="LowerCase" class="requirment-bad">have atleast 1 lower case character</div>
                    <div id="Numbers" class="requirment-bad">have atleast 1 numeric character</div>
                    <div id="Symbols" class="requirment-bad">have atleast 1 special character</div>
                    <div id="strength" class="requirment-bad">Must have 3 of this parameters</div>
            </td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="LblEmail" runat="server" Text="מייל:" style="font-size:30px; text-align:right; color:black; "></asp:Label>
			</td>
			<td style=" direction:rtl;">
				<asp:TextBox ID="TxtEmail" runat="server" TextMode="Email"></asp:TextBox>
			</td>
			
		</tr>
		<tr>
			<td colspan ="2">
				<asp:Label ID="LblNotification" runat="server" Text=""></asp:Label>
			</td>
		</tr>
	</table>
        </div>
		<div style="font-size:large;text-align:center; width:35%; margin:auto" >
        <asp:Button ID="BtnRegister" runat="server" Text="הרשמה" Height="42px" Width="100%" style="font-size:20px;" OnClick="BtnRegister_Click" CssClass="Invisible"/>
		</div>

	
</div>
</asp:Content>
