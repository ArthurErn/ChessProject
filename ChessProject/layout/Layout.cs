using board;
using System;

namespace layout {
    class Layout {
        public static void printBoard(Board board) {
            for (int i  = 0; i < board.linhas; i++) {
                for (int j = 0; j < board.colunas; j++) {
                    if (board.piece(i, j) == null) {
                        Console.Write(" -  ");
                    } else {
                        Console.Write(board.piece(i, j) + " ");
                    }
                    
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
