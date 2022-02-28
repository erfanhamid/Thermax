using BEEERP.Models.Bridge;
using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BEEERP.Controllers.Account
{
    public partial class ChartOfAccount : System.Web.UI.Page
    {
        BEEContext context = new BEEContext();
        protected void Page_Init(object sender, EventArgs e)
        {
            var coa = context.ChartOfAccount.ToList();
            if(!IsPostBack)
            {
                coaTree.Nodes.Add(GenerateTopicsTreeRcursively(coa));
                var listOfgroup = context.ChartOfAccount.Where(x => x.Type == "g"||x.Type=="f").ToList().Select(x=>new { x.Id,x.Name}).ToList();
                listOfgroup.Insert(0, new { Id = 0, Name = "" });
                groupDropDown.DataSource = listOfgroup;
                groupDropDown.DataValueField = "Id";
                groupDropDown.DataTextField = "Name";
                groupDropDown.DataBind();
            }
           
        }

        public static TreeNode GenerateTopicsTreeRcursively(List<BEEERP.Models.SalesModule.ChartOfAccount> chartOfAccounts)
        {
            var root = new TreeNode("Chart Of Account","0");
            AddNodesRecursively(root, chartOfAccounts,0);
            return root;
        }


        public static TreeNode AddNodesRecursively(TreeNode rootnode, List<BEEERP.Models.SalesModule.ChartOfAccount> topics,int parentId)
        {
            int ID = parentId;
            var current = topics.Where(o => o.ParentId == ID);
            foreach (var item in current)
            {
                var child = new TreeNode(item.Name, item.Id.ToString());
                if(item.Type=="f"||item.Type=="g")
                {
                    child.ImageUrl = "~/Image/G.png";
                }
                else
                {
                    child.ImageUrl = "~/Image/L.png";
                }
                rootnode.ChildNodes.Add(child);

                AddNodesRecursively(child, topics,item.Id);
            }
            return rootnode;
        }

        protected void coaTree_SelectedNodeChanged(object sender, EventArgs e)
        {
            var value = coaTree.SelectedValue;
            opDateLevel.Visible = false;
            opLevel.Visible = false;
            opBalanceText.Visible = false;
            opDateText.Visible = false;
            subGroupOrItemLabel.Visible = false;
            subGroupOrLedgerDrop.Visible = false;
            subAccNameLabel.Visible = false;
            subAccNameText.Visible = false;
            saveLedgerAccount.Visible = false;
            if (value != null)
            {
                int coaId = Convert.ToInt32(value);
                var coa = context.ChartOfAccount.FirstOrDefault(x => x.Id == coaId);
                if (coa.Type == "l")
                {
                    //opText.Text=coa.
                    accName.Text = coa.Name;
                    accIdText.Text = coa.Id.ToString();
                    groupLedgerDrp.SelectedValue = "l";
                    groupDropDown.SelectedValue = coa.ParentId.ToString();
                    accType.Text = coa.Acctype;

                    var startDate = context.CompanySetup.FirstOrDefault().StartDate.ToString("dd-MM-yyyy");
                    opDateText.Text = startDate;
                    //var transection = context.Transection.Where(x => x.CAID == coa.Id && x.DocType == "OBE").FirstOrDefault();
                    var transection = context.Transection.Where(x => x.DocID == coa.Id && x.DocType == "AOB").FirstOrDefault();
                    if(transection != null)
                    {
                        opBalanceText.Text = transection.Amount.ToString();
                    }
                    //subGroupOrItemLabel.Visible = true;
                    //subGroupOrLedgerDrop.Visible = true;
                    //subAccNameLabel.Visible = true;
                    //subAccNameText.Visible = true;
                    saveLedgerAccount.Visible = false;
                    opDateLevel.Visible = false;
                    opLevel.Visible = false;
                    opBalanceText.Visible = false;
                    opDateText.Visible = false;
                    var parent = context.ChartOfAccount.FirstOrDefault(x => x.Id == coaId);
                    if (parent.RootAccountType != "ex" || parent.RootAccountType != "in")
                    {
                        opDateLevel.Visible = true;
                        opLevel.Visible = true;
                        opBalanceText.Visible = true;
                        opDateText.Visible = true;
                    }

                }
                else
                {
                    accName.Text = coa.Name;
                    accIdText.Text = coa.Id.ToString();
                    groupLedgerDrp.SelectedValue = "g";
                    groupDropDown.SelectedValue = coa.ParentId.ToString();
                    accType.Text = coa.Acctype;
                    opDateLevel.Visible = false;
                    opLevel.Visible = false;
                    opBalanceText.Visible = false;
                    opDateText.Visible = false;

                }

            }
        }

        protected void updateBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(accIdText.Text))
            {
                var accId = Convert.ToInt32(accIdText.Text);

                BEEERP.Models.SalesModule.ChartOfAccount coa = context.ChartOfAccount.FirstOrDefault(x => x.Id == accId);
                coa.Name = accName.Text;
                coa.ParentId = Convert.ToInt32(groupDropDown.SelectedValue);
                //coa.Type = accType.Text.ToLower();
                var parentAcc = context.ChartOfAccount.FirstOrDefault(x => x.Id == coa.ParentId);
                var opAmountText = opBalanceText.Text;
                decimal opAmount = 0.0m;
                if (!string.IsNullOrEmpty(opAmountText))
                {
                    opAmount = Convert.ToDecimal(opBalanceText.Text);
                }
                coa.RootAccountType = parentAcc.RootAccountType;
                if (/*opAmount >= 0 && (*/coa.RootAccountType != "in" ||coa.RootAccountType!="ex"/*)*/)
                {
                    if(groupLedgerDrp.SelectedValue.ToString().ToLower()=="l")
                    {
                        coa.InitialBalanceTransectionId = AccountModuleBridge.InsertUpdateFromChartOfAccount(ref context, coa, opAmount);
                    }
                    else
                    {
                        coa.InitialBalanceTransectionId = 0;
                    }

                }
                else
                {
                    coa.InitialBalanceTransectionId = 0;
                }
                context.Entry<BEEERP.Models.SalesModule.ChartOfAccount>(coa).State = System.Data.Entity.EntityState.Modified; ;
                context.SaveChanges();
                Response.Redirect(Request.RawUrl);
            }

        }

        protected void addBtn_Click(object sender, EventArgs e)
        {
            if(accIdText.Text==""||groupLedgerDrp.SelectedValue.ToString()!="g")
            {
                Response.Write("<script>alert('You need to select a group accounts to add ledger or accounts group.')</script>");
            }
            else
            {
                if (groupLedgerDrp.SelectedValue == "l")
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You Cannot add Group or Ledger in Ledger Accounts.')", true);
                    Response.Write("<script>alert('You Cannot add Group or Ledger Accounts Under Ledger Accounts.')</script>");
                }
                else
                {
                   
                    subGroupOrItemLabel.Visible = true;
                    subGroupOrLedgerDrop.Visible = true;
                    subAccNameLabel.Visible = true;
                    subAccNameText.Visible = true;
                    saveLedgerAccount.Visible = true;
                    subGroupOrLedgerDrop.ClearSelection();
                    subGroupOrLedgerDrop.Items.FindByValue("G").Selected = true;
                    //subGroupOrLedgerDrop. = "g";
                }
            }
        }

        protected void subGroupOrLedgerDrop_TextChanged(object sender, EventArgs e)
        {
            if(subGroupOrLedgerDrop.SelectedValue.ToLower()=="g")
            {
                opDateLevel.Visible = false;
                opLevel.Visible = false;
                opBalanceText.Visible = false;
                opDateText.Visible = false;
            }
            else
            {
                int coaId = Convert.ToInt32(accIdText.Text);
                var parent = context.ChartOfAccount.FirstOrDefault(x => x.Id == coaId);
                if (parent.RootAccountType == "ex" || parent.RootAccountType == "in")
                {
                    opDateLevel.Visible = false;
                    opLevel.Visible = false;
                    opBalanceText.Visible = false;
                    opDateText.Visible = false;
                }
                else
                {
                    opDateLevel.Visible = true;
                    opLevel.Visible = true;
                    opBalanceText.Visible = true;
                    var compStartDate = context.CompanySetup.FirstOrDefault().StartDate.ToString("dd-MM-yyyy");
                    opDateText.Text = compStartDate;
                    opDateText.Visible = true;
                }
                
            }
        }

        protected void saveLedgerAccount_Click(object sender, EventArgs e)
        {
            BEEERP.Models.SalesModule.ChartOfAccount chartOfAcc = new Models.SalesModule.ChartOfAccount();
            int coaId = 1;
            var countChartOfAcc = context.ChartOfAccount.Count();
            var isAccNameExist = context.ChartOfAccount.FirstOrDefault(x => x.Name.ToLower().Trim() == subAccNameText.Text.ToLower().Trim());
            if (isAccNameExist == null)
            {
                if (countChartOfAcc > 0)
                {
                    var maxId = context.ChartOfAccount.Max(x => x.Id);
                    coaId = maxId + 1;
                }
                if (subGroupOrLedgerDrop.Text.ToLower() == "l")
                {
                    int parentId = Convert.ToInt32(accIdText.Text);
                    var parent = context.ChartOfAccount.FirstOrDefault(x => x.Id == parentId);
                    chartOfAcc.Name = subAccNameText.Text;
                    chartOfAcc.ParentId = parentId;
                    chartOfAcc.Type = subGroupOrLedgerDrop.SelectedValue.ToString().ToLower();
                    chartOfAcc.Acctype = parent.Acctype;
                    chartOfAcc.RootAccountType = parent.RootAccountType;
                    chartOfAcc.Id = coaId;
                    decimal opAMount = 0.0m;
                    if (!string.IsNullOrEmpty(opBalanceText.Text))
                    {
                        opAMount = Convert.ToDecimal(opBalanceText.Text);
                    }
                    chartOfAcc.InitialBalanceTransectionId = AccountModuleBridge.InsertUpdateFromChartOfAccount(ref context, chartOfAcc, opAMount);
                    context.ChartOfAccount.Add(chartOfAcc);
                    context.SaveChanges();
                    Response.Write("<script>alert('Save Successfull.')</script>");
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    int parentId = Convert.ToInt32(accIdText.Text);

                    var parent = context.ChartOfAccount.FirstOrDefault(x => x.Id == parentId);
                    chartOfAcc.Id = coaId;
                    chartOfAcc.Name = subAccNameText.Text;
                    chartOfAcc.ParentId = parentId;
                    chartOfAcc.Type = subGroupOrLedgerDrop.SelectedValue.ToString().ToLower();
                    chartOfAcc.Acctype = parent.Acctype;
                    chartOfAcc.RootAccountType = parent.RootAccountType;
                    chartOfAcc.InitialBalanceTransectionId = 0;
                    context.ChartOfAccount.Add(chartOfAcc);
                    context.SaveChanges();
                    Response.Write("<script>alert('Save Successfull.')</script>");
                    Response.Redirect(Request.RawUrl);
                }
            }
            else
            {
                Response.Write("<script>alert('This account Name is already exist.')</script>");
            }
            
            
        }

        protected void deleteBtn_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(accIdText.Text);
            var coa = context.ChartOfAccount.FirstOrDefault(x => x.Id == id);
            if(coa!=null)
            {
                var isChildExist = context.ChartOfAccount.Count(x => x.ParentId == coa.Id);
                var transections = context.Transection.Count(x => x.DocID == id && x.DocType != "AOB");
                var transExist = context.Transection.Where(x => x.CAID == id).Count();
                // old code
                //if (transections <= 0 && transExist <= 0)
                    
                if (transExist <= 1)
                    
                {
                    if (isChildExist == 0)
                    {
                        context.ChartOfAccount.Remove(coa);
                        AccountModuleBridge.DeleteFromChartOfAccount(ref context, coa);
                        context.SaveChanges();
                        Response.Redirect(Request.RawUrl);
                    }
                    else
                    {
                        Response.Write("<script>alert('This Head Account has Child account. Please child account delete first.')</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('This account has Transaction.You need to remove all transactions related to this account and then delete.')</script>");
                }
                
               
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //public void GetChildNode(List<BEEERP.Models.SalesModule.ChartOfAccount> chartOfAccounts, List<BEEERP.Models.SalesModule.ChartOfAccount> childrenItem, int parentId,TreeNode node)
        //{
        //    foreach (var item in chartOfAccounts.Where(x => x.ParentId == parentId))
        //    {
        //        TreeNode treeNode = new TreeNode();
        //        treeNode.Text = item.Name;
        //        var childCoa = context.ChartOfAccount.Where(x => x.ParentId == item.Id).ToList();
        //        if (childCoa.Count > 0)
        //        {
        //            GetChildNode(chartOfAccounts, childCoa, item.Id);
        //        }
        //        coaTree.Nodes.Add(treeNode);
        //    }

        //}
    }
}