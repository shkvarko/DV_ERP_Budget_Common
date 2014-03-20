namespace ERP_Budget.Common
{
    partial class frmBudgetDepManagerList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBudgetDepManagerList));
            this.tableLayoutPanelBackGround = new System.Windows.Forms.TableLayoutPanel();
            this.treeList = new DevExpress.XtraTreeList.TreeList();
            this.colManagerName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCheckManager = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repItemChecked = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colCheckCoordinator = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCheckController = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repItemCheckEdit_ReadOnly = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.repItemlkpBudgetDep = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repItemUsersList = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelInfo = new DevExpress.XtraEditors.LabelControl();
            this.imglNodes = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanelBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemChecked)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemCheckEdit_ReadOnly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemlkpBudgetDep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemUsersList)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelBackGround
            // 
            this.tableLayoutPanelBackGround.ColumnCount = 1;
            this.tableLayoutPanelBackGround.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelBackGround.Controls.Add(this.treeList, 0, 1);
            this.tableLayoutPanelBackGround.Controls.Add(this.labelInfo, 0, 0);
            this.tableLayoutPanelBackGround.Controls.Add(this.tableLayoutPanel1, 0, 2);
            this.tableLayoutPanelBackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelBackGround.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelBackGround.Name = "tableLayoutPanelBackGround";
            this.tableLayoutPanelBackGround.RowCount = 3;
            this.tableLayoutPanelBackGround.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelBackGround.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelBackGround.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanelBackGround.Size = new System.Drawing.Size(561, 430);
            this.toolTipController.SetSuperTip(this.tableLayoutPanelBackGround, null);
            this.tableLayoutPanelBackGround.TabIndex = 0;
            // 
            // treeList
            // 
            this.treeList.AllowDrop = true;
            this.treeList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeList.Appearance.EvenRow.BackColor = System.Drawing.SystemColors.Info;
            this.treeList.Appearance.EvenRow.Options.UseBackColor = true;
            this.treeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colManagerName,
            this.colCheckManager,
            this.colCheckCoordinator,
            this.colCheckController});
            this.treeList.KeyFieldName = "BUDGETDEP_GUID_ID";
            this.treeList.Location = new System.Drawing.Point(3, 23);
            this.treeList.Name = "treeList";
            this.treeList.OptionsBehavior.AutoChangeParent = false;
            this.treeList.OptionsBehavior.AutoFocusNewNode = true;
            this.treeList.OptionsBehavior.AutoNodeHeight = false;
            this.treeList.OptionsBehavior.DragNodes = true;
            this.treeList.OptionsBehavior.ImmediateEditor = false;
            this.treeList.OptionsBehavior.KeepSelectedOnClick = false;
            this.treeList.OptionsBehavior.ShowEditorOnMouseUp = true;
            this.treeList.OptionsBehavior.SmartMouseHover = false;
            this.treeList.OptionsPrint.PrintPreview = true;
            this.treeList.OptionsView.EnableAppearanceEvenRow = true;
            this.treeList.OptionsView.ShowPreview = true;
            this.treeList.ParentFieldName = "BUDGETDEP_PARENT_GUID_ID";
            this.treeList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repItemCheckEdit_ReadOnly,
            this.repositoryItemMemoExEdit1,
            this.repItemlkpBudgetDep,
            this.repItemUsersList,
            this.repItemChecked});
            this.treeList.Size = new System.Drawing.Size(555, 373);
            this.treeList.TabIndex = 4;
            this.treeList.ToolTipController = this.toolTipController;
            this.treeList.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList_FocusedNodeChanged);
            this.treeList.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(this.treeList_CustomDrawNodeCell);
            this.treeList.CellValueChanging += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.treeList_CellValueChanging);
            this.treeList.CellValueChanged += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.treeList_CellValueChanged);
            // 
            // colManagerName
            // 
            this.colManagerName.Caption = "Пользователи";
            this.colManagerName.FieldName = "Фамилия Имя";
            this.colManagerName.Name = "colManagerName";
            this.colManagerName.OptionsColumn.AllowEdit = false;
            this.colManagerName.OptionsColumn.AllowSort = false;
            this.colManagerName.OptionsColumn.ReadOnly = true;
            this.colManagerName.Visible = true;
            this.colManagerName.VisibleIndex = 0;
            this.colManagerName.Width = 257;
            // 
            // colCheckManager
            // 
            this.colCheckManager.Caption = "Распорядитель";
            this.colCheckManager.ColumnEdit = this.repItemChecked;
            this.colCheckManager.FieldName = "Вкл.";
            this.colCheckManager.MinWidth = 30;
            this.colCheckManager.Name = "colCheckManager";
            this.colCheckManager.OptionsColumn.AllowSort = false;
            this.colCheckManager.Visible = true;
            this.colCheckManager.VisibleIndex = 1;
            this.colCheckManager.Width = 105;
            // 
            // repItemChecked
            // 
            this.repItemChecked.AutoHeight = false;
            this.repItemChecked.Name = "repItemChecked";
            // 
            // colCheckCoordinator
            // 
            this.colCheckCoordinator.Caption = "Согласователь";
            this.colCheckCoordinator.ColumnEdit = this.repItemChecked;
            this.colCheckCoordinator.FieldName = "Согласователь";
            this.colCheckCoordinator.Name = "colCheckCoordinator";
            this.colCheckCoordinator.OptionsColumn.AllowSort = false;
            this.colCheckCoordinator.Visible = true;
            this.colCheckCoordinator.VisibleIndex = 2;
            this.colCheckCoordinator.Width = 92;
            // 
            // colCheckController
            // 
            this.colCheckController.Caption = "Контролер";
            this.colCheckController.ColumnEdit = this.repItemChecked;
            this.colCheckController.FieldName = "Контролер";
            this.colCheckController.Name = "colCheckController";
            this.colCheckController.OptionsColumn.AllowSort = false;
            this.colCheckController.Visible = true;
            this.colCheckController.VisibleIndex = 3;
            this.colCheckController.Width = 80;
            // 
            // repItemCheckEdit_ReadOnly
            // 
            this.repItemCheckEdit_ReadOnly.AutoHeight = false;
            this.repItemCheckEdit_ReadOnly.Name = "repItemCheckEdit_ReadOnly";
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            this.repositoryItemMemoExEdit1.ShowIcon = false;
            // 
            // repItemlkpBudgetDep
            // 
            this.repItemlkpBudgetDep.AutoHeight = false;
            this.repItemlkpBudgetDep.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repItemlkpBudgetDep.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BUDGETDEP_NAME", "Бюджетное подразделение", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
            this.repItemlkpBudgetDep.DisplayMember = "BUDGETDEP_NAME";
            this.repItemlkpBudgetDep.Name = "repItemlkpBudgetDep";
            this.repItemlkpBudgetDep.NullText = "";
            this.repItemlkpBudgetDep.ValueMember = "BUDGETDEP_GUID_ID";
            // 
            // repItemUsersList
            // 
            this.repItemUsersList.AutoHeight = false;
            this.repItemUsersList.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repItemUsersList.Name = "repItemUsersList";
            this.repItemUsersList.NullText = "";
            this.repItemUsersList.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.DoubleClick;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.Controls.Add(this.btnSave, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 399);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(561, 31);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel1, null);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = global::ERP_Budget.Common.Properties.Resources.disk_blue_ok;
            this.btnSave.Location = new System.Drawing.Point(403, 4);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "ОК";
            this.btnSave.ToolTipController = this.toolTipController;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = global::ERP_Budget.Common.Properties.Resources.delete2;
            this.btnCancel.Location = new System.Drawing.Point(484, 4);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.ToolTipController = this.toolTipController;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelInfo
            // 
            this.labelInfo.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelInfo.Appearance.Options.UseFont = true;
            this.labelInfo.Location = new System.Drawing.Point(3, 3);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(50, 13);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.Text = "labelInfo";
            // 
            // imglNodes
            // 
            this.imglNodes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglNodes.ImageStream")));
            this.imglNodes.TransparentColor = System.Drawing.Color.Magenta;
            this.imglNodes.Images.SetKeyName(0, "ok_16.png");
            this.imglNodes.Images.SetKeyName(1, "warning.png");
            // 
            // frmBudgetDepManagerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 430);
            this.Controls.Add(this.tableLayoutPanelBackGround);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "frmBudgetDepManagerList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.toolTipController.SetSuperTip(this, null);
            this.Text = "frmBudgetDepManagerList";
            this.tableLayoutPanelBackGround.ResumeLayout(false);
            this.tableLayoutPanelBackGround.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemChecked)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemCheckEdit_ReadOnly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemlkpBudgetDep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemUsersList)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelBackGround;
        private DevExpress.XtraEditors.LabelControl labelInfo;
        private DevExpress.XtraTreeList.TreeList treeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colManagerName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCheckManager;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repItemChecked;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCheckCoordinator;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repItemCheckEdit_ReadOnly;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repItemlkpBudgetDep;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repItemUsersList;
        private DevExpress.Utils.ToolTipController toolTipController;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.ImageList imglNodes;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCheckController;
    }
}