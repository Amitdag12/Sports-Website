<%@ Page Title="" Language="C#" MasterPageFile="~/Excrcise.Master" AutoEventWireup="true" CodeBehind="FrontPage.aspx.cs" Inherits="WebApplication7.FrontPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .banner1{
            width:100%;
            height:30%;
        }
  #bg {
  top: 0; 
  height:30%;
  left: 0; 
  z-index: -1;
  /* Preserve aspet ratio */
  min-width: 100%;
  min-height: 30%;
}
  .Text{
      color:rgba(233, 235, 226, 0.91);
  }
  #banner2{
      height:40%;
      width:100%;
      position:relative; 
       z-index: -1;
  }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
    <td class="banner1" >
        <img src="Photos/man-carrying-barbel-791763.png"  id="bg" />
    </td>
            </tr>
        <tr>
    <td style="margin:auto;height:40%;width:100%">
        <img src="Photos/woman-doing-push-ups-2780762.png" id="banner2"  />
    </td>
            </tr>
        </table>
</asp:Content>