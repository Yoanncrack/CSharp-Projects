using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        #region member variables
        MarkUp[,] _cells;
        /// <summary>
        /// Represents player turn. Can be 1 or 2 (set to 1 by default)
        /// </summary>
        MarkUp _currentMarkUp;
        #endregion
        public MainWindow()
        {

            InitializeComponent();
            NewGame();
        }

        private void NewGame()
        {
            _cells = new MarkUp[3,3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _cells[i,j] = MarkUp.free;
                }
            }
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
            });
            _currentMarkUp = MarkUp.cross;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            if (_cells[row, column] != MarkUp.free)
            {
                return;
            }
            _cells[row, column] = _currentMarkUp;
            button.Content = (_currentMarkUp == MarkUp.cross) ? 'X' : 'O';
            if (checkWin())
            {
                NewGame();
                return;
            }
            _currentMarkUp = (_currentMarkUp == MarkUp.cross) ? MarkUp.nought : MarkUp.cross;
        }
        private bool checkWin()
        {
            return (checkColsWin() || checkRowsWin() || checkDiagsWin());
        }

        private bool checkRowsWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if ((_cells[0, i] == MarkUp.free || _cells[1, i] == MarkUp.free) && _cells[2, i] == MarkUp.free)
                {
                    return false;
                }

                if ((_cells[0, i] == _cells[1, i] && _cells[1, i] == _cells[2, i]))
                {
                    return true;
                }
            }
            return false;
        }

        private bool checkColsWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if ((_cells[i, 0] == MarkUp.free || _cells[i, 1] == MarkUp.free) && _cells[i, 2] == MarkUp.free)
                {
                    return false;
                }

                if ((_cells[i, 0] == _cells[i, 1] && _cells[i, 1] == _cells[i, 2]))
                {
                    return true;
                }
            }
            return false;
        }


        private bool checkDiagsWin()
        {
            return
                (
                    (
                        (
                        _cells[0, 0] != MarkUp.free && _cells[1,1] != MarkUp.free && _cells[2,2] != MarkUp.free 
                        )
                        &&
                        (
                            _cells[0, 0] == _cells[1, 1] && _cells[1, 1] == _cells[2, 2]
                        )
                    )
                    ||
                    (
                        (
                        _cells[0, 2] != MarkUp.free && _cells[1, 1] != MarkUp.free && _cells[2, 0] != MarkUp.free
                        )
                        &&
                        (
                            _cells[0, 2] == _cells[1, 1] && _cells[1, 1] == _cells[2, 0]
                        )
                    )
                );

        }

    }
}
