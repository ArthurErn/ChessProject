using board;
using System;

namespace layout {
    class Layout {
        public static void printBoard(Board board) {
            
            for (int i  = 0; i < board.linhas; i++) {
                Console.Write(8 - i + "|  ");
                for (int j = 0; j < board.colunas; j++) {
                    if (board.piece(i, j) == null) {
                        Console.Write(" -  ");
                    } else {
                        Layout.printPiece(board.piece(i, j));
                        Console.Write(" ");
                    }
                    
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine("     A   B   C   D   E   F   G   H");
        }

        public static void printPiece(Piece piece) {
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
