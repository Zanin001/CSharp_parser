
namespace AnalisadorLexico
{
    public class Token // ANA MATTOS
    {  
        public Type type { get; set;}
        public string lexem { get; set; }
        public int numLine { get; set; }
        public int colum { get; set; }
        public Type Type { get; set; }
        public string Lexem { get; set; }
        public int NumLine { get; set; }
        public int Column { get; set; }


        public Token(Type type, string lexem, int numLine, int column)
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
    }
}