using System;
using board;
using layout;
using chess;

namespace ChessProject {
    class Program {
        static void Main() {

            ChessPosition pos = new ChessPosition('c', 7);

            Console.WriteLine(pos);
            Console.WriteLine(pos.toPosition());
            Console.ReadLine();
        }
    }
}
