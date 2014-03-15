using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BMonGWData;

namespace BMonGWUI
{
    public partial class MainW : Form
    {
        public GItem currentProduct = null;
        public List<int> Favorites = null;

        public MainW(string[] args)
        {
            InitializeComponent();
            if (args.Length > 0)
            {
            }
        }
        public MainW()
        {
            InitializeComponent();
        }

        private void MainW_Shown(object sender, EventArgs e)
        {
            refreshProductBaseData();
            syncSelected();
        }
        private void MainW_Load(object sender, EventArgs e)
        {
            refreshTimer.Tick+=refreshTimer_Tick;
        }

        #region Daten Management
        BackgroundWorker init;
        public void refreshProductBaseData() {
            init = new BackgroundWorker();
            init.RunWorkerCompleted += init_RunWorkerCompleted;
            init.DoWork += init_DoWork;
            RefreshDateTime = "lade Daten...";
            init.RunWorkerAsync();
        }
        void init_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            refreshProductListAsync();
        }
        void init_DoWork(object sender, DoWorkEventArgs e)
        {
            Market.init();
        }

        Timer refreshTimer = new Timer();
        public void startAutorefresh()
        {
            refreshTimer.Interval = 900000; //15 Minuten
            refreshTimer.Start();
        }
        public void stopAutorefresh()
        {
            refreshTimer.Stop();
        }
        void refreshTimer_Tick(object sender, EventArgs e)
        {
 	        //CHECK:Was soll eigentlich aktualisiert werden?
        }
        public void loadFavorites(string path)
        {
            //try
            //{
                if (System.IO.File.Exists(path))
                {
                    Favorites = new List<int>();
                    string[] parts = System.IO.File.ReadAllLines(path);
                    foreach (string part in parts)
                    {
                        Favorites.Add(int.Parse(part));
                    }
                    refreshProductListAsync();
                }
            /*}
            catch (Exception e)
            {
                MessageBox.Show("Favoritendatei konnte nicht geöffnet werden.");
            }*/
        }
        #endregion
        #region UI Management
        /// <summary>
        /// aktualisiert alle angezeigten Daten mit den Backend-Daten
        /// </summary>
        public void refreshProductListAsync()
        {
            System.Threading.Thread t = new System.Threading.Thread((System.Threading.ThreadStart)delegate{
                RefreshDateTime = "aktualisiere...";
                refreshProductList();
                RefreshDateTime = DateTime.Now.ToString("F");
            });
            t.Start();
        }
        /// <summary>
        /// aktualisiert das angezeigte Diagramm (und die Zeitverlaufsliste)
        /// </summary>
        public void refreshGraph()
        {
            chartMain.Series.Clear();
            //foreach (GItem item in currentProducts)
            if (currentProduct != null && currentProduct.BuyList != null && currentProduct.SellList != null)
            {
                GItem item = currentProduct;
                {
                    //Preisliste
                    {
                        Series sa = new Series("Ankaufspreis");
                        Series sv = new Series("Verkaufspreis");
                        for (int i = 0; i < item.BuyList.Count && i < item.SellList.Count; i++)
                        {
                            //CHECK: soll item2 oder item3 verwendet werden?
                            //ListViewItem l=new ListViewItem(new string[]{item.First.Add(new TimeSpan(10000 * 15 * 60 * i)).ToString(), item.BuyList[i].Item2.ToString(), item.SellList[i].Item2.ToString()});
                            ListViewItem l = new ListViewItem(new string[] { item.BuyList[i].Item1.ToString(), item.BuyList[i].Item2.ToString(), item.SellList[i].Item2.ToString() });
                            listViewZeit.Items.Add(l);
                            sa.Points.AddXY(item.BuyList[i].Item1.ToOADate(), item.BuyList[i].Item2);
                            sv.Points.AddXY(item.SellList[i].Item1.ToOADate(), item.SellList[i].Item2);
                        }
                        sa.ChartType = SeriesChartType.Line;
                        sa.XValueType = ChartValueType.DateTime;
                        chartMain.Series.Add(sa);
                        sv.ChartType = SeriesChartType.Line;
                        sv.XValueType = ChartValueType.DateTime;
                        chartMain.Series.Add(sv);
                    }
                    //BUG: null refrence exception... (diesmal eine andere)
                    /*
                    //moving average long graph
                    {
                        int[] graph = item.movingAverageLongGraph();
                        Series s = new Series("langer bewegter Durchschnitt");
                        for (int i = 0; i < graph.Length; i++)
                        {
                            s.Points.AddXY(item.First.Add(new TimeSpan(10000 * 15 * 60 * i)).ToOADate(), graph[i]); //TODO: ausmultiplizieren
                        }
                        s.ChartType = SeriesChartType.Line;
                        s.XValueType = ChartValueType.DateTime;
                        chartMain.Series.Add(s);
                    }
                    //moving average short graph
                    {
                        int[] graph = item.movingAverageShortGraph();
                        Series s = new Series("Kurzer bewegter Durchschnitt");
                        for (int i = 0; i < graph.Length; i++)
                        {
                            s.Points.AddXY(item.First.Add(new TimeSpan(10000 * 15 * 60 * i)).ToOADate(), graph[i]); //TODO: ausmultiplizieren
                        }
                        s.ChartType = SeriesChartType.Line;
                        s.XValueType = ChartValueType.DateTime;
                        chartMain.Series.Add(s);
                    }
                    //exp average graph
                    {
                        int[] graph = item.expAverageGraph();
                        Series s = new Series("exp Durchschnitt"); //TODO: richtigen namen ermitteln
                        for (int i = 0; i < graph.Length; i++)
                        {
                            s.Points.AddXY(item.First.Add(new TimeSpan(10000 * 15 * 60 * i)).ToOADate(), graph[i]); //TODO: ausmultiplizieren
                        }
                        s.ChartType = SeriesChartType.Line;
                        s.XValueType = ChartValueType.DateTime;
                        chartMain.Series.Add(s);
                    }*/
                }
                toolStripButtonSave.Enabled = true;
                toolStripButtonRefreshGraph.Enabled = true;
            }
            else
            {
                toolStripButtonSave.Enabled = false;
                toolStripButtonRefreshGraph.Enabled = false;
            }
        }
        /// <summary>
        /// aktualisiert die Produktliste
        /// </summary>
        public void refreshProductList()
        {
            if (Market.Items == null)
            {
                return;
            }
            Dictionary<int, ListViewGroup> gruppen = new Dictionary<int, ListViewGroup>();
            GItemTypeConverter.forceInit();
            this.Invoke((Action)delegate
            {
                //ListView.SelectedListViewItemCollection selectedItems = listViewProdukte.SelectedItems;
                listViewProdukte.Items.Clear();
                listViewProdukte.Groups.Clear();
                foreach (GItemTypeConverter.GType type in GItemTypeConverter.AllItems)
                {
                    ListViewGroup g = new ListViewGroup();
                    g.Header = type.Name;
                    gruppen.Add(type.Id, g);
                    listViewProdukte.Groups.Add(g);
                }
                {
                    ListViewGroup g = new ListViewGroup();
                    g.Header = "Standard";
                    gruppen.Add(-1, g);
                    listViewProdukte.Groups.Add(g);
                }
            });

            //System.Threading.Tasks.Parallel.For(0, Market.Items.Count, (Action<int>)delegate(int index)
            System.Threading.Tasks.Parallel.For(0, 10, (Action<int>)delegate(int index)
            {
                //foreach (GItem item in Market.Items)
                GItem item = Market.Items[index];
                if (Favorites == null || Favorites.Contains(item.DataId))
                {
                    item.Refresh();
                    if (item.BuyList.Count > 0 && item.SellList.Count > 0)
                    {
                        ListViewItem i = new ListViewItem();
                        i.Text = item.Name;
                        if (gruppen.ContainsKey(item.TypeId))
                        {
                            i.Group = gruppen[item.TypeId];
                        }
                        else
                        {
                            i.Group = gruppen[-1];
                        }
                        switch (item.kurs) //BUG: nach dem auswählen einer favoriten liste wird fälschlicher weise -2 ermittelt (wegen zu wenig daten)
                        {
                            case 1:
                                i.ImageIndex = 2;
                                break;
                            case 0:
                                i.ImageIndex = 0;
                                break;
                            case -1:
                                i.ImageIndex = 1;
                                break;
                            case -2:
                            default:
                                i.ImageIndex = 3;
                                break;
                        }
                        if (currentProduct != null && item.DataId == currentProduct.DataId)
                        {
                            i.Selected = true;
                            currentProduct = item;
                        }
                        this.BeginInvoke((Action)delegate
                        {
                            listViewProdukte.Items.Add(i); //BUG: wenn programm beendet wird, kann control nicht gefunden werden
                        });
                    }
                }
            });
            /*
            foreach (ListViewItem item in selectedItems)
            {
                foreach (ListViewItem sitem in listViewProdukte.Items)
                {
                    if (sitem.Text == item.Text)
                    {
                        sitem.Selected = true;
                    }
                }
            }*/
        }
        /// <summary>
        /// synchronisiert die Auswahlen alle UI-Elemente und aktualisiert veraltete Komponenten
        /// </summary>
        void syncSelected()
        {
            /*currentProducts.Clear();
            foreach (ListViewItem item in listViewProdukte.Items)
            {
                currentProducts.Add(Market.GetItem(item.Text));
            }*/
            if (listViewProdukte.SelectedItems.Count == 0)
            {
                currentProduct = null;
            }
            else
            {
                currentProduct = Market.GetItem(listViewProdukte.SelectedItems[0].Text);
            }
            refreshGraph();
        }
        /// <summary>
        /// Setzt die angezeigte Statuszeichenfolge
        /// Diese Eigenschaft ist Threadsicher und kann auch aus nicht-UI Threads verwendet werden.
        /// </summary>
        public string RefreshDateTime
        {
            set
            {
                if (this.InvokeRequired) //Threadübergreifende Zugriffe ermöglichen
                {
                    this.BeginInvoke((Action)delegate
                    {
                        tRefreshDateTime.Text = value;
                    });
                }
                else
                {
                    tRefreshDateTime.Text = value;
                }
            }
            get
            {
                return tRefreshDateTime.Text;
            }
        }
        #endregion
        #region EventHandler
        private void listViewProdukte_SelectedIndexChanged(object sender, EventArgs e)
        {
            syncSelected();
        }

