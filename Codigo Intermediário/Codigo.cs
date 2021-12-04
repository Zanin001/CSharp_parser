using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace AnalisadorLexico
{
    class Codigo
    {
        public int temp { get; set; }
        public string codigo { get; set; }

        public Codigo(int temp, string codigo)
        {
            this.temp = temp;
            this.codigo = codigo;
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

        public void geradorLLVMIR()
        {
            //File.ReadAllText("", codigo);
        }
    }
}