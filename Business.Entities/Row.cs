using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftMachine.Business.Entities
{
    public class Enumerator {
        public string Value {get; set;}
        public Enumerator(string _Value)
        {
            this.Value = _Value;
        }
    }
    public class Enumerators : List<Enumerator> {}

    public class Row
    {
        #region Variables
        private string _dbName;
        private string _dbType;
        #endregion

        #region Properties
        
        public string dbName
        {
            get
            {
                return _dbName;
            }
            set
            {
                this._dbName = value.ToLowerInvariant();
                this.UpdateVSName(); // (dbName --> vsName) Actualiza el Nombre de parametro de Visual Studio, a partir del parametro dbName. 

            }
        }
        public string vsName
        {
            get
            {
                return Utilities.Conversion.convertToPropertyName(this.dbName, this.vsName, (this.Table.pkCounter > 1));
            }
        }
        public string dbType
        {
            get
            {
                return _dbType;
            }
            set
            {
                this._dbType = value;
                this.UpdateVSType(); // (dbType --> vsType) Actualiza el Tipo de parametro de Visual Studio, a partir del parametro dbType.
            }
        }
        public string vsType { get; set; }
        public Business.Entities.Method Method { get; set; }
        public Business.Entities.Enumerators ParameterDatails; //lista de enumeradores, solo se utiliza si el tipo de parametro es 'enum'
        //public string dbKey { get; set; }
        //public string dbExtra { get; set; }
        public bool isPK { get; set; }
        public Business.Entities.Table Table  { get; set; } //set on Inicialization
        public string º { get; set; }
        public string ForeignMethodParametersCall { get; set; }
        public bool AlreadyProcessed { get; set; } //indicates if this parameter has been taked in care by the method to add foraign entitites.
        public bool isAutoIncremental { get; set; }
        /// <summary>
        /// Indicates is the Parameter that represent the Relation Method (foreign method).  Parameter == this.Method.Relation.MainParameter)
        /// </summary>
        public bool isMainParameter {
            get
            {

                return (this.Method != null && this == this.Method.Relation.MainParameter) ? true : false;
            }
        }
        #endregion

        #region Constructors

        public Row(Business.Entities.Table oTable)
        {
            this.Inicializar(oTable);
        } 
        #endregion

        #region Metods

        public void Inicializar(Business.Entities.Table oTable)
        {
            this.ParameterDatails = new Enumerators();
            this.Method = null; //new Method();
            this.Table = oTable;
            this.dbName = string.Empty;
            this.dbType = string.Empty;
            //this.dbKey = string.Empty;
            //this.dbExtra = string.Empty;
            //this.vsName = string.Empty;
            this.vsType = string.Empty;
            //this.ForeignEntityName = string.Empty;
        }
        /// <summary>
        /// De acuerdo al dbName (DB), obtiene su equivalente en Visual Studio. (convert dbName to vsName)
        /// </summary>
        private void UpdateVSName() //actualiza los nombres y tipos de Visual Studio. (/ convert to VS type/ etc)
        {
            //convertToVSType();
        }
        /// <summary>
        /// De acuerdo al dbType (DB), obtiene su equivalente en Visual Studio. (convert dbType to vsType)
        /// </summary>
        private void UpdateVSType()
        {
            string ParameterType = this.dbType.Split('(')[0];
            //Detalles del Parámetro: tamaño/lista de enum.
            string[] ParameterDetails = new string[this.dbType.Replace('\'', ' ').Trim().Split(',').Count()];  //string[] ParameterSize = new string[50];
            ParameterDetails = this.dbType.Replace('\'', ' ').Trim().Split(',');

            switch (ParameterType)
            {
                case "VARCHAR":
                    this.vsType = "string"; //si uso toString, luego no lo puedo usar para crear la entidad.
                    break;
                case "INT":
                    this.vsType = "int";
                    if (this.isAutoIncremental)
                        this.vsType = string.Format("Nullable<{0}>", this.vsType);
                    break;
                case "MEDIUMINT":
                    this.vsType = "int";
                    break;
                case "SMALLINT":
                    this.vsType = "int";
                    break;
                case "TINYINT":
                    this.vsType = "int";
                    break;
                case "DECIMAL":
                    this.vsType = "decimal";
                    break;
                case "DOUBLE":
                    this.vsType = "decimal";
                    break;
                case "DATE":
                    this.vsType = "DateTime";//VER!
                    break;
                case "DATETIME":
                    this.vsType = "datetime";//VER!
                    break;
                case "ENUM":
                    this.vsType = "enum";
                    foreach (string ParameterDetail in ParameterDetails)
                    {
                        Business.Entities.Enumerator oParameterDetail = new Business.Entities.Enumerator(ParameterDetail);
                        this.ParameterDatails.Add(oParameterDetail);
                    }
                    break;
                case "TEXT":
                    this.vsType = "text";
                    break;
                case "BLOB":
                    this.vsType = "binary"; //VER!
                    break;
                default:
                    this.vsType = "string"; //si uso toString, luego no lo puedo usar para crear la entidad.
                    break;
            }
        }

        #endregion

    }
}
