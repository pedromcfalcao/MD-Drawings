using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures;
using Tekla.Structures.Drawing.Automation;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;
using Tekla.Structures.Model.UI;

namespace MD_Drawings
{
    internal class TeklaMDdrawings
    {
        public static Model MyModel = new Model();

        private static string DWG_Rule;

        private Model _model;

        public static void Estado_do_Modelo()
        {
            Console.WriteLine("\n Developed by STRUCTURAL RESEARCH, Manufactring drawings runner \n");
            Model TSM = new Model();
            if (TSM.GetConnectionStatus())
            {
                TeklaStructures.Connect();
                Console.WriteLine("Modelo ligado");
                ModelInfo modelInfo = MyModel.GetInfo();
                Console.WriteLine("Número do modelo: " + TSM.GetProjectInfo().ProjectNumber);
                Console.WriteLine("Nome do modelo: " + TSM.GetProjectInfo().Name);
                Console.WriteLine("Nome do ficheiro do modelo: " + modelInfo.ModelName);
                Console.WriteLine("Caminho do modelo: " + modelInfo.ModelPath + "\n");
            }
            else
            {
                Console.WriteLine("Atention model closed");
            }
        }

        public static ArrayList ListaTodasPecas()
        {
            Console.WriteLine("Listar todas as peças");
            ArrayList PECAS = new ArrayList();
            Model Model = new Model();
            ModelObjectEnumerator Objects = Model.GetModelObjectSelector().GetEnumerator();
            while (Objects.MoveNext())
            {
                if (Objects.Current is Part part)
                {
                    PECAS.Add(part);
                }
            }
            return PECAS;
        }

        public static ArrayList ListaPecasSelecionadas()
        {
            ArrayList PECAS = new ArrayList();
            Model Model = new Model();
            ModelObjectEnumerator ObjectEnum = new Tekla.Structures.Model.UI.ModelObjectSelector().GetSelectedObjects();
            while (ObjectEnum.MoveNext())
            {
                if (ObjectEnum.Current is Part part)
                {
                    PECAS.Add(part);
                }
            }
            return PECAS;
        }

        public static ArrayList ListaConjuntosSelecionados()
        {
            ArrayList Conjunto = new ArrayList();
            Model Model = new Model();
            ModelObjectEnumerator ObjectEnum = new Tekla.Structures.Model.UI.ModelObjectSelector().GetSelectedObjects();
            while (ObjectEnum.MoveNext())
            {
                if (ObjectEnum.Current is Assembly conj)
                {
                    Conjunto.Add(conj);
                }
            }
            return Conjunto;
        }

        public static void NumberingByLibrary()
        {
            Model TSM = new Model();
            if (TSM.GetConnectionStatus())
            {
                TeklaStructures.Connect();
                Operation.IsNumberingUpToDateAll();
                TeklaStructures.CommonTasks.PerformNumbering(fullNumbering: true);
                Console.WriteLine("Model numbering successful");
            }
        }

        public static void NumberingAll()
        {
            Tekla.Structures.ModelInternal.Operation.dotStartAction("FullNumbering", (string)null);

            Console.WriteLine("\n Numbering done \n");
        }

        public static void NumerarModelo()
        {
            Model TSM = new Model();
            if (TSM.GetConnectionStatus())
            {
                Operation.RunMacro("numbering.cs");
                Operation.RunMacro("..\\modeling\\numbering.cs");
                Console.WriteLine("Model numbered by running macro");
            }
            else
            {
                Console.WriteLine("Atention model closed");
            }
        }

        public static void executarMacro(string NomeMacro)
        {
            NomeMacro = "numbering.cs";
            Operation.RunMacro("numbering.cs");
            Operation.RunMacro("C:\\Shaymurtagh_Programs\\TEKLA\\FIRM_Murtagh_Test\\MACROS\\modeling\\numbering.cs");
            Console.WriteLine("executar macro");
        }

        public static void NumerarPorCodigo()
        {
            Model model = new Model();
            if (!model.GetConnectionStatus())
            {
                Console.WriteLine("Erro: Não foi possível conectar ao modelo.");
                return;
            }
            try
            {
                Operation.RunMacro("NumerarPorMacro.cs");
                Operation.RunMacro("..\\modeling\\NumerarPorMacro.cs");
                Console.WriteLine("Numeração realizada com sucesso via macro!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
        }

        public static void CriarDesenhoAutoDrawing()
        {
            Console.WriteLine("Start drawing creation");
            Model Model = new Model();
            DWG_Rule = "MDsDrawings.dproc";
            ArrayList Objects = ListaPecasSelecionadas();
            ArrayList Conjuntos = ListaConjuntosSelecionados();
            List<Identifier> partIDList = new List<Identifier>();
            NumerarModelo();
            foreach (Part obj in Objects)
            {
                if (obj != null)
                {
                    partIDList.Add(obj.Identifier);
                }
            }
            foreach (Assembly obj2 in Conjuntos)
            {
                if (obj2 != null)
                {
                    partIDList.Add(obj2.Identifier);
                }
            }
            AutoDrawingRule singleRule = new AutoDrawingRule(DWG_Rule);
            bool drawingGenerated = false;
            drawingGenerated = DrawingCreator.CreateDrawings(singleRule, partIDList, out var _);
            Console.WriteLine("");
            Console.WriteLine("SHAY MURTAGH DRAWINGS @ development ");
        }

        public static void AbrirDrawingList()
        {
            Console.WriteLine("\nAbrir lista de desenhos");
            TeklaStructures.CommonTasks.OpenDrawingList();
            Console.WriteLine("A lista de desenhos foi solicitada.");
        }
    }
}
