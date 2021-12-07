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

        public bool VerifyIfExistsInIdentifiers(Token token)
        {
            Symbol symbol = GetSymbol(token);

            if (symbol != null)
            {
                return true;
            }

            return false;
        }

        public bool CheckIfDeclaratedIdentifier(Token token)
        {
            if (!VerifyIfExistsInIdentifiers(token))
            {
                return false;
            }

            return true;
        }

        public bool CheckIfDuplicatedIdentifier(Token token)
        {
            if (VerifyIfExistsInIdentifiers(token))
            {
                return true;
            }
            return false;
        }

        private EType findMostRecentIdentifierType(Token token)
        {
            Symbol symbol = GetSymbol(token);

            if (symbol != null)
            {
                return symbol.DataType;
            }

            return 0;
        }
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

        public void SetCode(Token tk, string code)
        {
            Symbol symbol = GetSymbol(tk);
            if (symbol != null)
            {
                symbol.Code += code;
            }   
        }

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

        public Symbol GetSymbol(Token tk)
        {
            return Identifiers.ToList().Find(x => x.Name == tk.Lexem);
        }

    }
}
