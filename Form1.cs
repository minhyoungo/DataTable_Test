using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataTable_Test
{
    public partial class Form1 : Form
    {
        DataSet ds = new DataSet(); // 학급들에 대한 정보를 가지고 있을 Dataset

        public Form1()
        {
            InitializeComponent();
        }

        private void btnReg_Click(object sender, EventArgs e)
        {

            bool bCheckisTable = false;

            if (ds.Tables.Contains(cBoxRegClass.Text)) { 
                bCheckisTable = true;
            }
            
            DataTable dt = null;

            if (!bCheckisTable)
            {
                dt = new DataTable(cBoxRegClass.Text);

                DataColumn colName = new DataColumn("NAME", typeof(string));
                DataColumn colSex = new DataColumn("SEX", typeof(string));
                DataColumn colRef = new DataColumn("REF", typeof(string));

                dt.Columns.Add(colName);
                dt.Columns.Add(colSex);
                dt.Columns.Add(colRef);
            }
            else
            {
                dt = ds.Tables[cBoxRegClass.Text];
            }




            // Row 자료를 등록
            DataRow row = dt.NewRow();
            row["NAME"] = tBoxRegName.Text;
            if (rdoRegSexMale.Checked)
            {
                row["SEX"] = "남자";
            }
            else if (rdoRegSexFemale.Checked)
            {
                row["SEX"] = "여자";
            }

            row["REF"] = tboxRegRef.Text;

            //  생성된 Row를 Table에 등록
            //dt.Rows.Add(row);
            //if(bCheckisTable)
            //{
            //    ds.Tables.Remove(cboxViewClass.Text);
            //}

            if (bCheckisTable)
            {
                ds.Tables[cBoxRegClass.Text].Rows.Add(row);
            }
            else
            {
                dt.Rows.Add(row);
                ds.Tables.Add(dt);
            }

            // 데이터가 추가된 테이블을 다시 불러오기
            VIewRefresh();
        }

        private void btnViewDataDel_Click(object sender, EventArgs e)
        {
            int iSelectRow = dgViewInfo.SelectedRows[0].Index;
            ds.Tables[cBoxRegClass.Text].Rows.RemoveAt(iSelectRow);

            VIewRefresh();
        }

        private void cboxViewClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            VIewRefresh();
        }

        private void VIewRefresh()
        {
            dgViewInfo.DataSource = ds.Tables[cboxViewClass.Text];

            // DatagridVIew Cell 정렬 및 Number를 적용
            foreach (DataGridViewRow oRow in dgViewInfo.Rows)
            {
                oRow.HeaderCell.Value = oRow.Index.ToString();
            }
            dgViewInfo.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }
    }
}
