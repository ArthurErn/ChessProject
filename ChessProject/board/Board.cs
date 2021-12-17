namespace board {
    class Board {
        public int linhas { get; set; }
        public int colunas { get; set; }
        private Piece[,] pieces;
        public Piece piece(int linha, int coluna) {
            return pieces[linha, coluna];
        }

        public bool pieceExists(Position pos) {
            validatePosition(pos);
            return piece(pos) != null;
        }

        public Piece piece(Position pos) {
            return pieces[pos.linha, pos.coluna];
        }

        public Board(int linhas, int colunas) {
            this.linhas = linhas;
            this.colunas = colunas;
            pieces = new Piece[linhas, colunas];
        }

        public void insertPiece(Piece p, Position pos) {
            if (pieceExists(pos)) {
                throw new BoardException("There's another piece in this position");
            }
            pieces[pos.linha, pos.coluna] = p;
            p.position = pos;
        }

        public bool validPosition(Position pos) {
            if (pos.linha < 0 || pos.linha >= linhas || pos.coluna < 0 || pos.coluna >= colunas) {
                return false;
            }
            return true;
        }

        public void validatePosition(Position pos) {
            if (!validPosition(pos)) {
                throw new BoardException("Invalid position!");
            }
        }
    }
}
