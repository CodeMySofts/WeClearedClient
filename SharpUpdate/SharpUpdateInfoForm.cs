using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpUpdate
{
    public partial class SharpUpdateInfoForm : Form
    {
        /// <summary>
        /// Constructeur du formulaire
        /// </summary>
        /// <param name="applicationInfo"></param>
        /// <param name="updateInfo"></param>
        public SharpUpdateInfoForm(ISharpUpdatable applicationInfo, SharpUpdateXml updateInfo)
        {
            InitializeComponent();

            if (applicationInfo.ApplicationIcon != null)
            {
                Icon = applicationInfo.ApplicationIcon;
            }

            this.Text = applicationInfo.ApplicationName + " - Mise à jour";
            lblVersion.Text = String.Format("Version Actuelle: {0}\nVersion à jour: {1}",
                applicationInfo.ApplicationAssembly.GetName().Version.ToString(),
                updateInfo.Version.ToString());

            txtDescription.Text = updateInfo.Description;
        }

        /// <summary>
        /// Terminer la form quand le bouton retourner est utilisé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Empecher les keypress dans la rich textbox, a mois que ce soit pour faire un ctrl+c
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Control && e.KeyCode == Keys.C))
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
