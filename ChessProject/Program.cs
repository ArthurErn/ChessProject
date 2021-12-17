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
                    try {
                        Console.Clear();
                        Console.OutputEncoding = System.Text.Encoding.Unicode;
                        Layout.printGame(match);
                        

                        Console.WriteLine();

                        Console.Write("From: ");
                        Position start = Layout.readChessPosition().toPosition();
                        match.validateStartPosition(start);
                        bool[,] possiblePositions = match.board.piece(start).possibleMovements();

                        Console.Clear();
                        Layout.printBoard(match.board, possiblePositions);
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.Write("To: ");
                        Position end = Layout.readChessPosition().toPosition();
                        match.validateEndPosition(start, end);
                        match.makePlay(start, end);
                    } catch (BoardException e) {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                    }
                Layout.printBoard(match.board);
            } catch (BoardException e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}
