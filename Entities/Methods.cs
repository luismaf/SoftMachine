using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftMachine.Entities
{
    public class Methods: List<Method>
    {
        #region Methods
        public void FormatMethods(ref StringBuilder sb)
        {
            foreach (Entities.Method oMethod in this.ToList())
            {
                //oMethod.Prepare();
                sb.AppendLine(string.Format("\t\tpublic {0} {1} ({2})", oMethod.MethodReturnType, oMethod.vsName, oMethod.MethodParametersDefinition));
                sb.AppendLine("\t\t{");
                sb.AppendLine("\t\t\t//Instanciamos la conexión");
                sb.AppendLine("\t\t\tMySqlConnection oConnection = Data.DatabaseName.Adapter.newConnection();");
                sb.AppendLine("\t\t\tDataSet result = new DataSet();");
                sb.AppendLine("\t\t\t//try");
                sb.AppendLine("\t\t\t//{");
                sb.AppendLine("\t\t\tusing (oConnection)");
                sb.AppendLine("\t\t\t{");
                sb.AppendLine("\t\t\t\t//abrimos conexion");
                sb.AppendLine("\t\t\t\toConnection.Open();");
                sb.AppendLine("");
                sb.AppendLine("\t\t\t\t//Instanciamos el Comando");
                sb.AppendLine("\t\t\t\tMySqlCommand oCommand = oConnection.CreateCommand();");
                sb.AppendLine("\t\t\t\tusing (oCommand)");
                sb.AppendLine("\t\t\t\t{");
                sb.AppendLine("\t\t\t\t\t//asignamos la conexion");
                sb.AppendLine("\t\t\t\t\toCommand.Connection = oConnection;");
                sb.AppendLine("");
                sb.AppendLine("\t\t\t\t\t//utilizamos stored procedure");
                sb.AppendLine("\t\t\t\t\toCommand.CommandType = CommandType.StoredProcedure;");
                sb.AppendLine("");
                sb.AppendLine("\t\t\t\t\t//establecemos el nombre del stored procedure:");
                //establecemos el nombre del stored procedure
                sb.AppendLine(string.Format("\t\t\t\t\toCommand.CommandText = '{0}';", oMethod.dbName));
                /////////////////////////////////////
                ////asignamos parámetros:
                sb.AppendLine("\t\t\t\t\t//asignamos parámetros:");
                sb.AppendLine(oMethod.MethodInParametersLines); //oCommand.Parameters.AddWithValue('@_id', Id);");
                sb.AppendLine("");
                /////////////////////////////////////
                //Instanciamos la coleccion de {ENTIDAD}s"); ENTIDAD= convertToVarName(Table.dbName)
                sb.AppendLine(string.Format("\t\t\t\t\t //Instanciamos la coleccion de {0}s", oMethod.Table.vsName));
                sb.AppendLine(string.Format("\t\t\t\t\t Entities.{0}s o{0}s = new Entities.{0}s();", oMethod.Table.vsName));
                sb.AppendLine("");
                //verificar si hay parametros de salida:
                if (oMethod.OutParameters.Count == 0) //if (oMethod.OutParameters == null)
                {   //ExecuteNonQuery
                    sb.AppendLine("\t\t\t\t\t//Ejecutamos el oCommand, (no devuelve nada");
                    sb.AppendLine("\t\t\t\t\t//oCommand.ExecuteNonQuery();" + Environment.NewLine + Environment.NewLine);
                }
                else
                {
                    /////////////////////////////////////
                    //VER! Acá, por cada clave foranea (o directamente clave primaria de alguna otra clase), se debería instanciar un objeto de la "tabla" de la que es clave primaria, para utilizarlo luego.
                    //VER! Acá, por cada clave primaria de esta tabla, que a su vez sea CLAVE PRIMARIA en alguna otra clase, se debería instanciar un objeto (una colección) de la "tabla" de la que es clave primaria, para agregarlo al objeto item.
                    if (!oMethod.ForeignEntityInstantiationLines.Equals(string.Empty))
                    {
                        sb.AppendLine("\t\t\t\t\t //Instanciamos las entidades Foraneas correspondientes a la Capa de Datos");
                        sb.AppendLine(oMethod.ForeignEntityInstantiationLines);
                    }
                    /////////////////////////////////////
                    //ExecuteReader
                    sb.AppendLine("");
                    sb.AppendLine("\t\t\t\t\t //Ejecutamos el oCommand y retornamos los valores");
                    sb.AppendLine("\t\t\t\t\t MySqlDataReader oReader = oCommand.ExecuteReader();");
                    sb.AppendLine("\t\t\t\t\t using (oReader)");
                    sb.AppendLine("\t\t\t\t\t {");
                    sb.AppendLine("\t\t\t\t\t\t //si existe algun valor, creamos el objeto y lo almacenamos en la colección");
                    sb.AppendLine("\t\t\t\t\t\t while (oReader.Read())");
                    sb.AppendLine("\t\t\t\t\t\t {");
                    sb.AppendLine(string.Format("\t\t\t\t\t\t\t //Instanciamos el objeto o{0} (entidad: {0}) y las colecciones que contenga", oMethod.Table.vsName));
                    /////////////////////////////////////
                    //Instanciamos al objeto ENTIDAD
                    sb.AppendLine(string.Format("\t\t\t\t\t\t\t Entities.{0} o{0} = new Entities.{0}();", oMethod.Table.vsName));
                    sb.AppendLine("");

                    //cargamos los Parametros de salida:
                    sb.AppendLine(oMethod.MethodOutParametersLines);
                    sb.AppendLine();
                    sb.AppendLine(string.Format("\t\t\t\t\t\t\t //Agregamos el objeto {0} a la coleccion de {0}s", oMethod.Table.vsName));
                    sb.AppendLine(string.Format("\t\t\t\t\t\t\t o{0}s.Add(o{0});", oMethod.Table.vsName));
                    sb.AppendLine(string.Format("\t\t\t\t\t\t\t o{0} = null;", oMethod.Table.vsName));
                    sb.AppendLine("\t\t\t\t\t\t }");
                    sb.AppendLine("\t\t\t\t\t }");
                    sb.AppendLine("\t\t\t\t\t //retornamos la coleccion");
                    sb.AppendLine(string.Format("\t\t\t\t\t return o{0}s;", oMethod.Table.vsName));
                    sb.AppendLine("\t\t\t\t }");
                    sb.AppendLine("\t\t\t }");
                    sb.AppendLine("\t\t\t /*}");
                    sb.AppendLine("\t\t\t catch (MySqlException exc)");
                    sb.AppendLine("\t\t\t {");
                    sb.AppendLine("\t\t\t\t Utilities.Log.Save('Error de MySQL', exc.Message); //capturamos el error de MySQL");
                    sb.AppendLine("\t\t\t\t return null;");
                    sb.AppendLine("\t\t\t }");
                    sb.AppendLine("\t\t\t catch (Exception e)");
                    sb.AppendLine("\t\t\t {");
                    sb.AppendLine("\t\t\t\t Utilities.Log.Save('Error General', e.Message); //capturamos cualquier error (distinto al anterior).");
                    sb.AppendLine("\t\t\t\t return null;");
                    sb.AppendLine("\t\t\t }");
                    sb.AppendLine("\t\t\t finally");
                    sb.AppendLine("\t\t\t {");
                    sb.AppendLine("\t\t\t\t oConnection.Close();");
                    sb.AppendLine("\t\t\t }//*/");
                    sb.AppendLine("\t\t }");
                }
            }
        }
        public void FormatStoredProcedures(ref StringBuilder sb, string DatabaseName)
        {
            foreach (Entities.Method oMethod in this.ToList())
            {
                sb.AppendLine(string.Format(" USE {0};", DatabaseName));
                sb.AppendLine(Environment.NewLine);

                sb.AppendLine(" DELIMITER ; ");
                sb.AppendLine(string.Format("DROP procedure IF EXISTS `{0}`;", oMethod.dbName));
                sb.AppendLine(" DELIMITER $$ ");
                sb.AppendLine(string.Format("CREATE PROCEDURE `{0}`.`{1}` ({2})", DatabaseName, oMethod.dbName, oMethod.spParameters));

                sb.AppendLine(" BEGIN ");
                if (oMethod.Type == Entities.Method.Types.List)
                {
                    if (oMethod.isForeign)
                    {
                        sb.AppendLine("\tSELECT\t" + oMethod.spColumns);
                        sb.AppendLine("\tFROM\t" + "`" + oMethod.Table.dbName + "`");
                        sb.AppendLine("\tWHERE\t" + oMethod.spWhere + ";");
                    }
                    else
                    {
                        sb.AppendLine("   IF" + oMethod.spConditions + "THEN");
                        sb.AppendLine("\tSELECT\t" + oMethod.spColumns);
                        sb.AppendLine();
                        sb.AppendLine("\tFROM\t" + "`" + oMethod.Table.dbName + "`" + ";");
                        sb.AppendLine();
                        sb.AppendLine("   ELSE ");
                        sb.AppendLine("\tSELECT\t" + oMethod.spColumns);
                        sb.AppendLine();
                        sb.AppendLine("\tFROM\t" + "`" + oMethod.Table.dbName + "`");
                        sb.AppendLine();
                        sb.AppendLine("\tWHERE\t" + oMethod.spWhere + ";");
                        sb.AppendLine("   END IF; ");
                    }
                }
                else if (oMethod.Type == Entities.Method.Types.Update)
                {
                    sb.AppendLine("\tINSERT INTO  " + "`" + oMethod.Table.dbName + "`" + " (");
                    sb.AppendLine("\t\t" + oMethod.spColumns + ")");
                    sb.AppendLine();
                    sb.AppendLine("\tVALUES (\t");
                    sb.AppendLine("\t\t" + oMethod.spValues + ")");
                    sb.AppendLine();
                    sb.AppendLine("\tON DUPLICATE KEY UPDATE"); //-
                    sb.AppendLine("\t\t" + oMethod.spOnDuplicateUpdate + ";");
                    sb.AppendLine();
                }
                else if (oMethod.Type == Entities.Method.Types.Delete)
                {
                    sb.AppendLine("\tDELETE FROM  " + "`" + oMethod.Table.dbName + "`");
                    sb.AppendLine("\tWHERE\t" + oMethod.spWhere + ";");
                    //sb.AppendLine("\tSELECT\trow_count() as rowsAffected; ");
                }
                sb.AppendLine(" END $$ ");
                sb.AppendLine(Environment.NewLine);

            }
        }
        #endregion
    }
}
