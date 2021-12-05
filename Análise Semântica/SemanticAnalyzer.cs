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

        public const string DEFINED_IDENTIFIER = "Identifier already defined: ";
        public const string DEFINED_SUBPROGRAM = "Subprogram already defined: ";
        public const string UNDEFINED_IDENTIFIER = "Identifier not defined: ";
        public const string UNDEFINED_SUBPROGRAM = "Subprogram not defined: ";
        public const string INVALID_OPERATION = "Invalid operation: ";

        private readonly IList<Symbol> Identifiers;
        private SemanticResult Result { get; set; }

        public SemanticAnalyzer()
        {
            Identifiers = new List<Symbol>();
            Result = new();
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
                Result.Add(token, UNDEFINED_IDENTIFIER);
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

            Result.Add(token, DEFINED_IDENTIFIER);
            return false;
        }

        public void CheckType(Token token)
        {
            if (!VerifyIfExistsInIdentifiers(token))
            {
                Result.Add(token, UNDEFINED_IDENTIFIER);
            }
            else
            {
                String type = findMostRecentIdentifierType(token);
                if (type == null)
                {
                    Result.Add(token, UNDEFINED_IDENTIFIER);
                }
            }
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
        public void PushSymbol(Token token)
        {
            if (CheckIfDeclaratedIdentifier(token))
            {
                Result.Add(token, DEFINED_IDENTIFIER);
            }
            else
            {
                Identifiers.Add(new Symbol(token, null, token.Lexem));
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
            if(t1.Type == EType.INTEIRO && t2.Type == EType.NUMERO)
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
                Result.Add(t2, INVALID_OPERATION);
                return false;
            }
        }

        public Symbol GetSymbol(Token tk)
        {
            return Identifiers.ToList().Find(x => x.Name == tk.Lexem);
        }

    }
}
