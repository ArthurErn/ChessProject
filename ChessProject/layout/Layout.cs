using board;
using System;
using chess;
using System.Collections.Generic;

namespace layout {
    class Layout {

        public static void printGame(ChessMatch match) {
            Layout.printBoard(match.board);
            Console.WriteLine();
            printCapturedPieces(match);
            Console.WriteLine("Turn: " + match.turn);
            Console.WriteLine("It is " + match.currentPlayer + "'s turn.");
            if (match.check) {
                Console.WriteLine("CHECK!");
            }
        }

        public static void printCapturedPieces(ChessMatch match) {
            Console.WriteLine("Captured pieces: ");
            Console.Write("White: ");
            printCouple(match.capturedPieces(Color.White));
            Console.WriteLine();
            Console.Write("Black: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            printCouple(match.capturedPieces(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
            Console.WriteLine();

        }

        public static void printCouple(HashSet<Piece> couple) {
            Console.Write("[");
            foreach (Piece x in couple) {
                Console.Write(x + ", ");

            }
            Console.Write("]");
        }

        public static void printBoard(Board board) {

            for (int i = 0; i < board.linhas; i++) {
                Console.Write(8 - i + "|  ");
                for (int j = 0; j < board.colunas; j++) {
                    Layout.printPiece(board.piece(i, j));
                    Console.Write(" ");

                }
                Console.WriteLine("|");
                Console.WriteLine(" |");
            }
            Console.WriteLine("     _____________________________");
            Console.WriteLine("     A   B   C   D   E   F   G   H");
        }

        public static void printBoard(Board board, bool[,] possiblePositions) {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor highlightedBackground = ConsoleColor.Gray;


            for (int i = 0; i < board.linhas; i++) {
                Console.Write(8 - i + "|  ");
                for (int j = 0; j < board.colunas; j++) {
                    if (possiblePositions[i, j]) {
                        Console.BackgroundColor = highlightedBackground;
                    } else {
                        Console.BackgroundColor = originalBackground;
                    }
                    Layout.printPiece(board.piece(i, j));
                    Console.BackgroundColor = originalBackground;
                    Console.Write(" ");

                }
                Console.WriteLine("|");
                Console.WriteLine(" |");
            }
            Console.WriteLine("     _____________________________");
            Console.WriteLine("     A   B   C   D   E   F   G   H");
            Console.BackgroundColor = originalBackground;
        }

        public static ChessPosition readChessPosition() {
            string position = Console.ReadLine();
            char coluna = position[0];
            int linha = int.Parse(position[1] + "");
            return new ChessPosition(coluna, linha);
        }

        public static void printPiece(Piece piece) {
            if (piece == null) {
                Console.Write(" - ");
            } else {
                if (piece.color == Color.White) {
                    Console.Write(piece);
                } else {
                    ConsoleColor defaultColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(piece);
                    Console.ForegroundColor = defaultColor;
                }
            }
        }
    }
}
