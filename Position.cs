using System;

namespace vampi {
    public struct Position {
        public int x;
        public int y;

        public Position(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public Position(Position pos) {
            this.x = pos.x;
            this.y = pos.y;
        }
    }

}
