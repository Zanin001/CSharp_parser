using Enums;
using System.Collections.Generic;
using System.Linq;

namespace AnalisadorLexico.Análise_Semântica
{
    public class SemanticAnalyzer
    {

        private readonly IList<Symbol> Identifiers;

        public SemanticAnalyzer()
        {
            Identifiers = new List<Symbol>();
        }

        /* 
         * verifica se já existe o identificador na lista de simbolos 
         */
        public bool VerifyIfExistsInIdentifiers(Token token)
        {
            Symbol symbol = GetSymbol(token);

            if (symbol != null)
            {
                return true;
            }

            return false;
        }

        /* 
         * verifica se já foi declarado o identificador na lista de simbolos 
         */
        public bool CheckIfDeclaratedIdentifier(Token token)
        {
            if (!VerifyIfExistsInIdentifiers(token))
            {
                return false;
            }

            return true;
        }

        /* 
         * verifica se o identificador é duplicado
         */
        public bool CheckIfDuplicatedIdentifier(Token token)
        {
            if (VerifyIfExistsInIdentifiers(token))
            {
                return true;
            }
            return false;
        }

        /*
         * Adiciona um token na lista de identificadores, caso ele não exista
         */
        public bool PushSymbol(Token token)
        {
            if (CheckIfDeclaratedIdentifier(token))
            {
                return false;
            }
            else
            {
                Identifiers.Add(new Symbol(token, EType.NAO_IDENTIFICADO, token.Lexem));
                return true;
            }
        }

        /*
        * Seta o tipo do identificador caso ele exista
        */
        public bool SetType(Token name, Token type)
        {
            Symbol symbol = GetSymbol(name);
            if (symbol != null)
            {
                symbol.DataType = type.Type; 
                return true;  
            }
            return false;
        }

        /*
         * Seta o código do identificador caso ele exista
         */
        public void SetCode(Token tk, string code)
        {
            Symbol symbol = GetSymbol(tk);
            if (symbol != null)
            {
                symbol.Code += code;
            }   
        }

        /*
         * Efetua a checagem de tipos durante a atribuição de valores nas variáveis
         */
        public bool CheckAssignment(Symbol sb, Token tk)
        {
            if(tk.Type == EType.IDENTIFICADOR)
            {
                Symbol sb2 = GetSymbol(tk);
                if(sb.DataType == EType.INTEIRO && sb2.DataType == EType.INTEIRO)
                {
                    return true;
                }
                return false;
            }
            else if((sb.DataType == EType.INTEIRO && tk.Type == EType.NUMERO))
                return true;
            else
                return false;
        }

        /*
         * Retorna o identificador pelo nome
         */
        public Symbol GetSymbol(Token tk)
        {
            return Identifiers.ToList().Find(x => x.Name == tk.Lexem);
        }

    }
}
