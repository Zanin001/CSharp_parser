using System;
using System.IO;
using System.Collections.Generic;
using Enums;
using AnalisadorLexico;
using System.Collections.Generic;
using AnalisadorLexico.Análise_Semântica;

namespace AnalisadorSintatico
{
    public class PPR
    {
        SymbolTable st;
        Lexem lexem;
        Token token, token2;
        SemanticAnalyzer semanticAnalizer;

        public PPR (string path)  
        {
            st = new();
            lexem = new(path);
            semanticAnalizer = new();
            ProgramAnalyzer();
        }

        public void getToken()
        {
            token = lexem.Analyzer();
        }
        public bool ProgramAnalyzer()
        {
            getToken();
            if(token.Type == EType.PROGRAMA)
            {
                Console.WriteLine(token.Type + " ");
                getToken();
                if(token.Type == EType.IDENTIFICADOR)
                {
                    Console.WriteLine(token.Type + ": " + token.Lexem + " ");
                    st.AddToken(token);

                    getToken();
                    if(token.Type == EType.PONTO_E_VIRGULA) 
                    {
                        Console.WriteLine(token.Type + " ");
                        BlockAnalyzer();
                        getToken();
                        if(token.Type == EType.PONTO)
                        {
                            return true;
                        }
                        else
                        {
                            Error("Ponto esperado: ", token.NumLine, token.Column);
                            return false;
                        }
                    }
                    else
                    {
                        Error("Ponto e vírgula esperado: ", token.NumLine, token.Column);
                        return false;
                    }
                }
                else
                {
                    Error("Identificador esperado: ", token.NumLine, token.Column);
                    return false;
                }
            }
            else
            {
                Error("Programa Principal não encontrado: ", token.NumLine, token.Column);
                return false;
            }
        }

        public bool BlockAnalyzer()
        {
            return VarEtAnalyzer() && CommandAnalyzer();
        }

