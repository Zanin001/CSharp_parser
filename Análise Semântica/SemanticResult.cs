using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalisadorLexico.Análise_Semântica
{
    public class SemanticResult
    {

        public const string DEFINED_VARIABLE = "Variable already defined: ";
        public const string DEFINED_SUBPROGRAM = "Subprogram already defined: ";
        public const string UNDEFINED_VARIABLE = "Variable not defined: ";
        public const string UNDEFINED_SUBPROGRAM = "Subprogram not defined: ";
        public const string INVALID_OPERATION = "Invalid operation: ";

        private readonly IList<string> Results;


        public SemanticResult()
        {
            Results = new List<string>();
        }

        public void Add(Token token, string cause)
        {
            Results.Add(cause + token.GetName() + " at line " + token.GetLine());
        }

        public List<string> GetResults()
        {
            return Results.ToList();
        }
    }
}
