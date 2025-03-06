using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MD_Drawings;

namespace MD_Drawings
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TeklaMDdrawings.Estado_do_Modelo();
            TeklaMDdrawings.NumberingAll();
            TeklaMDdrawings.AbrirDrawingList();
            TeklaMDdrawings.CriarDesenhoAutoDrawing();
            Console.WriteLine("Press any key to close, the console");
            Console.ReadKey();
        }
    }
}
