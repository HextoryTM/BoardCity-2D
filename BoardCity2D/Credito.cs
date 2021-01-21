using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardCity2D
{
    class Credito
    {
        public String CD { get; }
        public String Nome { get; }
        public String Email { get; }
        public String RA { get; }
        public Texture2D Foto { get; }

        public Credito(String cd, String nome, String email, String ra, Texture2D foto)
        {
            CD = cd;
            Nome = nome;
            Email = email;
            RA = ra;
            Foto = foto;
        }

    }
}
