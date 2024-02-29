using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace PlayerC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        WMPLib.IWMPPlaylist playList;
        
        private void BtnAbrir_Click(object sender, EventArgs e)
        {
            ofdAbrir.Title = "Abrir mídia";
            ofdAbrir.Filter = "Arquivo mp4|*.mp4|Arquvio mp3|*.mp3";
           if (ofdAbrir.ShowDialog() == DialogResult.OK )
            {
                playList = player.playlistCollection.newPlaylist("Lista");

                foreach (var arquivo in ofdAbrir.FileNames)
                {
                    playList.appendItem(player.newMedia(arquivo));
                    lstPlayList.Items.Add(arquivo);


                    player.currentPlaylist = playList;
                    player.Ctlcontrols.play();
                }
            }
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            if(lstPlayList.Items.Count > 0)
            {
                sfdSalvar.Title = "Salvar PlayList";
                sfdSalvar.Filter = "Arquivo texto|*.txt";
                if(sfdSalvar.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter arquivo = new StreamWriter(sfdSalvar.FileName, false);
                    for (int i = 0; i < lstPlayList.Items.Count ; i++)
                    {
                        arquivo.WriteLine(lstPlayList.Items[i].ToString());
                    }
                    arquivo.Close();
                }
                
            }
            

        }

        private void BtnCarregar_Click(object sender, EventArgs e)
        {
            ofdAbrir.Title = "Abrir PlayList";
            ofdAbrir.Filter = "Arquivo texto|*.txt";
            ofdAbrir.Multiselect = false;
            if (ofdAbrir.ShowDialog() == DialogResult.OK)
            {
                StreamReader arquivo = new StreamReader(ofdAbrir.FileName);
                while (arquivo.Peek() != -1)
                {
                    lstPlayList.Items.Add(arquivo.ReadLine());
                }
                arquivo.Close();
            }
        }

        private void LstPlayList_DoubleClick(object sender, EventArgs e)
        {
            if (lstPlayList.Items.Count > 0)
            {
                player.URL = lstPlayList.SelectedItem.ToString();
                player.Ctlcontrols.play();
            }
        }

        private void BtnLimpar_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.stop();
            lstPlayList.Items.Clear();
        }
    }
}
