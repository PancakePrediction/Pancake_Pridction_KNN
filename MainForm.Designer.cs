
namespace Pancake_Pridction_KNN
{
    partial class MainForm
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
            this.rickTb = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.AuctionPage = new System.Windows.Forms.TabPage();
            this.topPanel1 = new System.Windows.Forms.Panel();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.BNB_Panel = new System.Windows.Forms.Panel();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel_min5True = new System.Windows.Forms.Panel();
            this.bottomPanel1 = new System.Windows.Forms.Panel();
            this.panel1_left_fortables = new System.Windows.Forms.Panel();
            this.groupBox311 = new System.Windows.Forms.GroupBox();
            this.panel_bigOrder_log = new System.Windows.Forms.Panel();
            this.Prediction_Table = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemRefreshOrderBooks = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.EnableAutobet_chkbox = new System.Windows.Forms.CheckBox();
            this.label_TimeTickDown = new System.Windows.Forms.Label();
            this.button_initBnb = new System.Windows.Forms.Button();
            this.button_double = new System.Windows.Forms.Button();
            this.button_ManualBull = new System.Windows.Forms.Button();
            this.bet_Amount_BNB_tbox = new System.Windows.Forms.TextBox();
            this.label_Balance = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.button_ManualBear = new System.Windows.Forms.Button();
            this.label_betsidt = new System.Windows.Forms.Label();
            this.OrderBooks_Table = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_PriceOffset = new System.Windows.Forms.Label();
            this.label_bnbUPUP = new System.Windows.Forms.Label();
            this.label_bnbDown = new System.Windows.Forms.Label();
            this.textBox_invervalVal = new System.Windows.Forms.TextBox();
            this.IntervalForOrders_checkbox = new System.Windows.Forms.CheckBox();
            this.label_LastPrice = new System.Windows.Forms.Label();
            this.label_lastLPTime = new System.Windows.Forms.Label();
            this.label_ChainLinkPrice = new System.Windows.Forms.Label();
            this.label_lockPrice = new System.Windows.Forms.Label();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Add1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Remove1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ClearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IndicatorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerForTickDown = new System.Windows.Forms.Timer(this.components);
            this.timer_for_orders = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.AuctionPage.SuspendLayout();
            this.topPanel1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage11.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.bottomPanel1.SuspendLayout();
            this.panel1_left_fortables.SuspendLayout();
            this.groupBox311.SuspendLayout();
            this.panel_bigOrder_log.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Prediction_Table)).BeginInit();
            this.contextMenuStrip3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OrderBooks_Table)).BeginInit();
            this.panel1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rickTb
            // 
            this.rickTb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rickTb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rickTb.Location = new System.Drawing.Point(0, 0);
            this.rickTb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rickTb.Name = "rickTb";
            this.rickTb.Size = new System.Drawing.Size(1160, 117);
            this.rickTb.TabIndex = 2;
            this.rickTb.Text = "";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.AuctionPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Arial", 9F);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1435, 673);
            this.tabControl1.TabIndex = 4;
            // 
            // AuctionPage
            // 
            this.AuctionPage.Controls.Add(this.topPanel1);
            this.AuctionPage.Controls.Add(this.bottomPanel1);
            this.AuctionPage.Controls.Add(this.panel1_left_fortables);
            this.AuctionPage.Location = new System.Drawing.Point(4, 24);
            this.AuctionPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AuctionPage.Name = "AuctionPage";
            this.AuctionPage.Size = new System.Drawing.Size(1427, 645);
            this.AuctionPage.TabIndex = 12;
            this.AuctionPage.Text = "PancakePredictor";
            this.AuctionPage.UseVisualStyleBackColor = true;
            // 
            // topPanel1
            // 
            this.topPanel1.Controls.Add(this.tabControl2);
            this.topPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topPanel1.Location = new System.Drawing.Point(0, 0);
            this.topPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.topPanel1.Name = "topPanel1";
            this.topPanel1.Size = new System.Drawing.Size(1160, 528);
            this.topPanel1.TabIndex = 6;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage11);
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Font = new System.Drawing.Font("Arial", 9F);
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1160, 528);
            this.tabControl2.TabIndex = 22;
            // 
            // tabPage11
            // 
            this.tabPage11.Controls.Add(this.BNB_Panel);
            this.tabPage11.Location = new System.Drawing.Point(4, 24);
            this.tabPage11.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage11.Size = new System.Drawing.Size(1152, 500);
            this.tabPage11.TabIndex = 3;
            this.tabPage11.Text = "Kline";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // BNB_Panel
            // 
            this.BNB_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BNB_Panel.Location = new System.Drawing.Point(3, 4);
            this.BNB_Panel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BNB_Panel.Name = "BNB_Panel";
            this.BNB_Panel.Size = new System.Drawing.Size(1146, 492);
            this.BNB_Panel.TabIndex = 23;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel_min5True);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(1152, 500);
            this.tabPage1.TabIndex = 4;
            this.tabPage1.Text = "binance Kline";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel_min5True
            // 
            this.panel_min5True.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_min5True.Location = new System.Drawing.Point(3, 4);
            this.panel_min5True.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel_min5True.Name = "panel_min5True";
            this.panel_min5True.Size = new System.Drawing.Size(1146, 492);
            this.panel_min5True.TabIndex = 24;
            // 
            // bottomPanel1
            // 
            this.bottomPanel1.Controls.Add(this.rickTb);
            this.bottomPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel1.Location = new System.Drawing.Point(0, 528);
            this.bottomPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bottomPanel1.Name = "bottomPanel1";
            this.bottomPanel1.Size = new System.Drawing.Size(1160, 117);
            this.bottomPanel1.TabIndex = 6;
            // 
            // panel1_left_fortables
            // 
            this.panel1_left_fortables.Controls.Add(this.groupBox311);
            this.panel1_left_fortables.Controls.Add(this.OrderBooks_Table);
            this.panel1_left_fortables.Controls.Add(this.panel1);
            this.panel1_left_fortables.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1_left_fortables.Location = new System.Drawing.Point(1160, 0);
            this.panel1_left_fortables.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1_left_fortables.Name = "panel1_left_fortables";
            this.panel1_left_fortables.Size = new System.Drawing.Size(267, 645);
            this.panel1_left_fortables.TabIndex = 7;
            // 
            // groupBox311
            // 
            this.groupBox311.Controls.Add(this.panel_bigOrder_log);
            this.groupBox311.Controls.Add(this.panel2);
            this.groupBox311.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox311.Font = new System.Drawing.Font("Arial", 9F);
            this.groupBox311.Location = new System.Drawing.Point(0, 296);
            this.groupBox311.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox311.Name = "groupBox311";
            this.groupBox311.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox311.Size = new System.Drawing.Size(267, 349);
            this.groupBox311.TabIndex = 13;
            this.groupBox311.TabStop = false;
            this.groupBox311.Text = "Setting";
            // 
            // panel_bigOrder_log
            // 
            this.panel_bigOrder_log.Controls.Add(this.Prediction_Table);
            this.panel_bigOrder_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_bigOrder_log.Location = new System.Drawing.Point(3, 288);
            this.panel_bigOrder_log.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel_bigOrder_log.Name = "panel_bigOrder_log";
            this.panel_bigOrder_log.Size = new System.Drawing.Size(261, 57);
            this.panel_bigOrder_log.TabIndex = 12;
            // 
            // Prediction_Table
            // 
            this.Prediction_Table.AllColumns.Add(this.olvColumn4);
            this.Prediction_Table.AllColumns.Add(this.olvColumn5);
            this.Prediction_Table.AllColumns.Add(this.olvColumn6);
            this.Prediction_Table.AllColumns.Add(this.olvColumn7);
            this.Prediction_Table.AllowDrop = true;
            this.Prediction_Table.BackColor = System.Drawing.Color.Black;
            this.Prediction_Table.CellEditUseWholeCell = false;
            this.Prediction_Table.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn4,
            this.olvColumn5,
            this.olvColumn6,
            this.olvColumn7});
            this.Prediction_Table.ContextMenuStrip = this.contextMenuStrip3;
            this.Prediction_Table.Cursor = System.Windows.Forms.Cursors.Default;
            this.Prediction_Table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Prediction_Table.Font = new System.Drawing.Font("Arial", 10F);
            this.Prediction_Table.FullRowSelect = true;
            this.Prediction_Table.GridLines = true;
            this.Prediction_Table.HideSelection = false;
            this.Prediction_Table.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Prediction_Table.IsSimpleDropSink = true;
            this.Prediction_Table.Location = new System.Drawing.Point(0, 0);
            this.Prediction_Table.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Prediction_Table.Name = "Prediction_Table";
            this.Prediction_Table.OverlayImage.Rotation = 10;
            this.Prediction_Table.OverlayText.Alignment = System.Drawing.ContentAlignment.TopLeft;
            this.Prediction_Table.OverlayText.Font = new System.Drawing.Font("Arial", 30F);
            this.Prediction_Table.OverlayText.InsetY = 100;
            this.Prediction_Table.OverlayText.Rotation = -20;
            this.Prediction_Table.OverlayText.Text = "";
            this.Prediction_Table.OverlayText.TextColor = System.Drawing.Color.LightPink;
            this.Prediction_Table.OverlayText.Transparency = 15;
            this.Prediction_Table.ShowGroups = false;
            this.Prediction_Table.ShowItemCountOnGroups = true;
            this.Prediction_Table.Size = new System.Drawing.Size(261, 57);
            this.Prediction_Table.SpaceBetweenGroups = 20;
            this.Prediction_Table.TabIndex = 16;
            this.Prediction_Table.UseCellFormatEvents = true;
            this.Prediction_Table.UseCompatibleStateImageBehavior = false;
            this.Prediction_Table.View = System.Windows.Forms.View.Details;
            this.Prediction_Table.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.PredictionTable_FormatRow);
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "RoundID";
            this.olvColumn4.MaximumWidth = 45;
            this.olvColumn4.MinimumWidth = 45;
            this.olvColumn4.Text = "ID";
            this.olvColumn4.Width = 45;
            this.olvColumn4.WordWrap = true;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "Prediction";
            this.olvColumn5.AspectToStringFormat = "";
            this.olvColumn5.MaximumWidth = 50;
            this.olvColumn5.MinimumWidth = 50;
            this.olvColumn5.Text = "KNN";
            this.olvColumn5.Width = 50;
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "Result";
            this.olvColumn6.AspectToStringFormat = "";
            this.olvColumn6.MaximumWidth = 60;
            this.olvColumn6.MinimumWidth = 60;
            this.olvColumn6.Text = "Result";
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "Time";
            this.olvColumn7.Text = "TIME";
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemRefreshOrderBooks});
            this.contextMenuStrip3.Name = "contextMenuStrip2";
            this.contextMenuStrip3.Size = new System.Drawing.Size(121, 26);
            // 
            // toolStripMenuItemRefreshOrderBooks
            // 
            this.toolStripMenuItemRefreshOrderBooks.Name = "toolStripMenuItemRefreshOrderBooks";
            this.toolStripMenuItemRefreshOrderBooks.Size = new System.Drawing.Size(120, 22);
            this.toolStripMenuItemRefreshOrderBooks.Text = "Refresh";
            this.toolStripMenuItemRefreshOrderBooks.Click += new System.EventHandler(this.toolStripMenuItemRefreshBooks_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.EnableAutobet_chkbox);
            this.panel2.Controls.Add(this.label_TimeTickDown);
            this.panel2.Controls.Add(this.button_initBnb);
            this.panel2.Controls.Add(this.button_double);
            this.panel2.Controls.Add(this.button_ManualBull);
            this.panel2.Controls.Add(this.bet_Amount_BNB_tbox);
            this.panel2.Controls.Add(this.label_Balance);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Controls.Add(this.button_ManualBear);
            this.panel2.Controls.Add(this.label_betsidt);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 18);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(261, 270);
            this.panel2.TabIndex = 11;
            // 
            // EnableAutobet_chkbox
            // 
            this.EnableAutobet_chkbox.AutoSize = true;
            this.EnableAutobet_chkbox.Font = new System.Drawing.Font("Arial", 9F);
            this.EnableAutobet_chkbox.Location = new System.Drawing.Point(3, 4);
            this.EnableAutobet_chkbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EnableAutobet_chkbox.Name = "EnableAutobet_chkbox";
            this.EnableAutobet_chkbox.Size = new System.Drawing.Size(106, 19);
            this.EnableAutobet_chkbox.TabIndex = 4;
            this.EnableAutobet_chkbox.Text = "EnableAutobet";
            this.EnableAutobet_chkbox.UseVisualStyleBackColor = true;
            this.EnableAutobet_chkbox.CheckedChanged += new System.EventHandler(this.KNN_Autobet_chkbox_CheckedChanged);
            // 
            // label_TimeTickDown
            // 
            this.label_TimeTickDown.AutoSize = true;
            this.label_TimeTickDown.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.label_TimeTickDown.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label_TimeTickDown.Location = new System.Drawing.Point(167, 99);
            this.label_TimeTickDown.Name = "label_TimeTickDown";
            this.label_TimeTickDown.Size = new System.Drawing.Size(85, 32);
            this.label_TimeTickDown.TabIndex = 9;
            this.label_TimeTickDown.Text = "02:30";
            // 
            // button_initBnb
            // 
            this.button_initBnb.BackColor = System.Drawing.Color.DarkGray;
            this.button_initBnb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_initBnb.ForeColor = System.Drawing.Color.White;
            this.button_initBnb.Location = new System.Drawing.Point(207, 32);
            this.button_initBnb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_initBnb.Name = "button_initBnb";
            this.button_initBnb.Size = new System.Drawing.Size(45, 26);
            this.button_initBnb.TabIndex = 0;
            this.button_initBnb.Text = "Re";
            this.button_initBnb.UseVisualStyleBackColor = false;
            this.button_initBnb.Click += new System.EventHandler(this.button_initBnb_Click);
            // 
            // button_double
            // 
            this.button_double.BackColor = System.Drawing.Color.Black;
            this.button_double.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_double.ForeColor = System.Drawing.Color.White;
            this.button_double.Location = new System.Drawing.Point(137, 32);
            this.button_double.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_double.Name = "button_double";
            this.button_double.Size = new System.Drawing.Size(64, 26);
            this.button_double.TabIndex = 0;
            this.button_double.Text = "+Double";
            this.button_double.UseVisualStyleBackColor = false;
            this.button_double.Click += new System.EventHandler(this.button_double_Click);
            // 
            // button_ManualBull
            // 
            this.button_ManualBull.BackColor = System.Drawing.Color.LimeGreen;
            this.button_ManualBull.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ManualBull.ForeColor = System.Drawing.Color.White;
            this.button_ManualBull.Location = new System.Drawing.Point(8, 137);
            this.button_ManualBull.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_ManualBull.Name = "button_ManualBull";
            this.button_ManualBull.Size = new System.Drawing.Size(244, 55);
            this.button_ManualBull.TabIndex = 0;
            this.button_ManualBull.Text = "Bet BULL";
            this.button_ManualBull.UseVisualStyleBackColor = false;
            this.button_ManualBull.Click += new System.EventHandler(this.button_ManualBull_Click);
            // 
            // bet_Amount_BNB_tbox
            // 
            this.bet_Amount_BNB_tbox.Location = new System.Drawing.Point(86, 35);
            this.bet_Amount_BNB_tbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bet_Amount_BNB_tbox.Name = "bet_Amount_BNB_tbox";
            this.bet_Amount_BNB_tbox.Size = new System.Drawing.Size(45, 21);
            this.bet_Amount_BNB_tbox.TabIndex = 3;
            this.bet_Amount_BNB_tbox.Text = "0.1";
            this.bet_Amount_BNB_tbox.TextChanged += new System.EventHandler(this.bet_Amount_BNB_tbox_TextChanged);
            // 
            // label_Balance
            // 
            this.label_Balance.AutoSize = true;
            this.label_Balance.Font = new System.Drawing.Font("Arial", 9F);
            this.label_Balance.Location = new System.Drawing.Point(3, 65);
            this.label_Balance.Name = "label_Balance";
            this.label_Balance.Size = new System.Drawing.Size(52, 15);
            this.label_Balance.TabIndex = 0;
            this.label_Balance.Text = "Balance";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Arial", 9F);
            this.label19.Location = new System.Drawing.Point(3, 36);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(77, 15);
            this.label19.TabIndex = 0;
            this.label19.Text = "Amount BNB";
            // 
            // button_ManualBear
            // 
            this.button_ManualBear.BackColor = System.Drawing.Color.Red;
            this.button_ManualBear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ManualBear.ForeColor = System.Drawing.Color.White;
            this.button_ManualBear.Location = new System.Drawing.Point(8, 197);
            this.button_ManualBear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_ManualBear.Name = "button_ManualBear";
            this.button_ManualBear.Size = new System.Drawing.Size(244, 54);
            this.button_ManualBear.TabIndex = 0;
            this.button_ManualBear.Text = "Bet BEAR";
            this.button_ManualBear.UseVisualStyleBackColor = false;
            this.button_ManualBear.Click += new System.EventHandler(this.button_ManualBear_Click);
            // 
            // label_betsidt
            // 
            this.label_betsidt.AutoSize = true;
            this.label_betsidt.Font = new System.Drawing.Font("Arial", 28F, System.Drawing.FontStyle.Bold);
            this.label_betsidt.ForeColor = System.Drawing.Color.LimeGreen;
            this.label_betsidt.Location = new System.Drawing.Point(5, 89);
            this.label_betsidt.Name = "label_betsidt";
            this.label_betsidt.Size = new System.Drawing.Size(126, 45);
            this.label_betsidt.TabIndex = 9;
            this.label_betsidt.Text = "BEAR";
            // 
            // OrderBooks_Table
            // 
            this.OrderBooks_Table.AllColumns.Add(this.olvColumn1);
            this.OrderBooks_Table.AllColumns.Add(this.olvColumn3);
            this.OrderBooks_Table.AllColumns.Add(this.olvColumn2);
            this.OrderBooks_Table.AllowDrop = true;
            this.OrderBooks_Table.BackColor = System.Drawing.Color.Azure;
            this.OrderBooks_Table.CellEditUseWholeCell = false;
            this.OrderBooks_Table.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn3,
            this.olvColumn2});
            this.OrderBooks_Table.ContextMenuStrip = this.contextMenuStrip3;
            this.OrderBooks_Table.Cursor = System.Windows.Forms.Cursors.Default;
            this.OrderBooks_Table.Dock = System.Windows.Forms.DockStyle.Top;
            this.OrderBooks_Table.Font = new System.Drawing.Font("Arial", 10F);
            this.OrderBooks_Table.FullRowSelect = true;
            this.OrderBooks_Table.GridLines = true;
            this.OrderBooks_Table.HideSelection = false;
            this.OrderBooks_Table.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.OrderBooks_Table.IsSimpleDropSink = true;
            this.OrderBooks_Table.Location = new System.Drawing.Point(0, 137);
            this.OrderBooks_Table.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OrderBooks_Table.Name = "OrderBooks_Table";
            this.OrderBooks_Table.OverlayImage.Rotation = 10;
            this.OrderBooks_Table.OverlayText.Alignment = System.Drawing.ContentAlignment.TopLeft;
            this.OrderBooks_Table.OverlayText.Font = new System.Drawing.Font("Arial", 30F);
            this.OrderBooks_Table.OverlayText.InsetY = 100;
            this.OrderBooks_Table.OverlayText.Rotation = -20;
            this.OrderBooks_Table.OverlayText.Text = "";
            this.OrderBooks_Table.OverlayText.TextColor = System.Drawing.Color.LightPink;
            this.OrderBooks_Table.OverlayText.Transparency = 15;
            this.OrderBooks_Table.ShowGroups = false;
            this.OrderBooks_Table.ShowItemCountOnGroups = true;
            this.OrderBooks_Table.Size = new System.Drawing.Size(267, 159);
            this.OrderBooks_Table.SpaceBetweenGroups = 20;
            this.OrderBooks_Table.TabIndex = 15;
            this.OrderBooks_Table.UseCellFormatEvents = true;
            this.OrderBooks_Table.UseCompatibleStateImageBehavior = false;
            this.OrderBooks_Table.View = System.Windows.Forms.View.Details;
            this.OrderBooks_Table.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.Pankou_Table_FormatRow);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Level";
            this.olvColumn1.MaximumWidth = 45;
            this.olvColumn1.MinimumWidth = 45;
            this.olvColumn1.Text = "Lvl";
            this.olvColumn1.Width = 45;
            this.olvColumn1.WordWrap = true;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Price";
            this.olvColumn3.AspectToStringFormat = "{0:f2}";
            this.olvColumn3.MaximumWidth = 70;
            this.olvColumn3.MinimumWidth = 70;
            this.olvColumn3.Text = "Price";
            this.olvColumn3.Width = 70;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "OrderDiff";
            this.olvColumn2.AspectToStringFormat = "{0:f2}%";
            this.olvColumn2.MaximumWidth = 70;
            this.olvColumn2.MinimumWidth = 70;
            this.olvColumn2.Text = "Diff";
            this.olvColumn2.Width = 70;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_PriceOffset);
            this.panel1.Controls.Add(this.label_bnbUPUP);
            this.panel1.Controls.Add(this.label_bnbDown);
            this.panel1.Controls.Add(this.textBox_invervalVal);
            this.panel1.Controls.Add(this.IntervalForOrders_checkbox);
            this.panel1.Controls.Add(this.label_LastPrice);
            this.panel1.Controls.Add(this.label_lastLPTime);
            this.panel1.Controls.Add(this.label_ChainLinkPrice);
            this.panel1.Controls.Add(this.label_lockPrice);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 137);
            this.panel1.TabIndex = 16;
            // 
            // label_PriceOffset
            // 
            this.label_PriceOffset.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.label_PriceOffset.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label_PriceOffset.Location = new System.Drawing.Point(97, 81);
            this.label_PriceOffset.Name = "label_PriceOffset";
            this.label_PriceOffset.Size = new System.Drawing.Size(99, 34);
            this.label_PriceOffset.TabIndex = 9;
            this.label_PriceOffset.Text = "-0.00%";
            this.label_PriceOffset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_bnbUPUP
            // 
            this.label_bnbUPUP.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label_bnbUPUP.ForeColor = System.Drawing.Color.Chocolate;
            this.label_bnbUPUP.Location = new System.Drawing.Point(111, 60);
            this.label_bnbUPUP.Name = "label_bnbUPUP";
            this.label_bnbUPUP.Size = new System.Drawing.Size(108, 20);
            this.label_bnbUPUP.TabIndex = 9;
            this.label_bnbUPUP.Text = "UP: 000.1";
            // 
            // label_bnbDown
            // 
            this.label_bnbDown.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label_bnbDown.ForeColor = System.Drawing.Color.Green;
            this.label_bnbDown.Location = new System.Drawing.Point(111, 37);
            this.label_bnbDown.Name = "label_bnbDown";
            this.label_bnbDown.Size = new System.Drawing.Size(108, 23);
            this.label_bnbDown.TabIndex = 9;
            this.label_bnbDown.Text = "Down: 000.1";
            // 
            // textBox_invervalVal
            // 
            this.textBox_invervalVal.Font = new System.Drawing.Font("Arial", 9F);
            this.textBox_invervalVal.Location = new System.Drawing.Point(78, 10);
            this.textBox_invervalVal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_invervalVal.Name = "textBox_invervalVal";
            this.textBox_invervalVal.Size = new System.Drawing.Size(70, 21);
            this.textBox_invervalVal.TabIndex = 3;
            this.textBox_invervalVal.Text = "2000";
            this.textBox_invervalVal.TextChanged += new System.EventHandler(this.textBox_invervalVal_TextChanged);
            // 
            // IntervalForOrders_checkbox
            // 
            this.IntervalForOrders_checkbox.AutoSize = true;
            this.IntervalForOrders_checkbox.Checked = true;
            this.IntervalForOrders_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IntervalForOrders_checkbox.Font = new System.Drawing.Font("Arial", 9F);
            this.IntervalForOrders_checkbox.Location = new System.Drawing.Point(7, 12);
            this.IntervalForOrders_checkbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.IntervalForOrders_checkbox.Name = "IntervalForOrders_checkbox";
            this.IntervalForOrders_checkbox.Size = new System.Drawing.Size(65, 19);
            this.IntervalForOrders_checkbox.TabIndex = 4;
            this.IntervalForOrders_checkbox.Text = "Interval";
            this.IntervalForOrders_checkbox.UseVisualStyleBackColor = true;
            this.IntervalForOrders_checkbox.CheckedChanged += new System.EventHandler(this.IntervalForOrders_checkbox_CheckedChanged);
            // 
            // label_LastPrice
            // 
            this.label_LastPrice.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.label_LastPrice.ForeColor = System.Drawing.Color.Black;
            this.label_LastPrice.Location = new System.Drawing.Point(13, 71);
            this.label_LastPrice.Name = "label_LastPrice";
            this.label_LastPrice.Size = new System.Drawing.Size(103, 22);
            this.label_LastPrice.TabIndex = 9;
            this.label_LastPrice.Text = "cp: 518.44";
            // 
            // label_lastLPTime
            // 
            this.label_lastLPTime.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.label_lastLPTime.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label_lastLPTime.Location = new System.Drawing.Point(13, 111);
            this.label_lastLPTime.Name = "label_lastLPTime";
            this.label_lastLPTime.Size = new System.Drawing.Size(162, 22);
            this.label_lastLPTime.TabIndex = 9;
            this.label_lastLPTime.Text = "5sec ago";
            // 
            // label_ChainLinkPrice
            // 
            this.label_ChainLinkPrice.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.label_ChainLinkPrice.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label_ChainLinkPrice.Location = new System.Drawing.Point(12, 91);
            this.label_ChainLinkPrice.Name = "label_ChainLinkPrice";
            this.label_ChainLinkPrice.Size = new System.Drawing.Size(103, 22);
            this.label_ChainLinkPrice.TabIndex = 9;
            this.label_ChainLinkPrice.Text = "Lp: 518.44";
            // 
            // label_lockPrice
            // 
            this.label_lockPrice.AutoSize = true;
            this.label_lockPrice.Font = new System.Drawing.Font("Arial", 19F, System.Drawing.FontStyle.Bold);
            this.label_lockPrice.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label_lockPrice.Location = new System.Drawing.Point(9, 37);
            this.label_lockPrice.Name = "label_lockPrice";
            this.label_lockPrice.Size = new System.Drawing.Size(90, 30);
            this.label_lockPrice.TabIndex = 9;
            this.label_lockPrice.Text = "518.44";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Add1ToolStripMenuItem,
            this.Remove1ToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(124, 48);
            // 
            // Add1ToolStripMenuItem
            // 
            this.Add1ToolStripMenuItem.Name = "Add1ToolStripMenuItem";
            this.Add1ToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.Add1ToolStripMenuItem.Text = "Add1";
            // 
            // Remove1ToolStripMenuItem
            // 
            this.Remove1ToolStripMenuItem.Name = "Remove1ToolStripMenuItem";
            this.Remove1ToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.Remove1ToolStripMenuItem.Text = "Remove";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClearToolStripMenuItem,
            this.IndicatorsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(135, 48);
            this.contextMenuStrip1.Opened += new System.EventHandler(this.contextMenuStrip1_Opened);
            // 
            // ClearToolStripMenuItem
            // 
            this.ClearToolStripMenuItem.Name = "ClearToolStripMenuItem";
            this.ClearToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.ClearToolStripMenuItem.Text = "Clear";
            // 
            // IndicatorsToolStripMenuItem
            // 
            this.IndicatorsToolStripMenuItem.Name = "IndicatorsToolStripMenuItem";
            this.IndicatorsToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.IndicatorsToolStripMenuItem.Text = "Indicators";
            this.IndicatorsToolStripMenuItem.Click += new System.EventHandler(this.ChangeIndicatorToolStripMenuItem_Click);
            // 
            // timerForTickDown
            // 
            this.timerForTickDown.Interval = 1000;
            this.timerForTickDown.Tick += new System.EventHandler(this.timerForTickDown_Tick);
            // 
            // timer_for_orders
            // 
            this.timer_for_orders.Enabled = true;
            this.timer_for_orders.Interval = 500;
            this.timer_for_orders.Tick += new System.EventHandler(this.timer_for_orders_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1435, 673);
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "Pancake_Predictor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.AuctionPage.ResumeLayout(false);
            this.topPanel1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage11.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.bottomPanel1.ResumeLayout(false);
            this.panel1_left_fortables.ResumeLayout(false);
            this.groupBox311.ResumeLayout(false);
            this.panel_bigOrder_log.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Prediction_Table)).EndInit();
            this.contextMenuStrip3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OrderBooks_Table)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Timer timerForTickDown;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ClearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem IndicatorsToolStripMenuItem;
        private System.Windows.Forms.TabPage AuctionPage;
        private System.Windows.Forms.Panel bottomPanel1;
        private System.Windows.Forms.GroupBox groupBox311;

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem Add1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Remove1ToolStripMenuItem;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel topPanel1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage11;
        private System.Windows.Forms.Panel BNB_Panel;
        private System.Windows.Forms.TextBox bet_Amount_BNB_tbox;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRefreshOrderBooks;
        private System.Windows.Forms.Panel panel1_left_fortables;
        private System.Windows.Forms.CheckBox IntervalForOrders_checkbox;
        private System.Windows.Forms.TextBox textBox_invervalVal;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer_for_orders;
        private System.Windows.Forms.Label label_betsidt;
        public System.Windows.Forms.RichTextBox rickTb;
        private System.Windows.Forms.Button button_ManualBear;
        private System.Windows.Forms.Button button_ManualBull;
        private System.Windows.Forms.Label label_TimeTickDown;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox EnableAutobet_chkbox;
        private System.Windows.Forms.Label label_lockPrice;
        private System.Windows.Forms.Button button_double;
        private System.Windows.Forms.Button button_initBnb;
        private System.Windows.Forms.Panel panel_bigOrder_log;
        private System.Windows.Forms.Label label_ChainLinkPrice;
        private System.Windows.Forms.Label label_LastPrice;
        private System.Windows.Forms.Label label_PriceOffset;
        private System.Windows.Forms.Label label_Balance;
        private System.Windows.Forms.Panel panel_min5True;
        private System.Windows.Forms.Label label_lastLPTime;
        private BrightIdeasSoftware.ObjectListView Prediction_Table;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
        private System.Windows.Forms.Label label_bnbUPUP;
        private System.Windows.Forms.Label label_bnbDown;
        private BrightIdeasSoftware.ObjectListView OrderBooks_Table;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
    }
}