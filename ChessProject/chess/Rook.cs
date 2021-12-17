using board;

namespace chess {
    class Rook : Piece {
        public Rook(Board board, Color color) : base(board, color) {
        }

        public override bool[,] possibleMovements() {
            bool[,] mat = new bool[board.linhas, board.colunas];
            Position pos = new Position(0, 0);

            //N
            pos.defineValues(position.linha - 1, position.coluna);
            while (board.validPosition(pos) && canMove(pos)){
                mat[pos.linha, pos.coluna] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color) {
                    break;
                }
                pos.linha = pos.linha - 1;

            }
            //S
            pos.defineValues(position.linha + 1, position.coluna);
            while (board.validPosition(pos) && canMove(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color) {
                    break;
                }
                pos.linha = pos.linha + 1;

            }
            //E
            pos.defineValues(position.linha, position.coluna + 1);
            while (board.validPosition(pos) && canMove(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color) {
                    break;
                }
                pos.coluna = pos.coluna + 1;

            }
            //W
            pos.defineValues(position.linha, position.coluna - 1);
            while (board.validPosition(pos) && canMove(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color) {
                    break;
                }
                pos.coluna = pos.coluna - 1;

            }
            return mat;
        }

        private bool canMove(Position pos) {
            Piece piece = board.piece(pos);
            return piece == null || piece.color != color;
        }

        public override string ToString() {
            return " R ";
        }
    }
}
