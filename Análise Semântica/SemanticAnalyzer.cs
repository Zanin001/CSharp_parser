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

        public bool VerifyIfExistsInIdentifiers(string name)
        {
            Symbol symbol = GetSymbol(name);

            if (symbol != null)
            {
                return true;
            }

            return false;
        }

        public bool CheckIfDeclaratedIdentifier(Token token)
        {
            if (!VerifyIfExistsInIdentifiers(token.Lexem))
            {
                Result.Add(token, UNDEFINED_IDENTIFIER);
                return false;
            }

            return true;
        }


        public bool CheckIfDuplicatedIdentifier(Token token)
        {
            if (VerifyIfExistsInIdentifiers(token.Lexem))
            {
                return true;
            }

            Result.Add(token, DEFINED_IDENTIFIER);
            return false;
        }

        public void CheckType(Token token)
        {
            if (!VerifyIfExistsInIdentifiers(token.Lexem))
            {
                Result.Add(token, UNDEFINED_IDENTIFIER);
            }
            else
            {
                String type = findMostRecentIdentifierType(token.Lexem);
                if (type == null)
                {
                    Result.Add(token, UNDEFINED_IDENTIFIER);
                }
            }
        }
        private string findMostRecentIdentifierType(string name)
        {
            Symbol symbol = GetSymbol(name);

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
                Identifiers.Add(new Symbol(null, token.Lexem));
            }
        }

        public void SetType(string name, string type)
        {
            Symbol symbol = GetSymbol(name);
            if (symbol != null)
            {
                symbol.DataType = type;
            }
        }

        public void CheckAssignment(Token t1, Token t2)
        {
            bool isValidNumeric = (t1.Type == EType.INTEIRO && t2.Type == EType.NUMERO);
            bool isValidLogic = t1.Type == EType.BOOLEANO && (t2.Type == EType.IDENTIFICADOR &&
                (t2.Lexem == "true" || t2.Lexem == "false" || t2.Lexem == "1" || t2.Lexem == "0"));

            if(!isValidNumeric || !isValidLogic)
            {
                Result.Add(t2, INVALID_OPERATION);
            }
        }

        public Symbol GetSymbol(string name)
        {
            return Identifiers.ToList().Find(x => x.Name == name);
        }

    }
}
