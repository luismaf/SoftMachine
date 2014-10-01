using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftMachine.Entities
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
       //private string _ForeignEntityInstantiationLines; //linea donde se instancian las entidades de la capa de datos, para llamar al metodo foraneo que cargue los datos de la entidad foranea contenida en la entidad correspondiente al método actual

       //private string _spParameters;
       //private string _spColumns;
       //private string _spValues;
       //private string _spOnDuplicateUpdate;
       //private string _spConditions;
       //private string _spWhere;
       private Rows _InParameters;
       private Rows _OutParameters;
       private bool _isForeign; //indeicates if the method is foreign.
       #endregion

        #region Properties
        public string dbName
        {
            get
            {
                string ParameterList = "";
                if (!this.MainMethod)
                {
                    ParameterList += "_by";
                    foreach (Entities.Row Parameter in this.InParameters)
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
                string ParameterVSname = "";
                if (this.isForeign) 
                {
                    foreach (Entities.Row Parameter in this._InParameters)
                    {
                        ParameterVSname += "By" + Utilities.Conversion.convertToEntityName(Parameter.dbName);
                    }
                }
                return this.Table.vsName + "." + this.Type.ToString() + ParameterVSname;
                //return Table.vsName + Utilities.Conversion.uppercaseFirst(this.Type.ToString());
            }
        }
        public Types Type { get; set; } //set on Inicialization
        public Entities.Table Table { get; set; } //set on Inicialization
        /// <summary>
        /// Comma Sparated Parameters used in VS Method definition.
        /// </summary>
        public string MethodParametersDefinition {get;set;}
        /// <summary>
        /// Comma Sparated Parameters used in VS Method Call.
        /// </summary>
        public string MethodParametersCall { get; set; }
        /// <summary>
        /// One line for each In Parameter, used in VS Method.
        /// </summary>
        public string MethodInParametersLines { get; set; }
        /// <summary>
        /// One line for each Out Parameter, used in VS Method.
        /// </summary>
        public string MethodOutParametersLines { get; set; }
        public string MethodReturnType { get; set; }
        public string ForeignEntityInstantiationLines{ get; set; }
        //linea donde se instancian las entidades de la capa de datos, para llamar al metodo foraneo que cargue los datos de la entidad foranea contenida en la entidad correspondiente al método actual
        //all In Parameters are PK
        public bool MainMethod { get; set; }
        public Entities.Rows InParameters //set on Inicialization
        {
            get
            {
                //processInParameters();
                return this._InParameters;
            }
            set
            {
                this._InParameters = value;

            }
        }
        public Entities.Rows OutParameters //set on Inicialization
        {
            get
            {
                return this._OutParameters;
            }
            set
            {
               this._OutParameters = value;
            }
        }
        public bool isForeign
        {
            get
            {
                return this._isForeign;
            }
        }

        //SP Properties:
        public string spParameters { get; set; }
        public string spColumns { get; set; }
        public string spValues { get; set; }
        public string spOnDuplicateUpdate { get; set; }
        public string spConditions { get; set; }
        public string spWhere { get; set; }
        #endregion

        #region Constructors
        public Method(Entities.Table oTable, Types oType,  Entities.Rows oParameters, bool isForeign)
        {
            this.Inicializar(oTable, oType, oParameters, isForeign);
        }
        #endregion

        #region Metods
        public void Inicializar(Entities.Table oTable, Types oType, Entities.Rows otherInParameters, bool isForeign)
        {

            //Inicialize Method Properties:
            this.MethodReturnType = "void";

            //inicialize SP Properties
            this.spParameters = string.Empty;
            this.spColumns = string.Empty;
            this.spValues = string.Empty;
            this.spOnDuplicateUpdate = string.Empty;
            this.spConditions = string.Empty;
            this.spWhere = string.Empty;

            this._InParameters = new Entities.Rows();
            this._OutParameters = new Entities.Rows();
            this.Table = oTable;
            this.Type = oType;
            this.loadParameters(otherInParameters);
            this._isForeign = isForeign;
        }

        private void loadParameters(Rows otherInParameters)
        {
           if (otherInParameters == null)
            {
                otherInParameters = new Entities.Rows();
                //establezco como parametro de entrada a todas las PK de la tabla.
                foreach (Entities.Row Parameter in this.Table.Rows)
                {
                    if (Parameter.isPK) //is a PK:
                    {
                        otherInParameters.Add(Parameter); //add PK parameters
                    }
                }
            }
            if (otherInParameters.Count > 0)
            {
                switch (this.Type)
                {
                    //List: Parametros de Entrada = PK | Parámetros de Salida = allColumns (executeReader)
                    case Types.List:
                        this.Summary = string.Format("\t\t /// Obtiene la Colección de {0}.", Table.vsName);
                        this._InParameters = otherInParameters; //add other parameters as PK
                        this._OutParameters =  this.Table.Rows; //los parametros de salida, siempre son todas las filas de la tabla.
                        processInParameters();
                        processOutParameters();
                        break;
                    //Update: Parametros de Entrada = allColumns | Parámetros de Salida = NULL (executeNoQuery)
                    case Types.Update:
                        this.Summary = string.Format("\t\t /// Inserta o Actualiza una instancia de la entidad {0}.", Table.vsName);
                        this._InParameters = this.Table.Rows; //add all paramters
                        processInParameters();

                        //set the comma separated MethodParametersDefinition Property, and use send/receive the entity as Item parameter.
                        this.MethodParametersDefinition = string.Format("Bussiness.Entities.{0} Item", this.Table.vsName); //+ "s";
                        this.MethodParametersCall = "Item";
                        //don't add any output parameter
                        //this._OutParameters = null;
                        break;
                    //Delete : Parametros de Entrada = PK | Parámetros de Salida = NULL (executeNoQuery)
                    case Types.Delete:
                        this.Summary = string.Format("\t\t /// Elimina un elemento de la entidad {0}.", Table.vsName);
                        this._InParameters = otherInParameters; //add other parameters as PK
                        processInParameters();
                        //don't add any output parameter
                        //this._OutParameters = null;
                        break;
                }
               
            }
        }
        private void processInParameters()
        {
            if (this._InParameters.Count > 0)
            {
                string MethodParamDefinition = string.Empty;
                string MethodParamCall = string.Empty;
                string MethodInParamLines = string.Empty;
                this.MethodParametersDefinition = string.Empty;
                this.MethodInParametersLines = string.Empty;
                //sp temp vars:
                string SPparameters = string.Empty;
                string SPcolumns = string.Empty;
                string SPvalues = string.Empty;
                string SPonDuplicateUpdate = string.Empty;
                string SPconditions = string.Empty;
                string SPwhere = string.Empty;

                foreach (Entities.Row Parameter in this._InParameters)
                {
                    //on 'update' type, the parameter is the entity, so check for that:
                    if (MethodParametersDefinition.Equals(string.Empty)) //then generate vsMethod Parameters.
                    {

                        MethodParamDefinition += Parameter.vsType + " " + Parameter.vsName + ", ";
                        MethodParamCall += Parameter.vsName + ", ";
                        //Add each Parameter to the SP: oCommand.Parameters.AddWithValue("@_variable", Variable)
                        MethodInParamLines += Environment.NewLine + string.Format("\t\t\t\t\t oCommand.Parameters.AddWithValue('@_{0}', {1});", Parameter.dbName, Parameter.vsName);
                    }
                    else //is an update method, and MethodParameter have been defined.
                    {
                        //get the values form the item:
                        MethodInParamLines += Environment.NewLine + string.Format("\t\t\t\t\t oCommand.Parameters.AddWithValue('@_{0}', Item.{1});", Parameter.dbName, Parameter.vsName);
                    }


                    //SP Vars:
                    if (Parameter.isPK || this.isForeign) //para evitar problemas con los parametros de entrada de los Stored Procedures.
                    {
                        //list/update/delete SP:
                        SPparameters += Environment.NewLine + "\t IN _" + Parameter.dbName + "\t" + Parameter.dbType + ", ";
                    }
                    //list SP:
                    SPconditions += " _" + Parameter.dbName + " IS NULL AND ";
                    //update SP:
                    SPcolumns += "`" + this.Table.dbName + "`" + "." + Parameter.dbName + ", " + Environment.NewLine + "\t\t";
                    SPvalues += "_" + Parameter.dbName + ", " + Environment.NewLine + "\t\t";
                    SPonDuplicateUpdate += "`" + Parameter.dbName + "`" + " = _" + Parameter.dbName + ", " + Environment.NewLine + "\t\t";
                    //delete SP:
                    SPwhere += "`" + Table.dbName + "`" + "." + Parameter.dbName + " = _" + Parameter.dbName + ", " + Environment.NewLine + "\t\t";
                }

                //remove the last ", " and return:
                this.MethodParametersDefinition = MethodParamDefinition.Substring(0, MethodParamDefinition.Length - 2);
                this.MethodParametersCall = MethodParamCall.Substring(0, MethodParamCall.Length - 2);
                this.MethodInParametersLines = MethodInParamLines;

                //sp Vars:
                if (!SPparameters.Equals(string.Empty)) this.spParameters = SPparameters.Substring(0, SPparameters.Length - 2);
                if (!SPconditions.Equals(string.Empty)) this.spConditions = SPconditions.Substring(0, SPconditions.Length - 4);
                if (!SPcolumns.Equals(string.Empty)) this.spColumns = SPcolumns.Substring(0, SPcolumns.Length - 6);
                if (!SPvalues.Equals(string.Empty)) this.spValues = SPvalues.Substring(0, SPvalues.Length - 8);
                if (!SPonDuplicateUpdate.Equals(string.Empty)) this.spOnDuplicateUpdate = SPonDuplicateUpdate.Substring(0, SPonDuplicateUpdate.Length - 6);
                if (!SPwhere.Equals(string.Empty)) this.spWhere = SPwhere.Substring(0, SPwhere.Length - 6);
            }
        }

        private void processOutParameters()
        {
            if (this._OutParameters.Count > 0)
            {
                //change the method return type form 'void' to:
                this.MethodReturnType = "Bussiness.Entities." + this.Table.vsName + "s";
                string ForeignEntityInstLines = string.Empty;
                string MethodOutParamLines = string.Empty;
                string ForeignEntityName = string.Empty;
                string SPcolumns = string.Empty;

                foreach (Entities.Row Parameter in this._OutParameters)
                {
                    //si el parametro NO tiene asignada una Tabla Foranea.
                    //if (Parameter.ForeignTable == null || Parameter.ForeignMethodName == null)
                    if (Parameter.ForeignMethodName == null)
                    {
                        MethodOutParamLines += Environment.NewLine + string.Format("\t\t\t\t\t\t\t o{0}.{1} = Convert.{2}(oReader['{3}']);", Table.vsName, Parameter.vsName, Utilities.Conversion.convertToConversionType(Parameter.vsType), Parameter.dbName);
                    } //si el parámetro tiene asignada una tabla foranea, y aún no ha sido procesado.
                    else if (! Parameter.ForeignMethodName.Equals(string.Empty))
                    {
                        //definimos el nombre de la entidad "foranea".
                        ForeignEntityName = Parameter.ForeignTable.vsName + (Parameter.ForeignTableIsCollection ? "s" : "");

                        //Creamos la linea donde se instancia la entidad/coleccion
                        ForeignEntityInstLines += Environment.NewLine + string.Format("\t\t\t\t\t Data.{0} o{1}Data = new Data.{0}.{1}();", Parameter.ForeignTable.vsName, ForeignEntityName);

                        MethodOutParamLines += Environment.NewLine + string.Format("\t\t\t\t\t\t\t //llamamos al metodo del objeto o{0}Data previamente instanciado y recuperamos sus datos:", Parameter.ForeignTable.vsName);
                        //Creamos la linea donde se definen los parametros:
                        MethodOutParamLines += Environment.NewLine + string.Format("\t\t\t\t\t\t\t o{0}.{1} = o{0}Data.{2} ({3});",
                            Table.vsName, ForeignEntityName, Parameter.ForeignMethodName, Parameter.vsForeignMethodParameters);
                        //oPaciente.Afiliados = oAfiliadoData.Listar((int)oPaciente.Id);

                        //por cada parametro asociado a esa tabla foranea, verificar si es clave primaria y agregarlo.
                        foreach (Entities.Row otherParameter in this._OutParameters)
                        {
                            if (otherParameter.ForeignMethodName != null && otherParameter.ForeignTable.vsName.Equals(Parameter.ForeignTable.vsName))
                            {
                                if (otherParameter.isPK)
                                {
                                    MethodOutParamLines += string.Format("\t\t\t\t\t\t\t o{0}.{1} = Convert.{2}(oReader['{3}']);", Table.vsName, Parameter.vsName, Utilities.Conversion.convertToConversionType(Parameter.vsType), Parameter.dbName);
                                }
                                //lo vacio para identificar a los parametros ya procesados.
                                otherParameter.ForeignMethodName = string.Empty; 
                            }
                        }
                    }
                    //List SP:
                    SPcolumns += "`" + this.Table.dbName + "`" + "." + Parameter.dbName + ", " + Environment.NewLine + "\t\t";
                }
                this.MethodOutParametersLines = MethodOutParamLines;
                this.ForeignEntityInstantiationLines = ForeignEntityInstLines;

                //sp Vars:
                if (!SPcolumns.Equals(string.Empty)) this.spColumns = SPcolumns.Substring(0, SPcolumns.Length - 6);
            }
        }
        #endregion


        public string Summary { get; set; }
    }
}
