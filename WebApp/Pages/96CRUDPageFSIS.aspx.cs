using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DBSystem.BLL;
using DBSystem.ENTITIES;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;

namespace WebApp.Pages
{
    public partial class _96CRUDPageFSIS : System.Web.UI.Page
    {
        static string pagenum = "";
        static string pid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                pagenum = Request.QueryString["page"];
                pid = Request.QueryString["pid"];

                BindTeamList();
                BindGuardianList();
                if (string.IsNullOrEmpty(pid))
                {
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    PlayerController sysmgr = new PlayerController();
                    Player info = null;
                    info = sysmgr.FindByPlayerID(int.Parse(pid));
                    if (info == null)
                    {
                        ShowMessage("Record is not in Database.", "alert alert-info");
                        Clear(sender, e);
                    }
                    else
                    {
                        ID.Text = info.PlayerID.ToString(); //NOT NULL in Database
                        FName.Text = info.FirstName; //NOT NULL in Database
                        LName.Text = info.LastName; //NOT NULL in Database
                        Age.Text = info.Age.ToString();
                        Gender.Text = info.Gender;
                        HCard.Text = info.AlbertaHealthCareNumber;
                        MAlert.Text = info.MedicalAlertDetails;

                        if (info.TeamID.HasValue) //NULL in Database
                        {
                            TeamList.SelectedValue = info.TeamID.ToString();
                        }
                        else
                        {
                            TeamList.SelectedValue = "0";
                        }
                        if (info.GuardianID.HasValue) //NULL in Database
                        {
                            GuardianList.SelectedValue = info.GuardianID.ToString();
                        }
                        else
                        {
                            GuardianList.SelectedValue = "0";
                        }
                    }
                }
            }
        }
        protected Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }
        protected void ShowMessage(string message, string cssclass)
        {
            MessageLabel.Attributes.Add("class", cssclass);
            MessageLabel.InnerHtml = message;
        }
        protected void BindTeamList()
        {
            try
            {
                TeamController sysmgr = new TeamController();
                List<Team> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.TeamName.CompareTo(y.TeamName));
                TeamList.DataSource = info;
                TeamList.DataTextField = nameof(Team.TeamName);
                TeamList.DataValueField = nameof(Team.TeamID);
                TeamList.DataBind();
                ListItem myitem = new ListItem();
                myitem.Value = "0";
                myitem.Text = "select...";
                TeamList.Items.Insert(0, myitem);
                //CategoryList.Items.Insert(0, "select...");

            }
            catch (Exception ex)
            {
                ShowMessage(GetInnerException(ex).ToString(), "alert alert-danger");
            }
        }
        protected void BindGuardianList()
        {
            try
            {
                GuardianController sysmgr = new GuardianController();
                List<Guardian> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.GuardianFullName.CompareTo(y.GuardianFullName));
                GuardianList.DataSource = info;
                GuardianList.DataTextField = nameof(Guardian.GuardianFullName);
                GuardianList.DataValueField = nameof(Guardian.GuardianID);
                GuardianList.DataBind();
                ListItem myitem = new ListItem();
                myitem.Value = "0";
                myitem.Text = "select...";
                GuardianList.Items.Insert(0, myitem);
                //SupplierList.Items.Insert(0, "select...");
            }
            catch (Exception ex)
            {
                ShowMessage(GetInnerException(ex).ToString(), "alert alert-danger");
            }
        }
        protected bool Validation(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FName.Text))
            {
                ShowMessage("First Name is required", "alert alert-info");
                return false;
            }
            else if (string.IsNullOrEmpty(LName.Text))
            {
                ShowMessage("Last Name is required", "alert alert-info");
                return false;
            }
            else if (TeamList.SelectedValue == "0")
            {
                ShowMessage("Team is required", "alert alert-info");
                return false;
            }
            else if (GuardianList.SelectedValue == "0")
            {
                ShowMessage("Guardian is required", "alert alert-info");
                return false;
            }
            else if (string.IsNullOrEmpty(Age.Text))
            {
                ShowMessage("Age is required", "alert alert-info");
                return false;
            }
            else if (string.IsNullOrEmpty(Gender.Text))
            {
                ShowMessage("Gender is required", "alert alert-info");
                return false;
            }
            return true;
        }
        protected void Back_Click(object sender, EventArgs e)
        {
            if (pagenum == "50")
            {
                Response.Redirect("50ASPControlsMultiRecordDropdownToSingleRecord.aspx");
            }
            else if (pagenum == "60")
            {
                Response.Redirect("60ASPControlsMultiRecDropToCustGridViewToSingleRec.aspx");
            }
            else if (pagenum == "70")
            {
                Response.Redirect("70ASPControlsMultiRecDropToDropToSingleRec.aspx");
            }
            else if (pagenum == "80")
            {
                Response.Redirect("80ASPControlsPartialStringSearchToCustGridViewToSingleRec.aspx");
            }
            else if (pagenum == "90")
            {
                Response.Redirect("90ASPControlsPartialStringSearchToDropToSingleRec.aspx");
            }
            else if (pagenum == "8")
            {
                Response.Redirect("Exercise08.aspx");
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        protected void Clear(object sender, EventArgs e)
        {
            ID.Text = "";
            FName.Text = "";
            LName.Text = "";
            Age.Text = "";
            Gender.Text = "";
            HCard.Text = "";
            MAlert.Text = "";
            TeamList.ClearSelection();
            GuardianList.ClearSelection();
        }
        protected void Update_Click(object sender, EventArgs e)
        {
            var isValid = Validation(sender, e);
            if (isValid)
            {
                try
                {
                    PlayerController sysmgr = new PlayerController();
                    Player item = new Player();
                    item.PlayerID = int.Parse(ID.Text);
                    item.FirstName = FName.Text.Trim();
                    item.LastName = LName.Text.Trim();
                    if (GuardianList.SelectedValue == "0")
                    {
                        item.GuardianID = null;
                    }
                    else
                    {
                        item.GuardianID = int.Parse(GuardianList.SelectedValue);
                    }
                    item.TeamID = int.Parse(TeamList.SelectedValue);
                    item.Age = int.Parse(Age.Text);
                    item.Gender = Gender.Text.Trim();
                    item.AlbertaHealthCareNumber = HCard.Text.Trim();
                    item.MedicalAlertDetails = MAlert.Text.Trim();
                    int rowsaffected = sysmgr.Update(item);
                    if (rowsaffected > 0)
                    {
                        ShowMessage("Record has been UPDATED", "alert alert-success");
                    }
                    else
                    {
                        ShowMessage("Record was not found", "alert alert-warning");
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage(GetInnerException(ex).ToString(), "alert alert-danger");
                }
            }
        }
        protected void Delete_Click(object sender, EventArgs e)
        {
            var isValid = true;
            if (isValid)
            {
                try
                {
                    PlayerController sysmgr = new PlayerController();
                    int rowsaffected = sysmgr.Delete(int.Parse(ID.Text));
                    if (rowsaffected > 0)
                    {
                        ShowMessage("Record has been DELETED", "alert alert-success");
                        Clear(sender, e);
                    }
                    else
                    {
                        ShowMessage("Record was not found", "alert alert-warning");
                    }
                    UpdateButton.Enabled = false;
                    DeleteButton.Enabled = false;
                }
                catch (Exception ex)
                {
                    ShowMessage(GetInnerException(ex).ToString(), "alert alert-danger");
                }
            }
        }
    }
}