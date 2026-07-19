using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;

namespace TicTacToe
{
    public class TicTacToe : IGameState<TicTacToe>
    {
        public TicTacToe(bool isWin = false, bool isLoss = false, bool isTie = false, bool isTerminal = false, int[][] board = null)
        {
            IsWin = isWin;
            IsLoss = isLoss;
            IsTie = isTie;
            IsTerminal = isTerminal;
            Board = board;
        }
        public bool IsWin { get; set; }

        public bool IsLoss { get; set; }

        public bool IsTie { get; set; }

        public bool IsTerminal { get; set; }

        public int[][] Board { get; set; }

        public int CheckEnd(int[][] board)
        {
            foreach (int[] row in board)
            {
                if (row[0] == row[1] && row[1] == row[2] && row[0] != 0)
                {
                    return row[0];
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (board[0][i] == board[1][i] && board[1][i] == board[2][i] && board[0][i] != 0)
                {
                    return board[0][i];
                }
            }
            if (board[0][0] == board[1][1] && board[1][1] == board[2][2] && board[0][0] != 0)
            {
                return board[0][0];
            }
            if (board[0][2] == board[1][1] && board[1][1] == board[2][0] && board[0][2] != 0)
            {
                return board[0][2];
            }
            foreach(int[] row in board)
            {
                foreach (int cell in row)
                {
                    if (cell == 0)
                    {
                        return 2;
                    }
                }
            }
            return 0;
        }

        public TicTacToe[] GetChildren(bool isMax)
        {
            List<TicTacToe> children = new List<TicTacToe>();
            if(IsTerminal)
            {
                return children.ToArray();
            }
            foreach(int[] row in Board)
            {
                foreach (int cell in row)
                {
                    if (cell == 0)
                    {
                        int[][] newBoard = [[0, 0, 0], [0, 0, 0], [0, 0, 0]];
                        for(int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                newBoard[i][j] = Board[i][j];
                            }
                        }
                        newBoard[Array.IndexOf(newBoard, row)][Array.IndexOf(row, cell)] = isMax ? 1 : -1;
                        switch (CheckEnd(newBoard))
                        {
                            case 1:
                                children.Add(new TicTacToe(isWin: true, isLoss: false, isTie: false, isTerminal: true, board: newBoard));
                                break;
                            case -1:
                                children.Add(new TicTacToe(isWin: false, isLoss: true, isTie: false, isTerminal: true, board: newBoard));
                                break;
                            case 0:
                                children.Add(new TicTacToe(isWin: false, isLoss: false, isTie: true, isTerminal: true, board: newBoard));
                                break;
                            default:
                                children.Add(new TicTacToe(board: newBoard));
                                break;
                        }
                    }
                }
            }
            return children.ToArray();
        }
    }
}
