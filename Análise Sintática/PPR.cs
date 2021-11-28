using System;
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
        Token token;
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
                    Symbols.Add(new Symbol("var", "nem ideia"));
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
                    //if(Symbol.IsDuplicated(token))
                    //{
                    //Symbol.AddTable(token, "variavel");
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
                //Symbol.AddType();
                getToken();
                return true;
            }
        }

        public bool CommandAnalyzer()
        {
            if(token.Type == EType.INICIO)
            {
                getToken();
                return SimpleCommandAnalyzer();
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
            return true;
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