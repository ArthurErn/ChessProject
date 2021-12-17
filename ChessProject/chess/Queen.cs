using board;

namespace chess {
    class Queen : Piece {
        public Queen(Board board, Color color) : base(board, color) {
        }

        public override bool[,] possibleMovements() {
            bool[,] mat = new bool[board.linhas, board.colunas];
            Position pos = new Position(0, 0);

            //NW
            pos.defineValues(position.linha - 1, position.coluna - 1);
            while (board.validPosition(pos) && canMove(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color) {
                    break;
                }
                pos.defineValues(pos.linha - 1, pos.coluna - 1);

            }
            //NE
            pos.defineValues(position.linha - 1, position.coluna + 1);
            while (board.validPosition(pos) && canMove(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color) {
                    break;
                }
                pos.defineValues(pos.linha - 1, pos.coluna + 1);

            }
            //SE
            pos.defineValues(position.linha + 1, position.coluna + 1);
            while (board.validPosition(pos) && canMove(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color) {
                    break;
                }
                pos.defineValues(pos.linha + 1, pos.coluna + 1);

            }
            //SW
            pos.defineValues(position.linha + 1, position.coluna - 1);
            while (board.validPosition(pos) && canMove(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color) {
                    break;
                }
                pos.defineValues(pos.linha + 1, pos.coluna - 1);

            }
            pos.defineValues(position.linha - 1, position.coluna);
            //N
            if (board.validPosition(pos) && canMove(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //E
            pos.defineValues(position.linha, position.coluna + 1);
            if (board.validPosition(pos) && canMove(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //S
            pos.defineValues(position.linha + 1, position.coluna);
            if (board.validPosition(pos) && canMove(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //W
            pos.defineValues(position.linha, position.coluna - 1);
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

            return " ♛ ";
        }
    }
}
