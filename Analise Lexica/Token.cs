using Enums;

namespace AnalisadorLexico
{
    public class Token // ANA MATTOS
    {  
        public EType Type { get; set; }
        public string Lexem { get; set; }
        public int NumLine { get; set; }
        public int Column { get; set; }
        // public string codigo { get; set; }  


        public Token(EType type, string lexem, int numLine, int column)
        {
            Type = type;
            Lexem = lexem;
            NumLine = numLine;
            Column = column;
        }

        public override string ToString()
        {
            return "Tipo: " + Type + ", Lexema: " + Lexem + ", Linha: " + NumLine + ", Coluna: " + Column;
        }

        public string GetName()
        {
            return Lexem;
        }

        public int GetLine()
        {
            return NumLine;
        }
    }
}