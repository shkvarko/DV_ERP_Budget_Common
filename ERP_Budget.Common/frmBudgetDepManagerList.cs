using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ERP_Budget.Common
{
    public enum enModeManagerList
    {
        Unkown = -1,
        BudgetDep = 0,
        Budget = 1
    }
    
    public partial class frmBudgetDepManagerList : DevExpress.XtraEditors.XtraForm
    {
        #region Переменные, Свойства, Константы
        
        private UniXP.Common.CProfile m_objProfile;
        public System.Guid DocID { get; set; }
        private List<CUser> m_objManagerList;
        private enModeManagerList modeManagerList;
        private const string strCaptionForm = "Согласователи и дополнительные распорядители";
        #endregion

        #region Конструктор
        public frmBudgetDepManagerList(UniXP.Common.CProfile objProfile)
        {
            InitializeComponent();

            m_objProfile = objProfile;
            DocID = System.Guid.Empty;
            m_objManagerList = null;
            modeManagerList = enModeManagerList.Unkown;
        }
        #endregion

        #region Права на бюджетное подразделение
        /// <summary>
        /// Обновляет список распорядителей и согласователей бюджетного подразделения
        /// </summary>
        /// <param name="uuidBudgetDetID">УИ бюджетного подразделения</param>
        /// <param name="strBudgetDepName">наименование бюджетного подразделения</param>
        /// <param name="objManagerList">список распорядителей и согласователей</param>
        public void LoadBudgetDepManagerList(System.Guid uuidBudgetDetID, System.String strBudgetDepName )
        {
            System.String strErr = System.String.Empty;
            try
            {
                modeManagerList = enModeManagerList.BudgetDep;
                DocID = uuidBudgetDetID;
                labelInfo.Text = strBudgetDepName;
                this.Text = strCaptionForm;

                treeList.Nodes.Clear();

                m_objManagerList = CBudgetDep.GetBudgetDepAdvManagerList(m_objProfile, DocID, ref strErr);
                
                if ((m_objManagerList != null) && (m_objManagerList.Count > 0))
                {
                    foreach (CUser objUser in m_objManagerList)
                    {
                        //добавляем узел в дерево
                        DevExpress.XtraTreeList.Nodes.TreeListNode objNode =
                            treeList.AppendNode(new object[] { objUser.UserFullName, objUser.IsBudgetDepManager, 
                                objUser.IsBudgetDepCoordinator, objUser.IsBudgetDepController }, null);

                        objNode.Tag = objUser;
                    }
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(strErr, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка обновления списка распорядителей и согласователей.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            return;

        }

        #endregion

        #region Права на бюджет
        /// <summary>
        /// Обновляет список распорядителей и согласователей бюджета
        /// </summary>
        /// <param name="uuidBudgetID">УИ бюджета</param>
        /// <param name="strBudgetName">наименование бюджета</param>
        /// <param name="objManagerList">список распорядителей и согласователей</param>
        public void LoadBudgetManagerList(System.Guid uuidBudgetID, System.String strBudgetName)
        {
            System.String strErr = System.String.Empty;
            try
            {
                modeManagerList = enModeManagerList.Budget;
                DocID = uuidBudgetID;
                labelInfo.Text = strBudgetName;
                this.Text = strCaptionForm;

                treeList.Nodes.Clear();

                m_objManagerList = CBudget.GetBudgetAdvManagerList(m_objProfile, DocID, ref strErr);

                if ((m_objManagerList != null) && (m_objManagerList.Count > 0))
                {
                    foreach (CUser objUser in m_objManagerList)
                    {
                        //добавляем узел в дерево
                        DevExpress.XtraTreeList.Nodes.TreeListNode objNode =
                            treeList.AppendNode(new object[] { objUser.UserFullName, objUser.IsBudgetDepManager, 
                                objUser.IsBudgetDepCoordinator, objUser.IsBudgetDepController }, null);

                        objNode.Tag = objUser;
                    }
                    ShowDialog();
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(strErr, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                    this.Close();
                }

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка обновления списка распорядителей и согласователей.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            return;
        }
        #endregion

        #region Сохранение изменений

        private System.Boolean SaveManagerList( ref System.String strErr )
        {
            System.Boolean bRet = false;
            try
            {
                if( ( treeList.Nodes.Count == 0 ) || ( m_objManagerList == null ) || ( m_objManagerList.Count == 0 ) )
                {
                    return bRet;
                }

                CUser objUser = null;
                System.Boolean bUserCanBeManager = false;
                System.Boolean bUserCanBeCoordinator = false;
                System.Boolean bUserCanBeController = false;
                
                System.Boolean bIsCoordinator = false;
                System.Boolean bIsManager = false;
                System.Boolean bIsController = false;
                
                List<CUser> objUserListForSave = new List<CUser>();

                foreach( DevExpress.XtraTreeList.Nodes.TreeListNode objNode in treeList.Nodes)
                {
                    if (objNode.Tag == null) { continue; }

                    objUser = (CUser)objNode.Tag;
                    objUser.IsBudgetDepCoordinator = false;
                    objUser.IsBudgetDepManager = false;
                    objUser.IsBudgetDepController = false;

                    bUserCanBeManager = objUser.DynamicRightsList.FindByName( ERP_Budget.Global.Consts.strDRManager ).IsEnable;
                    bUserCanBeCoordinator = objUser.DynamicRightsList.FindByName( ERP_Budget.Global.Consts.strDRCoordinator ).IsEnable;
                    bUserCanBeController = objUser.DynamicRightsList.FindByName(ERP_Budget.Global.Consts.strDRInspector).IsEnable;

                    bIsManager = System.Convert.ToBoolean(objNode.GetValue(colCheckManager));
                    bIsCoordinator = System.Convert.ToBoolean(objNode.GetValue(colCheckCoordinator));
                    bIsController = System.Convert.ToBoolean(objNode.GetValue(colCheckController));

                    objUser.IsBudgetDepManager = ((bIsManager == true) && (bUserCanBeManager == true));
                    objUser.IsBudgetDepCoordinator = ((bIsCoordinator == true) && (bUserCanBeCoordinator == true));
                    objUser.IsBudgetDepController = ((bIsController == true) && (bUserCanBeController == true));

                    if (objUser.IsBudgetDepCoordinator || objUser.IsBudgetDepManager || objUser.IsBudgetDepController )
                    {
                        objUserListForSave.Add( objUser );
                    }
                }

                if( modeManagerList == enModeManagerList.BudgetDep )
                {
                    bRet = CBudgetDep.SaveBudgetDepManagerList(m_objProfile, DocID, objUserListForSave, ref strErr);
                }
                else if (modeManagerList == enModeManagerList.Budget)
                {
                    bRet = CBudget.SaveBudgetManagerList(m_objProfile, DocID, objUserListForSave, ref strErr);
                }

            }
            catch (System.Exception f)
            {
                strErr += ("\nОшибка сохранения изменений в базе данных.\n" + f.Message);
            }
            finally
            {
            }
            return bRet;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                System.String strErr = System.String.Empty;

                System.Boolean bRes = SaveManagerList(ref strErr); 

                if (bRes == true)
                {
                    this.Close();
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(strErr, "Ошибка",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("btnSave_Click.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }
        #endregion

        #region Отмена изменений

        private void CancelChanges()
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                CancelChanges();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("btnCancel_Click.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }
        #endregion

        #region Отображение ячеек списка

        private void treeList_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            try
            {
                bool isFocusedCell = (e.Column == treeList.FocusedColumn && e.Node == treeList.FocusedNode);
                Brush brush = null;
                Rectangle r = e.Bounds;

                if ((e.Column == colCheckManager) || (e.Column == colCheckCoordinator) || (e.Column == colCheckController))
                {
                    CUser objUser = ( ( e.Node.Tag == null ) ? null : (CUser)e.Node.Tag );

                    System.Boolean HasRightDepManager = ( ( objUser == null ) ? false : objUser.DynamicRightsList.FindByName( Global.Consts.strDRManager ).IsEnable );
                    System.Boolean HasRightCoordinator = ((objUser == null) ? false : objUser.DynamicRightsList.FindByName(Global.Consts.strDRCoordinator).IsEnable);
                    System.Boolean HasRightController = ((objUser == null) ? false : objUser.DynamicRightsList.FindByName(Global.Consts.strDRInspector).IsEnable);
                    System.Boolean IsBlocked = ((objUser == null) ? false : objUser.IsBlocked);

                    if ((e.Column == colCheckManager) && ((HasRightDepManager == false) || (IsBlocked == true)))
                    {
                        brush = Brushes.Gray;
                    }

                    if ((e.Column == colCheckCoordinator) && ((HasRightCoordinator == false) || (IsBlocked == true)))
                    {
                        brush = Brushes.Gray;
                    }

                    if ((e.Column == colCheckController) && ((HasRightController == false) || (IsBlocked == true)))
                    {
                        brush = Brushes.Gray;
                    }
                }

                if( brush != null ) 
                {
                    e.Graphics.FillRectangle( brush, r );

                    if( isFocusedCell == true )
                    {
                        DevExpress.Utils.Paint.XPaint.Graphics.DrawFocusRectangle(e.Graphics, e.Bounds, SystemColors.WindowText, e.Appearance.BackColor);
                    }
                    e.Handled = true;
                }

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("treeList_CustomDrawNodeCell.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }

        private void treeList_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
        }

        private void treeList_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            try
            {
                if ((e.Column == colCheckManager) || (e.Column == colCheckCoordinator))
                {
                    CUser objUser = ((e.Node.Tag == null) ? null : (CUser)e.Node.Tag);

                    System.Boolean HasRightDepManager = ((objUser == null) ? false : objUser.DynamicRightsList.FindByName(Global.Consts.strDRManager).IsEnable);
                    System.Boolean HasRightCoordinator = ((objUser == null) ? false : objUser.DynamicRightsList.FindByName(Global.Consts.strDRCoordinator).IsEnable);
                    System.Boolean HasRightController = ((objUser == null) ? false : objUser.DynamicRightsList.FindByName(Global.Consts.strDRInspector).IsEnable);

                    System.Boolean IsBlocked = ((objUser == null) ? false : objUser.IsBlocked);

                    if ((e.Column == colCheckManager) && ((HasRightDepManager == false) || (IsBlocked == true)))
                    {
                        if (System.Convert.ToBoolean(e.Value) == true)
                        {
                            e.Node.SetValue(colCheckManager, false);
                        }
                    }

                    if ((e.Column == colCheckCoordinator) && ((HasRightCoordinator == false) || (IsBlocked == true)))
                    {
                        if (System.Convert.ToBoolean(e.Value) == true)
                        {
                            e.Node.SetValue(colCheckCoordinator, false);
                        }
                    }

                    if ((e.Column == colCheckController) && ((HasRightController == false) || (IsBlocked == true)))
                    {
                        if (System.Convert.ToBoolean(e.Value) == true)
                        {
                            e.Node.SetValue(colCheckController, false);
                        }
                    }

                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("treeList_CellValueChanging.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }

        private void treeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            try
            {
                CUser objUser = ((e.Node.Tag == null) ? null : (CUser)e.Node.Tag);

                System.Boolean HasRightDepManager = ((objUser == null) ? false : objUser.DynamicRightsList.FindByName(Global.Consts.strDRManager).IsEnable);
                System.Boolean HasRightCoordinator = ((objUser == null) ? false : objUser.DynamicRightsList.FindByName(Global.Consts.strDRCoordinator).IsEnable);
                System.Boolean HasRightController = ((objUser == null) ? false : objUser.DynamicRightsList.FindByName(Global.Consts.strDRInspector).IsEnable);

                System.Boolean IsBlocked = ((objUser == null) ? false : objUser.IsBlocked);

                colCheckManager.OptionsColumn.AllowFocus = ((HasRightDepManager == true) && (IsBlocked == false));

                colCheckCoordinator.OptionsColumn.AllowFocus = ((HasRightCoordinator == true) && (IsBlocked == false));

                colCheckController.OptionsColumn.AllowFocus = ((HasRightController == true) && (IsBlocked == false));
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("treeList_FocusedNodeChanged.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }

        #endregion

    }
}
