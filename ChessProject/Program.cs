using System;
using board;
using layout;

namespace ChessProject {
    class Program {
        static void Main() {
            Board board = new Board(8, 8);
            Layout.printBoard(board);
        }
    }
}
