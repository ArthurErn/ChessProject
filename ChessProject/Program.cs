using System;
using board;
using layout;
using chess;

namespace ChessProject {
    class Program {
        static void Main() {

            try {
                ChessMatch match = new ChessMatch();

                while (!match.finished) {
                    //Console.Clear();
                    Layout.printBoard(match.board);
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.Write("From: ");
                    Position start = Layout.readChessPosition().toPosition();
                    Console.Write("To: ");
                    Position end = Layout.readChessPosition().toPosition();

                    match.executeMove(start, end);
                }

                Layout.printBoard(match.board);
            } catch (BoardException e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}
