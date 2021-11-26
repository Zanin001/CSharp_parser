using System;
using System.IO;
using System.Collections.Generic;
using System.BufferedWriter;
using System.FileWriter;


namespace AnalisadorLexico
{
    class CodIntermediario
    {
        public int temp;
        public string codigo;

        public CodIntermediario(int t, String cod) {
		temp = t;
		codigo = cod;
	}

    public String geraPreambulo() {
		String print;
		print = "@.str = private unnamed_addr constant [3 * i8] c\"%d\\00\", align 1 \n";
		print += "; Function Attrs; noinline nounwind optnone uwtable \n";
		print += "define dso_local i32 @main() #0 { \n";
		return print;
	}


    public String geraCodigo() {
		return codigo;
	}
	
	public String geraEpilogo() {
		String print;
		print = "}\n";
		print += "declare dso_local i32 @printf(i8*, ...) #1\n";
		return print;
	}


    public void geraLLVMIR() {
		try {
			BufferedWriter _writer = new BufferedWriter(new FileWriter("codigo.ll"));
			_writer.write(geraPreambulo());
			_writer.write(geraCodigo());
			_writer.write(geraEpilogo());
			_writer.close();
		} catch (IOException e) {
		
			e.printStackTrace();
		}
		
	}


    }
}