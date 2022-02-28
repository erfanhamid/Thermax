<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChartOfAccount.aspx.cs" Inherits="BEEERP.Controllers.Account.ChartOfAccount" MasterPageFile="" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .button {
            padding-right: 3px;
            padding-left: 3px;
            color: #F0F0F0;
            box-sizing: border-box;
        }

        .left-panel {
            /*padding-left: 0%;
            padding-right: 5%;*/
            /*width: 20%;*/
            height: 100%;
            float: left;
            border-left: 2px;
            background-color: #222d32;
        }

        #coaTree {
            padding-left: 2%;
            padding-top: 5%;
            /*width: 288px;*/
        }

        .right-panel {
            /*padding-left: 3%;
            padding-right: 3%;*/
            background-color: #cccccc;
            float: left;
            
        }

        #Panel3 {
            width: 100%;
            background-color: red;
        }

        #Panel2 {
            padding-top: 3%;
            padding-left: 3%;
        }
       
        @media only screen and (min-width: 1080px) {
            .right-panel {
                width: 99%;
                margin-left: 0px;
            }

            .left-panel {
                width: 99%;
            }

        }
        @media only screen and (min-width: 1100px) {
            .right-panel {
                width: 60%;
                margin-left: 28px;
            }

            .left-panel {
                width: 35.5%;
            }

        }
        @media only screen and (min-width: 1280px) {
            .right-panel {
                width: 61%;
                margin-left: 28px;
            }

            .left-panel {
                width: 35%;
            }

        }
        @media only screen and (min-width: 1366px) {
            .right-panel {
                width: 61.25%;
                margin-left: 28px;
            }

            .left-panel {
                width: 35%;
            }
        }
         @media only screen and (min-width: 1920px) {
            .right-panel {
                width: 62.3%;
                margin-left: 28px;
            }

            .left-panel {
                width: 35%;
            }
        }
    </style>
</head>
<body style="height: 100%; font-family: Helvetica Neue,Helvetica,Arial,sans-serif; font-size: 14px; width: 80%; background-color: #F0F0F0; padding-left: 10%; padding-right: 5%;">
    <div style="background-color: #3c8dbc; height: 50px; width: 100%; color: #3c8dbc; padding: 0%; text-align: center;">
        <span style="color: #fff; font-size: 20px; line-height: 50px; padding-left: 2%;"><b>Chart Of Account</b></span>
    </div>
    <form id="form1" runat="server" style="padding-top: .5%;">
        <asp:Panel ID="Panel1" runat="server">
            <asp:Panel ID="Panel3" runat="server" BorderColor="White" BorderStyle="None" Style="margin-top: 0px">
                <asp:Panel ID="Panel4" runat="server" CssClass="left-panel" BorderColor="#3C8DBC" BorderStyle="Solid">
                    <asp:TreeView ID="coaTree" runat="server" ForeColor="White" OnSelectedNodeChanged="coaTree_SelectedNodeChanged" ExpandDepth="1">
                    </asp:TreeView>
                </asp:Panel>
                <asp:Panel ID="Panel5" runat="server" CssClass="right-panel" BorderColor="#3C8DBC" BorderStyle="Solid">
                    <asp:Panel ID="Panel2" runat="server" BorderColor="#3366CC">
                        &nbsp;
                        <br />
                        <asp:Label ID="Label5" runat="server" Font-Bold="true" Style="margin-bottom: 0px" Text="Account ID" Width="100px"></asp:Label>
                        <asp:TextBox ID="accIdText" runat="server" Enabled="False" Font-Bold="True" Height="30px" Width="50%"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="Label1" runat="server" Font-Bold="true" Style="margin-bottom: 0px" Text="Account Name" Width="100px"></asp:Label>
                        <asp:TextBox ID="accName" runat="server" Font-Bold="True" Height="30px" Width="50%"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Group/Ledger" Width="100px"></asp:Label>
                        <asp:DropDownList ID="groupLedgerDrp" runat="server" Enabled="False" Font-Bold="True" Height="31px" Width="50.5%">
                            <asp:ListItem Value="g">Group</asp:ListItem>
                            <asp:ListItem Value="l">Ledger</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <br />
                        <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="Group Name" Width="100px"></asp:Label>
                        <asp:DropDownList ID="groupDropDown" runat="server" Enabled="False" Font-Bold="True" Height="31px" Width="50.5%">
                        </asp:DropDownList>
                        <br />
                        <br />
                        <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="Account type" Width="100px"></asp:Label>
                        <asp:TextBox ID="accType" runat="server" Enabled="False" Font-Bold="True" Height="30px" Width="50%"></asp:TextBox>
                        <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="addBtn" runat="server" Font-Bold="True" Height="35px" OnClick="addBtn_Click" Text="Add" Width="100px" />
                        <asp:Button ID="updateBtn" runat="server" Font-Bold="True" Height="35px" OnClick="updateBtn_Click" Text="Update" Width="100px" />
                        <asp:Button ID="deleteBtn" runat="server" Font-Bold="True" Height="35px" OnClick="deleteBtn_Click" Text="Delete" Width="100px" />
                        <br />
                        <br />
                        <asp:Label ID="subGroupOrItemLabel" runat="server" Font-Bold="true" Text="Group/Ledger" Visible="False" Width="100px"></asp:Label>
                        <asp:DropDownList ID="subGroupOrLedgerDrop" runat="server" AutoPostBack="True" Font-Bold="True" Height="31px" OnTextChanged="subGroupOrLedgerDrop_TextChanged" Visible="False" Width="493px">
                            <asp:ListItem Value="G">Group</asp:ListItem>
                            <asp:ListItem Value="L">Ledger</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <br />
                        <asp:Label ID="subAccNameLabel" runat="server" Font-Bold="true" Style="margin-bottom: 0px" Text="Account Name" Visible="False" Width="100px"></asp:Label>
                        <asp:TextBox ID="subAccNameText" runat="server" Font-Bold="True" Height="30px" Visible="False" Width="50%"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="opLevel" runat="server" Font-Bold="true" Style="margin-bottom: 0px" Text="Opening Balance" Visible="False" Width="100px"></asp:Label>
                        <asp:TextBox ID="opBalanceText" runat="server" Font-Bold="True" Height="30px" Visible="False" Width="50%"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="opDateLevel" runat="server" Font-Bold="true" Style="margin-bottom: 0px" Text="Date" Visible="False" Width="100px"></asp:Label>
                        <asp:TextBox ID="opDateText" runat="server" Enabled="False" Font-Bold="True" Height="30px" Visible="False" Width="50%"></asp:TextBox>
                        <br />
                        <br />

                        <asp:Button ID="saveLedgerAccount" runat="server" Font-Bold="True" Height="35px" OnClick="saveLedgerAccount_Click" Text="Save Account" Visible="False" Width="140px" />
                        <br />
                        <br />
                        <p style="text-align: right;">
                            <a href="/home/Index" class="button" style="font-weight: 700; color: black;">Go to Home</a>

                        </p>
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>


        </asp:Panel>
    </form>
</body>
</html>
