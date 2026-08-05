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
            Count = 0;
            Children = new List<CheckersState>();
            Board = board;
            Value = EvaluateBoard(board);
        }

        private static int EvaluateBoard(PieceState[,] board)
        {
            int value = 0;
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    switch (board[row, col])
                    {
                        case PieceState.Red:
                            value += 1;
                            break;
                        case PieceState.RedKing:
                            value += 3;
                            break;
                        case PieceState.Black:
                            value -= 1;
                            break;
                        case PieceState.BlackKing:
                            value -= 3;
                            break;
                    }
                }
            }
            return value;
        }
        private static readonly (int rowDelta, int colDelta)[] KingMoveDirections = new (int, int)[]
        {
            (-1, -1), (-1, 1), (1, -1), (1, 1)
        };

        private static readonly (int rowDelta, int colDelta)[] RedPawnMoveDirections = new (int, int)[]
        {
            (-1, 1), (1, 1)
        };

        private static readonly (int rowDelta, int colDelta)[] BlackPawnMoveDirections = new (int, int)[]
        {
            (-1, -1), (1, -1)
        };

        public void GenerateChildren(bool isMax)
        {
            Children.Clear();

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    switch (Board[row, col])
                    {
                        case PieceState.Red:
                            if (isMax)
                            {
                                GeneratePawnMoves(row, col, RedPawnMoveDirections, PieceState.Black, PieceState.BlackKing, PieceState.Red);
                            }
                            break;
                        case PieceState.Black:
                            if (!isMax)
                            {
                                GeneratePawnMoves(row, col, BlackPawnMoveDirections, PieceState.Red, PieceState.RedKing, PieceState.Black);
                            }
                            break;
                        case PieceState.RedKing:
                            if (isMax)
                            {
                                GenerateKingMoves(row, col, PieceState.Black, PieceState.BlackKing, PieceState.RedKing);
                            }
                            break;
                        case PieceState.BlackKing:
                            if (!isMax)
                            {
                                GenerateKingMoves(row, col, PieceState.Red, PieceState.RedKing, PieceState.BlackKing);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            if (Children.Count == 0)
            {
                var terminalState = new CheckersState((PieceState[,])Board.Clone())
                {
                    IsTerminal = true,
                    IsLoss = true
                };

                Children.Add(terminalState);
                return;
            }

            var enemyPieces = GetEnemyPieces(isMax);
            bool enemyHasPieces = false;
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (enemyPieces.Contains(Board[row, col]))
                    {
                        enemyHasPieces = true;
                        break;
                    }
                }
                if (enemyHasPieces)
                {
                    break;
                }
            }

            if (!enemyHasPieces)
            {
                foreach (var child in Children)
                {
                    child.IsTerminal = true;
                    child.IsWin = true;
                }
            }
        }

        private static HashSet<PieceState> GetEnemyPieces(bool isMax)
        {
            return isMax
                ? new HashSet<PieceState> { PieceState.Black, PieceState.BlackKing }
                : new HashSet<PieceState> { PieceState.Red, PieceState.RedKing };
        }

        private void GeneratePawnMoves(int fromRow, int fromCol, (int rowDelta, int colDelta)[] directions, PieceState enemy, PieceState enemyKing, PieceState pawn)
        {
            foreach (var (rowDelta, colDelta) in directions)
            {
                int adjacentRow = fromRow + rowDelta;
                int adjacentCol = fromCol + colDelta;

                if (!IsOnBoard(adjacentRow, adjacentCol))
                {
                    continue;
                }

                if (Board[adjacentRow, adjacentCol] == PieceState.Empty)
                {
                    AddChild(fromRow, fromCol, adjacentRow, adjacentCol, pawn, isMax: true);
                }
                else if (Board[adjacentRow, adjacentCol] == enemy || Board[adjacentRow, adjacentCol] == enemyKing)
                {
                    int landingRow = fromRow + 2 * rowDelta;
                    int landingCol = fromCol + 2 * colDelta;

                    if (IsOnBoard(landingRow, landingCol) && Board[landingRow, landingCol] == PieceState.Empty)
                    {
                        AddChild(fromRow, fromCol, landingRow, landingCol, pawn, isMax: true, captureRow: adjacentRow, captureCol: adjacentCol);
                    }
                }
            }
        }

        private void GenerateKingMoves(int fromRow, int fromCol, PieceState enemy, PieceState enemyKing, PieceState king)
        {
            foreach (var (rowDelta, colDelta) in KingMoveDirections)
            {
                int adjacentRow = fromRow + rowDelta;
                int adjacentCol = fromCol + colDelta;

                if (!IsOnBoard(adjacentRow, adjacentCol))
                {
                    continue;
                }

                if (Board[adjacentRow, adjacentCol] == PieceState.Empty)
                {
                    AddChild(fromRow, fromCol, adjacentRow, adjacentCol, king, isMax: true);
                }
                else if (Board[adjacentRow, adjacentCol] == enemy || Board[adjacentRow, adjacentCol] == enemyKing)
                {
                    int landingRow = fromRow + 2 * rowDelta;
                    int landingCol = fromCol + 2 * colDelta;

                    if (IsOnBoard(landingRow, landingCol) && Board[landingRow, landingCol] == PieceState.Empty)
                    {
                        AddChild(fromRow, fromCol, landingRow, landingCol, king, isMax: true, captureRow: adjacentRow, captureCol: adjacentCol);
                    }
                }
            }
        }

        private void AddChild(int fromRow, int fromCol, int toRow, int toCol, PieceState piece, bool isMax, int? captureRow = null, int? captureCol = null)
        {
            var newBoard = (PieceState[,])Board.Clone();
            newBoard[fromRow, fromCol] = PieceState.Empty;
            newBoard[toRow, toCol] = piece;

            if (captureRow.HasValue)
            {
                newBoard[captureRow.Value, captureCol.Value] = PieceState.Empty;
            }

            bool isPromotion = (piece == PieceState.Red && toRow == 0) ||
                               (piece == PieceState.Black && toRow == 7);

            if (isPromotion)
            {
                newBoard[toRow, toCol] = piece == PieceState.Red ? PieceState.RedKing : PieceState.BlackKing;
            }

            var child = new CheckersState(newBoard);

            var enemyPieces = GetEnemyPieces(isMax);
            bool enemyHasPieces = false;
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (enemyPieces.Contains(newBoard[row, col]))
                    {
                        enemyHasPieces = true;
                        break;
                    }
                }
                if (enemyHasPieces)
                {
                    break;
                }
            }

            if (!enemyHasPieces)
            {
                child.IsTerminal = true;
                child.IsWin = true;
            }

            Children.Add(child);
        }

        private static bool IsOnBoard(int row, int col)
        {
            return row >= 0 && row < 8 && col >= 0 && col < 8;
        }
    }
}
