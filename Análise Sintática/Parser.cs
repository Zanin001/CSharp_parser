using AnalisadorLexico;
using AnalisadorLexico.Análise_Semântica;

namespace AnalisadorSintatico
{
    //TODO: IMPLEMENTAR CLASSE MÃE
    public abstract class Parser
    {
        public Lexem lexem;
        public Token token;
        public SemanticAnalyzer sa;
        public int temp = 1;
        public string cod, identifier;

        public Parser(string path)
        {
            lexem = new(path);
            sa = new();
        }

        public void getToken()
        {
            token = lexem.Analyzer();
        }
    }
}