        public bool VarEtAnalyzer()
        {
            getToken();
            if(token.Type == EType.VAR)
            {
                getToken();
                if(token.Type == EType.IDENTIFICADOR)
                {
                    while(token.Type == EType.IDENTIFICADOR)
                    {
                        if(VarAnalyzer())
                        {
                            if(token.Type == EType.PONTO_E_VIRGULA)
                            {
                                getToken();
                                return true;
                            }
                            else
                            {
                                Error("Ponto e virgula esperado: ", token.NumLine, token.Column);
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return false;
                }
                else
                {
                    Error("Identificador esperado: ", token.NumLine, token.Column);
                    return false;
                }
            }
            else
            {
                Error("Var esperado: ", token.NumLine, token.Column);
                return false;
            }
        }

        public bool VarAnalyzer()
        {
            do
            {
                if(token.Type == EType.IDENTIFICADOR)
                {
                    getToken();
                    if(token.Type == EType.VIRGULA || token.Type == EType.DOISPONTOS)
                    {
                        if(token.Type == EType.VIRGULA)
                        {
                            getToken();
                            if(token.Type == EType.DOISPONTOS)
                            {
                                Error("Esperado um identificador ou tipo: ", token.NumLine, token.Column);
                                return false;
                            }
                        }
                    }
                    else
                    {
                        Error("Esperado virgula ou dois pontos: ", token.NumLine, token.Column);
                        return false;
                    }
                    //}
                    //else
                    //{
                        //Error("Token Duplicado")
                    //}
                }
                else
                {
                    Error("Esperado um identificador: ", token.NumLine, token.Column);
                    return false;
                }
            }
            while(token.Type != EType.DOISPONTOS);
            getToken();
            return TypeAnalyzer();
        }

        public bool TypeAnalyzer()
        {
            if(token.Type != EType.INTEIRO || token.Type != EType.BOOLEANO)
            {
                Error("Esperado um tipo: ", token.NumLine, token.Column);
                return false;
            }
            else
            {
                semanticAnalizer.PushSymbol(token);
                getToken();
                return true;
            }
        }

        public bool CommandAnalyzer()
        {
            if(token.Type == EType.INICIO)
            {
                getToken();
                SimpleCommandAnalyzer();
                while(token.Type != EType.FIM)
                {
                    if(token.Type == EType.PONTO_E_VIRGULA)
                    {
                        getToken();
                        if(token.Type != EType.FIM)
                        {
                            SimpleCommandAnalyzer();
                        }
                    }
                    else
                    {
                        Error("Esperado ponto e virgula: ", token.NumLine, token.Column);
                        return false;
                    }
                    getToken();
                }
                return true;
            }
            else
            {
                Error("Esperado INICIO: ", token.NumLine, token.Column);
                return false;
            }
        }

        public bool SimpleCommandAnalyzer()
        {
            if(token.Type == EType.IDENTIFICADOR)
                return AssignmentProcedureAnalyzer();
            else if(token.Type == EType.ESCREVA)
                return WriteAnalyzer();
            return false;
        }

        public bool AssignmentProcedureAnalyzer()
        {
            token2 = token;
            getToken();
            if(token.Type == EType.ATRIBUICAO)
                return AssignmentAnalyzer();
            else
            {
                Error("Esperado uma atribuição: ", token.NumLine, token.Column);
                return false; 
            }
        }
        //TODO: IMPLEMENTAR ANALIZADOR DE CALCULOS
        public bool AssignmentAnalyzer()
        {
            List<Token> assignment = new();
            getToken();
            assignment.Add(token);
            switch(token.Type)
            {
                case EType.NUMERO:
                    getToken();
                    while(token.Type != EType.PONTO_E_VIRGULA)
                    {
                        if(token.Type == EType.MAIS || token.Type == EType.DIVISAO || token.Type == EType.MENOS ||
                            token.Type == EType.MULTIPLICACAO)
                        {
                            assignment.Add(token);
                            getToken();
                            if(token.Type == EType.NUMERO)
                            {
                                assignment.Add(token);
                                getToken();
                            }
                            else
                            {
                                //Error();
                                return false;
                            }
                        }
                        else
                        {
                            //Error();
                            return false;
                        }
                    }
                    //Symbol.AddValor(token2, token);
                    return true;

                case EType.BOOLEANO:
                    return true;

                default:
                    //Error();
                    return false;
            }
        }

        //TODO: IMPLEMENTAR ANALISADOR DE ESCRITA
        public void WriteAnalyzer()
        {
            getToken();
            if (token.Type == EType.ABRE_PARENTESIS)
            {
                getToken();
                if (token.Type == EType.IDENTIFICADOR)
                {
                    /* if (ts.ts.get(t.conteudo) != null)
                    {
                        getToken();
                        if (t.tipo == Tipo.SFECHA_PARENTESIS)
                        {
                            getToken();
                            //Executa comando de escrita com t.valor
                        }
                        else
                            //erro(") esperado");
                    }
                    else
                        //erro("Identificador não encontrado"); */
                }
            }
            /*else
                erro("( esperado"); */
        }

        public void ReadAnalyzer()
        {
            getToken();
            if (token.Type == EType.ABRE_PARENTESIS)
            {
                getToken();
                if (token.Type == EType.IDENTIFICADOR)
                {
                    /* if (ts.ts.get(t.conteudo) != null)
                    {
                        getToken();
                        if (token.Type == EType.FECHA_PARENTESIS)
                        {
                            getToken();
                            //Executa comando de leitura e coloca valor em t.valor
                        }
                        else
                            erro(") esperado");
                    }
                    else
                        erro("Identificador não encontrado"); */
                }
            }
                /*else
                erro("( esperado"); */
        }


        //TODO: IMPLEMENTAR ERROS
        public void Error(string mensage, int line, int column)
        {
            Console.WriteLine(mensage + line + ", ", column);
        }
    }
}