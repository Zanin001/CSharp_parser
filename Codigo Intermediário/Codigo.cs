using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace AnalisadorLexico
{
    public class Codigo
    {
        public int temp;
        public string codigo;

        public Codigo(int temp, string cod)
        {
            this.temp = temp;
            this.codigo = cod;
            
        }

        public string geraInicioCod()
        {
            string mostra;
            mostra = "@.str = private unnamed_addr constant [3 * i8] c\"%d\\00\", align 1 \n";
            mostra += "; Function Attrs; noinline nounwind optnone uwtable \n";
            mostra += "define dso_local i32 @main() #0 { \n";
            return mostra;
        }

        public string geraCodigo()
        {
            return codigo;
        }

        public string geraFimCod()
        {
            string mostra;
            mostra = "}\n";
            mostra += "declare dso_local i32 @printf(i8*, ...) #1\n";
            return mostra;
        }

        public async void geradorLLVMIR()
        {
            using StreamWriter f = new StreamWriter("llvm.ll");
            f.Write(geraInicioCod());
            f.Write(geraCodigo());
            f.Write(geraFimCod());
            f.Close();            
        }
    }
}