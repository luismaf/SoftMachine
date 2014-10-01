using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftMachine.Business.Entities
{
    public class Method
    {
       #region Enum
       public enum Types
        {
            List = 0,
            Update = 1,
            Delete = 2,
        }
       #endregion

       #region Variables
       #endregion

        #region Properties

        public string dbName
        {
            get
            {
                string ParameterList = string.Empty;
                if (!this.isMain) 
                {
                    ParameterList += "_by";
                    foreach (Business.Entities.Row Parameter in this.InParameters)
                    {
                        ParameterList += "_" + Parameter.dbName;
                    }
                }
                return Table.dbName + "_" + this.Type.ToString().ToLowerInvariant();
            }
        }
        public string vsName
        {
            get
            {
                string ParameterVSname = string.Empty;
                if (! this.isMain) 
                {
                    foreach (Business.Entities.Row Parameter in this.InParameters)
                    {
                        ParameterVSname += "By" + Utilities.Conversion.convertToEntityName(Parameter.dbName);
                    }
                }
                return this.Table.vsName + "." + this.Type.ToString() + ParameterVSname;
                //return Table.vsName + Utilities.Conversion.uppercaseFirst(this.Type.ToString());
            }
        }
        public Types Type { get; set; } //set on Inicialization
        public Business.Entities.Table Table { get; set; } //set on Inicialization
        public Business.Entities.Rows InParameters { get; set; } //set on Inicialization
        public Business.Entities.Rows OutParameters { get; set; } //set on Inicialization
        public Business.Entities.Relation Relation { get; set; }
        /// <summary>
        /// to add an automatic comment about the method.
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// Indeicates if the method is a "Main" Method (not foreign) --> this.Relation == null
        /// </summary>
        public bool isMain
        {
            get
            {
                return (this.Relation == null)? true : false;
            }
        }
        public string MethodReturnType { get; set; }
        #endregion

        #region Constructors
        //Constructor for a "Main Method"
        public Method(Business.Entities.Table oTable, Types oType)
        {
            this.Inicializar(oTable, oType);
        }
        //Constructor for a "Relation Method"
        public Method(Relation _Relation)
        {
            // TODO: Complete member initialization
            this.Relation = _Relation;
        }
        #endregion
        //inicialized by a Relation -> "Relation Method"
        public void Inicializar(Business.Entities.Table oTable, Business.Entities.Relation _Relation)
        {
            this.Type = Business.Entities.Method.Types.List;
            this.Table = oTable;
            //this.loadParameters();
        }

        #region Metods
        //inicialized by a Table -> "Main Method"
        public void Inicializar(Business.Entities.Table oTable, Types oType)
        {
            //Inicialize Method Properties:
            this.MethodReturnType = "void";
            this.InParameters.Definition = string.Empty;

            //inicialize SP Properties
            this.InParameters = new Business.Entities.Rows();
            this.OutParameters = new Business.Entities.Rows();
            this.Table = oTable;
            this.Type = oType;
            this.loadParameters();
        }

        private void loadParameters()
        {
            switch (this.Type)
            {
                //List: Parametros de Entrada = PK | Parámetros de Salida = allParameters (executeReader)
                case Types.List: generateListMethod();
                    break;
                //Update: Parametros de Entrada = allParameters | Parámetros de Salida = NULL (executeNoQuery)
                case Types.Update: generateUpdateMethod();
                    break;
                //Delete : Parametros de Entrada = PK | Parámetros de Salida = NULL (executeNoQuery)
                case Types.Delete: generateDeleteMethod();
                    break;
            }
        }
        private void generateListMethod()
        {
            this.Summary = string.Format("\t\t /// Obtiene la Colección de {0}.", Table.vsName);
            this.MethodReturnType = "Business.Entities." + this.Table.vsName + "s";
            //set parameters:
            this.InParameters = (this.Relation == null) ? this.Table.pkParameters : this.Relation.Parameters; //if there is no relation, it's an standard method, add the PK
            this.OutParameters = this.Table.Rows; //los parametros de salida, siempre son todas las filas de la tabla.
            this.InParameters.processInList();
            this.OutParameters.processOutList();
        }

        private void generateUpdateMethod()
        {
            this.Summary = string.Format("\t\t /// Inserta o Actualiza una instancia de la entidad {0}.", Table.vsName);
            this.InParameters = this.Table.Rows; //add all paramters

            //set the comma separated MethodParametersDefinition Property, and use send/receive the entity as Item parameter.
            this.InParameters.Definition = string.Format("Business.Entities.{0} Item", this.Table.vsName); //+ "s";
            this.InParameters.Call = "Item";
            this.InParameters.processUpdate();
            //don't add any output parameter
        }

        private void generateDeleteMethod()
        {
            this.Summary = string.Format("\t\t /// Elimina un elemento de la entidad {0}.", Table.vsName);
            this.InParameters = (this.Relation == null) ? this.Table.pkParameters : Relation.Parameters; //if there is no relation, it's an standar method, add the PK
            this.InParameters.processInList();
            //don't add any output parameter
        }
        #endregion
    }
}
