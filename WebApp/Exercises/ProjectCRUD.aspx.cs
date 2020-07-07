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


namespace WebApp.Exercises
{
    public partial class ProjectCRUD : System.Web.UI.Page
    {
        static string pagenum = "";
        static string pid = "";
        static string add = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                pagenum = Request.QueryString["page"];
                pid = Request.QueryString["pid"];
                add = Request.QueryString["add"];
                BindSchoolList();
                //BindProgramList();
                if (string.IsNullOrEmpty(pid))
                {
                    Response.Redirect("~/Default.aspx");
                }
                else if (add == "yes")
                {
                    UpdateButton.Enabled = false;
                    DeleteButton.Enabled = false;
                }
                else
                {
                    AddButton.Enabled = false;
                    ProgramController sysmgr = new ProgramController();
                    Program info = null;
                    info = sysmgr.FindByPGID(int.Parse(pid));
                    if (info == null)
                    {
                        ShowMessage("Record is not in Database.", "alert alert-info");
                        Clear(sender, e);
                    }
                    else
                    {
                        PID.Text = info.ProgramID.ToString(); //NOT NULL in Database
                        PName.Text = info.ProgramName; //NOT NULL in Database
                        DName.Text = info.DiplomaName;
                        Tuition.Text = info.Tuition.ToString();
                        IntTuition.Text = info.InternationalTuition.ToString();
                        if (info.SchoolCode != null) //NULL in Database
                        {
                            SchoolList.SelectedValue = info.SchoolCode.ToString();
                        }
                        else
                        {
                            SchoolList.SelectedValue = "0";
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
        protected void BindSchoolList()
        {
            try
            {
                SchoolControllers sysmgr = new SchoolControllers();
                List<School> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.SchoolName.CompareTo(y.SchoolName));
                SchoolList.DataSource = info;
                SchoolList.DataTextField = nameof(School.SchoolName);
                SchoolList.DataValueField = nameof(School.SchoolCode);
                SchoolList.DataBind();
                ListItem myitem = new ListItem();
                myitem.Value = "0";
                myitem.Text = "select...";
                SchoolList.Items.Insert(0, myitem);
                //CategoryList.Items.Insert(0, "select...");

            }
            catch (Exception ex)
            {
                ShowMessage(GetInnerException(ex).ToString(), "alert alert-danger");
            }
        }
        protected void BindProgramList()
        {
            try
            {
                ProgramController sysmgr = new ProgramController();
                List<Program> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.ProgramName.CompareTo(y.ProgramName));
                ProgramList.DataSource = info;
                ProgramList.DataTextField = nameof(Program.ProgramName);
                ProgramList.DataValueField = nameof(Program.ProgramID);
                ProgramList.DataBind();
                ListItem myitem = new ListItem();
                myitem.Value = "0";
                myitem.Text = "select...";
                ProgramList.Items.Insert(0, myitem);
            }
            catch (Exception ex)
            {
                ShowMessage(GetInnerException(ex).ToString(), "alert alert-danger");
            }
        }
        protected bool Validation(object sender, EventArgs e)
        {
            double tu = 0;
            double IntTu = 0;
            if (string.IsNullOrEmpty(PName.Text))
            {
                ShowMessage("Program Name is required", "alert alert-info");
                return false;
            }
            else if (string.IsNullOrEmpty(DName.Text))
            {
                ShowMessage("Diploma Name is required", "alert alert-info");
                return false;
            }
            else if (SchoolList.SelectedValue == "0")
            {
                ShowMessage("School is required", "alert alert-info");
                return false;
            }
            else if (string.IsNullOrEmpty(Tuition.Text))
            {
                ShowMessage("Tution is required", "alert alert-info");
                return false;
            }
            else if (string.IsNullOrEmpty(IntTuition.Text))
            {
                ShowMessage("International Tution is required", "alert alert-info");
                return false;
            }
            else if (!double.TryParse(Tuition.Text, out tu))
            {
                
                    ShowMessage("Tution must be real number", "alert alert-info");
                    return false;
                            }
            else if (!double.TryParse(IntTuition.Text, out IntTu))
            {
              
                    ShowMessage("International Tution must be real number", "alert alert-info");
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
            else
            {
                Response.Redirect("~/Exercises/Project.aspx");
            }
        }
        protected void Clear(object sender, EventArgs e)
        {
            PName.Text = "";
            PID.Text = "";
            DName.Text = "";
            Tuition.Text = "";
            IntTuition.Text = "";
        }
        protected void Add_Click(object sender, EventArgs e)
        {
            var isValid = Validation(sender, e);
            if (isValid)
            {
                try
                {
                    ProgramController sysmgr = new ProgramController();
                    Program item = new Program();
                    //No ProductID here as the database will give a new one back when we add
                    item.ProgramName = PName.Text.Trim(); //NOT NULL in Database
                    //CategoryID can be NULL in Database but NOT NULL when record is added in this CRUD page
                    item.SchoolCode = SchoolList.SelectedValue;
                    item.DiplomaName = DName.Text;
                    item.Tuition = decimal.Parse(Tuition.Text);
                    item.InternationalTuition = decimal.Parse(IntTuition.Text);
                    int newID = sysmgr.Add(item);
                    PID.Text = newID.ToString();
                    ShowMessage("Record has been ADDED", "alert alert-success");
                    AddButton.Enabled = false;
                    UpdateButton.Enabled = true;
                    DeleteButton.Enabled = true;
                 }
                catch (Exception ex)
                {
                    ShowMessage(GetInnerException(ex).ToString(), "alert alert-danger");
                }
            }
        }
        protected void Update_Click(object sender, EventArgs e)
        {
            var isValid = Validation(sender, e);
            if (isValid)
            {
                try
                {
                    //ProductController sysmgr = new ProductController();
                    //Product item = new Product();
                    //item.ProgramName = PName.Text.Trim();
                    //item.ProductID = int.Parse(ID.Text);
                    //item.ProductName = Name.Text.Trim();
                    //if (SupplierList.SelectedValue == "0")
                    //{
                    //  item.SupplierID = null;
                    //}
                    //else
                    // {
                    //    item.SupplierID = int.Parse(SupplierList.SelectedValue);
                    // }
                    //item.CategoryID = int.Parse(CategoryList.SelectedValue);
                    //item.UnitPrice = decimal.Parse(UnitPrice.Text);
                    //item.Discontinued = Discontinued.Checked;
                    ProgramController sysmgr = new ProgramController();
                    Program item = new Program();
                    //No ProductID here as the database will give a new one back when we add
                    item.ProgramID = int.Parse(PID.Text);
                    item.ProgramName = PName.Text.Trim(); //NOT NULL in Database
                    //CategoryID can be NULL in Database but NOT NULL when record is added in this CRUD page
                    item.SchoolCode = SchoolList.SelectedValue;
                    item.DiplomaName = DName.Text;
                    item.Tuition = decimal.Parse(Tuition.Text);
                    item.InternationalTuition = decimal.Parse(IntTuition.Text);
                    //int newID = sysmgr.Add(item);
                    //PID.Text = newID.ToString();

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
                    ProgramController sysmgr = new ProgramController();
                    int rowsaffected = sysmgr.Delete(int.Parse(PID.Text));
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
                    AddButton.Enabled = true;
                }
                catch (Exception ex)
                {
                    ShowMessage(GetInnerException(ex).ToString(), "alert alert-danger");
                }
            }
        }
    }   
}