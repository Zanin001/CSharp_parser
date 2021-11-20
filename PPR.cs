using System;
using System.IO;
using System.Collections.Generic;

namespace AnalisadorLexico
{
    public class PPR
    {
        SymbolTable st;
        Lexem lexem;
        Token token;
        public PPR (string path)  
        {
            st = new();
            lexem = new(path);
            ProgramAnalyzer();
        }

        public void getToken()
        {
            token = lexem.Analyzer();
        }
        public bool ProgramAnalyzer()
        {
            getToken();
            if(token.Type == Type.PROGRAMA)
            {
                Console.WriteLine(token.Type + " ");
                getToken();
                if(token.Type == Type.IDENTIFICADOR)
                {
                    Console.WriteLine(token.Type + ": " + token.Lexem + " ");
                    st.AddToken(token);
                    getToken();
                    if(token.Type == Type.PONTO_E_VIRGULA) 
                    {
                        Console.WriteLine(token.Type + " ");
                        BlockAnalyzer();
                        getToken();
                        if(token.Type == Type.PONTO)
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
            if(token.Type == Type.VAR)
            {
                getToken();
                if(token.Type == Type.IDENTIFICADOR)
                {
                    while(token.Type == Type.IDENTIFICADOR)
                    {
                        if(VarAnalyzer())
                        {
                            if(token.Type == Type.PONTO_E_VIRGULA)
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
                if(token.Type == Type.IDENTIFICADOR)
                {
                    //if(Symbol.IsDuplicated(token))
                    //{
                    //Symbol.AddTable(token, "variavel");
                    getToken();
                    if(token.Type == Type.VIRGULA || token.Type == Type.DOISPONTOS)
                    {
                        if(token.Type == Type.VIRGULA)
                        {
                            getToken();
                            if(token.Type == Type.DOISPONTOS)
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
            while(token.Type != Type.DOISPONTOS);
            getToken();
            return TypeAnalyzer();
        }

        public bool TypeAnalyzer()
        {
            if(token.Type != Type.INTEIRO || token.Type != Type.BOOLEANO)
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
            if(token.Type == Type.INICIO)
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
            if(token.Type == Type.IDENTIFICADOR)
                return AssignmentProcedureAnalyzer();
            else if(token.Type == Type.ESCREVA)
                return WriteAnalyzer();
            return false;
        }

        public bool AssignmentProcedureAnalyzer()
        {
            getToken();
            if(token.Type == Type.ATRIBUICAO)
                return AssignmentAnalyzer();
            else
            {
                Error("Esperado uma atribuição: ", token.NumLine, token.Column);
                return false; 
            }
        }
        public bool AssignmentAnalyzer()
        {
            return true;
        }
        public bool WriteAnalyzer()
        {
            return true;
        }
        public void Error(string mensage, int line, int column)
        {
            Console.WriteLine(mensage + line + ", ", column);
        }
    }
}