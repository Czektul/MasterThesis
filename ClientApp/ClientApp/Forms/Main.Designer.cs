namespace ClientApp
{
    partial class Main
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
            this.table1 = new XPTable.Models.Table();
            this.columnModel1 = new XPTable.Models.ColumnModel();
            this.gcId = new XPTable.Models.TextColumn();
            this.gcName = new XPTable.Models.TextColumn();
            this.gcFirstName = new XPTable.Models.TextColumn();
            this.gcLastName = new XPTable.Models.TextColumn();
            this.tableModel1 = new XPTable.Models.TableModel();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.btnEditUser = new System.Windows.Forms.Button();
            this.btnFIle = new System.Windows.Forms.Button();
            this.form1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.table1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGetUsers
            // 
            this.btnGetUsers.Location = new System.Drawing.Point(12, 12);
            this.btnGetUsers.Name = "btnGetUsers";
            this.btnGetUsers.Size = new System.Drawing.Size(88, 23);
            this.btnGetUsers.TabIndex = 0;
            this.btnGetUsers.Text = "Pobierz dane";
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
            // table1
            // 
            this.table1.ColumnModel = this.columnModel1;
            this.table1.Location = new System.Drawing.Point(12, 41);
            this.table1.Name = "table1";
            this.table1.Size = new System.Drawing.Size(415, 397);
            this.table1.TabIndex = 8;
            this.table1.TableModel = this.tableModel1;
            this.table1.Text = "table1";
            this.table1.SelectionChanged += new XPTable.Events.SelectionEventHandler(this.table1_SelectionChanged);
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
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(212, 12);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(98, 23);
            this.btnAddUser.TabIndex = 9;
            this.btnAddUser.Text = "Nowy użytkownik";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // btnEditUser
            // 
            this.btnEditUser.Location = new System.Drawing.Point(316, 12);
            this.btnEditUser.Name = "btnEditUser";
            this.btnEditUser.Size = new System.Drawing.Size(111, 23);
            this.btnEditUser.TabIndex = 10;
            this.btnEditUser.Text = "Edytuj użytkownika";
            this.btnEditUser.UseVisualStyleBackColor = true;
            this.btnEditUser.Click += new System.EventHandler(this.btnEditUser_Click);
            // 
            // btnFIle
            // 
            this.btnFIle.Location = new System.Drawing.Point(433, 41);
            this.btnFIle.Name = "btnFIle";
            this.btnFIle.Size = new System.Drawing.Size(75, 23);
            this.btnFIle.TabIndex = 11;
            this.btnFIle.Text = "Wślij plik";
            this.btnFIle.UseVisualStyleBackColor = true;
            this.btnFIle.Click += new System.EventHandler(this.btnFIle_Click);
            // 
            // form1BindingSource
            // 
            this.form1BindingSource.DataSource = typeof(ClientApp.Main);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 450);
            this.Controls.Add(this.btnFIle);
            this.Controls.Add(this.btnEditUser);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.table1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnGetUsers);
            this.Name = "Main";
            this.Text = "PRACA MAGISTERSKA";
            ((System.ComponentModel.ISupportInitialize)(this.table1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).EndInit();
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
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Button btnEditUser;
        private System.Windows.Forms.Button btnFIle;
    }
}