        private void listViewZeit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButtonRefresh_ButtonClick(object sender, EventArgs e)
        {
            refreshProductBaseData();
        }

        private void toolStripButtonRefreshAnalytics_Click(object sender, EventArgs e)
        {
            refreshProductListAsync();
        }

        private void toolStripButtonRefreshGraph_Click(object sender, EventArgs e)
        {
            refreshGraph();
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            if (currentProduct != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Diagramm speichern";
                sfd.Filter = "PNG|*.png";
                sfd.FileName = "Auswertung für " + currentProduct.Name + " (" + currentProduct.PriceLastChanged.ToString("dd.MM.yyyy HH.mm.ss") + ")";
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    chartMain.SaveImage(sfd.FileName, ChartImageFormat.Png); //TODO: andere Dateiformate zulassen
                }
            }
        }

        private void toolStripButtonGroupsVisible_Click(object sender, EventArgs e)
        {
            listViewProdukte.ShowGroups = !listViewProdukte.ShowGroups;
            toolStripButtonGroupsVisible.Checked = listViewProdukte.ShowGroups;
        }

        private void doubleTrackBarAnalyticsRange_ValueChanged(object sender, EventArgs e)
        {
            refreshGraph();
        }

        private void toolStripButtonOpenFavorites_Click(object sender, EventArgs e)
        {
            if (Favorites == null)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Favoritendatei öffnen";
                ofd.Filter = "Favoriten|*.bwgf|Textdatei|*.txt";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    toolStripButtonOpenFavorites.Checked = true;
                    loadFavorites(ofd.FileName);
                }
            }
            else
            {
                Favorites = null;
                toolStripButtonOpenFavorites.Checked = false;
                refreshProductListAsync();
            }
        }
        #endregion
    }
}
