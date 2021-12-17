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

        public abstract bool[,] possibleMovements();
    }
}
