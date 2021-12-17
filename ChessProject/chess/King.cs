using board;
using System;

namespace chess {
    class King : Piece {
        public King(Board board, Color color, ChessMatch match) : base(board, color) {
            this.match = match;
        }

        private ChessMatch match;
        private bool testRookForCastle(Position pos) {
            Piece piece = board.piece(pos);
            return piece != null && piece is Rook && piece.color == color && piece.qtMovement == 0;
        }
        public override bool[,] possibleMovements() {
            bool[,] mat = new bool[board.linhas, board.colunas];
            Position pos = new Position(0, 0);

            pos.defineValues(position.linha - 1, position.coluna);
            //N
            if (board.validPosition(pos) && canMove(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //NE
            pos.defineValues(position.linha - 1, position.coluna + 1);
            if (board.validPosition(pos) && canMove(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //E
            pos.defineValues(position.linha, position.coluna + 1);
            if (board.validPosition(pos) && canMove(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //SE
            pos.defineValues(position.linha + 1, position.coluna + 1);
            if (board.validPosition(pos) && canMove(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //S
            pos.defineValues(position.linha + 1, position.coluna);
            if (board.validPosition(pos) && canMove(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //SW
            pos.defineValues(position.linha + 1, position.coluna - 1);
            if (board.validPosition(pos) && canMove(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //W
            pos.defineValues(position.linha, position.coluna - 1);
            if (board.validPosition(pos) && canMove(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //NW
            pos.defineValues(position.linha - 1, position.coluna - 1);
            if (board.validPosition(pos) && canMove(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //castle
            if (qtMovement == 0 && !match.check) {
                Position rookPositionSC = new Position(position.linha, position.coluna + 3);
                if (testRookForCastle(rookPositionSC)) {
                    Position p1 = new Position(position.linha, position.coluna + 1);
                    Position p2 = new Position(position.linha, position.coluna + 2);
                    if (board.piece(p1) == null && board.piece(p2) == null) {
                        mat[position.linha, position.coluna + 2] = true;
                    }
                }
                Position rookPositionBC = new Position(position.linha, position.coluna - 4);
                if (testRookForCastle(rookPositionBC)) {
                    Position p1 = new Position(position.linha, position.coluna - 1);
                    Position p2 = new Position(position.linha, position.coluna - 2);
                    Position p3 = new Position(position.linha, position.coluna - 3);
                    if (board.piece(p1) == null && board.piece(p2) == null && board.piece(p3) == null) {
                        mat[position.linha, position.coluna - 2] = true;
                    }
                }
            }
            return mat;
        }

        private bool canMove(Position pos) {
            Piece piece = board.piece(pos);
            return piece == null || piece.color != color;
        }

        public override string ToString() {
            
            return " ♚ ";
        }
    }
}
