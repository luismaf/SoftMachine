using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftMachine.Business.Entities
{
    public class Tables : List<Table>
    {
        #region Methods
        public void GenerateMethods(ref StringBuilder sbMethods, ref StringBuilder sbStoredProcedures, string DatabaseName, System.Windows.Forms.ListBox.SelectedObjectCollection selectedTablesCollection)
        {
            foreach (Business.Entities.Table oTable in this.ToList())
            {
                //Generate Foreign List Methods:
                foreach (Business.Entities.Table ForeignTable in this.ToList())
                    oTable.addForeignMethods(ForeignTable, true); //analizeOnly = true
                oTable.addMainMethods();
                foreach (Business.Entities.Table ForeignTable in this.ToList())
                    oTable.addForeignMethods(ForeignTable, false); //analizeOnly = false

                oTable.Methods.FormatMethods(ref sbMethods);
                oTable.Methods.FormatStoredProcedures(ref sbStoredProcedures, DatabaseName);

                ////solo le da formato a las entidades seleccionadas:
                //foreach (string Item in selectedTablesCollection)
                //{
                //    if (Item.Equals(oTable.dbName))
                //    {
                //        //dar formato a los Métodos de Visual Studio:
                //        oTable.Methods.FormatMethods(ref sbMethods);
                //        oTable.Methods.FormatStoredProcedures(ref sbStoredProcedures, DatabaseName);
                //    }
                //}
            }
        }

        private void addRelation(Business.Entities.Table initialTable)
        {
            //puede haber como maximo una relacion por cada tabla foranea:
            foreach (Business.Entities.Table otherTable in this.ToList())
            {
                Business.Entities.Relation oRelation = new Business.Entities.Relation();

                foreach (Business.Entities.Row initialRow in initialTable.Rows)
                {
                    foreach (Business.Entities.Row otherRow in otherTable.Rows)
                    {
                        //Si la "Otra Fila" es PK y se llama igual que la Fila Inicial, la agrega como parametro del método:
                        if (otherRow.isPK && otherRow.dbName == initialRow.dbName)
                        {
                            //si la Fila "Inicial" (no la "Otra Fila") tambien es PK, la "Otra Tabla" tiene menos PK que la Tabla Inicial --> la "Otra Tabla" es una coleccion dentro de la Tabla Inicial.
                            //o la Fila "Primaria" (a deferencia de la "Fila Foranea") no es PK --> la "Otra Tabla" es un Entidad.
                            if ((initialRow.isPK && otherTable.pkCounter < initialTable.pkCounter) || !initialRow.isPK)
                            {
                                //isPK == true --> colección          isPK == false --> entidad
                                oRelation.Type = (initialRow.isPK) ? Business.Entities.Relation.Types.Collection : Business.Entities.Relation.Types.Entity;
                                //agrego el parametro de la relación:
                                oRelation.Parameters.Add(initialRow);
                                //si no hay un parámetro principal, lo agrego:
                                if (oRelation.MainParameter == null) oRelation.MainParameter = initialRow;

                            }
                        }
                    }
                }
                //si hay alguna coincidencia.
                if (oRelation.Parameters.Count > 0)
                {
                    //agrego las tablas a la Relación.
                    oRelation.InitialTable = initialTable; //agrega la tabla inicial (a la relación)
                    oRelation.OtherTable = otherTable; //agrega la otra tabla (a la relación)

                    //Para cada parametro, lo agrega a la relación.
                    foreach (Business.Entities.Row initialRow in initialTable.Rows)
                        foreach (Business.Entities.Row oParameter in oRelation.Parameters)
                            if (initialRow == oParameter)
                                initialRow.Method = new Business.Entities.Method(oRelation);
                            //oParameter.isPK = true;
                }
            }
        }
            
        public void FormatBusinessEntityLayer(ref StringBuilder sb, string DatabaseName)
        {
            string ProyectName = Utilities.Conversion.convertToEntityName(DatabaseName);

            foreach (Business.Entities.Table oTable in this.ToList())
            {
                sb.AppendLine(string.Format("namespace {0}.Business.Entities", ProyectName));
                sb.AppendLine("{");
                sb.AppendLine(string.Format("\tpublic class {0} : BusinessEntitiy", oTable.vsName));
                sb.AppendLine("\t{");
                sb.AppendLine("\t\t #region Properties");
                sb.AppendLine("");
                foreach (Business.Entities.Row oRow in oTable.Rows)
                {
                    if (oRow.Method.vsName == null)
                    {
                        sb.AppendLine(string.Format("\t\t public {0} {1} {{ get; set; }}", oRow.vsType, oRow.vsName));
                    }
                    else if (!oRow.ForeignEntityName.Equals(string.Empty))
                    {
                        sb.AppendLine(string.Format("\t\t public Business.Entities.{0} o{1} {{ get; set; }}", oRow.ForeignEntityName));
                    }
                }
                sb.AppendLine("");
                sb.AppendLine("\t\t #endregion");
                sb.AppendLine("");
                sb.AppendLine("\t\t #region Constructors");
                sb.AppendLine("");
                sb.AppendLine(string.Format("\t\t public {0}()",oTable.vsName));
                sb.AppendLine("\t\t {");
                sb.AppendLine("\t\t\t this.Initialize();");
                sb.AppendLine("\t\t }");
                sb.AppendLine("\t\t #endregion");
                sb.AppendLine("");
                sb.AppendLine("\t\t #region Metods");
                sb.AppendLine("");
                sb.AppendLine("\t\t /// <summary>");
                sb.AppendLine("\t\t /// Inicializa la entidad a los valores por defecto.");
                sb.AppendLine("\t\t /// </summary>");
                sb.AppendLine("\t\t public void Initialize()");
                sb.AppendLine("\t\t {");
                sb.AppendLine("\t\t\t ");
                //sb.AppendLine("\t\t\t this.Id = null;");
                //sb.AppendLine("\t\t\t this.Name = \"\";");
                sb.AppendLine("\t\t }");
                sb.AppendLine("\t\t #endregion");
                sb.AppendLine("\t}");
                sb.AppendLine("}");
                sb.AppendLine();
                sb.AppendLine();
                }
            }

        public void FormatBusinessLogicLayer(ref StringBuilder sb, string DatabaseName)
        {
            string ProyectName = Utilities.Conversion.convertToEntityName(DatabaseName);

            foreach (Business.Entities.Table oTable in this.ToList())
            {
                sb.AppendLine();
                sb.AppendLine(string.Format("namespace {0}.Business.Logic", ProyectName));
                sb.AppendLine("{");
                sb.AppendLine(string.Format("\tpublic class {0} : BusinessLogic", oTable.vsName));
                sb.AppendLine("\t{");
                sb.AppendLine("\t\t #region Constructors");
                sb.AppendLine("");
                sb.AppendLine(string.Format("\t\t public Data.Database.{0} Database;", oTable.vsName));
                sb.AppendLine("");
                sb.AppendLine(string.Format("\t\t public {0}()", oTable.vsName));
                sb.AppendLine("\t\t {");
                sb.AppendLine(string.Format("\t\t\t Database = new Data.Database.{0}();", oTable.vsName));
                sb.AppendLine("\t\t }");
                sb.AppendLine("");
                sb.AppendLine("\t\t #endregion");
                sb.AppendLine("");
                sb.AppendLine("\t\t #region Metods");
                sb.AppendLine("");
                foreach (Business.Entities.Method oMethod in oTable.Methods)
                {
                    sb.AppendLine("\t\t /// <summary>");
                    sb.AppendLine(oMethod.Summary);
                    sb.AppendLine("\t\t /// </summary>");
                    //sb.AppendLine("\t\t /// <returns>Coleccion de ...</returns>");
                    sb.AppendLine(string.Format("\t\t public {0} {1}({2})", oMethod.MethodReturnType, oMethod.vsName, oMethod.InParameters.Definition)); //Business.Entities.Areas
                    sb.AppendLine("\t\t {");
                    if (oMethod.OutParameters.Count > 0) //si hay parametros de salida ==> MethodReturnType = \"void\" :
                        sb.AppendLine(string.Format("\t\t\t return Database.{0}({1});", oMethod.vsName, oMethod.InParameters.Call));
                    else sb.AppendLine(string.Format("\t\t\t Database.{0}({1});", oMethod.vsName, oMethod.InParameters.Call));
                    //else sb.AppendLine("\t\t\t return;");
                    sb.AppendLine("\t\t }");
                    sb.AppendLine("");
                }
                sb.AppendLine("\t\t #endregion");
                sb.AppendLine("\t}");
                sb.AppendLine("}");
                sb.AppendLine("");
                sb.AppendLine("");
            }

        }
        public void FormatBusinessDataLayer(ref StringBuilder sb, string DatabaseName)
        {
            string ProyectName = Utilities.Conversion.convertToEntityName(DatabaseName);
            foreach (Business.Entities.Table oTable in this.ToList())
            { }
        }
        #endregion
    }
}
