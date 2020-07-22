<%@ Page Title="" Language="C#" MasterPageFile="~/Excrcise.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication7.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

  <div style="text-align:center;margin:auto;font-size:40px;background-color: rgba(128, 128, 128, 0.74);padding: 20px;
    border-radius: 25px; width:40%;"">
   <table style="margin:auto;text-align:center; ">
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
			</td>
           
			
		</tr>
       <tr>
           <td colspan ="2">
				<asp:Label ID="LblNotification" runat="server" Text=""></asp:Label>
			</td>
       </tr>
</table>
  </div>
     <div style="font-size:large;text-align:center; width:30%; margin:auto" >
        <asp:Button ID="BtnRegister" runat="server" Text="כניסה" Height="42px" Width="100%" style="font-size:20px;" OnClick="BtnRegister_Click" CssClass="Invisible" />
	</div>
  <div  style="text-align:center;margin:auto;font-size:34px;">
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
	<a href="Register.aspx">הרשמה</a>
  </div>

</asp:Content>
