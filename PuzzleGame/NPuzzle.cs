/*Purpose:              PROG 2370 Assignment3
 *Created By:           Aubrey Delong, Jay Jang
 *
 *Revision History:     Created October 16,2017 by Aubrey
 *                      added NPuzzle Class.
 */

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ADelongJJangAssignment3
{
    /// <summary>
    /// NPuzzle class
    /// </summary>
    public partial class NPuzzle : Form
    {
        /// <summary>
        /// default tile, panel, and form sizes
        /// </summary>
        private const int PANEL_WIDTH = 330;
        private const int PANEL_HEIGHT = 330;
        private const int FORM_WIDTH = 438;
        private const int FORM_HEIGHT = 520;
        private const int DEFAULT_TILE_NUM = 6;
        private const int TILE_SIZE_PLUS_GAP = 55;
        private int numberOfRows = 0;
        private int numberOfColumns = 0;
        public static int clearNumber = 0;
        public static String[,] tileArray;

        /// <summary>
        /// initialize
        /// </summary>
        public NPuzzle()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Click event of generate tiles
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event argument</param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                numberOfRows = int.Parse(txtRowNumber.Text);
                numberOfColumns = int.Parse(txtColumnNumber.Text);
            }
            catch (Exception)
            {

                MessageBox.Show("Please input available number only.");
                txtRowNumber.Text = "";
                txtColumnNumber.Text = "";
                this.ActiveControl = txtRowNumber;
                return;
            }

            clearNumber = numberOfRows * numberOfColumns;
            
            if (numberOfRows < 1 ||
               numberOfColumns < 1 ||
               numberOfRows == 1 && numberOfColumns == 1)
            {
                MessageBox.Show("Please input available puzzle size.");
                txtRowNumber.Text = "";
                txtColumnNumber.Text = "";
                this.ActiveControl = txtRowNumber;
                return;
            }

            Tile.CreateArray(numberOfRows, numberOfColumns);

            Tile.ShuffleTile(numberOfRows, numberOfColumns);

            CreateTile();

            ChangeSize();            
        }

        /// <summary>
        /// create tile buttons
        /// </summary>
        public void CreateTile()
        {
            panel1.Controls.Clear();
            int startX = 0;
            int startY = 0;
            int WIDTH = 50;
            int HEIGHT = 50;
            int VGAP = 5;

            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    Bitmap HW = Properties.Resources.p;
                    Tile t = new Tile(this, panel1);
                    t.Left = startX;
                    t.Top = startY;
                    t.Width = WIDTH;
                    t.Height = HEIGHT;
                    t.Text = tileArray[i, j];
                    t.Click += Tile_Click;
                    t.Image = HW;
                    if (t.Text == clearNumber.ToString() || tileArray[i, j] == null || tileArray[i, j] == "")
                    {
                        t.Width = 0;
                        t.Height = 0;
                    }
                    panel1.Controls.Add(t);
                    startX += WIDTH + VGAP;

                }
                startX = 0;
                startY += HEIGHT + VGAP;
            }
        }

        /// <summary>
        /// Change form and panel size
        /// </summary>
        private void ChangeSize()
        {
            int numberOfExtraRows = 0;
            int numberOfExtraColumns = 0;
            int newFormHeight = 0;
            int newFormWidth = 0;
            int newPanelHeight = 0;
            int newPanelWidth = 0;

            if (numberOfRows > DEFAULT_TILE_NUM || numberOfColumns > DEFAULT_TILE_NUM)
            {
                //change form/panel width and height
                if (numberOfRows > DEFAULT_TILE_NUM && numberOfColumns > DEFAULT_TILE_NUM)
                {
                    numberOfExtraRows = numberOfRows - DEFAULT_TILE_NUM;
                    numberOfExtraColumns = numberOfColumns - DEFAULT_TILE_NUM;
                    newFormHeight = numberOfExtraRows * TILE_SIZE_PLUS_GAP + FORM_HEIGHT;
                    newFormWidth = numberOfExtraColumns * TILE_SIZE_PLUS_GAP + FORM_WIDTH;
                    newPanelHeight = numberOfExtraRows * TILE_SIZE_PLUS_GAP + PANEL_HEIGHT;
                    newPanelWidth = numberOfExtraColumns * TILE_SIZE_PLUS_GAP + PANEL_WIDTH;

                    this.Size = new Size(newFormWidth, newFormHeight);
                    panel1.Size = new Size(newPanelWidth, newPanelHeight);
                }
                //only change form/panel width
                else if (numberOfColumns > DEFAULT_TILE_NUM)
                {
                    numberOfExtraColumns = numberOfColumns - DEFAULT_TILE_NUM;
                    newFormWidth = numberOfExtraColumns * TILE_SIZE_PLUS_GAP + FORM_WIDTH;
                    newPanelWidth = numberOfExtraColumns * TILE_SIZE_PLUS_GAP + PANEL_WIDTH;

                    this.Size = new Size(newFormWidth, FORM_HEIGHT);
                    panel1.Size = new Size(newPanelWidth, PANEL_HEIGHT);
                }
                //only change form/panel height
                else if (numberOfRows > DEFAULT_TILE_NUM)
                {
                    numberOfExtraRows = numberOfRows - DEFAULT_TILE_NUM;
                    newFormHeight = numberOfExtraRows * TILE_SIZE_PLUS_GAP + FORM_HEIGHT;
                    newPanelHeight = numberOfExtraRows * TILE_SIZE_PLUS_GAP + PANEL_HEIGHT;

                    this.Size = new Size(FORM_WIDTH, newFormHeight);
                    panel1.Size = new Size(PANEL_WIDTH, newPanelHeight);
                }
            }

            if(numberOfRows < DEFAULT_TILE_NUM || numberOfColumns < DEFAULT_TILE_NUM)
            {
            //change panel to smaller width and height
                if (numberOfRows < DEFAULT_TILE_NUM && numberOfColumns < DEFAULT_TILE_NUM)
                {
                    numberOfExtraRows = DEFAULT_TILE_NUM - numberOfRows;
                    numberOfExtraColumns = DEFAULT_TILE_NUM - numberOfColumns;
                    newPanelHeight = PANEL_HEIGHT - TILE_SIZE_PLUS_GAP * numberOfExtraRows;
                    newPanelWidth = PANEL_WIDTH - TILE_SIZE_PLUS_GAP * numberOfExtraColumns;

                    this.Size = new Size(FORM_WIDTH, FORM_HEIGHT);
                    panel1.Size = new Size(newPanelWidth, newPanelHeight);
                }
                else if(numberOfRows < DEFAULT_TILE_NUM)
                {
                    numberOfExtraRows = DEFAULT_TILE_NUM - numberOfRows;
                    newPanelHeight = PANEL_HEIGHT - TILE_SIZE_PLUS_GAP * numberOfExtraRows;
                    panel1.Size = new Size(PANEL_WIDTH, newPanelHeight);
                }
                else if(numberOfColumns < DEFAULT_TILE_NUM)
                {
                    numberOfExtraColumns = DEFAULT_TILE_NUM - numberOfColumns;
                    newPanelWidth = PANEL_WIDTH - TILE_SIZE_PLUS_GAP * numberOfExtraColumns;

                    this.Size = new Size(FORM_WIDTH, FORM_HEIGHT);
                    panel1.Size = new Size(newPanelWidth, PANEL_HEIGHT);
                }
            }
            
            if(numberOfRows == DEFAULT_TILE_NUM && numberOfColumns == DEFAULT_TILE_NUM)
            {
                this.Size = new Size(FORM_WIDTH, FORM_HEIGHT);
                panel1.Size = new Size(PANEL_WIDTH, PANEL_HEIGHT);
            }

        }

        /// <summary>
        /// tile click event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event argument</param>
        private void Tile_Click(object sender, EventArgs e)
        {
            Tile tile = sender as Tile;
            int cCol = 0;
            int cRow = 0;
            string tmp = "";
            bool change = false;

            //finds the index of where the clicked button is located
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    if (tileArray[i, j] == tile.Text)
                    {
                        cRow = i;
                        cCol = j;
                    }

                }
            }

            //left
            if (cCol != 0 && tileArray[cRow,cCol-1] == clearNumber.ToString())
            {
                tmp = tileArray[cRow, cCol - 1]; // save value to temp first and move.
                tileArray[cRow, cCol - 1] = tileArray[cRow, cCol];
                tileArray[cRow, cCol] = tmp;
                change = true;
            }
            //right
            else if (cCol < tileArray.GetLength(1) - 1 && tileArray[cRow, cCol + 1] == clearNumber.ToString())
            {
                tmp = tileArray[cRow, cCol + 1];
                tileArray[cRow, cCol + 1] = tileArray[cRow, cCol];
                tileArray[cRow, cCol] = tmp;
                change = true;
            }
            //top
            else if (cRow != 0 && tileArray[cRow - 1, cCol] == clearNumber.ToString())
            {
                tmp = tileArray[cRow - 1, cCol];
                tileArray[cRow - 1, cCol] = tileArray[cRow, cCol];
                tileArray[cRow, cCol] = tmp;
                change = true;
            }
            //bottom
            else if (cRow < tileArray.GetLength(0) - 1 && tileArray[cRow + 1, cCol] == clearNumber.ToString())
            {
                tmp = tileArray[cRow + 1, cCol];
                tileArray[cRow + 1, cCol] = tileArray[cRow, cCol];
                tileArray[cRow, cCol] = tmp;
                change = true;
            }
            if (change == true)
            {
                CreateTile(); 
            }
            
            CheckComplete();
        }

        /// <summary>
        /// check if puzzle completed
        /// </summary>
        private void CheckComplete()
        {
            int txtNumber = 1;

            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    if (tileArray[i, j] != txtNumber.ToString())
                    {
                        return;
                    }
                    txtNumber++;
                }
            }

            MessageBox.Show("Congratulations! Completed Puzzle!");
            panel1.Controls.Clear();
            txtRowNumber.Text = "";
            txtColumnNumber.Text = "";
            this.ActiveControl = txtRowNumber;
        }

        /// <summary>
        /// load file and put into array
        /// </summary>
        /// <param name="fileName">name of file</param>
        private void DoLoad (string fileName)
        {
            string currentLine;
            String[] newTileArray;
            string[] sizeArray;
            StreamReader reader = new StreamReader(fileName);
            int i = 0;

            //clear row and column text boxes
            txtRowNumber.Clear();
            txtColumnNumber.Clear();

            //extract array size from file
            string size = reader.ReadLine();
            sizeArray = size.Split('*');
            numberOfRows = int.Parse(sizeArray[0]);
            numberOfColumns = int.Parse(sizeArray[1]);
            tileArray = new string[numberOfRows, numberOfColumns];
            clearNumber = numberOfRows * numberOfColumns;

            while(i < numberOfRows)
            {
                currentLine = reader.ReadLine();
                if (currentLine == "")
                {
                    continue;
                }
                else
                {
                    newTileArray = new String[numberOfColumns];
                    newTileArray = currentLine.Split(',');
                    for (int j = 0; j < numberOfColumns; j++)
                    {
                        if (newTileArray[j] == null || newTileArray[j] == "")
                        {
                            tileArray[i, j] = clearNumber.ToString();
                            continue;
                        }
                        else
                        {
                            tileArray[i, j] = newTileArray[j];
                        }
                    }
                }

                i++;
            }

            reader.Close();
        }

        /// <summary>
        /// Save puzzle to file
        /// </summary>
        /// <param name="fileName">name of file</param>
        /// <param name="saveDlg">save dialog object</param>
        private void DoSave(string fileName, SaveFileDialog saveDlg)
        {
            Stream fileStream;
            fileStream = saveDlg.OpenFile();
            StreamWriter writer = new StreamWriter(fileStream);

            //save the array size
            writer.WriteLine(tileArray.GetLength(0) + "*" + tileArray.GetLength(1));

            for (int i = 0; i < numberOfRows; i++)
            {

                for (int j = 0; j < numberOfColumns; j++)
                {
                    if (tileArray[i, j] == clearNumber.ToString())
                    {
                        if (j == numberOfColumns - 1)
                        {
                            writer.Write(tileArray[i, j] = "");
                        }
                        else
                        {
                            writer.Write(tileArray[i, j] = ",");
                        }
                    }
                    else if (j == numberOfColumns - 1)
                    {
                        writer.Write(tileArray[i, j]);
                    }
                    else
                    {
                        writer.Write(tileArray[i, j] += ",");
                    }
                }

                writer.WriteLine("");
            }

            writer.Close();
            fileStream.Close();
        }

        /// <summary>
        /// save n puzzle game to txt file
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event argument</param>
        private void tsmiSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.CreatePrompt = false;
            saveDlg.OverwritePrompt = true;
            saveDlg.DefaultExt = ".txt";
            saveDlg.FileName = "nPuzzle";
            saveDlg.Filter = "Text file(*.txt)|*.txt|ALL files (*.*)|*.*";
            saveDlg.InitialDirectory = "C:\\";

            DialogResult result = saveDlg.ShowDialog();
            switch (result)
            {
                case DialogResult.Abort:
                    break;
                case DialogResult.Cancel:
                    break;
                case DialogResult.Ignore:
                    break;
                case DialogResult.No:
                    break;
                case DialogResult.None:
                    break;
                case DialogResult.OK:
                    try
                    {
                        string filename = saveDlg.FileName;
                        DoSave(filename, saveDlg);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error in file save");
                    }

                    break;
                case DialogResult.Retry:
                    break;
                case DialogResult.Yes:
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Open saved npuzzle game
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event argument</param>
        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            dlgOpen.DefaultExt = ".txt";
            dlgOpen.FileName = "nPuzzle";
            dlgOpen.Filter = "Text file(*.txt)|*.txt|ALL files (*.*)|*.*";
            dlgOpen.InitialDirectory = "C:\\";
            DialogResult result = dlgOpen.ShowDialog();
            switch (result)
            {
                case DialogResult.Abort:
                    break;
                case DialogResult.Cancel:
                    break;
                case DialogResult.Ignore:
                    break;
                case DialogResult.No:
                    break;
                case DialogResult.None:
                    break;
                case DialogResult.OK:
                    try
                    {
                        string fileName = dlgOpen.FileName;
                        DoLoad(fileName);
                        CreateTile();
                        ChangeSize();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error in file load");
                    }
                    break;
                case DialogResult.Retry:
                    break;
                case DialogResult.Yes:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Close form
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event argument</param>
        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
