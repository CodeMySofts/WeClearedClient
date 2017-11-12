using System;
using System.Windows.Forms;

namespace SharpUpdate
{
    internal partial class SharpUpdateAcceptForm : Form
    {
        /// <summary>
        /// Le programme qui met a jour les informations
        /// </summary>
        private ISharpUpdatable applicationInfo;

        /// <summary>
        /// Les informations de mise a jour
        /// </summary>
        private SharpUpdateXml updateInfo;

        /// <summary>
        /// La frame qui propose la mise a jour
        /// </summary>
        private SharpUpdateInfoForm updateInfoForm;

        /// <summary>
        /// Constructeur de la frame
        /// </summary>
        /// <param name="applicationInfo"></param>
        /// <param name="updateInfo"></param>
        internal SharpUpdateAcceptForm(ISharpUpdatable applicationInfo, SharpUpdateXml updateInfo)
        {
            InitializeComponent();

            this.applicationInfo = applicationInfo;
            this.updateInfo = updateInfo;

            this.Text = applicationInfo.ApplicationName + " - Mise à jour disponible";

            // Afficher l'icone si ce dernier n'est pas nul
            if (applicationInfo.ApplicationIcon != null)
            {
                Icon = applicationInfo.ApplicationIcon;
            }

            // Afficher le numéro de version
            lblNewVersion.Text = string.Format("Nouvelle version: {0}", updateInfo.Version.ToString());

        }

        /// <summary>
        /// Événement quand on clique sur Oui
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYes_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        /// <summary>
        /// Événement quand on clique sur Non
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNo_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        /// <summary>
        /// Événement quand on clique sur détails
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDetails_Click(object sender, EventArgs e)
        {
            if (this.updateInfoForm == null)
            {
                updateInfoForm = new SharpUpdateInfoForm(this.applicationInfo, this.updateInfo);
            }

            updateInfoForm.ShowDialog(this);
        }
    }
}
