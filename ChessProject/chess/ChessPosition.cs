using board;

namespace chess {
    class ChessPosition {
        public ChessPosition(char coluna, int linha) {
            this.coluna = coluna;
            this.linha = linha;
        }

        public char coluna { get; set; }
        public int linha { get; set; }

        public Position toPosition() {
            return new Position(8 - linha, coluna - 'a');
        }

        public override string ToString() {
            return "" + coluna + linha;
        }
    }
}
