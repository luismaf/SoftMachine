using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftMachine.Business.Entities
{
    public class Table
    {
        #region Variables
        private string _dbName; //set on Inicialization
        private string _vsName; //set on Inicialization
        private int _pkCounter; //set on Inicialization
        //public bool _SinglePKtable; //set on Inicialization
        private Business.Entities.Methods _Methods;
        private Business.Entities.Rows _Rows;        
        #endregion

        #region Properties
        public Business.Entities.Rows Rows
        {
            get
            {
                //ARREGLAR!!
                //this.pkCounter = 0; 
                //foreach (Business.Entities.Row oRow in this._Rows)
                //{
                //    if (oRow.isPK) //count the number of PK in the Table
                //        this.pkCounter++;
                //}
                 //Check if is a table with only one PK (to set the MyTableNameId to Id)
                //set vsName acording to the number of PK of the table.
                //foreach (Business.Entities.Row oRow in this._Rows) 
                //    oRow.vsName = Utilities.Conversion.convertToPropertyName(oRow.dbName, this.vsName, (this.pkCounter > 1));
                return this._Rows;
            }
            set
            {
                this._Rows = value;
            }
        }
        public Business.Entities.Rows pkParameters
        {
            get
            {
                Business.Entities.Rows _pkParameters = new Rows();
                
                //establezco como parametro PK a todas las PK de la tabla.
                foreach (Business.Entities.Row Parameter in this.Rows)
                {
                    if (Parameter.isPK) //is a PK:
                    {
                        _pkParameters.Add(Parameter); //add PK parameters
                    }
                }
                return _pkParameters;
            }
        }
        public Business.Entities.Methods Methods { get; set; }
        public Business.Entities.Relations Relations { get; set; }
        public int pkCounter {
            get
            {
                this.pkCounter = 0;
                foreach (Business.Entities.Row Parameter in this.Rows)
                {
                    if (Parameter.isPK) //is a PK:
                    {
                        this.pkCounter++;
                    }
                }
                return this.pkCounter;
            }
            set {
                this.pkCounter = value;
                
            }
        }
        public string dbName { get; set; }
        public string vsName { get; set; }

        #endregion

        #region Constructors
        public Table(string dbName)
        {
            this.Inicializar(dbName);
        } 
        #endregion

        #region Metods
        public void Inicializar(string dbName)
        {
            this.dbName = dbName.ToLowerInvariant();
            this.vsName = Utilities.Conversion.convertToEntityName(dbName);
            this.pkCounter = 0;
            this.Rows = new Business.Entities.Rows();
            this.Methods = new Business.Entities.Methods();
        }

        public void addMainMethods()
        {
            foreach (Business.Entities.Method.Types methodType in Enum.GetValues(typeof(Business.Entities.Method.Types)))
            {
                Business.Entities.Method mainMethod = new Business.Entities.Method(this, methodType);
                //mainMethod.Prepare();
                this.Methods.Add(mainMethod);
            }
        }
        #endregion
    }
}
