using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftMachine.Business.Entities
{
    public class Rows: List<Row>
    {
        #region Enumerators
        public enum Types
        {
            Row,
            In,
            Out
        }
        #endregion
        #region Properties
        /// <summary>
        /// Is storing Rows, In Parameters, or OutParameters.
        /// </summary>
        public Types Type { get; set; }

        /// <summary>
        /// Comma Sparated Parameters used in VS Method definition.
        /// </summary>
        public string Definition { get; set; }
        /// <summary>
        /// Comma Sparated Parameters used in VS Method Call.
        /// </summary>
        public string Call { get; set; }
        /// <summary>
        /// One line for each In Parameter, used in VS Method.
        /// </summary>
        public string InLines { get; set; }
        /// <summary>
        /// One line for each Out Parameter, used in VS Method.
        /// </summary>
        public string OutLines { get; set; }

        //SP Properties:
        public string spIn { get; set; }
        public string spList { get; set; }
        public string spValues { get; set; }
        public string spOnDuplicateUpdate { get; set; }
        public string spConditions { get; set; }
        public string spWhere { get; set; }
        public string ForeignEntityInstantiationLines { get; set; }
       
        #endregion

        #region Methods

        internal void processInList()
        {
            if (this.Count > 0)
            {
                this.InLines = string.Empty;

                this.Definition = string.Empty;
                this.Call = string.Empty;
                this.InLines = string.Empty;
                //string SubMethodCall = string.Empty; //call another method to update the contained items.
                //sp temp vars:
                this.spIn = string.Empty;
                this.spList = string.Empty;
                this.spValues = string.Empty;
                this.spOnDuplicateUpdate = string.Empty;
                this.spConditions = string.Empty;
                this.spWhere = string.Empty;
                this.Call = string.Empty;

                foreach (Business.Entities.Row Parameter in this)
                {
                    //set the parameters in the method definition.
                    this.Definition += Parameter.vsType + " " + Parameter.vsName + ", ";

                    if (Parameter.isMainParameter)
                    {
                        this.Call += Parameter.vsName + ", ";
                    }
                    else
                    {
                        this.Call += string.Format("Convert.{0}(oReader[\"{1}\"]), ", Utilities.Conversion.convertToConversionType(Parameter.vsType), Parameter.dbName);
                    }
                    //Add each Parameter to the SP: oCommand.Parameters.AddWithValue("@_variable", Variable)
                    this.InLines += Environment.NewLine + string.Format("\t\t\t\t\t oCommand.Parameters.AddWithValue(\"@_{0}\", {1});", Parameter.dbName, Parameter.vsName);

                    //remove the last ", " and return:
                    if (!this.Definition.Equals(string.Empty)) this.Definition = this.Definition.Substring(0, this.Definition.Length - 2);
                    if (!this.Call.Equals(string.Empty)) this.Call = this.Call.Substring(0, this.Call.Length - 2);

                    processInSP(Parameter);
                }
            }
        }
        internal void processUpdate()
        {
            if (this.Count > 0)
            {
                this.InLines = string.Empty;

                this.Definition = string.Empty;
                this.Call = string.Empty;
                this.InLines = string.Empty;
                //string SubMethodCall = string.Empty; //call another method to update the contained items.
                //sp temp vars:
                this.spIn = string.Empty;
                this.spList = string.Empty;
                this.spValues = string.Empty;
                this.spOnDuplicateUpdate = string.Empty;
                this.spConditions = string.Empty;
                this.spWhere = string.Empty;
                this.Call = string.Empty;

                foreach (Business.Entities.Row Parameter in this)
                {
                    //si es un Metodo Principal o no, pero es Clave Primaria de la Tabla Actual:
                    if (Parameter.Method == null || Parameter.isPK)
                    {
                        //get the values form the item:
                        this.InLines += Environment.NewLine + string.Format("\t\t\t\t\t oCommand.Parameters.AddWithValue(\"@_{0}\", Item.{1});", Parameter.Method.Relation.OtherTable.vsName, Parameter.vsName);
                    } //if is an entitiy:
                    if (Parameter.isMainParameter)  //no agrego el 'AddWithValue', invoco al metodo directamente:
                    {
                        this.InLines += Environment.NewLine + Environment.NewLine + string.Format("\t\t\t\t\t\t\t //Instanciamos la capa de datos de {0} y llamamos al metodo 'Update' del objeto o{0}Data:", Parameter.Method.Relation.OtherTable.vsName);
                        this.InLines += Environment.NewLine + string.Format("\t\t\t\t\t Data.Database.{0} o{0} = new Data.Database.{0}();", Parameter.Method.Relation.OtherTable.vsName);

                        // Si es una colección:
                        if (Parameter.Method.Relation.Type == Business.Entities.Relation.Types.Collection)
                        {
                            this.InLines += Environment.NewLine + string.Format("\t\t\t\t\t foreach (Business.Entities.{0} o{0} in Item.{1})", Parameter.Method.Relation.OtherTable.vsName, Parameter.vsName);
                            this.InLines += Environment.NewLine + "\t\t\t\t\t {";
                            this.InLines += Environment.NewLine + string.Format("\t\t\t\t\t\t o{0}Data.Update (o{0});", Parameter.Method.Relation.OtherTable.vsName);
                            this.InLines += Environment.NewLine + "\t\t\t\t\t }";
                            // Ejemplo:
                            // Data.Database.Transaction oDataTransaction = new Data.Database.Transaction();
                            // foreach (Business.Entities.Transaction oTransaction in item.Transactions)
                            // {
                            //      oDataTransaction.Update(oTransaction);
                            // }
                        }

                        else //Sino, si es una entidad:
                        {
                            this.InLines += Environment.NewLine + string.Format("\t\t\t\t\t\t o{0}Data.Update (o{0});", Parameter.Method.Relation.OtherTable.vsName);
                            // Ejemplo: 
                            // Data.Database.Transaction oDataTransaction = new Data.Database.Transaction();
                            // Transaction.Update(item.Transaction);    
                        }
                    }

                    //remove the last ", " and return:
                    if (!this.Definition.Equals(string.Empty)) this.Definition = this.Definition.Substring(0, this.Definition.Length - 2);
                    if (!this.Call.Equals(string.Empty)) this.Call = this.Call.Substring(0, this.Call.Length - 2);
                    processInSP(Parameter);
                }
            }
        }

        internal void processInSP(Business.Entities.Row Parameter)
        {
            //SP Vars:
            //Si el parametro es PK o se trata de un metodo foraneo:
            if (Parameter.isPK || Parameter.Method != null) //para evitar problemas con los parametros de entrada de los Stored Procedures.
            {
                //list/update/delete SP:
                this.spIn += Environment.NewLine + "\t IN _" + Parameter.dbName + "\t" + Parameter.dbType + ", ";
            }
            this.spConditions += " _" + Parameter.dbName + " IS NULL AND ";
            //update SP:
            //this.spList += "`" + this.Table.dbName + "`" + "." + Parameter.dbName + ", " + Environment.NewLine + "\t\t";
            this.spList += Parameter.dbName + ", " + Environment.NewLine + "\t\t";
            this.spValues += "_" + Parameter.dbName + ", " + Environment.NewLine + "\t\t";
            this.spOnDuplicateUpdate += "`" + Parameter.dbName + "`" + " = _" + Parameter.dbName + ", " + Environment.NewLine + "\t\t";
            //delete SP:
            //this.spWhere += "`" + Table.dbName + "`" + "." + Parameter.dbName + " = _" + Parameter.dbName + ", " + Environment.NewLine + "\t\t";
            this.spWhere += Parameter.dbName + " = _" + Parameter.dbName + ", " + Environment.NewLine + "\t\t";

            //sp Vars:
            if (!this.spIn.Equals(string.Empty)) this.spIn = this.spIn.Substring(0, this.spIn.Length - 2);
            if (!this.spConditions.Equals(string.Empty)) this.spConditions = this.spConditions.Substring(0, this.spConditions.Length - 4);
            if (!this.spList.Equals(string.Empty)) this.spList = this.spList.Substring(0, this.spList.Length - 6);
            if (!this.spValues.Equals(string.Empty)) this.spValues = this.spValues.Substring(0, this.spValues.Length - 8);
            if (!this.spOnDuplicateUpdate.Equals(string.Empty)) this.spOnDuplicateUpdate = this.spOnDuplicateUpdate.Substring(0, this.spOnDuplicateUpdate.Length - 6);
            if (!this.spWhere.Equals(string.Empty)) this.spWhere = this.spWhere.Substring(0, this.spWhere.Length - 6);
        }



        internal void processOutList()
        {
            if (this.Count > 0)
            {
                //change the method return type form \"void\" to:
                string ForeignEntityInstLines = string.Empty;
                string MethodOutParamLines = string.Empty;
                string spList = string.Empty;
                foreach (Business.Entities.Row Parameter in this)
                {
                    //if (Parameter.isMainParameter) //if (Parameter.Method != null) // hay un metodo relacionado.
                    //si el parametro NO tiene asignada una Tabla Foranea.
                    //si el parámetro es de un metodo Principal, o NO es de un metodo Principal pero es clave primaria, entonces: hay que agregarlo.
                    if (Parameter.isMainParameter || Parameter.isPK) 
                    {
                        MethodOutParamLines += Environment.NewLine + string.Format("\t\t\t\t\t\t\t o{0}.{1} = Convert.{2}(oReader[\"{3}\"]);", Parameter.Method.Relation.InitialTable.vsName, Parameter.vsName, Utilities.Conversion.convertToConversionType(Parameter.vsType), Parameter.dbName);
                    } //si el parámetro tiene asignada una tabla foranea, y aún no ha sido procesado.
                    if (! Parameter.isMainParameter) 
                    {
                        //Creamos la linea donde se instancia la entidad/coleccion
                        ForeignEntityInstLines += Environment.NewLine + string.Format("\t\t\t\t\t Data.Database.{0} o{0}Data = new Data.Database.{0}();", Parameter.Method.Relation.OtherTable.vsName);
                        //ForeignEntityInstLines += Environment.NewLine + string.Format("\t\t\t\t\t Data.Database.{0} o{1}Data = new Data.Database.{0}.{1}();", Parameter.otherTable.vsName, Parameter.ForeignEntityName);

                        MethodOutParamLines += Environment.NewLine + Environment.NewLine + string.Format("\t\t\t\t\t\t\t //llamamos al metodo del objeto o{0}Data previamente instanciado y recuperamos sus datos:", Parameter.Method.Relation.OtherTable.vsName);
                        //Creamos la linea donde se definen los parametros:
                        MethodOutParamLines += Environment.NewLine + string.Format("\t\t\t\t\t\t\t o{0}.{1} = o{2}Data.{3} ({4});",
                            Parameter.Method.Relation.InitialTable.vsName, Parameter.Method.Relation.OtherEntitiyName, Parameter.Method.Relation.OtherTable.vsName, Parameter.Method.vsName, Parameter.ForeignMethodParametersCall);
                        //oDocument.Transaction = oTransactionData.ListByTransactionId (Convert.ToInt32(oReader["transaction_id"]));

                        //oPaciente.Afiliados = oAfiliadoData.Database.Listar((int)oPaciente.Id);

                        //por cada parametro asociado a esa tabla foranea, verificar si es clave primaria y agregarlo.
                        foreach (Business.Entities.Row otherParameter in this)
                        {
                            if (otherParameter.Method.vsName != null && otherParameter.Method.Relation.OtherTable.vsName.Equals(otherParameter.Method.Relation.OtherTable.vsName))
                            {
                                if (otherParameter.isPK)
                                {
                                    MethodOutParamLines += Environment.NewLine + string.Format("\t\t\t\t\t\t\t o{0}.{1} = Convert.{2}(oReader[\"{3}\"]);", otherParameter.Method.Relation.InitialTable.vsName, otherParameter.vsName, Utilities.Conversion.convertToConversionType(otherParameter.vsType), otherParameter.dbName);
                                    //MethodOutParamLines += string.Format("\t\t\t\t\t\t\t o{0}.{1} = Convert.{2}(oReader[\"{3}\"]);", Table.vsName, Parameter.vsName, Utilities.Conversion.convertToConversionType(Parameter.vsType), Parameter.dbName);
                                }
                                //lo dentifico como parametro ya procesado.
                                //otherParameter.AlreadyProcessed = true; 
                            }
                        }
                    }
                    //List SP:
                    this.spList += Parameter.dbName + ", " + Environment.NewLine + "\t\t";
                }
                this.OutLines = MethodOutParamLines;

                //sp Vars:
                if (!this.spList.Equals(string.Empty)) this.spList = this.spList.Substring(0, this.spList.Length - 6);
            }
        }
        #endregion


        //foreach (Business.Entities.Method oMethod in this.ToList())

    }
}
