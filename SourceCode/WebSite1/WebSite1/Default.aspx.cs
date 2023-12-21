using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class _Default : Page
{
    private string path = AppDomain.CurrentDomain.BaseDirectory + "bakupData.csv";
    private DataTable DGV;
    protected void Page_Load(object sender, EventArgs e)
    {
        DGV = new DataTable();
        DGV.Columns.Add("name", typeof(string));
        DGV.Columns.Add("old", typeof(string));
        DGV.Columns.Add("birthday", typeof(string));
        if (!IsPostBack)
        {
            SaveCSV(DGV, path, true);
        }
    }
  
    protected void _btn_Click(object sender, EventArgs e)
    {
        int action = 0;
        DGV = OpenCSV(AppDomain.CurrentDomain.BaseDirectory + "bakupData.csv");
        if (_btn.Text.Equals("修改帳號"))
        {
            if (string.IsNullOrEmpty(txtSelectRow.Text))
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('請先選擇修改行列')", true);
            }
            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                if(i== Int32.Parse(txtSelectRow.Text))
                {
                    DGV.Rows[i][0] = txt_name.Text;
                    DGV.Rows[i][1] = txt_old.Text;
                    DGV.Rows[i][2] = txt_birthday.Text;
                    SaveCSV(DGV, path, false);
                    action = 1;
                }
            }
            txtSelectRow.Text = "";
        }
        else
        {
            DataRow row = DGV.NewRow();
            row[0] = txt_name.Text;
            row[1] = txt_old.Text;
            row[2] = txt_birthday.Text;
            DGV.Rows.Add(row);
            SaveCSV(DGV, path, false);
            action = 0;
        }
        GridView1.DataSource = DGV;
        GridView1.DataBind();
        Clear(action);
    }
    private void Clear(int action)
    {
        if (action ==1)
        {
            txt_name.Text = "";
            txt_old.Text = "";
            txt_birthday.Text = "";
            _btn.Text = "建立帳號";
        }
        else
        {
            txt_name.Text = "";
            txt_old.Text = "";
            txt_birthday.Text = "";
        }
    }
    protected void GridView1_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        string[] ar = e.CommandArgument.ToString().Split(',');
        DGV = OpenCSV(AppDomain.CurrentDomain.BaseDirectory + "bakupData.csv");
        if (e.CommandName == "Upd")
        {
            txt_name.Text = ar[0].ToString();
            txt_old.Text = ar[1].ToString();
            txt_birthday.Text = ar[2].ToString();
            _btn.Text = "修改帳號";
            chkSelectRow(ar[0].ToString(), ar[1].ToString(), ar[2].ToString(), DGV);

        }
        else if(e.CommandName == "Del")
        {
            chkSelectRow(ar[0].ToString(), ar[1].ToString(), ar[2].ToString(), DGV);
            if(!string.IsNullOrEmpty(txtSelectRow.Text))
            {
                DGV.Rows.RemoveAt(Int32.Parse(txtSelectRow.Text));
                SaveCSV(DGV, path, false);
                GridView1.DataSource = DGV;
                GridView1.DataBind();
            }
        }
    }
    public static bool SaveCSV(DataTable dt, string fullPath,bool first)
    {
        try
        {
            FileStream fs = File.Create(fullPath);
            fs.Close();
            using (StreamWriter sw = new StreamWriter(fullPath, true))
            {
                string data = "";
                if (first)
                {
                    sw.WriteLine(data);
                }
                //寫出各行數據
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    data = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string str = dt.Rows[i][j].ToString();
                        str = string.Format("\"{0}\"", str);
                        data += str;
                        if (j < dt.Columns.Count - 1)
                        {
                            data += ",";
                        }
                    }
                    sw.WriteLine(data);
                }
                sw.Close();
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
    public static DataTable OpenCSV(string filePath)
    {
        DataTable dt = new DataTable();
        string strLine = "";
        string[] aryLine = null;
        dt.Columns.Add("name", typeof(string));
        dt.Columns.Add("old", typeof(string));
        dt.Columns.Add("birthday", typeof(string));

        using (FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
        {
            using (StreamReader sr = new StreamReader(fs))
            {
                while ((strLine = sr.ReadLine()) != null)
                {
                    aryLine = strLine.Split(',');
                    if (aryLine.Length ==1)
                    {
                        break;
                    }
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < 3; j++)
                    {
                        dr[j] = aryLine[j].Replace("\"", "");
                    }
                    dt.Rows.Add(dr);
                }
                sr.Close();
                fs.Close();
            }
        }
        return dt;
    }

    private void chkSelectRow(string name, string old, string birthday,DataTable dt)
    {
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i][0].ToString().Equals(name) &&
                dt.Rows[i][1].ToString().Equals(old) &&
                dt.Rows[i][2].ToString().Equals(birthday))
            {
                txtSelectRow.Text = i.ToString();
                return;
            }
        }
    }
}