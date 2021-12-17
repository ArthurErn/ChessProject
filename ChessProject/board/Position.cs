namespace board {
    class Position {
        public Position(int linha, int coluna) {
            this.linha = linha;
            this.coluna = coluna;
        }

        public int linha { get; set; }
        public int coluna { get; set; }

        public override string ToString() {
            return linha + ", " + coluna;
        }
    }
}
