using board;

namespace chess {
    class ChessMatch {
        public Board board { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
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

        public void makePlay(Position start, Position end) {
            executeMove(start, end);
            turn++;
            changePlayer();

        }

        public void validateStartPosition(Position pos) {
            if (board.piece(pos) == null) {
                throw new BoardException("There's no piece in that slot!");
            }
            if (currentPlayer != board.piece(pos).color) {
                throw new BoardException("That piece is not yours!");
            }
            if (!board.piece(pos).possibleMovementsExists()) {
                throw new BoardException("No movements possible for that piece!");
            }
        }

        public void validateEndPosition(Position start, Position end) {
            if (!board.piece(start).canMoveTo(end)) {
                throw new BoardException("Invalid end position!");
            }
        }

        private void changePlayer() {
            if (currentPlayer == Color.White) {
                currentPlayer = Color.Black;
            } else {
                currentPlayer = Color.White;
            }
        }

        public void executeMove(Position start, Position end) {
            Piece p = board.removePiece(start);
            p.incrementPieceMoveQuantity();
            Piece capturedPiece = board.removePiece(end);
            board.insertPiece(p, end);
        }
    }
}
