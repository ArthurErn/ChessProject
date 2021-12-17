using System;
using board;
using layout;
using chess;

namespace ChessProject {
    class Program {
        static void Main() {

            try {
                Board board = new Board(8, 8);

                board.insertPiece(new Rook(board, Color.Black), new Position(0, 0));
                board.insertPiece(new Rook(board, Color.Black), new Position(2, 3));
                board.insertPiece(new King(board, Color.Black), new Position(1, 3));

                board.insertPiece(new Rook(board, Color.White), new Position(7, 5));
                board.insertPiece(new King(board, Color.White), new Position(5, 3));

                Layout.printBoard(board);
            } catch (BoardException e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}
