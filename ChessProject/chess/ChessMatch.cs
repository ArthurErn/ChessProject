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
        public Piece vulnerableEnPassant; 

        public ChessMatch() {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            check = false;
            vulnerableEnPassant = null;
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

        public bool testCheckmate(Color color) {
            if (!isInCheck(color)) {
                return false;            
            }
            foreach (Piece x in piecesInGame(color)) {
                bool[,] mat = x.possibleMovements();
                for (int i = 0; i < board.linhas; i++) {
                    for (int j = 0; j < board.colunas; j++) {
                        if (mat[i, j]) {
                            Position start = x.position;
                            Position end = new Position(i, j);
                            Piece capturedPiece = executeMove(start, end);
                            bool testCheck = isInCheck(color);
                            undoMovement(start, end, capturedPiece);
                            if (!testCheck) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void insertNewPiece(char coluna, int linha, Piece piece) {
            board.insertPiece(piece, new ChessPosition(coluna, linha).toPosition());
            pieces.Add(piece);
        }

        public void insertPieces() {
            /*insertNewPiece('a', 1, new Rook(board, Color.White));
            insertNewPiece('h', 1, new Rook(board, Color.White));*/
            insertNewPiece('e', 1, new King(board, Color.White, this));
            insertNewPiece('c', 1, new Bishop(board, Color.White));
            insertNewPiece('f', 1, new Bishop(board, Color.White));
            insertNewPiece('g', 1, new Knight(board, Color.White));
            insertNewPiece('b', 1, new Knight(board, Color.White));
            insertNewPiece('d', 1, new Queen(board, Color.White));
            insertNewPiece('a', 2, new Pawn(board, Color.White, this));
            /* insertNewPiece('b', 2, new Pawn(board, Color.White, this));
            insertNewPiece('c', 2, new Pawn(board, Color.White, this));
            insertNewPiece('d', 2, new Pawn(board, Color.White, this));
            insertNewPiece('e', 2, new Pawn(board, Color.White, this));
            insertNewPiece('f', 2, new Pawn(board, Color.White, this));
            insertNewPiece('g', 2, new Pawn(board, Color.White, this));
            insertNewPiece('h', 2, new Pawn(board, Color.White, this));*/


            insertNewPiece('e', 8, new King(board, Color.Black, this));
            insertNewPiece('a', 8, new Rook(board, Color.Black));
            insertNewPiece('h', 8, new Rook(board, Color.Black));
            insertNewPiece('c', 8, new Bishop(board, Color.Black));
            insertNewPiece('f', 8, new Bishop(board, Color.Black));
            insertNewPiece('g', 8, new Knight(board, Color.Black));
            insertNewPiece('b', 8, new Knight(board, Color.Black));
            insertNewPiece('d', 8, new Queen(board, Color.Black));
            insertNewPiece('h', 7, new Pawn(board, Color.Black, this));
            /*insertNewPiece('b', 7, new Pawn(board, Color.Black, this));
            insertNewPiece('c', 7, new Pawn(board, Color.Black, this));
            insertNewPiece('d', 7, new Pawn(board, Color.Black, this));
            insertNewPiece('e', 7, new Pawn(board, Color.Black, this));
            insertNewPiece('f', 7, new Pawn(board, Color.Black, this));
            insertNewPiece('g', 7, new Pawn(board, Color.Black, this));
            insertNewPiece('a', 7, new Pawn(board, Color.Black, this));*/
        }

        public void undoMovement(Position start, Position end, Piece capturedPiece) {
            Piece piece = board.removePiece(end);
            piece.decrementPieceMoveQuantity();
            if (capturedPiece != null) {
                board.insertPiece(capturedPiece, end);
                captured.Remove(capturedPiece);
            }
            board.insertPiece(piece, start);
            if (piece is King && end.coluna == start.coluna + 2) {
                Position startRook = new Position(start.linha, start.coluna + 3);
                Position endRook = new Position(start.linha, start.coluna + 1);
                Piece Rook = board.removePiece(endRook);
                Rook.decrementPieceMoveQuantity();
                board.insertPiece(Rook, startRook);
            }
            if (piece is King && end.coluna == start.coluna - 2) {
                Position startRook = new Position(start.linha, start.coluna - 4);
                Position endRook = new Position(start.linha, start.coluna - 1);
                Piece Rook = board.removePiece(endRook);
                Rook.decrementPieceMoveQuantity();
                board.insertPiece(Rook, startRook);
            }
            if (piece is Pawn) {
                if (start.coluna != end.coluna && capturedPiece == vulnerableEnPassant) {
                    Piece pawn = board.removePiece(end);
                    Position posP;
                    if (pawn.color == Color.White) {
                        posP = new Position(3, end.coluna);
                    } else {
                        posP = new Position(4, end.coluna);
                    }
                    board.insertPiece(pawn, posP);
                }
            }
        }

        public void makePlay(Position start, Position end) {
            Piece capturedPiece = executeMove(start, end);  
            if (isInCheck(currentPlayer)) {
                undoMovement(start, end, capturedPiece);
                throw new BoardException("You can't put yourself in check!");
            }
            Piece piece = board.piece(end);
            if (piece is Pawn) {
                if ((piece.color == Color.White && end.linha == 0) || (piece.color == Color.Black && end.linha == 7)) {
                    piece = board.removePiece(end);
                    pieces.Remove(piece);
                    Piece queen = new Queen(board, piece.color);
                    board.insertPiece(queen, end);
                    pieces.Add(queen);
                }
            }

            if (isInCheck(enemy(currentPlayer))) {
                check = true;
            } else {
                check = false;
            }
            if (testCheckmate(enemy(currentPlayer))) {
                finished = true;
            } else {
                turn++;
                changePlayer();
            }
            
            if (piece is Pawn && (end.linha == start.linha - 2 || end.linha == start.linha + 2)) {
                vulnerableEnPassant = piece;
            } else {
                vulnerableEnPassant = null;
            }
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
            if (!board.piece(start).possibleMovement(end)) {
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
            //castle
            if (p is King && end.coluna == start.coluna + 2) {
                Position startRook = new Position(start.linha, start.coluna + 3);
                Position endRook = new Position(start.linha, start.coluna + 1);
                Piece Rook = board.removePiece(startRook);
                Rook.incrementPieceMoveQuantity();
                board.insertPiece(Rook, endRook);
            }
            if (p is King && end.coluna == start.coluna - 2) {
                Position startRook = new Position(start.linha, start.coluna - 4);
                Position endRook = new Position(start.linha, start.coluna - 1);
                Piece Rook = board.removePiece(startRook);
                Rook.incrementPieceMoveQuantity();
                board.insertPiece(Rook, endRook);
            }

            if (p is Pawn) {
                if (start.coluna != end.coluna && capturedPiece == null) {
                    Position posP;
                    if (p.color == Color.White) {
                        posP = new Position(end.linha + 1, end.coluna);
                    } else {
                        posP = new Position(end.linha - 1, end.coluna);
                    }
                    capturedPiece = board.removePiece(posP);
                    captured.Add(capturedPiece);
                }
            }
            return capturedPiece;
        }
    }
}
