using Enums;

namespace AnalisadorLexico
{
    //TODO: IMPLEMENTAR ANALISE SEMANTICA
    public class Symbol
    {
        public Token Token { get; set; }
        public EType DataType { get; set; }
        public string Name { get; set; }
        public string Scope { get; set; }
        public string Valor { get; set; }
        public string Address { get; set; }
        public string Code { get; set; }

        public Symbol(Token token, EType dataType, string scope, string valor, string address, string code)
        {
            Token = token;
            DataType = dataType;
            Scope = scope;
            Valor = valor;
            Address = address;
            Code = code;
        }

        public Symbol(Token token, EType dataType, string name)
        {
            Token = token;
            DataType = dataType;
            Name = name;
        }
    }
}