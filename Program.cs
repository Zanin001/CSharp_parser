using System;
using System.IO;
using System.Collections.Generic;
using AnalisadorSintatico;

namespace AnalisadorLexico
{
    class Program
    {
        static void Main(string[] args) // ANA MATTOS
        {
            PPR ppr = new PPR("entrada_texto.txt");
            ppr.parse();
        }
    }
}