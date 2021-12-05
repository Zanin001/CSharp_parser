using System;
using System.IO;
using System.Collections.Generic;
using Enums;
using AnalisadorLexico;
using AnalisadorLexico.Análise_Semântica;

namespace AnalisadorSintatico
{
    public class PPR:Parser
    {
        SymbolTable st;
        Lexem lexem;
        Token token, token2;
        SemanticAnalyzer sa;
        List<Token> tokensList = new List<Token>();

        public PPR (string path):base(path)
        {
            st = new();
            lexem = new Lexem(path);
            sa = new();
            ProgramAnalyzer();

            saveCode();
        }

        public void getToken()
        {
            token = lexem.Analyzer();
        }

        //AQUI
        public void parse()
        {
            ProgramAnalyzer();

            saveCode();
        }

        public bool ProgramAnalyzer()
        {
            getToken();
            if(token.Type == EType.PROGRAMA)
            {
                System.Console.WriteLine(token.Type +": " + token.Lexem);
                getToken();
                if(token.Type == EType.IDENTIFICADOR)
                {
                    //AQUI
                    System.Console.WriteLine(token.Type +": " + token.Lexem);

                    sa.PushSymbol(token);
                    getToken();
                    if(token.Type == EType.PONTO_E_VIRGULA)
                    {
                        System.Console.WriteLine(token.Type +": " + token.Lexem);
                        
                        BlockAnalyzer();
                        getToken();
                        if(token.Type == EType.PONTO)
                        {
                            System.Console.WriteLine(token.Type +": " + token.Lexem);
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
            getToken();
            bool var = VarAnalyzer();
            bool cmd = CommandAnalyzer();
            return var && cmd;
        }

        public bool VarEtAnalyzer()
        {
            getToken();
            if (token.Type == EType.VAR)
            {
                getToken();
                if (token.Type == EType.IDENTIFICADOR)
                {
                    while (token.Type == EType.IDENTIFICADOR)
                    {
                        if (VarAnalyzer())
                        {
                            if (token.Type == EType.PONTO_E_VIRGULA)
                            {
                                getToken();
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
                    return true;
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
                if (token.Type == EType.IDENTIFICADOR)
                {
                    if(!sa.CheckIfDuplicatedIdentifier(token))
                    {
                        token.codigo = geraTemp();
                        sa.PushSymbol(token);
                        tokensList.Add(token);
                        getToken();
                        if (token.Type == EType.VIRGULA || token.Type == EType.DOISPONTOS)
                        {
                            if (token.Type == EType.VIRGULA)
                            {
                                getToken();
                                if (token.Type == EType.DOISPONTOS)
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
                    }
                    else
                    {
                        Error("Identificador duplicado: ",  token.NumLine, token.Column);
                    }
                }
                else
                {
                    Error("Esperado um identificador: ", token.NumLine, token.Column);
                    return false;
                }
            }
            while (token.Type != EType.DOISPONTOS);
            getToken();
            return TypeAnalyzer();
        }

        public bool TypeAnalyzer()
        {
            if (token.Type != EType.INTEIRO || token.Type != EType.BOOLEANO)
            {
                Error("Esperado um tipo: ", token.NumLine, token.Column);
                return false;
            }
            else
            {
                //TODO: Analise Sintatica deve somente pedir para adicionar o tipo, quem deve fazer as verificações se
                // o identifier ja esta declarado ou se os tipos são iguais é a Analise Semantica
                foreach(Token tk in tokensList)
                {
                    sa.SetType(tk, token);
                }
                getToken();
                return true;
            }
        }

        public bool CommandAnalyzer()
        {
            if (token.Type == EType.INICIO)
            {
                getToken();
                SimpleCommandAnalyzer();
                while (token.Type != EType.FIM)
                {
                    if (token.Type == EType.PONTO_E_VIRGULA)
                    {
                        getToken();
                        if (token.Type != EType.FIM)
                        {
                            SimpleCommandAnalyzer();
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        Error("Esperado ponto e virgula: ", token.NumLine, token2.Column);
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
            if (token.Type == EType.IDENTIFICADOR)
                return AssignmentProcedureAnalyzer();
            else if (token.Type == EType.ESCREVA)
                return WriteAnalyzer();
            return false;
        }

        public bool AssignmentProcedureAnalyzer()
        {
            token2 = token;
            getToken();
            if (token.Type == EType.ATRIBUICAO)
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
            switch (token.Type)
            {
                case EType.NUMERO:
                    getToken();
                    while (token.Type != EType.PONTO_E_VIRGULA)
                    {
                        if (token.Type == EType.MAIS || token.Type == EType.DIVISAO || token.Type == EType.MENOS ||
                            token.Type == EType.MULTIPLICACAO)
                        {
                            assignment.Add(token);
                            getToken();
                            if (token.Type == EType.NUMERO)
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
        public bool WriteAnalyzer()
        {
            getToken();
            if (token.Type == EType.ABRE_PARENTESIS)
            {
                getToken();
                if (token.Type == EType.IDENTIFICADOR)
                {
                    if(sa.CheckIfDeclaratedIdentifier(token)){
                        geraCod("call i32 (i8*, ...) @printf( i8* " + token.codigo + " ) nounwind");
                        getToken();
                        if(token.Type == EType.FECHA_PARENTESIS)
                        {
                            getToken();
                            return true;
                        }
                        else
                        {
                            //Error
                            return false;
                        }
                    }
                    else
                    {
                        //Error
                        return false;
                    }
                }
                else
                {
                    //Error
                    return false;
                }
            }
            else
            {
                //Error
                return false;
            }
        }

        //TODO: IMPLEMENTAR ERROS
        public void Error(string mensage, int line, int column)
        {
            Console.WriteLine(mensage +": ("+ line +","+column+")");
        }

        public string geraTemp()
        {
            int i = temp++;
            string nome = '%' + i.ToString();
            geraCod(nome + " = alloca i32, align 4");
            return nome;
        }

        public void geraCod(string comando)
        {
            cod += comando + "\n";
        }

        public void saveCode()
        {
            Codigo codigo = new Codigo(temp, cod);
            codigo.geradorLLVMIR();
        }
    }
}