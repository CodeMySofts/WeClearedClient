using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeCleared.Core;
using SharpUpdate;
using System.Reflection;

namespace WeCleared
{
    public partial class frmWeCleared : Form, ISharpUpdatable
    {
        #region Constructeur

        public frmWeCleared()
        {
            InitializeComponent();

            // Classe d'enregistrement des parametres
            Settings = new SettingsClass(this);
            Settings.ReadMainSettings();

            // Attribution du WoWPath
            WoWPath = MainSettings["WoWPath"];
            if (IsWoWPathValid(WoWPath))
            {
                lblPath.Text = WoWPath;
            }

            // Addons Parser class
            AddonsParser = new AddonsParser(this);

            // Zip Class
            ZipClass = new ZipClass(this);

            // Démarrer le timer général.
            tmrRefresh.Start();
            tmrRefresh_Tick(this, EventArgs.Empty);

            // Notify Icon
            NIcon = new NotifyIcon
            {
                BalloonTipIcon = ToolTipIcon.Info,
                BalloonTipText = "Vous pouvez réouvrir We Cleared Client via le system tray.",
                BalloonTipTitle = "We Cleared minimisé.",
                Icon = Properties.Resources.Icon,
                Text = "Cliquez pour réouvrir We Cleared.",
                Visible = false
            };
            NIcon.MouseClick += NIcon_Click;

            // Mises à jour de l'UI
            dataGridAddons.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridAddons.RowTemplate.Height = 50;
            btnUpdate.BackColor = Settings.GetEnabled("AutoUpdate") ? Color.DarkGreen : Color.DarkRed;
            DefineTooltips();

            // ISharpUpdatable UI
            lblUpdateClient.Text = ApplicationAssembly.GetName().Version.ToString();
            Updater = new SharpUpdater(this);
            Updater.DoUpdate();
        }

        #endregion

        #region Variables

        private SharpUpdater Updater { get; set; }

        /// <summary>
        /// Objet tooltip qui permet d'afficher les tooltips sur les contrôles
        /// </summary>
        internal ToolTip MainToolTip { get; set; }

        /// <summary>
        /// Texte affiché dans le label au haut de l'application pour afficher les erreurs
        /// </summary>
        internal string ExceptionString { get; set; }

        /// <summary>
        /// Classe d'enregistrement des variables
        /// </summary>
        internal SettingsClass Settings { get; set; }

        /// <summary>
        /// Icône utilisé pour le system tray
        /// </summary>
        internal NotifyIcon NIcon { get; set; }

        /// <summary>
        /// Class qui permet la manipulation des Zips.
        /// </summary>
        internal AddonsParser AddonsParser { get; set; }

        /// <summary>
        /// Class qui contien les fonctions d'extraction.
        /// </summary>
        internal ZipClass ZipClass { get; set; }

        /// <summary>
        /// Mis à jour via la sauvegarde de settings.
        /// </summary>
        internal string WoWPath { get; set; }

        /// <summary>
        /// Contien les parametre qui seront sauvegardés entre les utilisations.
        /// </summary>
        internal Dictionary<string, string> MainSettings = new Dictionary<string, string>
        {
            ["WoWPath"] = "",
            ["AutoUpdate"] = ""
        };

        /// <summary>
        /// Permet de suivrre les incrémentations de la barre de progres lors de l'update
        /// </summary>
        internal int ProgressCount { get; set; }
        internal int ProgressTarget { get; set; }

        #endregion

        #region Fonctions

        /// <summary>
        /// Remettre la barre de chargement à 0 et commencer la progression
        /// </summary>
        /// <param name="p_Steps"></param>
        internal void StartProgressBar(int p_Steps)
        {
            lblInfo.Text = "Début de la mise à jour.";
            pgbUpdating.Value = 0;
            pgbUpdating.Show();
            ProgressTarget += p_Steps;
            tmrProgress.Start();
        }

        /// <summary>
        /// Continuer la progression de la mise à jour
        /// </summary>
        /// <param name="p_Steps"></param>
        /// <param name="p_Info"></param>
        internal void ContinueProgress(int p_Steps, string p_Info)
        {
            lblInfo.Text = p_Info;
            ProgressTarget += p_Steps;
        }

