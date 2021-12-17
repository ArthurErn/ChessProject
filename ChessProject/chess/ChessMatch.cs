using board;

namespace chess {
    class ChessMatch {
        public Board board { get; private set; }
        private int turn;
        private Color currentPlayer;
        public bool finished { get; private set; }

        public ChessMatch() {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            insertPieces();
        }

        public void insertPieces() {
            board.insertPiece(new Rook(board, Color.White), new ChessPosition('c', 1).toPosition());
            board.insertPiece(new Rook(board, Color.White), new ChessPosition('c', 2).toPosition());
            board.insertPiece(new Rook(board, Color.White), new ChessPosition('d', 2).toPosition());
            board.insertPiece(new Rook(board, Color.White), new ChessPosition('e', 1).toPosition());
            board.insertPiece(new Rook(board, Color.White), new ChessPosition('e', 2).toPosition());
            board.insertPiece(new King(board, Color.White), new ChessPosition('d', 1).toPosition());

            board.insertPiece(new Rook(board, Color.Black), new ChessPosition('c', 7).toPosition());
            board.insertPiece(new Rook(board, Color.Black), new ChessPosition('c', 8).toPosition());
            board.insertPiece(new Rook(board, Color.Black), new ChessPosition('d', 7).toPosition());
            board.insertPiece(new Rook(board, Color.Black), new ChessPosition('e', 7).toPosition());
            board.insertPiece(new Rook(board, Color.Black), new ChessPosition('e', 8).toPosition());
            board.insertPiece(new King(board, Color.Black), new ChessPosition('d', 8).toPosition());
        }

        public void executeMove(Position start, Position end) {
            Piece p = board.removePiece(start);
            p.incrementPieceMoveQuantity();
            Piece capturedPiece = board.removePiece(end);
            board.insertPiece(p, end);
        }
    }
}
