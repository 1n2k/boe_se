namespace BMonGWUI
{
    partial class MainW
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainW));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Rüstung", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Rucksack", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Verbrauchsartikel", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Kontainer", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Werkmaterialien", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("(E) Gathering", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup7 = new System.Windows.Forms.ListViewGroup("(E) Gizmo", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup8 = new System.Windows.Forms.ListViewGroup("Mini", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup9 = new System.Windows.Forms.ListViewGroup("Werkzeuge", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup10 = new System.Windows.Forms.ListViewGroup("(E) Trinket", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup11 = new System.Windows.Forms.ListViewGroup("Trophäen", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup12 = new System.Windows.Forms.ListViewGroup("(E) Upgrade Component", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup13 = new System.Windows.Forms.ListViewGroup("Waffen", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Testprodukt 1", 2);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Testprodukt 2", 0);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Testprodukt 3", 1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Testprodukt 4", 3);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chartMain = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.listViewZeit = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListTendenzen = new System.Windows.Forms.ImageList(this.components);
            this.listViewProdukte = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tRefreshDateTime = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButtonGroupsVisible = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRefreshProducts = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRefreshAnalytics = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRefreshGraph = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.doubleTrackBarAnalyticsRange = new BMonGWUI.DoubleTrackBar();
            this.toolStripButtonOpenFavorites = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 194F));
            this.tableLayoutPanel1.Controls.Add(this.chartMain, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.listViewZeit, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.listViewProdukte, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.doubleTrackBarAnalyticsRange, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1015, 391);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // chartMain
            // 
            chartArea1.Name = "ChartArea1";
            this.chartMain.ChartAreas.Add(chartArea1);
            this.chartMain.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartMain.Legends.Add(legend1);
            this.chartMain.Location = new System.Drawing.Point(178, 44);
            this.chartMain.Name = "chartMain";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartMain.Series.Add(series1);
            this.chartMain.Size = new System.Drawing.Size(640, 344);
            this.chartMain.TabIndex = 3;
            this.chartMain.Text = "chart1";
            // 
            // listViewZeit
            // 
            this.listViewZeit.AllowColumnReorder = true;
            this.listViewZeit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewZeit.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader1});
            this.listViewZeit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewZeit.LargeImageList = this.imageListTendenzen;
            this.listViewZeit.Location = new System.Drawing.Point(824, 44);
            this.listViewZeit.Name = "listViewZeit";
            this.listViewZeit.Size = new System.Drawing.Size(188, 344);
            this.listViewZeit.SmallImageList = this.imageListTendenzen;
            this.listViewZeit.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.listViewZeit.TabIndex = 2;
            this.listViewZeit.UseCompatibleStateImageBehavior = false;
            this.listViewZeit.View = System.Windows.Forms.View.Details;
            this.listViewZeit.SelectedIndexChanged += new System.EventHandler(this.listViewZeit_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Zeitpunkt";
            this.columnHeader2.Width = 97;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Ankaufen";
            this.columnHeader3.Width = 45;
            // 
            // imageListTendenzen
            // 
            this.imageListTendenzen.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTendenzen.ImageStream")));
            this.imageListTendenzen.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTendenzen.Images.SetKeyName(0, "tendenz_neutral.png");
            this.imageListTendenzen.Images.SetKeyName(1, "tendenz_stagnierend.png");
            this.imageListTendenzen.Images.SetKeyName(2, "tendenz_steigend.png");
            this.imageListTendenzen.Images.SetKeyName(3, "tendenz_unbekannt.png");
            // 
            // listViewProdukte
            // 
            this.listViewProdukte.AllowColumnReorder = true;
            this.listViewProdukte.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewProdukte.CausesValidation = false;
            this.listViewProdukte.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5});
            this.listViewProdukte.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup1.Header = "Rüstung";
            listViewGroup1.Name = "ListViewGroupArmor";
            listViewGroup2.Header = "Rucksack";
            listViewGroup2.Name = "listViewGroupBag";
            listViewGroup3.Header = "Verbrauchsartikel";
            listViewGroup3.Name = "listViewGroupConsumable";
            listViewGroup4.Header = "Kontainer";
            listViewGroup4.Name = "listViewGroupContainer";
            listViewGroup5.Header = "Werkmaterialien";
            listViewGroup5.Name = "listViewGroupCrafting";
            listViewGroup6.Header = "(E) Gathering";
            listViewGroup6.Name = "listViewGroupGathering";
            listViewGroup7.Header = "(E) Gizmo";
            listViewGroup7.Name = "listViewGroupGizmo";
            listViewGroup8.Header = "Mini";
            listViewGroup8.Name = "listViewGroupMini";
            listViewGroup9.Header = "Werkzeuge";
            listViewGroup9.Name = "listViewGroupTool";
            listViewGroup10.Header = "(E) Trinket";
            listViewGroup10.Name = "listViewGroupTrinket";
            listViewGroup11.Header = "Trophäen";
            listViewGroup11.Name = "listViewGroupTrophy";
            listViewGroup12.Header = "(E) Upgrade Component";
            listViewGroup12.Name = "listViewGroupUpgrade_Component";
            listViewGroup13.Header = "Waffen";
            listViewGroup13.Name = "listViewGroupWeapon";
            this.listViewProdukte.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4,
            listViewGroup5,
            listViewGroup6,
            listViewGroup7,
            listViewGroup8,
            listViewGroup9,
            listViewGroup10,
            listViewGroup11,
            listViewGroup12,
            listViewGroup13});
            this.listViewProdukte.HideSelection = false;
            listViewItem1.Group = listViewGroup1;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.Group = listViewGroup1;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.Group = listViewGroup9;
            listViewItem3.StateImageIndex = 0;
            listViewItem4.Group = listViewGroup13;
            listViewItem4.StateImageIndex = 0;
            this.listViewProdukte.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4});
            this.listViewProdukte.LargeImageList = this.imageListTendenzen;
            this.listViewProdukte.Location = new System.Drawing.Point(3, 44);
            this.listViewProdukte.MultiSelect = false;
            this.listViewProdukte.Name = "listViewProdukte";
            this.listViewProdukte.Size = new System.Drawing.Size(169, 344);
            this.listViewProdukte.SmallImageList = this.imageListTendenzen;
            this.listViewProdukte.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewProdukte.TabIndex = 1;
            this.listViewProdukte.UseCompatibleStateImageBehavior = false;
            this.listViewProdukte.View = System.Windows.Forms.View.Details;
            this.listViewProdukte.SelectedIndexChanged += new System.EventHandler(this.listViewProdukte_SelectedIndexChanged);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Produkt";
            this.columnHeader5.Width = 166;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonGroupsVisible,
            this.toolStripButtonOpenFavorites,
            this.toolStripButtonRefreshProducts,
            this.toolStripButtonRefreshAnalytics,
            this.toolStripButtonRefreshGraph,
            this.tRefreshDateTime,
            this.toolStripButtonSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1015, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tRefreshDateTime
            // 
            this.tRefreshDateTime.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tRefreshDateTime.Name = "tRefreshDateTime";
            this.tRefreshDateTime.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tRefreshDateTime.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripButtonGroupsVisible
            // 
            this.toolStripButtonGroupsVisible.Checked = true;
            this.toolStripButtonGroupsVisible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonGroupsVisible.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonGroupsVisible.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonGroupsVisible.Image")));
            this.toolStripButtonGroupsVisible.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonGroupsVisible.Name = "toolStripButtonGroupsVisible";
            this.toolStripButtonGroupsVisible.Size = new System.Drawing.Size(120, 22);
            this.toolStripButtonGroupsVisible.Text = "Produkte &gruppieren";
            this.toolStripButtonGroupsVisible.ToolTipText = "Gibt an, ob Produkte auf basis ihrer Kategorie in Gruppen angezeigt werden sollen" +
    ".";
            this.toolStripButtonGroupsVisible.Click += new System.EventHandler(this.toolStripButtonGroupsVisible_Click);
            // 
            // toolStripButtonRefreshProducts
            // 
            this.toolStripButtonRefreshProducts.Image = global::BMonGWUI.Properties.Resources.refresh;
            this.toolStripButtonRefreshProducts.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.toolStripButtonRefreshProducts.Name = "toolStripButtonRefreshProducts";
            this.toolStripButtonRefreshProducts.Size = new System.Drawing.Size(90, 22);
            this.toolStripButtonRefreshProducts.Text = "Produktliste";
            this.toolStripButtonRefreshProducts.ToolTipText = "Ruft die aktuellen Daten aus dem Internet ab und aktualisiert alle Berechnungen, " +
    "die durch die neuen Daten ungültig geworden sind.";
            this.toolStripButtonRefreshProducts.Click += new System.EventHandler(this.toolStripButtonRefresh_ButtonClick);
            // 
            // toolStripButtonRefreshAnalytics
            // 
            this.toolStripButtonRefreshAnalytics.Image = global::BMonGWUI.Properties.Resources.refresh;
            this.toolStripButtonRefreshAnalytics.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefreshAnalytics.Name = "toolStripButtonRefreshAnalytics";
            this.toolStripButtonRefreshAnalytics.Size = new System.Drawing.Size(84, 22);
            this.toolStripButtonRefreshAnalytics.Text = "Prognosen";
            this.toolStripButtonRefreshAnalytics.ToolTipText = "Startet die Berechnung der Kursprognosen für alle Items neu. Es werden keine neue" +
    "n Daten abgerufen, wenn diese bereits aktuell sind.";
            this.toolStripButtonRefreshAnalytics.Click += new System.EventHandler(this.toolStripButtonRefreshAnalytics_Click);
            // 
            // toolStripButtonRefreshGraph
            // 
            this.toolStripButtonRefreshGraph.Image = global::BMonGWUI.Properties.Resources.refresh;
            this.toolStripButtonRefreshGraph.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefreshGraph.Name = "toolStripButtonRefreshGraph";
            this.toolStripButtonRefreshGraph.Size = new System.Drawing.Size(83, 22);
            this.toolStripButtonRefreshGraph.Text = "Diagramm";
            this.toolStripButtonRefreshGraph.ToolTipText = "Berechnet und zeichnet das Diagramm neu. Es werden keine Daten abgerufen, wenn di" +
    "ese bereits aktuell sind.";
            this.toolStripButtonRefreshGraph.Click += new System.EventHandler(this.toolStripButtonRefreshGraph_Click);
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonSave.Image = global::BMonGWUI.Properties.Resources.s_save;
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(59, 22);
            this.toolStripButtonSave.Text = "Graph";
            this.toolStripButtonSave.ToolTipText = "Speichert alle Graphen für das aktuell angezeigte Produkt.";
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Verkaufen";
            this.columnHeader1.Width = 45;
            // 
            // doubleTrackBarAnalyticsRange
            // 
            this.doubleTrackBarAnalyticsRange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doubleTrackBarAnalyticsRange.Location = new System.Drawing.Point(178, 3);
            this.doubleTrackBarAnalyticsRange.Maximum = 1;
            this.doubleTrackBarAnalyticsRange.Minimum = 0;
            this.doubleTrackBarAnalyticsRange.Name = "doubleTrackBarAnalyticsRange";
            this.doubleTrackBarAnalyticsRange.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.doubleTrackBarAnalyticsRange.Size = new System.Drawing.Size(640, 35);
            this.doubleTrackBarAnalyticsRange.SmallChange = 1;
            this.doubleTrackBarAnalyticsRange.TabIndex = 4;
            this.doubleTrackBarAnalyticsRange.Text = "doubleTrackBar1";
            this.doubleTrackBarAnalyticsRange.ValueLeft = 0;
            this.doubleTrackBarAnalyticsRange.ValueRight = 1;
            this.doubleTrackBarAnalyticsRange.ValueChanged += new System.EventHandler(this.doubleTrackBarAnalyticsRange_ValueChanged);
            // 
            // toolStripButtonOpenFavorites
            // 
            this.toolStripButtonOpenFavorites.Image = global::BMonGWUI.Properties.Resources.s_open;
            this.toolStripButtonOpenFavorites.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenFavorites.Name = "toolStripButtonOpenFavorites";
            this.toolStripButtonOpenFavorites.Size = new System.Drawing.Size(76, 22);
            this.toolStripButtonOpenFavorites.Text = "Favoriten";
            this.toolStripButtonOpenFavorites.Click += new System.EventHandler(this.toolStripButtonOpenFavorites_Click);
            // 
            // MainW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 416);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainW";
            this.Text = "Börsenmonitor";
            this.Load += new System.EventHandler(this.MainW_Load);
            this.Shown += new System.EventHandler(this.MainW_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListView listViewZeit;
        private System.Windows.Forms.ListView listViewProdukte;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefreshProducts;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ImageList imageListTendenzen;
        private System.Windows.Forms.ToolStripLabel tRefreshDateTime;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMain;
        private System.Windows.Forms.ToolStripButton toolStripButtonGroupsVisible;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefreshAnalytics;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefreshGraph;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private DoubleTrackBar doubleTrackBarAnalyticsRange;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpenFavorites;
    }
}

