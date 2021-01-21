using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BoardCity2D
{
    class Dado
    {
        public int Rodar()
        {
            Random rnd = new Random();
            int nmro = rnd.Next(0, 6);
            return nmro;
        }
    }
}