using System;
using System.IO;
using System.Collections.Generic;

namespace AnalisadorLexico
{
    class Program
    {
        static void Main(string[] args) // ANA MATTOS
        {
            string path, option;
            bool x = true;
            while(x)
            {
                Console.Write("Caminho do arquivo: ");
                path = Console.ReadLine();
                try
                {
                    Lexem lexem = new(path);
                    List<Token> tokens = lexem.GetLexem();
                    Console.WriteLine("Tokens: " + tokens.Count);
                    for (int i = 0; i < tokens.Count; i++)
                    {
                        Token token = tokens[i];
                        Console.WriteLine(token.ToString()); 
                    }
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine("Tabela de símbolos");

                    foreach(var item in lexem.SymbolTable.DictionaryToArray())
                    {
                        Console.WriteLine(item);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Não foi possivel ler o arquivo");
                }
                Console.WriteLine("Deseja tentar novamente? [Y/N]");
                option = Console.ReadLine();
                if(option == "Y" || option == "y")
                    x = true;
                else if(option == "N" || option == "n")
                    x = false;
                else
                {
                    Console.WriteLine("Opção invalida");
                    x = false;
                }
            }

        }
    }
}
