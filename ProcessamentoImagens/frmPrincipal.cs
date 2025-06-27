using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace ProcessamentoImagens
{
    public partial class frmPrincipal : Form
    {
        private Image image;
        private Bitmap imageBitmap;

        public frmPrincipal()
        {
            InitializeComponent();
            pictBoxImg1.SizeMode = PictureBoxSizeMode.Zoom;
            pictBoxImg2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void btnAbrirImagem_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "Arquivos de Imagem (*.jpg;*.png)|*.jpg;*.gif;*.bmp;*.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(openFileDialog.FileName);
                pictBoxImg1.Image = image;
                imageBitmap = (Bitmap)image;
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            pictBoxImg1.Image = null;
            pictBoxImg2.Image = null;
        }

        private void btnLuminanciaComDMA_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.convert_to_grayDMA(imageBitmap, imgDest);
            pictBoxImg2.Image = imgDest;
        }


        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void bttPretoBrancoDMA_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.black_whiteDMA(imageBitmap, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void bttDilatacao_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.black_whiteDMA(imageBitmap, imgDest);

            Bitmap novaImg = new Bitmap(imgDest);

            //vetores que representam o elemento estruturante(posicoes que vamos dilatar quando detectarmos um objeto)
            int[] coordsX = { -1, 0, 1, -1, 0, 1, -1, 0, 1 };
            int[] coordsY = { -1, -1, -1, 0, 0, 0, 1, 1, 1 };

            Filtros.dilatacao(novaImg, imgDest, coordsX, coordsY);

            pictBoxImg2.Image = imgDest;

        }

        private void bttErosao_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.black_whiteDMA(imageBitmap, imgDest);

            Bitmap novaImg = new Bitmap(imgDest);

            //vetores que representam o elemento estruturante(posicoes que vamos dilatar quando detectarmos um objeto)
            int[] coordsX = { -1, 0, 1, -1, 1, -1, 0, 1 };
            int[] coordsY = { -1, -1, -1, 0, 0, 1, 1, 1 };

            Filtros.erosao(novaImg, imgDest, coordsX, coordsY);

            pictBoxImg2.Image = imgDest;
        }

        private void bttAbertura_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.black_whiteDMA(imageBitmap, imgDest);
            Bitmap novaImg = new Bitmap(imgDest);

            int[] coordsX = { -1, 0, 1, -1, 1, -1, 0, 1 };
            int[] coordsY = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] coordsX2 = { -1, 0, 1, 2, -1, 0, 1, 2, -1, 0, 1, 2, -1, 0, 1, 2 };
            int[] coordsY2 = { -1, -1, -1, -1, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2 };

            Filtros.erosao(novaImg, imgDest, coordsX, coordsY);
            imageBitmap = new Bitmap(imgDest);

            Filtros.dilatacao(imageBitmap, imgDest, coordsX, coordsY);
            pictBoxImg2.Image = imgDest;
        }

        private void bttFechamento_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.black_whiteDMA(imageBitmap, imgDest);
            Bitmap novaImg = new Bitmap(imgDest);

            //pontos que devem ser objeto na erosao, e pontos que pintamos na dilatacao
            int[] coordsX = { -1, 0, 1, -1, 1, -1, 0, 1 };
            int[] coordsY = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] coordsX2 = { -1, 0, 1, 2, -1, 0, 1, 2, -1, 0, 1, 2, -1, 0, 1, 2 };
            int[] coordsY2 = { -1, -1, -1, -1, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2 };

            Filtros.dilatacao(novaImg, imgDest, coordsX, coordsY);
            imageBitmap = new Bitmap(imgDest);

            Filtros.erosao(imageBitmap, imgDest, coordsX, coordsY);
            pictBoxImg2.Image = imgDest;
        }

        private void bttRLC_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.black_whiteDMA(imageBitmap, imgDest);

            Bitmap novaImg = new Bitmap(imgDest);

            Filtros.run_length_coding(novaImg, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void bttDilatacaoDMA_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.black_whiteDMA(imageBitmap, imgDest);

            Bitmap novaImg = new Bitmap(imgDest);

            int[] vetX = { -1, 0, 1, -1, 0, 1, -1, 0, 1 };
            int[] vetY = { -1, -1, -1, 0, 0, 0, 1, 1, 1 };
            int origemX = 1, origemY = 1;

            Filtros.Dilatacao_NovaDMA(novaImg, imgDest, vetX,vetY, origemX, origemY);
            pictBoxImg2.Image = imgDest;
        }

        private void bttErosaoDMA_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.black_whiteDMA(imageBitmap, imgDest);

            Bitmap novaImg = new Bitmap(imgDest);

            //vetores que representam o elemento estruturante(posicoes que vamos dilatar quando detectarmos um objeto)
            int[] vetX = { -1, 0, 1, -1, 0, 1, -1, 0, 1 };
            int[] vetY = { -1, -1, -1, 0, 0, 0, 1, 1, 1 };

            Filtros.Erosao_NovaDMA(novaImg, imgDest, vetX, vetY);

            pictBoxImg2.Image = imgDest;
        }

        private void bttFechamentoDMA_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.black_whiteDMA(imageBitmap, imgDest);

            Bitmap novaImg = new Bitmap(imgDest);

            //vetores que representam o elemento estruturante(posicoes que vamos dilatar quando detectarmos um objeto)
            int[] vetX = { -1, 0, 1, -1, 0, 1, -1, 0, 1 };
            int[] vetY = { -1, -1, -1, 0, 0, 0, 1, 1, 1 };

            Filtros.Dilatacao_NovaDMA(novaImg, imgDest, vetX, vetY, 0, 0);
            novaImg = new Bitmap(imgDest);

            int[] vetX2 = { -2, -1, 0, 1, 2, -2, -1, 0, 1, 2, -2, -1, 0, 1, 2, -2, -1, 0, 1, 2, -2, -1, 0, 1, 2 };
            int[] vetY2 = { -2, -2, -2, -2, -2, -1, -1, -1, -1, -1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2 };
            Filtros.Erosao_NovaDMA(novaImg, imgDest, vetX2, vetY2);

            pictBoxImg2.Image = imgDest;
        }

        private void bttAberturaDMA_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.black_whiteDMA(imageBitmap, imgDest);

            Bitmap novaImg = new Bitmap(imgDest);

            //vetores que representam o elemento estruturante(posicoes que vamos dilatar quando detectarmos um objeto)
            int[] vetX = { -1, 0, 1, -1, 0, 1, -1, 0, 1 };
            int[] vetY = { -1, -1, -1, 0, 0, 0, 1, 1, 1 };

            Filtros.Erosao_NovaDMA(novaImg, imgDest, vetX, vetY);
            novaImg = new Bitmap(imgDest);

            int[] vetX2 = { -2, -1, 0, 1, 2, -2, -1, 0, 1, 2, -2, -1, 0, 1, 2, -2, -1, 0, 1, 2, -2, -1, 0, 1, 2 };
            int[] vetY2 = { -2, -2, -2, -2, -2, -1, -1, -1, -1, -1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2 };
            Filtros.Dilatacao_NovaDMA(novaImg, imgDest, vetX2, vetY2, 0,0);

            pictBoxImg2.Image = imgDest;
        }
    }
}
