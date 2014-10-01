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

        //public static string convertToPropertyName(string columnName, string EntityName)
        //{
        //    string newName = string.Empty;
        //    //string EntityName = convertTableNameToEntityName(tableName);
        //    string[] words = columnName.Split('_');
        //    //string[] words = columnName.ToLower().Split('_');
        //    for (int i = 0; i < words.Length; i++) //i=1 ==> won't uppercase the first word.
        //    {
        //        newName += uppercaseFirst(words[i]);
        //    }
        //    if (newName.Length > EntityName.Length && newName.Substring(0, EntityName.Length).Equals(EntityName))
        //        newName = newName.Substring(EntityName.Length, newName.Length - EntityName.Length);
        //    return newName;
        //}

        //to avoid name crop when there is more than one parameter. i.e.: (Id, CompanyId) -> (BranchId, CompanyId)
        public static string convertToPropertyName(string columnName, string EntityName, bool SinglePKtable)
        {
            string newName = string.Empty;
            string[] words = columnName.Split('_');
            //string[] words = columnName.ToLower().Split('_');
            for (int i = 0; i < words.Length; i++) //i=1 ==> won't uppercase the first word.
            {
                newName += uppercaseFirst(words[i]);
            }
            if (SinglePKtable && newName.Length > EntityName.Length && newName.Substring(0, EntityName.Length).Equals(EntityName))
                newName = newName.Substring(EntityName.Length, newName.Length - EntityName.Length);
            return newName;
        }
        /// <summary>
        /// Recibe un Column Type (DB) a un  PropertyType (VS). Se utiliza para generar las entidades.
        /// </summary>
        public static string convertToPropertyType(string columnType)
        {
            string propertyType = string.Empty;

            columnType = columnType.Split('(')[0];

            switch (columnType)
            {
                case "VARCHAR":
                    propertyType = "string"; //si uso toString, luego no lo puedo usar para crear la entidad.
                    break;
                case "INT":
                    propertyType = "int";
                    break;
                case "MEDIUMINT":
                    propertyType = "int";
                    break;
                case "SMALLINT":
                    propertyType = "int";
                    break;
                case "TINYINT":
                    propertyType = "int";
                    break;
                case "DECIMAL":
                    propertyType = "decimal";
                    break;
                case "DOUBLE":
                    propertyType = "decimal";
                    break;
                case "DATE":
                    propertyType = "date";//VER!
                    break;
                case "DATETIME":
                    propertyType = "datetime";//VER!
                    break;
                case "TEXT":
                    propertyType = "text";
                    break;
                case "BLOB":
                    propertyType = "binary"; //VER!
                    break;
                default:
                    propertyType = "string"; //si uso toString, luego no lo puedo usar para crear la entidad.
                    break;
            }
            return propertyType;
        }

        /// <summary>
        /// Recibe un PropertyType (VS) y Devuelve 'To{tipo_variable}'. Es para para convertir los Parametros a VS, usando la función ConvertTo;
        /// </summary>
        public static string convertToConversionType(string propertyType)
        {
            string ConvertToTypeVS = string.Empty;
            switch (propertyType)
            {
                case "int":
                    ConvertToTypeVS = "ToInt32";
                    break;
                default:
                    ConvertToTypeVS = "To" + uppercaseFirst(propertyType);
                    break;
            }
            return ConvertToTypeVS;
        }
        #endregion
    }
}
