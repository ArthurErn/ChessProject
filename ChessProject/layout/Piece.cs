namespace layout {
    class Piece {
        public Piece(Position position, Board board, Color color) {
            this.position = position;
            this.board = board;
            this.color = color;
            qtMovement = 0;
        }

        public Position position { get; set; }
        public Color color { get; protected set; }
        public int qtMovement { get; protected set; }
        public Board board { get; protected set; }
    }
}
