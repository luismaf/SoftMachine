using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftMachine.Entities
{
    public class Tables : List<Table>
    {
        #region Methods
        public void GenerateMethods(ref StringBuilder sbMethods, ref StringBuilder sbStoredProcedures, string DatabaseName, System.Windows.Forms.ListBox.SelectedObjectCollection selectedTablesCollection)
        {
            foreach (Entities.Table oTable in this.ToList())
            {
                //Generate Foreign List Methods:
                foreach (Entities.Table ForeignTable in this.ToList())
                    oTable.addForeignMethods(ForeignTable, true); //analizeOnly = true
                oTable.addMainMethods();
                foreach (Entities.Table ForeignTable in this.ToList())
                    oTable.addForeignMethods(ForeignTable, false); //analizeOnly = false

                //solo le da formato a las entidades seleccionadas:
                foreach (string Item in selectedTablesCollection)
                {
                    if (Item.Equals(oTable.dbName))
                    {
                        //dar formato a los Métodos de Visual Studio:
                        oTable.Methods.FormatMethods(ref sbMethods);
                        oTable.Methods.FormatStoredProcedures(ref sbStoredProcedures, DatabaseName);
                    }
                }
            }
        }

        public void FormatBusinessEntityLayer(ref StringBuilder sb, string DatabaseName)
        {
            string ProyectName = Utilities.Conversion.convertToEntityName(DatabaseName);

            foreach (Entities.Table oTable in this.ToList())
            {
                sb.AppendLine(string.Format("namespace {0}.Business.Entities", ProyectName));                sb.AppendLine("{");                sb.AppendLine(string.Format("\tpublic class {0} : BusinessEntitiy", oTable.vsName));                sb.AppendLine("\t{");                sb.AppendLine("\t\t #region Properties");                sb.AppendLine("");                foreach (Entities.Row oRow in oTable.Rows)                {                    sb.AppendLine(string.Format("\t\t public {0} {1} {{ get; set; }}", oRow.vsType, oRow.vsName));                }                sb.AppendLine("");                sb.AppendLine("\t\t #endregion");                sb.AppendLine("");                sb.AppendLine("\t\t #region Constructors");                sb.AppendLine("");                sb.AppendLine(string.Format("\t\t public {0}()",oTable.vsName));                sb.AppendLine("\t\t {");                sb.AppendLine("\t\t\t this.Initialize();");                sb.AppendLine("\t\t }");                sb.AppendLine("\t\t #endregion");                sb.AppendLine("");                sb.AppendLine("\t\t #region Metods");                sb.AppendLine("");                sb.AppendLine("\t\t /// <summary>");                sb.AppendLine("\t\t /// Inicializa la entidad a los valores por defecto.");                sb.AppendLine("\t\t /// </summary>");                sb.AppendLine("\t\t public void Initialize()");                sb.AppendLine("\t\t {");
                sb.AppendLine("\t\t\t ");
                //sb.AppendLine("\t\t\t this.Id = null;");                //sb.AppendLine("\t\t\t this.Name = '';");                sb.AppendLine("\t\t }");                sb.AppendLine("\t\t #endregion");                sb.AppendLine("\t}");                sb.AppendLine("}");
                sb.AppendLine();
                sb.AppendLine();
                }
            }

        public void FormatBusinessLogicLayer(ref StringBuilder sb, string DatabaseName)
        {
            string ProyectName = Utilities.Conversion.convertToEntityName(DatabaseName);

            foreach (Entities.Table oTable in this.ToList())
            {
                sb.AppendLine();
                sb.AppendLine(string.Format("namespace {0}.Business.Logic", ProyectName));
                sb.AppendLine("{");
                sb.AppendLine(string.Format("\tpublic class {0} : BusinessLogic", oTable.vsName));                sb.AppendLine("\t{");                sb.AppendLine("\t\t #region Constructors");                sb.AppendLine("");
                sb.AppendLine(string.Format("\t\t public Data.Database.{0} Database;", oTable.vsName));                sb.AppendLine("");
                sb.AppendLine(string.Format("\t\t public {0}()", oTable.vsName));                sb.AppendLine("\t\t {");
                sb.AppendLine(string.Format("\t\t\t Database = new Data.Database.{0}();", oTable.vsName));                sb.AppendLine("\t\t }");                sb.AppendLine("");                sb.AppendLine("\t\t #endregion");                sb.AppendLine("");                sb.AppendLine("\t\t #region Metods");
                sb.AppendLine("");
                foreach (Entities.Method oMethod in oTable.Methods)
                {
                    sb.AppendLine("\t\t /// <summary>");
                    sb.AppendLine(oMethod.Summary);
                    sb.AppendLine("\t\t /// </summary>");
                    //sb.AppendLine("\t\t /// <returns>Coleccion de ...</returns>");
                    sb.AppendLine(string.Format("\t\t public {0} {1}({2})", oMethod.MethodReturnType, oMethod.vsName, oMethod.MethodParametersDefinition)); //Business.Entities.Areas
                    sb.AppendLine("\t\t {");
                    if (oMethod.OutParameters.Count > 0) //si hay parametros de salida ==> MethodReturnType = 'void' :
                        sb.AppendLine(string.Format("\t\t\t return Database.{0}({1});", oMethod.vsName, oMethod.MethodParametersCall));
                    else sb.AppendLine(string.Format("\t\t\t Database.{0}({1});", oMethod.vsName, oMethod.MethodParametersCall));
                    //else sb.AppendLine("\t\t\t return;");
                    sb.AppendLine("\t\t }");
                    sb.AppendLine("");
                }                sb.AppendLine("\t\t #endregion");                sb.AppendLine("\t}");                sb.AppendLine("}");
                sb.AppendLine("");
                sb.AppendLine("");
            }

        }

        public void FormatBusinessDataLayer(ref StringBuilder sb, string DatabaseName)
        {
            string ProyectName = Utilities.Conversion.convertToEntityName(DatabaseName);
            foreach (Entities.Table oTable in this.ToList())
            { }
        }
        #endregion
    }
}
