using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private string findMostRecentIdentifierType(Token token)
        {
            Symbol symbol = GetSymbol(token);

            if (symbol != null)
            {
                return symbol.DataType;
            }

            return null;
        }
        public bool PushSymbol(Token token)
        {
            if (CheckIfDeclaratedIdentifier(token))
            {
                return false;
            }
            else
            {
                Identifiers.Add(new Symbol(token, null, token.Lexem));
                return true;
            }
        }

        public bool SetType(Token name, Token type)
        {
            Symbol symbol = GetSymbol(name);
            if (symbol != null)
            {
                symbol.DataType = type.Type.ToString(); 
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

        public bool CheckAssignment(Token t1, Token t2)
        {
            if((t1.Type == EType.INTEIRO && t2.Type == EType.NUMERO) || (t1.Type == EType.INTEIRO && t2.Type == EType.INTEIRO))
            {
                return true;
            }
            
            else if(t1.Type == EType.BOOLEANO && (t2.Type == EType.IDENTIFICADOR &&
                (t2.Lexem == "true" || t2.Lexem == "false" || t2.Lexem == "1" || t2.Lexem == "0")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Symbol GetSymbol(Token tk)
        {
            return Identifiers.ToList().Find(x => x.Name == tk.Lexem);
        }

    }
}
