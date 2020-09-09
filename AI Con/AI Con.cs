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
using System.Threading;
using ImageConverter;

namespace Convert_AI_Con
{
    public partial class AiCon : Form
    {

        int mm = 0;
        public AiCon()
        {
            InitializeComponent();
            InitializeInitValue();
        }

        int initlblConverLocationX = 0;
        int initlblImageLocationX = 0;
        int initlblToLocationX = 0;
        int initlblIconLocationX = 0;

        private void InitializeInitValue()
        {
            initlblConverLocationX = lblConver.Left;
            initlblImageLocationX = lblImage.Left;
            initlblToLocationX = lblTo.Left;
            initlblIconLocationX = lblIcon.Left;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.bmp) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var input = openFileDialog.FileName;
                    string output = GetOutputLocation(input);
                    PngIconConverter.Convert(input, output, 256);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception Caught", ex);
            }
        }

        private void imgDragAndDrop_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
        private void imgDragAndDrop_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                var input = ((string[])e.Data.GetData(DataFormats.FileDrop, false))[0];
                string extension = Path.GetExtension(input);
                if (extension == @".jpg" || extension == @".png" || extension == @".jpeg" || extension == @".jpe" || extension == @".bmp")
                {
                    string output = GetOutputLocation(input);
                    PngIconConverter.Convert(input, output, 256);
                }
                else
                {
                    MessageBox.Show("File type not supported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception Caught", ex);
            }
        }

        private static string GetOutputLocation(string input)
        {
            string directory = Path.GetDirectoryName(input);
            string name = Path.GetFileNameWithoutExtension(input);
            var output = directory + "\\" + name + ".ico";
            return output;
        }

        private void imgLogo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                imgLogo.Left = e.X + imgLogo.Left;
            }
            MoveLogo();

        }

        private void MoveLogo()
        {
            int onTriggerLblMoveWidth = 10;
            int imgLogoLocationX = imgLogo.Left - onTriggerLblMoveWidth;

            var intLblConverLocationX = imgLogo.Left + imgLogo.Size.Width;
            var intLblImageLocationX = intLblConverLocationX + lblConver.Size.Width;
            var intLblToLocationX = intLblImageLocationX + lblImage.Size.Width;
            var intLblIconLocationX = intLblToLocationX + lblTo.Size.Width;

            if (imgLogoLocationX <= initlblConverLocationX)
            {
                ChangeLabelTitleLocationX(intLblConverLocationX, intLblImageLocationX, intLblToLocationX, intLblIconLocationX);
            }
            else if ((imgLogoLocationX <= initlblImageLocationX) && (imgLogoLocationX > initlblConverLocationX))
            {
                intLblImageLocationX = imgLogo.Left + imgLogo.Size.Width;
                intLblToLocationX = intLblImageLocationX + lblImage.Size.Width;
                intLblIconLocationX = intLblToLocationX + lblTo.Size.Width;
                ChangeLabelTitleLocationX(initlblConverLocationX, intLblImageLocationX, intLblToLocationX, intLblIconLocationX);
            }
            else if ((imgLogoLocationX <= initlblToLocationX) && (imgLogoLocationX > initlblImageLocationX))
            {
                intLblToLocationX = imgLogo.Left + imgLogo.Size.Width;
                intLblIconLocationX = intLblToLocationX + lblTo.Size.Width;
                ChangeLabelTitleLocationX(initlblConverLocationX, initlblImageLocationX, intLblToLocationX, intLblIconLocationX);
            }
            else if ((imgLogoLocationX <= initlblIconLocationX) && (imgLogoLocationX > initlblToLocationX))
            {
                intLblIconLocationX = imgLogo.Left + imgLogo.Size.Width;
                ChangeLabelTitleLocationX(initlblConverLocationX, initlblImageLocationX, initlblToLocationX, intLblIconLocationX);
            }
            else
            {
                ChangeLabelTitleLocationX(initlblConverLocationX, initlblImageLocationX, initlblToLocationX, initlblIconLocationX);
            }
        }

        private void ChangeLabelTitleLocationX(int intLblConverLocationX, int intLblImageLocationX, int intLblToLocationX, int intLblIconLocationX)
        {
            lblConver.Left = intLblConverLocationX;
            lblImage.Left = intLblImageLocationX;
            lblTo.Left = intLblToLocationX;
            lblIcon.Left = intLblIconLocationX;
        }

        private void imgLogo_MouseUp(object sender, MouseEventArgs e)
        {
            if (imgLogo.Left < 8)
                imgLogo.Left = 8;
            else if (imgLogo.Left > 213)
                imgLogo.Left = 213;

            MoveLogo();
        }

        private void lblDragAndQuotes_MouseEnter(object sender, EventArgs e)
        {
            imgDragAndDrop.BackgroundImage = imgDragAndDrop.InitialImage;
        }

        private void lblDragAndQuotes_MouseLeave(object sender, EventArgs e)
        {
            imgDragAndDrop.BackgroundImage = imgDragAndDrop.ErrorImage;
        }

        private void lblCorp_Click(object sender, EventArgs e)
        {
            try
            {
                mm = mm + 1;
                if (mm > 1)
                    mm = 0;
                if (mm == 0)
                {
                    imgHelloMascot.Cursor = Cursors.Default;
                    imgHelloMascot.Image = null;
                }
                else if (mm == 1)
                {
                    imgHelloMascot.Image = imgHelloMascot.InitialImage;
                    imgHelloMascot.Cursor = Cursors.Hand;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception", ex);
            }
        }

        private void imgMascot_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (mm == 1)
                    {
                        Random rnd = new Random();

                        int color = rnd.Next(1, 6);
                        if (color == 1)
                            lblDragAndQuotes.ForeColor = Color.Aqua;
                        if (color == 2)
                            lblDragAndQuotes.ForeColor = Color.Yellow;
                        if (color == 3)
                            lblDragAndQuotes.ForeColor = Color.BurlyWood;
                        if (color == 4)
                            lblDragAndQuotes.ForeColor = Color.OrangeRed;
                        if (color == 5)
                            lblDragAndQuotes.ForeColor = Color.LightGreen;
                        int i = rnd.Next(1, 21);
                        if (i == 1)
                            lblDragAndQuotes.Text = "Hi... I'm Muharmadin.";
                        if (i == 2)
                            lblDragAndQuotes.Text = "Better an oops than a what if.";
                        if (i == 3)
                            lblDragAndQuotes.Text = "Don't be the same, be better.";
                        if (i == 4)
                            lblDragAndQuotes.Text = "You live once so think twice.";
                        if (i == 5)
                            lblDragAndQuotes.Text = "Don't let idiot ruin your day.";
                        if (i == 6)
                            lblDragAndQuotes.Text = "Sometimes you win & sometimes you learn.";
                        if (i == 7)
                            lblDragAndQuotes.Text = "Everyday is new beginning.";
                        if (i == 8)
                            lblDragAndQuotes.Text = "Choose kindness, and laugh often.";
                        if (i == 9)
                            lblDragAndQuotes.Text = "Only dead fish go with the flow.";
                        if (i == 10)
                            lblDragAndQuotes.Text = "Think deeper aim higher.";
                        if (i == 11)
                            lblDragAndQuotes.Text = "If you don't do it someone else will.";
                        if (i == 12)
                            lblDragAndQuotes.Text = "You get back exactly what you put in.";
                        if (i == 13)
                            lblDragAndQuotes.Text = "If monday were shoes they'd be crocs.";
                        if (i == 14)
                            lblDragAndQuotes.Text = "Be silly be honest be kind.";
                        if (i == 15)
                            lblDragAndQuotes.Text = "Just because it isn't wrong doesn't make it right.";
                        if (i == 16)
                            lblDragAndQuotes.Text = "The first wealth is health.";
                        if (i == 17)
                            lblDragAndQuotes.Text = "We rise by lifting others.";
                        if (i == 18)
                            lblDragAndQuotes.Text = "Keep moving.";
                        if (i == 19)
                            lblDragAndQuotes.Text = "For succes, attitude is equally as important as ability.";
                        if (i == 20)
                            lblDragAndQuotes.Text = "You will never have this day again, so make it count.";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception", ex);
            }
        }

        private void imgMascot_MouseUp(object sender, MouseEventArgs e)
        {
            lblDragAndQuotes.Text = "Drag and Drop Here";
            lblDragAndQuotes.ForeColor = Color.Black;
        }

        int page = 1;
        private void AiCon_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            page = 1;
            pages();
        }
        private void pages()
        {
            imgHelp.Visible = true;
            imgNextHelp.Visible = true;
            imgBackHelp.Visible = true;
            panelHelp.Visible = true;
            if (page == 1)
            {
                imgHelp.Image = imgHelp.BackgroundImage;
                imgHelp.Cursor = Cursors.Hand;
                imgBackHelp.Image = null;
                imgNextHelp.Image = imgNextHelp.ErrorImage;
                imgNextHelp.Cursor = Cursors.Hand;
                imgBackHelp.Cursor = Cursors.Default;
            }
            if (page == 2)
            {
                imgHelp.Image = imgHelp.ErrorImage;
                imgHelp.Cursor = Cursors.Hand;
                imgBackHelp.Image = imgBackHelp.InitialImage;
                imgNextHelp.Image = imgNextHelp.ErrorImage;
                imgNextHelp.Cursor = Cursors.Hand;
                imgBackHelp.Cursor = Cursors.Hand;
            }
            if (page == 3)
            {
                imgHelp.Image = imgHelp.InitialImage;
                imgHelp.Cursor = Cursors.Hand;
                imgBackHelp.Image = imgBackHelp.InitialImage;
                imgNextHelp.Image = imgNextHelp.InitialImage;
                imgNextHelp.Cursor = Cursors.Hand;
                imgBackHelp.Cursor = Cursors.Hand;
            }
            if (page > 3)
            {
                imgHelp.Visible = false;
                imgNextHelp.Visible = false;
                imgBackHelp.Visible = false;
                panelHelp.Visible = false;
            }

        }

        private void pagesplus(object sender, EventArgs e)
        {
            page = page + 1;
            pages();
        }

        private void pagesminus(object sender, EventArgs e)
        {
            page = page - 1;
            pages();
        }

        private void panelHelp_Click(object sender, EventArgs e)
        {
            page = 4;
            pages();
        }
    }
}
