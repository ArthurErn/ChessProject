using board;
using System.Collections.Generic;

namespace chess {
    class ChessMatch {
        public Board board { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;
        public bool check { get; private set; }

        public ChessMatch() {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            check = false;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            insertPieces();
        }

        private Color enemy(Color color) {
            if (color == Color.White) {
                return Color.Black;
            } else {
                return Color.White;
            }
        }

        private Piece king(Color color) {
            foreach (Piece x in piecesInGame(color)) {
                if (x is King) {
                    return x;
                }
                
            }
            return null;
        }

        public bool isInCheck(Color color) {
            Piece K = king(color);
            if (K == null) {
                throw new BoardException("There is no " + color + " king!");
            }

            foreach (Piece x in piecesInGame(enemy(color))) {
                bool[,] mat = x.possibleMovements();
                if (mat[K.position.linha, K.position.coluna]) {
                    return true;
                }

            }
            return false;
        }

        public void insertNewPiece(char coluna, int linha, Piece piece) {
            board.insertPiece(piece, new ChessPosition(coluna, linha).toPosition());
            pieces.Add(piece);
        }

        public void insertPieces() {
            insertNewPiece('a', 1, new Rook(board, Color.White));
            insertNewPiece('h', 1, new Rook(board, Color.White));
            insertNewPiece('e', 1, new King(board, Color.White));
            insertNewPiece('e', 4, new Rook(board, Color.White));

            insertNewPiece('a', 8, new Rook(board, Color.Black));
            insertNewPiece('h', 8, new Rook(board, Color.Black));
            insertNewPiece('e', 8, new King(board, Color.Black));
        }

        public void undoMovement(Position start, Position end, Piece capturedPiece) {
            Piece piece = board.removePiece(end);
            piece.decrementPieceMoveQuantity();
            if (capturedPiece != null) {
                board.insertPiece(capturedPiece, end);
                captured.Remove(capturedPiece);
            }
            board.insertPiece(piece, start);
        }

        public void makePlay(Position start, Position end) {
            Piece capturedPiece = executeMove(start, end);
            if (isInCheck(currentPlayer)) {
                undoMovement(start, end, capturedPiece);
                throw new BoardException("You can't put yourself in check!");
            }
            if (isInCheck(enemy(currentPlayer))) {
                check = true;
            } else {
                check = false;
            }
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

        public HashSet<Piece> capturedPieces(Color color) {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach (Piece x in captured) {
                if (x.color == color) {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> piecesInGame(Color color) {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach (Piece x in pieces) {
                if (x.color == color) {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(capturedPieces(color));
            return aux;
        }

        public Piece executeMove(Position start, Position end) {
            Piece p = board.removePiece(start);
            p.incrementPieceMoveQuantity();
            Piece capturedPiece = board.removePiece(end);
            board.insertPiece(p, end);
            if (capturedPiece != null) {
                captured.Add(capturedPiece);
            }
            return capturedPiece;
        }
    }
}
