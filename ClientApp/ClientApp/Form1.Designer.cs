namespace ClientApp
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnGetUsers = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.form1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.table1 = new XPTable.Models.Table();
            this.columnModel1 = new XPTable.Models.ColumnModel();
            this.gcId = new XPTable.Models.TextColumn();
            this.gcName = new XPTable.Models.TextColumn();
            this.gcFirstName = new XPTable.Models.TextColumn();
            this.gcLastName = new XPTable.Models.TextColumn();
            this.tableModel1 = new XPTable.Models.TableModel();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.table1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGetUsers
            // 
            this.btnGetUsers.Location = new System.Drawing.Point(12, 12);
            this.btnGetUsers.Name = "btnGetUsers";
            this.btnGetUsers.Size = new System.Drawing.Size(88, 23);
            this.btnGetUsers.TabIndex = 0;
            this.btnGetUsers.Text = "PobierzDane";
            this.btnGetUsers.UseVisualStyleBackColor = true;
            this.btnGetUsers.Click += new System.EventHandler(this.btnGetUsers_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(106, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // form1BindingSource
            // 
            this.form1BindingSource.DataSource = typeof(ClientApp.Form1);
            // 
            // table1
            // 
            this.table1.ColumnModel = this.columnModel1;
            this.table1.Location = new System.Drawing.Point(12, 41);
            this.table1.Name = "table1";
            this.table1.Size = new System.Drawing.Size(438, 397);
            this.table1.TabIndex = 8;
            this.table1.TableModel = this.tableModel1;
            this.table1.Text = "table1";
            // 
            // columnModel1
            // 
            this.columnModel1.Columns.AddRange(new XPTable.Models.Column[] {
            this.gcId,
            this.gcName,
            this.gcFirstName,
            this.gcLastName});
            // 
            // gcId
            // 
            this.gcId.Text = "Id";
            // 
            // gcName
            // 
            this.gcName.Text = "Name";
            // 
            // gcFirstName
            // 
            this.gcFirstName.Text = "FirstName";
            // 
            // gcLastName
            // 
            this.gcLastName.Text = "LastName";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.table1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnGetUsers);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.table1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetUsers;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.BindingSource form1BindingSource;
        private XPTable.Models.Table table1;
        private XPTable.Models.ColumnModel columnModel1;
        private XPTable.Models.TableModel tableModel1;
        private XPTable.Models.TextColumn gcId;
        private XPTable.Models.TextColumn gcName;
        private XPTable.Models.TextColumn gcFirstName;
        private XPTable.Models.TextColumn gcLastName;
    }
}

