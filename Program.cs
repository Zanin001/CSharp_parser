using System;
using System.IO;
using System.Collections.Generic;
using AnalisadorSintatico;

namespace AnalisadorLexico
{
    class Program
    {
        static void Main2(string[] args) // ANA MATTOS
        {
            bool x = true;
            while (x) 
            {
                string path, option;
               
                Console.Write("Caminho do arquivo: ");
                path = Console.ReadLine();
                try
                {
                    PPR ppr = new PPR(path);
                    ppr.parse();

                    Console.WriteLine("O seguinte código intermediário foi gerado: \n");
                    string filePath = GetFilePath();
                    Console.WriteLine(filePath);

                    foreach (string line in File.ReadLines(filePath))
                    {
                        Console.WriteLine(line);
                    }

                    Console.WriteLine("\nO código foi gerado e está disponível no arquivo >>> llvm.ll <<< na pasta raiz do compilador.");

                }
                catch (Exception)
                {
                    Console.WriteLine("Não foi possivel ler o arquivo");
                }

                Console.WriteLine("Deseja tentar novamente? [Y/N]");
                option = Console.ReadLine();
                if (option == "Y" || option == "y")
                    x = true;
                else if (option == "N" || option == "n")
                    x = false;
                else
                {
                    Console.WriteLine("Opção invalida");
                    x = false;
                }
            }
        }


        static string GetFilePath()
        {
            string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            _filePath = Directory.GetParent(_filePath).FullName;
            _filePath = Directory.GetParent(Directory.GetParent(_filePath).FullName).FullName;
            
            return _filePath + "\\llvm.ll";
        }
    }
}