namespace board {
    abstract class Piece {
        public Piece(Board board, Color color) {
            position = null;
            this.board = board;
            this.color = color;
            qtMovement = 0;
        }

        public void incrementPieceMoveQuantity() {
            qtMovement++;
        }
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int qtMovement { get; protected set; }
        public Board board { get; protected set; }

        public bool possibleMovementsExists() {
            bool[,] mat = possibleMovements();
            for (int i = 0; i < board.linhas; i++) {
                for (int j = 0; j < board.colunas; j++) {
                    if (mat[i, j]) {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool canMoveTo(Position pos) {
            return possibleMovements()[pos.linha, pos.coluna];        
        }

        public abstract bool[,] possibleMovements();
    }
}
