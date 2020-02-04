using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        //"新增"程式碼
        private void Button1_Click(object sender, EventArgs e)
        {
            string constr = "Server=LAPTOP-2M6MNSP8\\SQLEXPRESS;Database=Average Wage;Integrated Security=SSPI;";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = constr;
            if ((comboBox1.Text == "請選擇") || (comboBox2.Text == "請選擇"))
            {
                MessageBox.Show("請選擇年份及行業表!");
            }
            else
            {
                if ((textBox1.Text==null) || (textBox2.Text==null))
                {
                    MessageBox.Show("新增時，職類別、行業別的欄位為必填!");
                }
                else
                {
                    conn.Open();
                    int time = 0;
                    string[] content = new string[4];
                    string sql = "INSERT INTO " + comboBox2.Text + comboBox1.Text+ "(年度,職類別,行業別";
                    if (textBox4.Text !="") { sql += ",受僱員工人數"; time++;content[0] = textBox4.Text; }
                    if (textBox3.Text != "") { sql += ",總薪資"; time++; content[1] = textBox3.Text; }
                    if (textBox5.Text != "") { sql += ",經常性薪資"; time++; content[2] = textBox5.Text; }
                    if (textBox6.Text != "") { sql += ",非經常性薪資"; time++; content[3] = textBox6.Text; }
                    sql += ") VALUES ("+ comboBox1.Text + ",'" + textBox1.Text + "','" + textBox2.Text+"'";
                    for(int i=0;time>0;i++)
                    {
                        if (content[i] != null)
                        {
                            sql += ("," + content[i]);
                            time--;
                        }
                        else continue;
                    }
                    sql += ")";
                    SqlDataAdapter sqlData = new SqlDataAdapter(@sql, conn);
                    DataTable dt = new DataTable();
                    sqlData.Fill(dt);
                    dataGridView1.DataSource = dt;
                    conn.Close();
                }
            }
        }
        //"刪除"程式碼
        private void Button4_Click(object sender, EventArgs e)
        {
            string constr = "Server=LAPTOP-2M6MNSP8\\SQLEXPRESS;Database=Average Wage;Integrated Security=SSPI;";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = constr;
            if ((comboBox1.Text == "請選擇") || (comboBox2.Text == "請選擇"))
            {
                MessageBox.Show("請選擇年份及行業表!");
            }
            else
            {
                conn.Open();
                int time = 0;
                string[] content = new string[6];
                string sql = "DELETE FROM " + comboBox2.Text + comboBox1.Text + " WHERE ";
                if (textBox1.Text != "" && (textBox1.Text != "NULL")) { time++; content[0] = (" 職類別='"+textBox1.Text+"'"); }
                if (textBox2.Text != "" && (textBox2.Text != "NULL")) { time++; content[1] = (" 行業別='"+textBox2.Text+"'"); }
                if (textBox4.Text != "" && (textBox4.Text != "NULL")) { time++; content[2] =(" 受僱員工人數>="+textBox4.Text); }
                if (textBox3.Text != "" && (textBox3.Text != "NULL")) { time++; content[3] =(" 總薪資>="+textBox3.Text); }
                if (textBox5.Text != "" && (textBox5.Text != "NULL")) { time++; content[4] =(" 經常性薪資>="+textBox5.Text); }
                if (textBox6.Text != "" && (textBox6.Text != "NULL")) { time++; content[5] =(" 非經常性薪資>="+textBox6.Text); }
                if (textBox1.Text == "NULL") { time++; content[0] = (" 職類別 IS " + textBox1.Text); }
                if (textBox2.Text == "NULL") { time++; content[1] = (" 行業別 IS " + textBox2.Text); }
                if (textBox4.Text == "NULL") { time++; content[2] = (" 受僱員工人數 IS " + textBox4.Text); }
                if (textBox3.Text == "NULL") { time++; content[3] = (" 總薪資 IS " + textBox3.Text); }
                if (textBox5.Text == "NULL") { time++; content[4] = (" 經常性薪資 IS " + textBox5.Text); }
                if (textBox6.Text == "NULL") { time++; content[5] = (" 非經常性薪資 IS " + textBox6.Text); }
                if (time == 0) MessageBox.Show("請至少填入一項!");
                else
                {
                    for (int i = 0;i<6;i++)
                    {
                        if (time == 1) sql +=content[i];
                        else
                        {
                            if (content[i] != null)
                            {
                                sql += (content[i] + " and ");
                                time--;
                            }
                            else continue;
                        }
                    }                    
                    SqlDataAdapter sqlData = new SqlDataAdapter(@sql, conn);
                    DataTable dt = new DataTable();
                    sqlData.Fill(dt);
                    dataGridView1.DataSource = dt;
                    conn.Close();
                }
            }
        }
        //"修改"程式碼
        private void Button3_Click(object sender, EventArgs e)
        {
            string constr = "Server=LAPTOP-2M6MNSP8\\SQLEXPRESS;Database=Average Wage;Integrated Security=SSPI;";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = constr;
            if ((comboBox1.Text == "請選擇") || (comboBox2.Text == "請選擇"))
            {
                MessageBox.Show("請選擇年份及行業表!");
            }
            else
            {
                if ((textBox1.Text == null) || (textBox2.Text == null))
                {
                    MessageBox.Show("修改時，職類別、行業別的欄位為必填且不得任意修改!");
                }
                else
                {
                    conn.Open();
                    int time = 0;
                    string[] content = new string[4];
                    string sql = "UPDATE " + comboBox2.Text + comboBox1.Text + " SET ";
                    if (textBox4.Text != "") { time++; content[0] =("受僱員工人數="+textBox4.Text); }
                    if (textBox3.Text != "") { time++; content[1] =("總薪資="+textBox3.Text); }
                    if (textBox5.Text != "") { time++; content[2] =("經常性薪資="+textBox5.Text); }
                    if (textBox6.Text != "") { time++; content[3] =("非經常性薪資="+textBox6.Text); }
                    if (time == 0) MessageBox.Show("請輸入至少一項要修改的項目!");
                    else
                    {
                        for (int i=0;i<4;i++ )
                        {
                            if (time == 1) sql +=content[i];
                            else
                            {
                                if (content[i] != null)
                                {
                                    sql += (content[i] + ",");
                                    time--;
                                }
                                else continue;
                            }
                        }
                        try
                        {
                            sql += (" WHERE 職類別='" + textBox1.Text + "' AND 行業別='" + textBox2.Text + "'");
                            SqlDataAdapter sqlData = new SqlDataAdapter(@sql, conn);
                            DataTable dt = new DataTable();
                            sqlData.Fill(dt);
                            dataGridView1.DataSource = dt;
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("此修改項目不存在或不允許!",ex.Message);
                        }
                    }
                    
                    
                }
            }
        }
        //"查詢"程式碼
        private void Button2_Click(object sender, EventArgs e)
        {
            string constr = "Server=LAPTOP-2M6MNSP8\\SQLEXPRESS;Database=Average Wage;Integrated Security=SSPI;";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = constr;
            if ((comboBox1.Text != "請選擇") && (comboBox2.Text != "請選擇"))
            {
                conn.Open();
                int time = 0;
                string sql = "SELECT * FROM " + comboBox2.Text + comboBox1.Text;
                string[] content = new string[6];
                if (textBox3.Text != "") { content[0] = " 總薪資>=" + textBox3.Text; time++; }
                if (textBox5.Text != "") { content[1] = " 經常性薪資 >=" + textBox5.Text; time++; }
                if (textBox6.Text != "") { content[2] = " 非經常性薪資>=" + textBox6.Text; time++; }
                if (textBox4.Text != "") { content[3] = " 受僱員工人數>=" + textBox4.Text; time++; }
                if (textBox1.Text != "") { content[4] = " 職類別='" + textBox1.Text + "' "; time++; }
                if (textBox2.Text != "") { content[5] = " 行業別='" + textBox2.Text + "' "; time++; }

                if (time > 0)
                {
                    sql += " WHERE ";
                    Boolean j = true;
                    for (int i = 0; i < 6; i++)
                    {
                        if (time == 1) sql += content[i];
                        else
                        {

                            if ((j == true) && (content[i] != null))
                            {
                                sql += content[i];
                                j = false;
                            }
                            else if ((j == false) && (content[i] != null))
                            {
                                sql += (" and " + content[i]);
                            }
                            else continue;
                        }
                    }
                }

                SqlDataAdapter sqlData = new SqlDataAdapter(@sql, conn);
                DataTable dt = new DataTable();
                sqlData.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            else
            {
                MessageBox.Show("請選擇年份及行業表!");
            }


        }

    }
}
