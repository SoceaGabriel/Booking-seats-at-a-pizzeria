using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using App.Controller;
using App.DataModels;
using App.Interface;

namespace App.Interface
{
    public partial class Login : Form
    {
        Thread th;
        private LoginService lgn;
        private int ok = 0;
        public Login()
        {
            InitializeComponent();
            lgn = new LoginService();
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            var loginList = lgn.GetAll();
            foreach (var rez in loginList)
            {
                if ((rez.USER_LOGIN == this.username_textbox.Text) && (rez.PASSWORD_LOGIN == this.password_textbox.Text))
                {
                    ok = 1;
                }
            }
            if (ok == 1)
            {
                this.Close();
                th = new Thread(OpenNewForm);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
                text_autentificare.Text = "V-ati autentificat cu succes!";
            }
            else
            {
                text_autentificare.Text = "Eroare, date de autentificare sunt incorecte!";
            }
        }

        private void OpenNewForm()
        {
            ProiectBD appl = new ProiectBD();
            Application.Run(appl);
        }
    }
}
