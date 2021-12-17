using board;
using System;

namespace chess {
    class Pawn : Piece {
        public Pawn(Board board, Color color) : base(board, color) {
        }

        private bool enemyExists(Position pos) {
            Piece piece = board.piece(pos);
            return piece != null && piece.color != color;
        }

        private bool free(Position pos) {
            return board.piece(pos) == null;
        }

        public override bool[,] possibleMovements() {
            bool[,] mat = new bool[board.linhas, board.colunas];
            Position pos = new Position(0, 0);
            if (color == Color.White) {
                pos.defineValues(position.linha - 1, position.coluna);
                if (board.validPosition(pos) && free(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.defineValues(position.linha - 2, position.coluna);
                if (board.validPosition(pos) && free(pos) && qtMovement == 0) {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.defineValues(position.linha - 1, position.coluna - 1);
                if (board.validPosition(pos) && enemyExists(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.defineValues(position.linha - 1, position.coluna + 1);
                if (board.validPosition(pos) && enemyExists(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
            } else {
                pos.defineValues(position.linha + 1, position.coluna);
                if (board.validPosition(pos) && free(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.defineValues(position.linha + 2, position.coluna);
                if (board.validPosition(pos) && free(pos) && qtMovement == 0) {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.defineValues(position.linha + 1, position.coluna - 1);
                if (board.validPosition(pos) && enemyExists(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.defineValues(position.linha + 1, position.coluna + 1);
                if (board.validPosition(pos) && enemyExists(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
            }
            return mat;
        }

        private bool canMove(Position pos) {
            Piece piece = board.piece(pos);
            return piece == null || piece.color != color;
        }

        public override string ToString() {

            return " ♟";
        }
    }
}
