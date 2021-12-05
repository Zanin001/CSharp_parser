using System;
using System.IO;
using System.Collections.Generic;
using Enums;

namespace AnalisadorLexico
{

    public class Lexem
    {
        private StreamReader St { get; set; }
        private char Character { get; set; }
        private char CharPeek { get; set; }
        private int CharPositon { get; set; }
        private int Line { get; set; }
        private int Colum { get; set; }
        private string Path { get; set; }
        private bool FileEnds { get; set; }
        private List<Token> TokensList { get; set; }
        public SymbolTable SymbolTable { get; private set; }

        public Lexem(string path) // GABRIEL ZANIN
        {
            //Abre o arquivo
            St = new StreamReader(path);
            // Inicia a linha
            Line = 1;
            FileEnds = false;
            //Cria uma nova tablea de simbolos
            SymbolTable = new();
        }
        public char NextChar() // GABRIEL ZANIN
        {
            //Le a primeira posição do arquivo
            CharPositon = St.Read();
            //Caso for -1 quer dizer que o arquivo terminou
            if (CharPositon != -1)
            {
                //Converte para char
                Character = Convert.ToChar(CharPositon);
                if (Character == '\n')
                {
                    Line++;
                    Colum = 0;
                }
                else
                    Colum++;
                return Character;
            }
            else
                FileEnds = true;
            return ' ';

        }

        // Verifica qual é o próximo caractere sem consumir ele da memoria
        public void PeekNextChar() // CARLOS KAYSON
        {
            int charPeekPosition = St.Peek();
            if (charPeekPosition != -1)
                CharPeek = Convert.ToChar(St.Peek());
            else
                CharPeek = ' ';
        }

        public Token Analyzer() // GABRIEL ZANIN / CARLOS KAYSON / BRUNO ADAUTO
        {
            string lexem = "";
            int col = 0;
            Character = NextChar();

            while (!FileEnds)
            {
                if (Character == ':')
                {
                    //Verifica se o próximo caracter é =
                    PeekNextChar();
                    if (CharPeek == '=')
                    {
                        // Caso for consome próximo caracter e cria um novo token
                        NextChar();
                        return new Token(EType.ATRIBUICAO, ":=", Line, Colum);
                    }
                    else
                        return new Token(EType.TIPO, ":", Line, Colum);
                }
                else if (Character == ';')
                    return new Token(EType.PONTO_E_VIRGULA, ";", Line, Colum);
                else if (Character == '.')
                    return new Token(EType.PONTO, ".", Line, Colum);
                else if (Character == '+')
                    return new Token(EType.MAIS, "+", Line, Colum);
                else if (Character == '-')
                    return new Token(EType.MENOS, "-", Line, Colum);
                else if (Character == '*')
                    return new Token(EType.MULTIPLICACAO, "*", Line, Colum);
                else if (Character == '/')
                    return new Token(EType.DIVISAO, "/", Line, Colum);
                else if (Character == '(')
                    return new Token(EType.ABRE_PARENTESIS, "(", Line, Colum);
                else if (Character == ')')
                    return new Token(EType.FECHA_PARENTESIS, ")", Line, Colum);
                //Caso for um comentario ignora
                else if (Character == '{')
                {
                    PeekNextChar();

                    while (CharPeek != '}')
                    {
                        NextChar();
                        PeekNextChar();
                    }
                    NextChar();
                }
                //Caso for um letra ou _ le até não ser mais
                else if (Char.IsLetter(Character) || Character == '_')
                {
                    col = Colum;
                    lexem += Character;
                    PeekNextChar();


                    while (Char.IsLetter(CharPeek) || CharPeek == '_')
                    {
                        NextChar();
                        lexem += Character;
                        PeekNextChar();
                    }
                    //Verifica se é uma palavra reservada
                    if (lexem == "programa")
                        return new Token(EType.PROGRAMA, lexem, Line, col);
                    else if (lexem == "var")
                        return new Token(EType.VAR, lexem, Line, col);
                    else if (lexem == "inicio")
                        return new Token(EType.INICIO, lexem, Line, col);
                    else if (lexem == "fim")
                        return new Token(EType.FIM, lexem, Line, col);
                    else if (lexem == "escreva")
                        return new Token(EType.ESCREVA, lexem, Line, col);
                    else if (lexem == "inteiro")
                        return new Token(EType.INTEIRO, lexem, Line, col);
                    else if (lexem == "booleano")
                        return new Token(EType.BOOLEANO, lexem, Line, col);
                    else
                    {
                        // Caso não for adiciona na tabela de simbolos e cria um novo token identificador
                        Token token = new(EType.IDENTIFICADOR, lexem, Line, col);
                        SymbolTable.AddToken(token);
                        return token;
                    }

                }
                //Verifica se é um numero
                else if (Char.IsDigit(Character))
                {
                    col = Colum;
                    lexem += Character;
                    PeekNextChar();

                    while (Char.IsDigit(CharPeek))
                    {
                        NextChar();
                        lexem += Character;
                        PeekNextChar();
                    }

                    return new Token(EType.NUMERO, lexem, Line, col);
                }
                NextChar();
            }
            lexem += Character;
            return new Token(EType.ERRO, lexem, Line, Colum);
        }

        public List<Token> GetLexem() //ANA MATTOS
        {
            TokensList = new List<Token>();
            Token newToken = Analyzer();
            // Verifica cada todos os caracteres até for um erro ou terminar
            while (newToken.Type != EType.ERRO && CharPositon != -1)
            {
                TokensList.Add(newToken);
                newToken = Analyzer();
            }
            St.Close();
            return TokensList;
        }
    }
}