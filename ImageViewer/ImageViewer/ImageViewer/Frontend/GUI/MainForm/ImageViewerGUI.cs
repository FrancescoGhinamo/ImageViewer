using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageViewer
{
    public partial class ImageViewer : Form
    {

        #region PrivateFields
        /// <summary>
        /// Array of images contained in the parent folder of the selected picture
        /// </summary>
        private String[] images;

        private String[] allowedExtensions = {".jpg", ".png", ".bmp", ".jpeg", ".ico"};

        /// <summary>
        /// Index of the shown picture
        /// </summary>
        private int currentIndex;

        #endregion PrivateFields

        #region Constructor

        public ImageViewer()
        {
            InitializeComponent();
            images = null;
            currentIndex = 0;
        }
        #endregion Constructor


        #region EventHandlers
        private void ImageViewer_Load(object sender, EventArgs e)
        {

        }

        private void FileMnuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPicture();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (images != null)
            {
                Forward();
               
            }
        }

        private void btnBackward_Click(object sender, EventArgs e)
        {
            if (images != null)
            {
                Backward();
            }
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            Rotate();
        }

        #endregion EventHandlers

        #region PictureShowing
        /// <summary>
        /// The method lets the user choose a picture from file system
        /// Saves in a local array all the pictures saved in the parent folder of the chosen image
        /// </summary>
        public void OpenPicture()
        {
            OpenFileDialog open = new OpenFileDialog();

            if (open.ShowDialog() == DialogResult.OK)
            {
                
                string selectedPath = open.FileName;
                if (allowedExtensions.Contains(selectedPath.Substring(selectedPath.LastIndexOf('.'))))
                {
                    string parentDirPath = selectedPath.Substring(0, selectedPath.LastIndexOf('\\'));
                    string[] _files = Directory.GetFiles(parentDirPath);

                
                    List<string> _pic = new List<string>();
                    for(int i = 0; i < _files.Length; i++)
                    {
                        if(allowedExtensions.Contains(_files[i].Substring(_files[i].LastIndexOf('.')))) {
                            _pic.Add(_files.ElementAt(i));
                        }
                    }

                    images = _pic.ToArray();
                    for (currentIndex = 0; currentIndex < images.Length; currentIndex++)
                    {
                        if (selectedPath.Equals(images[currentIndex]))
                        {
                            break;
                        }
                    }
                    ShowPicture(currentIndex);
                }
                
            }
        }

        /// <summary>
        /// Displays the picture stored at the given index
        /// </summary>
        /// <param name="index">Index in the pictures array</param>
        public void ShowPicture(int index)
        {
            try
            {
                pbImage.ImageLocation = images[index];
            }
            catch (IndexOutOfRangeException e)
            {
                MessageBox.Show(this, e.Message, "Error");
            }


        }

        /// <summary>
        /// Shows the next picture in the array
        /// </summary>
        public void Forward()
        {
            currentIndex = (currentIndex + 1) % images.Length;
            ShowPicture(currentIndex);
        }

        /// <summary>
        /// Shows the previous picture in the array
        /// </summary>
        public void Backward()
        {
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = images.Length - 1;
            }
            ShowPicture(currentIndex);
        }

        /// <summary>
        /// Rotation of the image
        /// </summary>
        public void Rotate()
        {
            if(pbImage.Image != null)
            {
                pbImage.Image.RotateFlip(RotateFlipType.Rotate90FlipXY);
            }
        }

        #endregion PictureShowing

        
    }
}
