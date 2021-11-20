using System;

namespace AnalisadorLexico
{
    //TODO: IMPLEMENTAR ANALISE SEMANTICA
    class Symbol
    {
        public string DataType { get; set; }
        public string Scope { get; set; }
        public string Valor { get; set; }
        public string Address { get; set; }
        public string Code { get; set; }

        public Symbol(string dataType, string scope, string valor, string address, string code)
        {
            DataType = dataType;
            Scope = scope;
            Valor = valor;
            Address = address;
            Code = code;
        }         
    }
}