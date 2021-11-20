using System;
using System.Collections.Generic;

namespace AnalisadorLexico
{
    //BRUNO ADAUTO
    public class SymbolTable
    {
        public Dictionary<string, Token> Table;

        /*
         * Construtor da classe, instancia um novo Dictionary
         */
        public SymbolTable()
        {
            Table = new Dictionary<string, Token>();
        }

        /*
         * Adiciona novo token ao dicionário
         */
        public void AddToken(Token token)
        {
            Table.TryAdd(token.Lexem, token);
        }

        /*
         * Obtém token do dicionário de acordo com a key
         */
        public Token GetToken(Token token)
        {
            if (Table.ContainsKey(token.Lexem))
            {
                return Table[token.Lexem];
            }
            else
            {
                return null;
            }
            //throw new InvalidOperationException(key + " não encontrada na lista de símbolos.");
        }

        /*
         * Adiciona os dados do dicionário em uma lista
         */
        public List<string> DictionaryToArray()
        {
            List<string> symbols = new();

            foreach (KeyValuePair<string, Token> keyValues in Table)
            {
                symbols.Add("Chave: " + keyValues.Key + ", Token: " + keyValues.Value);
            }
            return symbols;
        }
    }
}