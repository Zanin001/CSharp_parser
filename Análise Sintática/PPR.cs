using System;
using System.Collections.Generic;
using Enums;
using AnalisadorLexico;

namespace AnalisadorSintatico
{
    public class PPR:Parser
    {
        List<Token> tokensList = new List<Token>();
        Token token2;
        public PPR (string path):base(path)
        {
        }

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
                getToken();
                if(token.Type == EType.IDENTIFICADOR)
                {
                    sa.PushSymbol(token);
                    identifier = token.Lexem;
                    getToken();
                    if(token.Type == EType.PONTO_E_VIRGULA)
                    {
                        if(BlockAnalyzer())
                        {
                            getToken();
                            if(token.Type == EType.PONTO)
                            {
                                return true;
                            }
                            else
                            {
                                Error("Ponto esperado", token.NumLine, token.Column);
                                return false;
                            }
                        }
                        else
                        {
                            Error("Erro ai ler bloco", token.NumLine, token.Column );
                            return false;
                        }
                    }
                    else
                    {
                        Error("Ponto e vírgula esperado", token.NumLine, token.Column);
                        return false;
                    }
                }
                else
                {
                    Error("Identificador esperado", token.NumLine, token.Column);
                    return false;
                }
            }
            else
            {
                Error("Programa Principal não encontrado", token.NumLine, token.Column);
                return false;
            }
        }

        public bool BlockAnalyzer()
        {
            getToken();
            return VarEtAnalyzer() && CommandAnalyzer();
        }

        public bool VarEtAnalyzer()
        {
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
                    Error("Identificador esperado", token.NumLine, token.Column);
                    return false;
                }
            }
            else
            {
                Error("Var esperado", token.NumLine, token.Column);
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
                        sa.PushSymbol(token);
                        sa.SetCode(token, geraTemp());
                        tokensList.Add(token);
                        getToken();
                        if (token.Type == EType.VIRGULA || token.Type == EType.TIPO)
                        {
                            if (token.Type == EType.VIRGULA)
                            {
                                getToken();
                                if (token.Type != EType.IDENTIFICADOR)
                                {
                                    Error("Esperado um identificador ou tipo", token.NumLine, token.Column);
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            Error("Esperado virgula ou dois pontos", token.NumLine, token.Column);
                            return false;
                        }
                    }
                    else
                    {
                        Error("Identificador duplicado",  token.NumLine, token.Column);
                        return false;
                    }
                }
                else
                {
                    Error("Esperado um identificador", token.NumLine, token.Column);
                    return false;
                }
            }
            while (token.Type != EType.TIPO);
            getToken();
            return TypeAnalyzer();
        }

        public bool TypeAnalyzer()
        {
            if (token.Type == EType.INTEIRO || token.Type == EType.BOOLEANO)
            {
                foreach(Token tk in tokensList)
                {
                    if(!sa.SetType(tk, token))
                    {
                        Error("Erro ao atribuir tipo", token.NumLine, token.Column);
                        return false; 
                    }
                }
                getToken();
                return true;
            }
            else
            {
                Error("Esperado um tipo", token.NumLine, token.Column);
                return false;
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
                            Error("FIM esperado", token.NumLine, token2.Column);
                            return true;
                        }
                    }
                    else
                    {
                        Error("Esperado ponto e virgula", token.NumLine, token2.Column);
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
                Error("Esperado uma atribuição", token.NumLine, token.Column);
                return false;
            }
        }

        //TODO: IMPLEMENTAR ANALIZADOR DE CALCULOS
        public bool AssignmentAnalyzer()
        {
            getToken();
            switch (token.Type)
            {
                case EType.NUMERO:
                    getToken();
                    while (token.Type != EType.PONTO_E_VIRGULA)
                    {
                        if (token.Type == EType.MAIS || token.Type == EType.DIVISAO || token.Type == EType.MENOS ||
                            token.Type == EType.MULTIPLICACAO)
                        {
                            getToken();
                            if (token.Type == EType.NUMERO)
                            {
                                getToken();
                            }
                            else
                            {
                                Error("Esperado um numero", token.NumLine, token2.Column);
                                return false;
                            }
                        }
                        else
                        {
                            Error("Esperado um operador", token.NumLine, token2.Column);
                            return false;
                        }
                    }
                    //Symbol.AddValor(token2, token);
                    return true;

                case EType.BOOLEANO:
                    return true;

                default:
                    Error("Esperado um numero ou booleano", token.NumLine, token2.Column);
                    return false;
            }
        }

        public bool WriteAnalyzer()
        {
            getToken();
            if (token.Type == EType.ABRE_PARENTESIS)
            {
                getToken();
                if (token.Type == EType.IDENTIFICADOR)
                {
                    if(sa.CheckIfDeclaratedIdentifier(token)){

                        Symbol sb = sa.GetSymbol(token);  
                        geraCod("call i32 (i8*, ...) @printf( i8* " + sb.Code + " ) nounwind");
                        getToken();
                        if(token.Type == EType.FECHA_PARENTESIS)
                        {
                            getToken();
                            return true;
                        }
                        else
                        {
                            Error("Esperado um fecha parentesis", token.NumLine, token2.Column);
                            return false;
                        }
                    }
                    else
                    {
                        Error("Idenitificador não existe", token.NumLine, token2.Column);
                        return false;
                    }
                }
                else
                {
                    Error("Esperado um identificador", token.NumLine, token2.Column);
                    return false;
                }
            }
            else
            {
                Error("Esperado um abre parentesis", token.NumLine, token2.Column);
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
            Codigo codigo = new Codigo(temp, cod, identifier);
            codigo.geradorLLVMIR();
        }
    }
}