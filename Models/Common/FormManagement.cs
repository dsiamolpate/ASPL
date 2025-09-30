using ASPL.CLASSES;
using ASPL.STARTUP.FORMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace ASPL.Models.Common
{
    public enum ComboType
    {
        YesNo
    }

    public enum RecordMove
    {
        Next,
        Previous
    }

    public enum MessageType
    {
        Save,
        Update,
        Delete,
        Recall
    }

    public sealed class FormManagement
    {
        private static FormManagement instance = null;
        private static readonly object o = new object();

        private List<CommonOption> commonOptions;

        private FormManagement()
        {
            commonOptions = DbConnectivity.Instance.GetOptions();
        }

        public static FormManagement Instance
        {
            get
            {
                lock (o)
                {
                    if (instance == null)
                    {
                        instance = new FormManagement();
                    }
                    return instance;
                }
            }
        }



        public delegate void SelectRecordFunctionality();

        public void ProcessFormOpening<T>(ref Panel FormPanel, ref Panel BackPanel, string FormName) where T : Form 
        {
            T objForm =  null;

            foreach (object currObj in FormPanel.Controls)
            {
                Form currForm = null;
                try
                {
                    currForm = (Form)currObj;
                }
                catch
                {
                    continue;
                }

                if (currForm.GetType() == typeof(T))
                {
                    objForm = (T)currForm;
                }
                else
                {
                    currForm.Hide();
                }

            }

            if (objForm == null)
            {

                objForm = (T)Activator.CreateInstance(typeof(T));

                try
                {
                    Panel pnlMain = (Panel)objForm.Controls.Find("pnlMain", true)[0];
                    if (pnlMain != null)
                    {
                        pnlMain.Anchor = System.Windows.Forms.AnchorStyles.None;
                        pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    }
                }
                catch
                {

                }

                Label lblHeading = new Label();
                lblHeading.Text = FormName; // objForm.Text;
                lblHeading.Dock = DockStyle.Top;
                lblHeading.AutoSize = true;
                lblHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.0F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
                lblHeading.Name = "lblHeading";
                objForm.Controls.Add(lblHeading);

                objForm.TopLevel = false;
                objForm.Dock = DockStyle.Fill;
                objForm.WindowState = FormWindowState.Maximized;
                objForm.FormBorderStyle = FormBorderStyle.None;
                FormPanel.Controls.Add(objForm);
                
            }
            else
            {
                try
                {
                    Label lblHeading = (Label)objForm.Controls.Find("lblHeading", true)[0];
                    lblHeading.Text = FormName;
                }
                catch
                {

                }
            }
            objForm.Visible = true;

            BackPanel.SendToBack();

        }

        public T ProcessModelFormOpening<T>(ref Panel FormPanel) where T : Form
        {

            foreach (object currObj in FormPanel.Controls)
            {
                Form currForm = null;
                try
                {
                    currForm = (Form)currObj;
                }
                catch
                {
                    continue;
                }

                if (currForm != null)
                {
                    currForm.Hide();
                }

            }

            return ProcessModelFormOpening<T>();
        }

        public T ProcessModelFormOpening<T>() where T : Form
        {
            T objForm = (T)Activator.CreateInstance(typeof(T));
            objForm.FormBorderStyle = FormBorderStyle.None;

            Panel pnlMain = new Panel();
            pnlMain.BorderStyle = BorderStyle.FixedSingle;
            pnlMain.Width = objForm.Width;
            pnlMain.Height = objForm.Height;

            objForm.Controls.Add(pnlMain);

            objForm.ShowDialog();

            return objForm;
        }

        public FrmSearch SearchFormOnKeyDown(ref TextBox txtSearchBox, KeyEventArgs e, string TableKey, bool AutoSearchDecision)
        {

            FrmSearch frmSearch = new FrmSearch(TableKey, true);


            if (e.KeyCode.ToString().StartsWith("NumPad"))
            {
                return null;
            }

            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                txtSearchBox.Text = "";

                return frmSearch;
            }

            if (e.KeyCode == Keys.Enter)
            {
                int n = -1;
                int.TryParse(txtSearchBox.Text, out n);
                if (n > 0)
                {
                    frmSearch.val = txtSearchBox.Text;
                    if (frmSearch.IsValuefound())
                    {
                        return frmSearch;
                    }
                }
                txtSearchBox.Text = "";
                frmSearch.val = "";
            }
            else if (char.IsDigit((char)e.KeyCode))
            {
                return null;
            }
            else if (char.IsLetter((char)e.KeyCode))
            {
                txtSearchBox.Text = "";
                frmSearch.val = ((char)e.KeyCode).ToString();
            }
            else
            {
                txtSearchBox.Text = "";
                return frmSearch;
            }

            frmSearch.ShowDialog();
            return frmSearch;
        }

        public FrmSearch SearchFormOnKeyDown(ref DataGridView dataGridView, int rowIndex, int colIndex, KeyEventArgs e, string TableKey, bool AutoSearchDecision)
        {
            var dataGridViewCell = dataGridView.Rows[rowIndex].Cells[colIndex];

            FrmSearch frmSearch = new FrmSearch(TableKey, true);

            if (dataGridViewCell == null || dataGridViewCell.Value == null)
            {
                frmSearch.ShowDialog();
                return frmSearch;
            }

            if (e.KeyCode.ToString().StartsWith("NumPad"))
            {
                return null;
            }

            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                dataGridViewCell.Value = "";

                return frmSearch;
            }

            if (e.KeyCode == Keys.Enter)
            {
                int n = -1;
                int.TryParse(dataGridViewCell.Value.ToString(), out n);
                if (n > 0)
                {
                    frmSearch.val = dataGridViewCell.Value.ToString();
                    if (frmSearch.IsValuefound())
                    {
                        return frmSearch;
                    }
                }
                dataGridViewCell.Value = "";
                frmSearch.val = "";
            }
            else if (char.IsDigit((char)e.KeyCode))
            {
                return null;
            }
            else if (char.IsLetter((char)e.KeyCode))
            {
                dataGridViewCell.Value = "";
                frmSearch.val = ((char)e.KeyCode).ToString();
            }
            else
            {
                dataGridViewCell.Value = "";
                return frmSearch;
            }

            frmSearch.ShowDialog();
            return frmSearch;
        }



        public FrmSearch SearchFormOnKeyDown(ref DataGridViewTextBoxEditingControl txtSearchBox, PreviewKeyDownEventArgs e, string TableKey, bool AutoSearchDecision)
        {


            FrmSearch frmSearch = new FrmSearch(TableKey, true);


            if (e.KeyCode.ToString().StartsWith("NumPad"))
            {
                return null;
            }

            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                txtSearchBox.Text = "";

                return frmSearch;
            }

            if (e.KeyCode == Keys.Enter)
            {
                int n = -1;
                int.TryParse(txtSearchBox.Text, out n);
                if (n > 0)
                {
                    frmSearch.val = txtSearchBox.Text;
                    if (frmSearch.IsValuefound())
                    {
                        return frmSearch;
                    }
                }
                txtSearchBox.Text = "";
                frmSearch.val = "";
            }
            else if (char.IsDigit((char)e.KeyCode))
            {
                return null;
            }
            else if (char.IsLetter((char)e.KeyCode))
            {
                txtSearchBox.Text = "";
                frmSearch.val = ((char)e.KeyCode).ToString();
            }
            else
            {
                txtSearchBox.Text = "";
                return frmSearch;
            }

            frmSearch.ShowDialog();
            return frmSearch;
        }

        public void DownKeyGridSelection_KeyUpDown(ref TextBox txtSearch, ref DataGridView dataGridView, KeyEventArgs e, SelectRecordFunctionality SelectRecord)
        {

            if (char.IsLetterOrDigit(((char)e.KeyCode)))
            {
                txtSearch.Text = ((char)e.KeyCode).ToString();
                txtSearch.Focus();
                txtSearch.SelectionStart = txtSearch.Text.Length;
                return;
            }

            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Home || e.KeyCode == Keys.End)
            {
                SelectRecord.Invoke();

                if (e.KeyCode == Keys.Up && dataGridView.CurrentRow.Index <= 0)
                {
                    txtSearch.Focus();
                }
                else
                {
                    dataGridView.Focus();
                }
            }

        }

        public void FillCombo(ref ComboBox cmbCombo, ComboType comboType, string selectedValue = null)
        {

            cmbCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCombo.BackColor = cmbCombo.Parent.BackColor;

            var customSource = commonOptions
                .Where(x => x.OptType.Equals(comboType.ToString()))
                .ToList();

            FillCombo(ref cmbCombo, customSource, selectedValue);

        }

        public void FillCombo(ref ComboBox cmbCombo, List<CommonOption> commonOptions, string selectedValue = null)
        {
            cmbCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCombo.BackColor = cmbCombo.Parent.BackColor;

            var customSource = commonOptions
                .Select(x => new { Name = x.OptName, Value = x.OptValue })
                .ToList();

            cmbCombo.DataSource = customSource;
            cmbCombo.DisplayMember = "Name";
            cmbCombo.ValueMember = "Value";

            if (selectedValue != null)
            {

                cmbCombo.SelectedValue = selectedValue;
            }

        }

        public void FillCombo(ref ComboBox cmbCombo, string comboType, string selectedValue = null)
        {
            var customSource = DbConnectivity.Instance.GetOptions(comboType)
                .ToList();

            FillCombo(ref cmbCombo, customSource, selectedValue);
        }

        public void NumericKeyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char[] ctrlKeys = new char[] { '\b', '\r', '\u0001', '\u001b' };
            var c = ctrlKeys.Where(x => x == e.KeyChar).FirstOrDefault();
            if (c != '\0')
            {
                return;
            }

            var txtData = ((TextBox)sender);
            var txtValue = txtData.Text + e.KeyChar.ToString();

            var points = txtValue.Split('.');
            if (points.Length == 2)
            {
                if (points[1].Trim().Length >= 3)
                {
                    e.Handled = true;
                    return;
                }
            }

            if (e.KeyChar == ' ' || !txtValue.isNumber())
            {
                e.Handled = true;
            }


        }

        public void PercentageTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char[] ctrlKeys = new char[] { '\b', '\r', '\u0001', '\u001b' };
            var c = ctrlKeys.Where(x => x == e.KeyChar).FirstOrDefault();
            if (c != '\0')
            {
                return;
            }

            var txtData = ((TextBox)sender);
            var txtValue = txtData.Text + e.KeyChar.ToString();

            var points = txtValue.Split('.');
            if (points.Length == 2)
            {
                if(points[1].Trim().Length>=3)
                {
                    e.Handled = true;
                    return;
                }
            }

            if (e.KeyChar == ' ' || !txtValue.isNumber() || Convert.ToDouble(txtValue) >= 100)
            {
                e.Handled = true;
            }


        }


        public bool CheckForBlank(params Control[] controls)
        {
            foreach (var ctrl in controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox txtData = (TextBox)ctrl;
                    if (txtData.Text.Trim().Length == 0)
                    {
                        txtData.Focus();
                        return false;
                    }
                }
                else if (ctrl is ComboBox)
                {
                    ComboBox cmbData = (ComboBox)ctrl;
                    if (cmbData.SelectedValue.ToString() == "-1")
                    {
                        cmbData.Focus();
                        return false;
                    }
                }

            }

            return true;
        }

        public bool CheckForZero(params Control[] controls)
        {
            foreach (var ctrl in controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox txtData = (TextBox)ctrl;
                    if (txtData.Text.Trim().Length == 0)
                    {
                        txtData.Focus();
                        return false;
                    }

                    int currValue = 0;
                    int.TryParse(txtData.Text, out currValue);
                    if (currValue <= 0)
                    {
                        txtData.Focus();
                        return false;
                    }
                }

            }

            return true;
        }


        public void SelectRecordFromGrid(DataGridView dataGridView, RecordMove recordMove, Models.Common.FormManagement.SelectRecordFunctionality selectRecord)
        {
            if (dataGridView.Rows.Count == 0)
            {
                return;
            }

            if (recordMove == RecordMove.Next && dataGridView.SelectedRows[0].Index == dataGridView.Rows.Count - 1)
            {
                return;
            }

            if (recordMove == RecordMove.Previous && dataGridView.SelectedRows[0].Index == 0)
            {
                return;
            }

            int movementCount = 1;
            if (recordMove == RecordMove.Previous)
            {
                movementCount = movementCount * -1;
            }

            dataGridView.Rows[dataGridView.SelectedRows[0].Index + movementCount].Selected = true;
            selectRecord.Invoke();

        }

        public void ShowMessage(ref Label lblMessage, MessageType MessageType, bool IsSuccess)
        {
            lblMessage.Text = GetMessage(MessageType, IsSuccess);
        }

        public void ShowMessage(MessageType MessageType, bool IsSuccess)
        {
            MessageBox.Show(GetMessage(MessageType, IsSuccess), IsSuccess ? "Message" : "Error Message");
        }

        private string GetMessage(MessageType MessageType, bool IsSuccess)
        {
            if (IsSuccess)
            {
                string successMessage = "Done";

                switch (MessageType)
                {
                    case MessageType.Save:
                        {
                            successMessage = "Saved Successfully !!!";
                            break;
                        }
                    case MessageType.Update:
                        {
                            successMessage = "Updated Successfully !!!";
                            break;
                        }
                    case MessageType.Delete:
                        {
                            successMessage = "Deleted Successfully !!!";
                            break;
                        }
                    case MessageType.Recall:
                        {
                            successMessage = "Recalled Successfully !!!";
                            break;
                        }
                    default:
                        break;
                }

                return successMessage;
            }
            else
            {
                return "Unable to Proceed. Contact System Administrator";
            }
        }

        public void ClearAllTextBox(ref Panel pnlMain)
        {
            foreach (Control c in pnlMain.Controls)
            {
                if (c is TextBox)
                {
                    TextBox t = (TextBox)c;
                    t.Clear();
                }
            }
        }

    }
}
