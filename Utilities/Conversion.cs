using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftMachine.Utilities
{
    public static class Conversion
    {
        #region Properties
        //public string dbName;
        //public string vsName;
        #endregion
        
        #region Metods
        public static string uppercaseFirst(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            char[] a = text.ToLower().ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
        /// <summary>
        ///Convert Table Name (DB) to Entitiy Name (using VS nomenclature: remove all '_' and add uppercase the first letter of each word)
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns>VS Variable Name</returns>
        public static string convertToEntityName(string tableName)
        {
            string newName = string.Empty;
            string[] words = tableName.ToLower().Split('_');
            for (int i = 0; i < words.Length; i++) //i=1 ==> won't uppercase the first word.
            {
                newName += uppercaseFirst(words[i]);
            }
            return newName;
        }

        //public static string convertToPropertyName(string RowName, string EntityName)
        //{
        //    string newName = string.Empty;
        //    //string EntityName = convertTableNameToEntityName(tableName);
        //    string[] words = RowName.Split('_');
        //    //string[] words = RowName.ToLower().Split('_');
        //    for (int i = 0; i < words.Length; i++) //i=1 ==> won't uppercase the first word.
        //    {
        //        newName += uppercaseFirst(words[i]);
        //    }
        //    if (newName.Length > EntityName.Length && newName.Substring(0, EntityName.Length).Equals(EntityName))
        //        newName = newName.Substring(EntityName.Length, newName.Length - EntityName.Length);
        //    return newName;
        //}

        //to avoid name crop when there is more than one parameter. i.e.: (Id, CompanyId) -> (BranchId, CompanyId)
        public static string convertToPropertyName(string RowName, string EntityName, bool SinglePKtable)
        {
            string newName = string.Empty;
            string[] words = RowName.Split('_');
            //string[] words = RowName.ToLower().Split('_');
            for (int i = 0; i < words.Length; i++) //i=1 ==> won't uppercase the first word.
            {
                newName += uppercaseFirst(words[i]);
            }
            if (SinglePKtable && newName.Length > EntityName.Length && newName.Substring(0, EntityName.Length).Equals(EntityName))
                newName = newName.Substring(EntityName.Length, newName.Length - EntityName.Length);
            return newName;
        }
        ///// <summary>
        ///// Recibe un Row Type (DB) a un  ParameterType (VS). Se utiliza para generar las entidades.
        ///// </summary>
        //private void convertToParameterType(Business.Entities.Row oRow)           
        //{
        //    string ParameterType = oRow.dbType.Split('(')[0];
        //    //Detalles del Parámetro: tamaño/lista de enum.
        //    //string[] ParameterSize = new string[50];
        //    string[] ParameterDetails = new string[oRow.dbType.Replace("'", "").Trim().Split(',').Count()];
        //    ParameterDetails = oRow.dbType.Replace("'", "").Trim().Split(','); 
        //    switch (ParameterType)
        //    {
        //        case "VARCHAR":
        //            ParameterType = "string"; //si uso toString, luego no lo puedo usar para crear la entidad.
        //            break;
        //        case "INT":
        //            ParameterType = "int";
        //            if (oRow.isAutoIncremental)
        //                ParameterType = string.Format("Nullable<{0}>", ParameterType);
        //            break;
        //        case "MEDIUMINT":
        //            ParameterType = "int";
        //            break;
        //        case "SMALLINT":
        //            ParameterType = "int";
        //            break;
        //        case "TINYINT":
        //            ParameterType = "int";
        //            break;
        //        case "DECIMAL":
        //            ParameterType = "decimal";
        //            break;
        //        case "DOUBLE":
        //            ParameterType = "decimal";
        //            break;
        //        case "DATE":
        //            ParameterType = "date";//VER!
        //            break;
        //        case "DATETIME":
        //            ParameterType = "datetime";//VER!
        //            break;
        //        case "ENUM":
        //            ParameterType = "enum";
        //            foreach (string ParameterDetail in ParameterDetails)
        //            {
        //                Business.Entities.Enumerator oParameterDetail = new Business.Entities.Enumerator(ParameterDetail);
        //                oRow.ParameterDatails.Add(oParameterDetail);
        //            }
        //            break;
        //        case "TEXT":
        //            ParameterType = "text";
        //            break;
        //        case "BLOB":
        //            ParameterType = "binary"; //VER!
        //            break;
        //        default:
        //            ParameterType = "string"; //si uso toString, luego no lo puedo usar para crear la entidad.
        //            break;
        //    }
        //    return ParameterType;
        //}

        /// <summary>
        /// Recibe un ParameterType (VS) y Devuelve 'To{tipo_variable}'. Es para para convertir los Parametros a VS, usando la función ConvertTo;
        /// </summary>
        public static string convertToConversionType(string ParameterType)
        {
            string ConvertToTypeVS = string.Empty;
            switch (ParameterType)
            {
                case "int":
                    ConvertToTypeVS = "ToInt32";
                    break;
                default:
                    ConvertToTypeVS = "To" + uppercaseFirst(ParameterType);
                    break;
            }
            return ConvertToTypeVS;
        }
        #endregion
    }
}
