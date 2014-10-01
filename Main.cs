using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SoftMachine
{
    public partial class Main : Form
    {
        #region Incialize

        //Instanciamos las Clases:
        Entities.Tables oTables = new Entities.Tables();

        public Main()
        {
            InitializeComponent();
        }
        #endregion

        #region Data Binding
        private void BindTables(string database)
        {
            clbTables.Items.Clear();
            DataTable dt = new DataTable();
            

            string query = "show tables FROM " + database;
            Utilities.Connection.QueryDB(dt, query, database);

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                string dbName = dt.Rows[x][0].ToString();

                //Llenamos el checkbox:
                clbTables.Items.Add(dbName);

                //Obtenemos información de cada tabla:
                DataTable dtInfo = new DataTable();
                query = string.Format("explain `{0}`", dbName); //string query = string.Format("SELECT COLUMN_NAME, DATA_TYPE, COLUMN_KEY, EXTRA FROM information_schema.COLUMNS WHERE TABLE_SCHEMA='{0}' AND TABLE_NAME='{1} ORDER BY TABLE_NAME; `{0}`", DataBase, tableName);
                Utilities.Connection.QueryDB(dtInfo, query, database);
                //Obtenemos la información de cada Row

                //Instanciamos cada tabla [ y llenamos el objeto]:
                Entities.Table oTable = new Entities.Table(dbName);

                for (int z = 0; z < dtInfo.Rows.Count; z++)
                {
                    Entities.Row oRow = new Entities.Row(oTable);
                    oRow.dbName = dtInfo.Rows[z]["Field"].ToString();
                    oRow.dbType = dtInfo.Rows[z]["Type"].ToString().ToUpperInvariant();
                    oRow.dbKey = dtInfo.Rows[z]["Key"].ToString().ToUpperInvariant();
                    oRow.dbExtra = dtInfo.Rows[z]["Extra"].ToString().ToLowerInvariant();
                    oTable.Rows.Add(oRow);
                }
                //Agregamos cada tabla a la colección:
                oTables.Add(oTable);
            }
        }
        #endregion

        #region Code Generation

        private void Generate()
        {
            StringBuilder sbMethods = new StringBuilder();
            StringBuilder sbStoredProcedures = new StringBuilder();
            StringBuilder sbEntities = new StringBuilder();

            string DatabaseName = txtDB.Text;
            oTables.GenerateMethods(ref sbMethods, ref sbStoredProcedures, DatabaseName, clbTables.SelectedItems);
            //oTables.FormatBusinessEntityLayer(ref sbEntities, DatabaseName);
            oTables.FormatBusinessLogicLayer(ref sbEntities, DatabaseName);
            txtSP.Text = "";
            txtCode.Text = "";
            txtSP.AppendText(sbStoredProcedures.ToString());
            //txtCode.AppendText(sbMethods.ToString());
            txtCode.AppendText(sbEntities.ToString());
        }
        #endregion

        #region SP Generation

   #endregion

        #region Events
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            if (clbTables.Items.Count > 0)
            {
                if (btnSelectAll.Text.Equals("Select All"))
                {
                    for (int i = 0; i < clbTables.Items.Count; i++)
                    {
                        clbTables.SetItemChecked(i, true);
                    }

                    btnSelectAll.Text = "Un-Select All";
                    return;
                }

                if (btnSelectAll.Text.Equals("Un-Select All"))
                {
                    for (int i = 0; i < clbTables.Items.Count; i++)
                    {
                        clbTables.SetItemChecked(i, false);
                    }

                    btnSelectAll.Text = "Select All";
                    return;
                }
            }

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            this.Generate();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.Show();
            about.Focus();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "SQL File (*.sql)|*.sql|Text File (*.txt)|*.txt";
            saveFileDialog1.FilterIndex = 0;

            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string fileName = saveFileDialog1.FileName;
            System.IO.File.WriteAllText(fileName, txtCode.Text);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (this.ConnectionPrerequisite() == false)
            {
                return;
            }

            Utilities.Connection.thisConnection = Utilities.Connection.SetConnection(txtServer.Text,txtPort.Text,txtUserId.Text,txtPassword.Text,txtDB.Text);

            try            {
                Utilities.Connection.thisConnection.Open();
                if (Utilities.Connection.thisConnection.State == ConnectionState.Open)
                {
                    gbSelectTables.Enabled = true;
                    gbOptions.Enabled = true;
                    gbCode.Enabled = true;
                    this.BindTables(txtDB.Text);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
            finally
            {
                Utilities.Connection.thisConnection.Close();
            }
            //thisConnection = null;
        }

        private bool ConnectionPrerequisite()
        {
            if (txtServer.Text.Equals(""))
            {
                MessageBox.Show("Server is required","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtServer.Focus();
                return false;
            }

            if (txtPort.Text.Equals(""))
            {
                MessageBox.Show("Port is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPort.Focus();
                return false;
            }

            if (txtDB.Text.Equals(""))
            {
                MessageBox.Show("DatabaseName name is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDB.Focus();
                return false;
            }


            return true;
        }
        #endregion

    }
}
