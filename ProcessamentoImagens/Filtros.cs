using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics.Tracing;

namespace ProcessamentoImagens
{
    class Filtros
    {
        public static void convert_to_grayDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;
            Int32 gs;

            //lock dados bitmap origem
            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            //lock dados bitmap destino
            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataSrc.Stride - (width * pixelSize);

            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                int r, g, b;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        b = *(src++); //está armazenado dessa forma: b g r 
                        g = *(src++);
                        r = *(src++);
                        gs = (Int32)(r * 0.2990 + g * 0.5870 + b * 0.1140);
                        *(dst++) = (byte)gs;
                        *(dst++) = (byte)gs;
                        *(dst++) = (byte)gs;
                    }
                    src += padding;//soma pra não mexer no padding, para o ponteiro ir para a proxima linha
                    dst += padding;
                }
            }
            //unlock imagem origem
            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            //unlock imagem destino
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        public static void espelhoVerticalDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;

            //lock dados bitmap origem 
            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            //lock dados bitmap destino
            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataSrc.Stride - (width * pixelSize);

            unsafe
            {
                byte* src1 = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                int r, g, b;
                for (int y = 0; y < height; y++)
                {
                    byte* aux = dst;
                    aux = aux + bitmapDataDst.Stride * (height - 1 - y);
                    for (int x = 0; x < width; x++)
                    {
                        b = *(src1++); //está armazenado dessa forma: b g r 
                        g = *(src1++);
                        r = *(src1++);

                        *(aux++) = (byte)b;
                        *(aux++) = (byte)g;
                        *(aux++) = (byte)r;
                    }
                    src1 += padding;
                }
            }
            //unlock imagem origem 
            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            //unlock imagem destino
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        public static void black_whiteDMA(Bitmap bSrc, Bitmap bDst)
        {
            int height = bSrc.Height;
            int width = bSrc.Width;
            int pixelSize = 3;

            BitmapData bDataSrc = bSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bDataDst = bDst.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bDataSrc.Stride - width * pixelSize;

            unsafe
            {
                byte* src = (byte*)bDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bDataDst.Scan0.ToPointer();

                for(int y = 0; y < height; y++)
                {
                    for(int x = 0; x < width; x++)
                    {
                        int b = *(src++);
                        int g = *(src++);
                        int r = *(src++);
                        int res = (int)(r * 0.2990 + g * 0.5870 + b * 0.1140);
                        if (res <= 127)
                            res = 0;
                        else
                            res = 255;
                        *(dst)++ = (byte)res;
                        *(dst)++ = (byte)res;
                        *(dst)++ = (byte)res;
                    }
                    src += padding;
                    dst += padding;
                }
            }
            bSrc.UnlockBits(bDataSrc);
            bDst.UnlockBits(bDataDst);
        }

        public static void dilatacao(Bitmap bSrc, Bitmap bDst, int[] vetX, int[] vetY)
        {
            int height = bSrc.Height;
            int width = bSrc.Width;

            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    Color p = bSrc.GetPixel(x, y);
                    if(p.R == 0)
                    {
                        //encontrou objeto, aplica o elemento estruturante
                        for(int i = 0; i < vetX.Length; i++)
                        {
                            int novoX = x + vetX[i];
                            int novoY = y + vetY[i];
                            if(novoX > 0 && novoX < width && novoY > 0 && novoY < height)
                            {
                                bDst.SetPixel(novoX, novoY, Color.FromArgb(0, 0, 0));
                            }
                        }
                    }
                }
            }
        }

        public static void erosao(Bitmap bSrc, Bitmap bDst, int[] vetX, int[] vetY)
        {
            int height = bSrc.Height;
            int width = bSrc.Width;

            for (int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    bool erodir = true;
                    for(int i = 0; i < vetX.Length && erodir; i++)
                    {
                        int novoX = x + vetX[i];
                        int novoY = y + vetY[i];

                        if(novoX < 0 || novoX >= width || novoY < 0 || novoY >= height)//se estiver fora, nao podemos aplicar a erosao
                        {
                            erodir = false;
                        }
                        else
                        {
                            if(bSrc.GetPixel(novoX, novoY).R != 0)//se nao for objeto, nao podemos erodir
                            {
                                erodir = false;
                            }
                        }
                    }
                    if (erodir)
                    {
                        bDst.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                    }
                    else
                    {
                        bDst.SetPixel(x, y, Color.FromArgb(255,255,255));
                    }
                }
            }
        }

        public static void run_length_coding(Bitmap bSrc, Bitmap bDst)
        {
            int height = bSrc.Height;
            int width = bSrc.Width;

            int corAtual= 0, corAnt = -1, qtde= 0;

            List<int> rlc = new List<int>();
            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    corAtual = bSrc.GetPixel(x, y).R;
                    if(corAtual == corAnt)
                    {
                        qtde++;
                    }
                    else
                    {
                        if (corAnt != -1)
                        {
                            rlc.Add(qtde);
                            rlc.Add(corAnt);
                        }
                        qtde = 1;
                    }
                    corAnt = corAtual;
                }
                rlc.Add(qtde);
                rlc.Add(corAnt);
                corAnt = -1;
                qtde = 1;
                rlc.Add(-1);//para indicar proxima linha
                rlc.Add(-1);
            } 
            for(int i = 0, x = 0, y = 0; i < rlc.Count; i += 2)
            {
                if (rlc[i] != -1)
                {
                    qtde = rlc[i];
                    int cor = rlc[i + 1];
                    for (int j = 0; j < qtde; j++)
                    {
                        bDst.SetPixel(x, y, Color.FromArgb(cor, cor, cor));
                        x++;
                    }
                }
                else
                {
                    x = 0;
                    y++;
                }
            }
        }

        public static void Dilatacao_NovaDMA(Bitmap bsrc, Bitmap bdst, int[] vetX, int[] vetY, int origemX, int origemY)
        {
            int height = bsrc.Height;
            int width = bsrc.Width;
            int pixelsize = 3;

            BitmapData bdatasrc = bsrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bdatadst = bdst.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bdatasrc.Stride;
            int padding = stride - width * pixelsize;
            unsafe
            {
                byte* src = (byte*) bdatasrc.Scan0.ToPointer();
                byte* dst = (byte*)bdatadst.Scan0.ToPointer();
                int pos;
                for (int y = 0; y < height; y++)
                {
                    for(int x = 0; x < width; x++)
                    {
                        if (src[x * pixelsize + y * stride] == 0)//se é objeto, aplicar mascara dilatacao
                        {
                            for(int i = 0; i < vetX.Length; i++)
                            {
                                int novoX = x + vetX[i] - origemX;
                                int novoY = y + vetY[i] - origemY;
                                if (novoX >= 0 && novoX < width && novoY >= 0 && novoY < height)
                                {
                                    pos = novoX * pixelsize + novoY * stride;
                                    dst[pos] = dst[pos + 1] = dst[pos + 2] = 0;//dilatando
                                }
                            }
                        }
                    }
                }
            }
            bsrc.UnlockBits(bdatasrc);
            bdst.UnlockBits(bdatadst);
        }

        public static void Erosao_NovaDMA(Bitmap bsrc, Bitmap bdst, int[] vetX, int[] vetY)
        {
            int height = bsrc.Height;
            int width = bsrc.Width;
            int pixelsize = 3;

            BitmapData bdatasrc = bsrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bdatadst = bdst.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bdatasrc.Stride;
            int padding = stride - width * pixelsize;
            unsafe
            {
                byte* src = (byte*)bdatasrc.Scan0.ToPointer();
                byte* dst = (byte*)bdatadst.Scan0.ToPointer();
                int pos;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        pos = x * pixelsize + y * stride;
                        if (src[pos] == 0)//se é objeto, verificar se pode aplicar erosao
                        {
                            bool aplicarErosao = true;
                            for (int i = 0; i < vetX.Length && aplicarErosao; i++)
                            {
                                int novoX = x + vetX[i];
                                int novoY = y + vetY[i];

                                if (novoX < 0 || novoX >= width || novoY < 0 || novoY >= height)
                                {
                                    aplicarErosao = false;
                                }
                                else
                                {
                                    if (src[novoX * pixelsize + novoY * stride] != 0)
                                    {
                                        aplicarErosao = false;
                                    }
                                }
                            }
                            if (aplicarErosao)
                            {
                                dst[pos] = dst[pos + 1] = dst[pos + 2] = 0;
                            }
                            else
                            {
                                dst[pos] = dst[pos + 1] = dst[pos + 2] = 255;
                            }
                        }
                    }
                }
            }
            bsrc.UnlockBits(bdatasrc);
            bdst.UnlockBits(bdatadst);
        }
    }
}

/*
 * pixel é armazenado como bgr 
 * pixel é 3 bytes
*/