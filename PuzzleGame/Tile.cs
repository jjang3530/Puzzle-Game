/*Purpose:              PROG 2370 Assignment3
 *Created By:           Aubrey Delong, Jay Jang
 *
 *Revision History:     Created October 16,2017 by Jay
 *                      added Tile Class.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADelongJJangAssignment3
{   
    /// <summary>
    /// Tile Class
    /// </summary>
     class Tile : Button
    {
        //public static string tileRetult; complete
        private NPuzzle form;
        private Panel board;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="form">NPuzzle form</param>
        /// <param name="board">NPuzzle board</param>
        public Tile(NPuzzle form, Panel board)
        {
            this.form = form;
            this.board = board;
        }

        /// <summary>
        /// Creat Array of Tiles
        /// </summary>
        /// <param name="numberOfRows">number of rows</param>
        /// <param name="numberOfColumns">number of columns</param>
        public static void CreateArray(int numberOfRows, int numberOfColumns)
        {
            int txtNumber = 1;
            NPuzzle.tileArray = new string[numberOfRows, numberOfColumns];
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {

                    NPuzzle.tileArray[i, j] = txtNumber.ToString();
                    txtNumber++;
                }
            }
        }

        /// <summary>
        /// create shuffle tiles 
        /// </summary>
        /// <param name="numberOfRows">number of rows</param>
        /// <param name="numberOfColumns">number of columns</param>
        public static void ShuffleTile(int numberOfRows, int numberOfColumns)
        {
            int rndCount = 0;
            int cCol = 0;
            int cRow = 0;
            string tmp = "";
            Random rnd = new Random();
            string[,] tileArray = NPuzzle.tileArray;

            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    if (tileArray[i, j] == NPuzzle.clearNumber.ToString())
                    {
                        cRow = i;
                        cCol = j;
                    }

                }
            }

            do
            {
                int pos = rnd.Next(0, 4);

                if (pos == 0)
                {
                    //left
                    if (cCol != 0)
                    {
                        tmp = tileArray[cRow, cCol - 1];
                        tileArray[cRow, cCol - 1] = tileArray[cRow, cCol];
                        tileArray[cRow, cCol] = tmp;
                        cCol--;
                        rndCount++;
                    }
                }
                else if (pos == 1)
                {
                    //right
                    if (cCol < tileArray.GetLength(1) - 1)
                    {
                        tmp = tileArray[cRow, cCol + 1];
                        tileArray[cRow, cCol + 1] = tileArray[cRow, cCol];
                        tileArray[cRow, cCol] = tmp;
                        cCol++;
                        rndCount++;
                    }
                }
                else if (pos == 2)
                {
                    //top
                    if (cRow != 0)
                    {
                        tmp = tileArray[cRow - 1, cCol];
                        tileArray[cRow - 1, cCol] = tileArray[cRow, cCol];
                        tileArray[cRow, cCol] = tmp;
                        cRow--;
                        rndCount++;
                    }
                }
                else
                {
                    //bottom
                    if (cRow < tileArray.GetLength(0) - 1)
                    {
                        tmp = tileArray[cRow + 1, cCol];
                        tileArray[cRow + 1, cCol] = tileArray[cRow, cCol];
                        tileArray[cRow, cCol] = tmp;
                        cRow++;
                        rndCount++;
                    }

                }
            } while (rndCount < 50);
        }
    }
}
