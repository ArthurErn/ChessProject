using board;
using System;

namespace chess {
    class Knight : Piece {
        public Knight(Board board, Color color) : base(board, color) {
        }

        public override bool[,] possibleMovements() {
            bool[,] mat = new bool[board.linhas, board.colunas];
            Position pos = new Position(0, 0);

            pos.defineValues(position.linha - 1, position.coluna - 2);
            //N
            if (board.validPosition(pos) && canMove(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //NE
            pos.defineValues(position.linha - 2, position.coluna - 1);
            if (board.validPosition(pos) && canMove(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //E
            pos.defineValues(position.linha - 2, position.coluna + 1);
            if (board.validPosition(pos) && canMove(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //SE
            pos.defineValues(position.linha + 1, position.coluna + 2);
            if (board.validPosition(pos) && canMove(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            return mat;
        }

        private bool canMove(Position pos) {
            Piece piece = board.piece(pos);
            return piece == null || piece.color != color;
        }

        public override string ToString() {

            return " ♞ ";
        }
    }
}
