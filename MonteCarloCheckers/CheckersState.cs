using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;

namespace MonteCarloCheckers
{
    public enum PieceState
    {
        Red,
        Black,
        RedKing,
        BlackKing,
        Empty
    }
    public class CheckersState : IMonteCarloGameState<CheckersState>
    {
        public bool IsWin { get; set; }
        public bool IsLoss { get; set; }
        public bool IsTie { get; set; }
        public bool IsTerminal { get; set; }
        public int Value { get; set; }
        public int Count { get; set; }
        public List<CheckersState> Children { get; set; }
        public PieceState[,] Board { get; set; }
        public CheckersState(PieceState[,] board)
        {
            IsWin = false;
            IsLoss = false;
            IsTie = false;
            IsTerminal = false;
            Value = 0;
            Count = 0;
            Children = new List<CheckersState>();
            Board = board;
        }
        public void GenerateChildren(bool isMax)
        {
            Children.Clear();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    switch (Board[i, j])
                    {
                        case PieceState.Red:
                            if (isMax)
                            {
                                if (i != 0 && j != 7)
                                {
                                    if (i > 1 && j < 6 && (Board[i - 1, j + 1] == PieceState.Black || Board[i - 1, j + 1] == PieceState.BlackKing) && Board[i - 2, j + 2] == PieceState.Empty)
                                    {
                                        var newBoard = (PieceState[,])Board.Clone();
                                        newBoard[i, j] = PieceState.Empty;
                                        newBoard[i - 1, j + 1] = PieceState.Empty;
                                        newBoard[i - 2, j + 2] = PieceState.Red;
                                        Children.Add(new CheckersState(newBoard));
                                    }
                                    else if (Board[i - 1, j + 1] == PieceState.Empty)
                                    {
                                        var newBoard = (PieceState[,])Board.Clone();
                                        newBoard[i, j] = PieceState.Empty;
                                        newBoard[i - 1, j + 1] = PieceState.Red;
                                        Children.Add(new CheckersState(newBoard));
                                    }
                                }
                                if (i != 7 && j != 7)
                                {
                                    if (i < 6 && j < 6 && (Board[i + 1, j + 1] == PieceState.Black || Board[i + 1, j + 1] == PieceState.BlackKing) && Board[i + 2, j + 2] == PieceState.Empty)
                                    {
                                        var newBoard = (PieceState[,])Board.Clone();
                                        newBoard[i, j] = PieceState.Empty;
                                        newBoard[i + 1, j + 1] = PieceState.Empty;
                                        newBoard[i + 2, j + 2] = PieceState.Red;
                                        Children.Add(new CheckersState(newBoard));
                                    }
                                    else if (Board[i + 1, j + 1] == PieceState.Empty)
                                    {
                                        var newBoard = (PieceState[,])Board.Clone();
                                        newBoard[i, j] = PieceState.Empty;
                                        newBoard[i + 1, j + 1] = PieceState.Red;
                                        Children.Add(new CheckersState(newBoard));
                                    }
                                }
                            }
                            break;
                        case PieceState.Black:
                            if (!isMax)
                            {
                                if (i != 0 && j != 0)
                                {

                                    if (i > 1 && j > 1 && (Board[i - 1, j - 1] == PieceState.Red || Board[i - 1, j - 1] == PieceState.RedKing) && Board[i - 2, j - 2] == PieceState.Empty)
                                    {
                                        var newBoard = (PieceState[,])Board.Clone();
                                        newBoard[i, j] = PieceState.Empty;
                                        newBoard[i - 1, j - 1] = PieceState.Empty;
                                        newBoard[i - 2, j - 2] = PieceState.Black;
                                        Children.Add(new CheckersState(newBoard));
                                    }
                                    else if (Board[i - 1, j - 1] == PieceState.Empty)
                                    {
                                        var newBoard = (PieceState[,])Board.Clone();
                                        newBoard[i, j] = PieceState.Empty;
                                        newBoard[i - 1, j - 1] = PieceState.Black;
                                        Children.Add(new CheckersState(newBoard));
                                    }
                                }
                                if (i != 7 && j != 0)
                                {
                                    if (i < 6 && j > 1 && (Board[i + 1, j - 1] == PieceState.Red || Board[i + 1, j - 1] == PieceState.RedKing) && Board[i + 2, j - 2] == PieceState.Empty)
                                    {
                                        var newBoard = (PieceState[,])Board.Clone();
                                        newBoard[i, j] = PieceState.Empty;
                                        newBoard[i + 1, j - 1] = PieceState.Empty;
                                        newBoard[i + 2, j - 2] = PieceState.Black;
                                        Children.Add(new CheckersState(newBoard));
                                    }
                                    else if (Board[i + 1, j - 1] == PieceState.Empty)
                                    {
                                        var newBoard = (PieceState[,])Board.Clone();
                                        newBoard[i, j] = PieceState.Empty;
                                        newBoard[i + 1, j - 1] = PieceState.Black;
                                        Children.Add(new CheckersState(newBoard));
                                    }
                                }
                            }
                            break;
                        case PieceState.RedKing:
                            if (isMax)
                            {
                                // Up-left diagonal
                                if (i != 0 && j != 0)
                                {
                                    if (i > 1 && j > 1 && (Board[i - 1, j - 1] == PieceState.Black || Board[i - 1, j - 1] == PieceState.BlackKing) && Board[i - 2, j - 2] == PieceState.Empty)
                                    {
                                        var newBoard = (PieceState[,])Board.Clone();
                                        newBoard[i, j] = PieceState.Empty;
                                        newBoard[i - 1, j - 1] = PieceState.Empty;
                                        newBoard[i - 2, j - 2] = PieceState.RedKing;
                                        Children.Add(new CheckersState(newBoard));
                                    }
                                    else if (Board[i - 1, j - 1] == PieceState.Empty)
                                    {
                                        var newBoard = (PieceState[,])Board.Clone();
                                        newBoard[i, j] = PieceState.Empty;
                                        newBoard[i - 1, j - 1] = PieceState.RedKing;
                                        Children.Add(new CheckersState(newBoard));
                                    }
                                }
                                // Up-right diagonal
                                if (i != 0 && j != 7)
                                {
                                    if (i > 1 && j < 6 && (Board[i - 1, j + 1] == PieceState.Black || Board[i - 1, j + 1] == PieceState.BlackKing) && Board[i - 2, j + 2] == PieceState.Empty)
                                    {
                                        var newBoard = (PieceState[,])Board.Clone();
                                        newBoard[i, j] = PieceState.Empty;
                                        newBoard[i - 1, j + 1] = PieceState.Empty;
                                        newBoard[i - 2, j + 2] = PieceState.RedKing;
                                        Children.Add(new CheckersState(newBoard));
                                    }
                                    else if (Board[i - 1, j + 1] == PieceState.Empty)
                                    {
                                        var newBoard = (PieceState[,])Board.Clone();
                                        newBoard[i, j] = PieceState.Empty;
                                        newBoard[i - 1, j + 1] = PieceState.RedKing;
                                        Children.Add(new CheckersState(newBoard));
                                    }
                                }
                                // Down-left diagonal
                                if (i != 7 && j != 0)
                                {
                                    if (i < 6 && j > 1 && (Board[i + 1, j - 1] == PieceState.Black || Board[i + 1, j - 1] == PieceState.BlackKing) && Board[i + 2, j - 2] == PieceState.Empty)
                                    {
                                        var newBoard = (PieceState[,])Board.Clone();
                                        newBoard[i, j] = PieceState.Empty;
                                        newBoard[i + 1, j - 1] = PieceState.Empty;
                                        newBoard[i + 2, j - 2] = PieceState.RedKing;
                                        Children.Add(new CheckersState(newBoard));
                                    }
                                    else if (Board[i + 1, j - 1] == PieceState.Empty)
                                    {
                                        var newBoard = (PieceState[,])Board.Clone();
                                        newBoard[i, j] = PieceState.Empty;
                                        newBoard[i + 1, j - 1] = PieceState.RedKing;
                                        Children.Add(new CheckersState(newBoard));
                                    }
                                }
                                // Down-right diagonal
                                if (i != 7 && j != 7)
                                {
                                    if (i < 6 && j < 6 && (Board[i + 1, j + 1] == PieceState.Black || Board[i + 1, j + 1] == PieceState.BlackKing) && Board[i + 2, j + 2] == PieceState.Empty)
                                    {
                                        var newBoard = (PieceState[,])Board.Clone();
                                        newBoard[i, j] = PieceState.Empty;
                                        newBoard[i + 1, j + 1] = PieceState.Empty;
                                        newBoard[i + 2, j + 2] = PieceState.RedKing;
                                        Children.Add(new CheckersState(newBoard));
                                    }
                                    else if (Board[i + 1, j + 1] == PieceState.Empty)
                                    {
                                        var newBoard = (PieceState[,])Board.Clone();
                                        newBoard[i, j] = PieceState.Empty;
                                        newBoard[i + 1, j + 1] = PieceState.RedKing;
                                        Children.Add(new CheckersState(newBoard));
                                    }
                                }
                            }
                            break;
                        case PieceState.BlackKing:
                            if (!isMax)
                            {
                                // Up-left diagonal
                                if (i != 0 && j != 0)
                                {
                                    if (i > 1 && j > 1 && (Board[i - 1, j - 1] == PieceState.Red || Board[i - 1, j - 1] == PieceState.RedKing) && Board[i - 2, j - 2] == PieceState.Empty)
                                    {
                                        var newBoard = (PieceState[,])Board.Clone();
                                        newBoard[i, j] = PieceState.Empty;
                                        newBoard[i - 1, j - 1] = PieceState.Empty;
                                        newBoard[i - 2, j - 2] = PieceState.BlackKing;
                                        Children.Add(new CheckersState(newBoard));
                                    }
                                    else if (Board[i - 1, j - 1] == PieceState.Empty)
                                    {
                                        var newBoard = (PieceState[,])Board.Clone();
                                        newBoard[i, j] = PieceState.Empty;
                                        newBoard[i - 1, j - 1] = PieceState.BlackKing;
                                        Children.Add(new CheckersState(newBoard));
                                    }
                                }
                                // Up-right diagonal
                                if (i != 0 && j != 7)
                                {
                                    if (i > 1 && j < 6 && (Board[i - 1, j + 1] == PieceState.Red || Board[i - 1, j + 1] == PieceState.RedKing) && Board[i - 2, j + 2] == PieceState.Empty)
                                    {
                                        var newBoard = (PieceState[,])Board.Clone();
                                        newBoard[i, j] = PieceState.Empty;
                                        newBoard[i - 1, j + 1] = PieceState.Empty;
                                        newBoard[i - 2, j + 2] = PieceState.BlackKing;
                                        Children.Add(new CheckersState(newBoard));
                                    }
                                    else if (Board[i - 1, j + 1] == PieceState.Empty)
                                    {
                                        var newBoard = (PieceState[,])Board.Clone();
                                        newBoard[i, j] = PieceState.Empty;
                                        newBoard[i - 1, j + 1] = PieceState.BlackKing;
                                        Children.Add(new CheckersState(newBoard));
                                    }
                                }
                                // Down-left diagonal
                                if (i != 7 && j != 0)
                                {
                                    if (i < 6 && j > 1 && (Board[i + 1, j - 1] == PieceState.Red || Board[i + 1, j - 1] == PieceState.RedKing) && Board[i + 2, j - 2] == PieceState.Empty)
                                    {
                                        var newBoard = (PieceState[,])Board.Clone();
                                        newBoard[i, j] = PieceState.Empty;
                                        newBoard[i + 1, j - 1] = PieceState.Empty;
                                        newBoard[i + 2, j - 2] = PieceState.BlackKing;
                                        Children.Add(new CheckersState(newBoard));
                                    }
                                    else if (Board[i + 1, j - 1] == PieceState.Empty)
                                    {
                                        var newBoard = (PieceState[,])Board.Clone();
                                        newBoard[i, j] = PieceState.Empty;
                                        newBoard[i + 1, j - 1] = PieceState.BlackKing;
                                        Children.Add(new CheckersState(newBoard));
                                    }
                                }
                                // Down-right diagonal
                                if (i != 7 && j != 7)
                                {
                                    if (i < 6 && j < 6 && (Board[i + 1, j + 1] == PieceState.Red || Board[i + 1, j + 1] == PieceState.RedKing) && Board[i + 2, j + 2] == PieceState.Empty)
                                    {
                                        var newBoard = (PieceState[,])Board.Clone();
                                        newBoard[i, j] = PieceState.Empty;
                                        newBoard[i + 1, j + 1] = PieceState.Empty;
                                        newBoard[i + 2, j + 2] = PieceState.BlackKing;
                                        Children.Add(new CheckersState(newBoard));
                                    }
                                    else if (Board[i + 1, j + 1] == PieceState.Empty)
                                    {
                                        var newBoard = (PieceState[,])Board.Clone();
                                        newBoard[i, j] = PieceState.Empty;
                                        newBoard[i + 1, j + 1] = PieceState.BlackKing;
                                        Children.Add(new CheckersState(newBoard));
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
