using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftMachine.Entities
{
    public class Table
    {
        #region Variables
        private string _dbName; //set on Inicialization
        private string _vsName; //set on Inicialization
        private int _pkCounter; //set on Inicialization
        //public bool _SinglePKtable; //set on Inicialization
        private Entities.Methods _Methods;
        private Entities.Rows _Rows;        
        #endregion

        #region Properties
        public Entities.Rows Rows
        {
            get
            {
                this.pkCounter = 0;
                bool singlePKtable = true;
                foreach (Entities.Row oRow in this._Rows)
                {
                    if (oRow.isPK) this.pkCounter++; //count the number of PK in the Table
                }
                if (this.pkCounter > 1) singlePKtable = false; //Check if is a table with only one PK (to set the MyTableNameId to Id)
                //set vsName acording to the number of PK of the table.
                foreach (Entities.Row oRow in this._Rows) 
                    oRow.vsName = Utilities.Conversion.convertToPropertyName(oRow.dbName, this.vsName, singlePKtable);
                return this._Rows;
            }
            set
            {
                this._Rows = value;
            }
        }
        public int pkCounter
        {
            get
            {
                return this._pkCounter;
            }
            set
            {
                this._pkCounter = value;
            }
        }

        public string dbName
        {
            get
            {
                return this._dbName;
            }
            set
            {
                this._dbName = value;
            }
        }
        public Entities.Methods Methods
        {
            get
            {
                return this._Methods;
            }
            set
            {
                this._Methods = value;
            }
        }
        public string vsName
        {
            get
            {
                return this._vsName;
            }
            set
            {
                this._vsName = value;
            }
        }

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
            this.Rows = new Entities.Rows();
            this.Methods = new Entities.Methods();
        }

        public void addMainMethods()
        {
            foreach (Entities.Method.Types methodType in Enum.GetValues(typeof(Entities.Method.Types)))
            {
                Entities.Method mainMethod = new Entities.Method(this, methodType, null, false);
                //mainMethod.Prepare();
                this.Methods.Add(mainMethod);
            }
        }
        public void addForeignMethods(Entities.Table ForeignTable, bool analizeOnly)
        {
            //foreach (Entities.Method.Types methodType in Enum.GetValues(typeof(Entities.Method.Types)))
            //{
            //    addForeignMethod(ForeignTable, methodType);
            //}
            addForeignMethod(ForeignTable, Entities.Method.Types.List, analizeOnly);
        }

        private void addForeignMethod(Entities.Table ForeignTable, Entities.Method.Types methodType, bool analizeOnly)
        {

            //bool tempBool = false;
            Entities.Rows ForeignInParameters = new Entities.Rows();

            foreach (Entities.Row thisRow in this._Rows)
            {
                foreach (Entities.Row foreignRow in ForeignTable.Rows)
                {
                    //Si la "Fila Foranea" es PK y se llama igual que la Fila "Actual", la agrega como parametro del método:
                    if (foreignRow.isPK && foreignRow.dbName == thisRow.dbName)
                    {
                        //si la Fila "Primaria" (a deferencia de la "Fila Foranea") no es PK:
                        if (thisRow.isPK == false)
                        {
                            thisRow.ForeignTable = ForeignTable; //indica que hay que reemplazar el parametro por la entidad foranea.
                            thisRow.ForeignTableIsCollection = false;  //indica que NO es una colección.
                            //tempBool = thisRow.isPK;
                            //thisRow.isPK = true; //para evitar problemas con los parametros de entrada de los Stored Procedures.
                            ForeignInParameters.Add(thisRow); //la agrego como parametro del método foráneo.
                            //thisRow.isPK = tempBool;
                            //if(this.dbName == ForeignTable.dbName) //es una Entidad Anidada dentro de la misma entidad.
                        }
                        //si la Fila "Primaria" (no foranea) tambien es PK, 
                        //y la "Tabla Foranea" tiene mas PK que la Tabla "Actual", la "Tabla Foranea" es coleccion de la Tabla "Actual".
                        else if (thisRow.isPK && ForeignTable.pkCounter < this.pkCounter)
                        {

                                thisRow.ForeignTable = ForeignTable; //indica que hay que reemplazar el parametro por la entidad foranea.
                                thisRow.ForeignTableIsCollection = true;  //indica que es una colección.
                                ForeignInParameters.Add(thisRow); //la agrego como parametro del método foráneo.
                                //if(this.dbName == ForeignTable.dbName) //es una Entidad Anidada dentro de la misma entidad.
                        }
                    }
                }
            }
            //la idea es que revise y agregue los parametros que coinciden y agregue la info del método.
            //el tema sería que no agregue un metodo distinto por cada coincidencia.
            if (ForeignInParameters.Count > 0 )
            {
                //Instanciamos el Metodo "Foraneo" y le agregamos la coleccion.
                Entities.Method newMethod = new Entities.Method(ForeignTable, methodType, ForeignInParameters, true);
                //newMethod.Prepare();
                if (!analizeOnly) this.Methods.Add(newMethod);
                //ForeignTable.RelatedTables = ForeignTable;
                foreach (Entities.Row thisRow in this.Rows)
                {
                    foreach (Entities.Row ForeignInParameter in ForeignInParameters)
                    {
                        //si los parametros coinciden, agrego la info del metodo foraneo dentro del parametro local, para luego poder llamar al metodo foraneo.
                        if (thisRow.dbName == ForeignInParameter.dbName)
                        {
                            //Agregamos la info del metodo dentro del parámetro.
                            thisRow.ForeignMethodName = newMethod.vsName;
                            thisRow.vsForeignMethodParameters = newMethod.MethodParametersDefinition;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
