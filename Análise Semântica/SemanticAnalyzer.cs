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

        public const string DEFINED_VARIABLE = "Variable already defined: ";
        public const string DEFINED_SUBPROGRAM = "Subprogram already defined: ";
        public const string UNDEFINED_IDENTIFIER = "Identifier not defined: ";
        public const string UNDEFINED_SUBPROGRAM = "Subprogram not defined: ";
        public const string INVALID_OPERATION = "Invalid operation: ";

        private readonly IList<Symbol> Symbols;
        private SemanticResult Result { get; set; }

        public SemanticAnalyzer()
        {
            Symbols = new List<Symbol>();
            Result = new();
        }

        // TODO: Implementar erro
        /* Verificar compatível com o uso(exemplo: variável usada que existe como nome de programa ou
        de procedimento na tabela de símbolos deve dar erro). */
        public bool CheckIfDeclaratedIdentifier(Token token)
        {
            if (!Symbols.VerifyIfExists(token.Lexem))
            {
                Result.Add(token, UNDEFINED_IDENTIFIER);
                return false;
            }

            return true;
        }




        /*  Verificação da ocorrência da duplicidade na declaração de um identificador (nome
        do programa, procedimento, função ou variável). Sempre que for detectado um novo
        identificador, deve ser feita uma busca para verificar as seguintes possibilidades:
        a) se ele for uma variável verificar se já não existe outra variável visível
        1
        de mesmo
        nome no mesmo nível de declaração e verificar se já não existe outro identificador
        de mesmo nome (que não seja uma variável) em qualquer nível inferior ou igual ao
        da variável agora analisada.
        b) Se for o nome de um procedimento ou função verificar se já não existe um outro
        identificador visível de qualquer tipo em nível igual ao inferior ao agora analisado. */

        public bool CheckIfDuplicatedIdentifier(Token token)
        {
            if (Symbols.VerifyIfExists(token.Lexem))
            {

            }

            return false;
        }


        /*  3) Verificação de compatibilidade de tipos. Sempre que ocorrer um comando de
        atribuição, verificar se a expressão tem o mesmo tipo da variável ou função que a
        recebe. */

        public void CheckType(Token token)
        {
            if (token.Type == EType.IDENTIFICADOR)
            {
                Symbol identifier = Symbols.ToList().Find(x => x.Name == token.Lexem);
                if (identifier == null) // verificar se existe na lista
                {
                    Result.Add(token, UNDEFINED_IDENTIFIER);
                }
                else
                {
                    String type = findMostRecentIdentifierType(token.getName());
                    if (type == null)
                    {
                        result.add(token, UNDEFINED_VARIABLE);
                    }
                    else
                    {
                        typeController.push(type);
                    }
                }
            }

        }


        private String findMostRecentIdentifierType(String name)
        {
            //for (int i = identifiers.size() - 1; i >= 0; i--)
            //{
            //    if (identifiers.get(i).getName().equals(name))
            //    {
            //        return identifiers.get(i).getType();
            //    }
            //}
            return null;
        }


        public void pushSymbol(Token token) {
            if (CheckIfDuplicatedIdentifier(token))
            {
                Result.Add(token, DEFINED_VARIABLE);
            }
            else
            {
                Symbols.Add(new Symbol(null, token.Lexem));
            }
        }


        /*
        4) Verificação dos comandos escreva e leia.


        5) Verificação de chamadas de procedimento e função.



        6) Verificação dos operadores unários – , + , nao.



        É fácil perceber que as chamadas para o analisador semântico não passam de linhas de
        comandos a serem inseridos no “corpo” do analisador sintático, nos locais apropriados.
        Vale lembrar que a Linguagem LPD não permite a passagem de parâmetros nos
        procedimentos e funções. Caso isso fosse permitido, então deveriamos também verificar a
        consistência no número de argumentos e parâmetros, bem como sua compatibilidade de
        tipos. */
    }
}