        /// <summary>
        /// Vérifier si le chemin de WoW est valid
        /// </summary>
        /// <returns></returns>
        internal bool IsWoWPathValid(string p_WoWPath)
        {
            if (Directory.Exists(p_WoWPath))
            {
                var l_InterfacePath = Directory.GetParent(p_WoWPath);
                if (l_InterfacePath.Exists && l_InterfacePath.Name == "Interface")
                {
                    var l_RealWoWPath = l_InterfacePath.Parent;
                    if (l_RealWoWPath?.Exists == true)
                    {
                        var l_WoWFiles = l_RealWoWPath.GetFiles();
                        if (l_WoWFiles.Any(x => x.Name == "Wow-64.exe"))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Executée par le Timer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            if (IsWoWPathValid(WoWPath))
            {
                AddonsParser.Parse();

                if (AddonsParser.Addons.Count > 0)
                {
                    dataGridAddons.DataSource = AddonsParser.AddonsList;
                    cmbSelectAddon.Items.Clear();
                    foreach (var l_Addon in AddonsParser.Addons)
                    {
                        cmbSelectAddon.Items.Add(l_Addon.Nom);
                    }
                    if (cmbSelectAddon.Text == string.Empty)
                    {
                        cmbSelectAddon.Text = AddonsParser.Addons[0].Nom;
                        if (Settings.GetEnabled(AddonsParser.Addons[0].Nom))
                        {
                            btnActivateAddon.Text = "X";
                            btnActivateAddon.BackColor = Color.DarkGreen;
                        }
                        else
                        {
                            btnActivateAddon.Text = "";
                            btnActivateAddon.BackColor = Color.DarkRed;
                        }
                    }
                    // Effectuer les mises à jour automatiquement si l'option est activée.
                    if (Settings.GetEnabled("AutoUpdate"))
                    {
                        AddonsParser.ApplyUpdates();
                    }
                    ExceptionString = string.Empty;
                }
                lblInfo.Text = ExceptionString;
            }
            else
            {
                ExceptionString = "Addons non trouvés, vérifiez le Dossier WoW.";
                lblInfo.Text = ExceptionString;
            }
        }

        /// <summary>
        /// Définir les tooltips pour les éléments.
        /// </summary>
        private void DefineTooltips()
        {
            // Création du tooltip
            MainToolTip = new ToolTip
            {
                AutoPopDelay = 10000,
                InitialDelay = 500,
                ReshowDelay = 500,
                ShowAlways = true
            };

            // Assigner les tooltips par controle
            MainToolTip.SetToolTip(this.btnActivateAddon, "Clickez ce bouton pour activer/désactiver l'utilisation du addon affiché dans la combo box de gauche.");
            MainToolTip.SetToolTip(this.btnUpdate, "Clickez à gauche pour forcer la mise à jour. Clickez à doite pour activer ou désactiver les mises à jour automatiques.");
            MainToolTip.SetToolTip(this.cmbSelectAddon, "Sélectionnez un addon dont vous désirez activer ou désactiver les mises à jour.");
            MainToolTip.SetToolTip(this.btnClose, "Quitter l'application.");
            MainToolTip.SetToolTip(this.btnMinimise, "Minimiser l'application.");
            MainToolTip.SetToolTip(this.btnFindFolder, "Clickez ici pour définir un dossier où trouver vos addons.");
            MainToolTip.SetToolTip(this.dataGridAddons, "Grilles d'affichage des addons.");
            MainToolTip.SetToolTip(this.lblPath, "Chemin d'accès où trouver les addons que l'on veut mettre à jour.");
        }

        #endregion

        #region Événements


        /// <summary>
        /// Exécuté toutes les secondes durant la progression de la mise a jour.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrProgress_Tick(object sender, EventArgs e)
        {
            ProgressCount += 1;
            pgbUpdating.Increment(1);
            if (ProgressCount >= ProgressTarget)
            {
                ProgressCount = 0;
                ProgressTarget = 0;
                tmrProgress.Stop();
            }
            if (pgbUpdating.Value == 100)
            {
                lblInfo.Text = "";
                pgbUpdating.Hide();
                pgbUpdating.Value = 0;
            }
        }

        /// <summary>
        /// Forcer les mises à jour.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void btnFindFolder_Click(object sender, EventArgs e)
        {
            var l_Dialog = new FolderBrowserDialog();
            var l_Result = l_Dialog.ShowDialog();
            if (l_Result == DialogResult.OK)
            {
                if (l_Dialog.SelectedPath.ToLower().Contains(@"interface\addOns"))
                {
                    MainSettings["WoWPath"] = l_Dialog.SelectedPath;
                }
                else if (l_Dialog.SelectedPath.ToLower().Contains("interface"))
                {
                    MainSettings["WoWPath"] = Path.Combine(l_Dialog.SelectedPath, "AddOns");
                }
                else
                {
                    MainSettings["WoWPath"] = Path.Combine(l_Dialog.SelectedPath, "Interface", "AddOns");
                }

                if (IsWoWPathValid(MainSettings["WoWPath"]))
                {
                    lblPath.Text = MainSettings["WoWPath"];
                    Settings.WriteMainSettings();
                }
                else
                {
                    lblPath.Text = "";
                    MainSettings["WoWPath"] = "";
                }

                WoWPath = MainSettings["WoWPath"];

                tmrRefresh_Tick(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Événements du bouton Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    Settings.SetEnabled("AutoUpdate", !Settings.GetEnabled("AutoUpdate"));
                    btnUpdate.BackColor = Settings.GetEnabled("AutoUpdate") ? Color.DarkGreen : Color.DarkRed;
                    break;
                case MouseButtons.Left:
                    if (!AddonsParser.Updating)
                    {
                        AddonsParser.ApplyUpdates();
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Fermer l'application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Executé quand on click sur le _
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMinimise_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            NIcon.Visible = true;
            NIcon.ShowBalloonTip(3000);
            ShowInTaskbar = false;
        }

        /// <summary>
        /// Evenement quand on click le Notify Icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        internal void NIcon_Click(object sender, EventArgs args)
        {
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            NIcon.Visible = false;
        }

        /// <summary>
        /// Lancé quand on sélectionne un addon dans le combo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSelectAddon_SelectedIndexChanged(object sender, EventArgs e)
        {
            var l_Text = ((ComboBox)sender).Text;
            var l_Addon = AddonsParser.Addons.FirstOrDefault(x => x.Nom == l_Text);
            if (l_Addon != null && Settings.GetEnabled(l_Addon.Nom))
            {
                btnActivateAddon.BackColor = Color.DarkGreen;
                btnActivateAddon.Text = "X";
            }
            else
            {
                btnActivateAddon.BackColor = Color.DarkRed;
                btnActivateAddon.Text = "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnActivateAddon_Click(object sender, EventArgs e)
        {
            var l_Text = cmbSelectAddon.Text;
            var l_Addon = AddonsParser.Addons.FirstOrDefault(x => x.Nom == l_Text);
            if (l_Addon != null && Settings.GetEnabled(l_Addon.Nom))
            {
                Settings.SetEnabled(l_Addon.Nom, false);
                btnActivateAddon.BackColor = Color.DarkRed;
                btnActivateAddon.Text = "";
            }
            else if (l_Addon != null && !Settings.GetEnabled(l_Addon.Nom))
            {
                Settings.SetEnabled(l_Addon.Nom, true);
                btnActivateAddon.BackColor = Color.DarkGreen;
                btnActivateAddon.Text = "X";
            }
            tmrRefresh_Tick(this, EventArgs.Empty);
        }

        /// <summary>
        /// Événement qui est lancé quand le contenu d'une cellule est modifié.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridAddons_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            switch (e.Column.Name)
            {
                case "Nom":
                    e.Column.Width = 200;
                    break;
                case "Description":
                    e.Column.Width = AddonsParser.AddonsList.Count > 5 ? 661 : 679;
                    break;
                case "Local":
                    e.Column.Width = 59;
                    break;
                case "Web":
                    e.Column.Width = 59;
                    break;
            }
        }

        /// <summary>
        /// Rendre la frame déplaçable.
        /// </summary>
        public const int WmNclbuttondown = 0xA1;
        public const int HtCaption = 0x2;
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr p_HWnd, int p_Msg, int p_WParam, int p_LParam);
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        private void Interface_MouseDown(object p_Sender, MouseEventArgs p_E)
        {
            if (p_E.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WmNclbuttondown, HtCaption, 0);
            }
        }

        #endregion
        
        #region ISharpUpdate Implementation

        /// <summary>
        /// La présente Assembly
        /// </summary>
        public Assembly ApplicationAssembly => Assembly.GetExecutingAssembly();

        /// <summary>
        /// L'icône de l'application
        /// </summary>
        public Icon ApplicationIcon => Icon;

        /// <summary>
        /// Nom de l'application
        /// </summary>
        public string ApplicationName => "We Cleared Client";

        /// <summary>
        /// Application Id, utilisé pour vérifier le xml de mise à jour
        /// </summary>
        public string ApplicationId => "We Cleared Client";

        /// <summary>
        /// Url utilisé pour la mise à jour de l'application
        /// </summary>
#if DEBUG
        public Uri UpdateXmlLocation => new Uri("http://192.168.0.59:44443/update/debug/update.xml");
#elif LOCAL
        public Uri UpdateXmlLocation => new Uri("http://192.168.0.59:44443/update/local/update.xml");
#else
        public Uri UpdateXmlLocation => new Uri("http://" + Dns.GetHostAddresses("codemylife.ca")[0] + ":44443" + "/update/release/update.xml");
#endif

        /// <summary>
        /// Le présent formulaire
        /// </summary>
        public Form Context => this;

        #endregion
    }
}
