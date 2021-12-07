using AnalisadorSintatico;

namespace AnalisadorLexico
{
    class main
    {
        static void Main(string[] args)
        {
            string path = "entrada_texto.txt";
            PPR ppr = new(path);
            ppr.parse();
        }

    }
}