using AnalisadorLexico;

namespace AnalisadorSintatico
{
    //TODO: IMPLEMENTAR CLASSE MÃE
    public abstract class Parser
    {
        SymbolTable st;
        Lexem lexem;
        Token token;

        public Parser(string path)
        {
            //st = new SymbolTable();
            lexem = new(path);
        }

        public void getToken()
        {
            token = lexem.Analyzer();
        }
    }
}