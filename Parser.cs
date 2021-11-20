using System;
using System.IO;
using System.Collections.Generic;

namespace AnalisadorLexico
{
    public abstract class Parser
    {
        SymbolTable st;
        Lexem lexem;
        Token token;

        public Parser(string path)
        {
            //st = new SymbolTable();
            lexem = new(path);
        }

        public void getToken()
        {
            token = lexem.Analyzer();
        }
    }
}