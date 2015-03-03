using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleCreator
{
    public class Puzzle
    {
        public String name;
        public int id;

        public Puzzle(String name, int id)
        {
            this.name = name;
            this.id = id;
        }

        public override string ToString()
        {
            return name;
        }

    }
}
