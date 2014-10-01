using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftMachine.Entities
{

    public class Row
    {
        #region Variables
        private string _dbName;
        private string _dbType;
        private string _dbKey;
        private string _dbExtra;
        private string _vsName;
        private string _vsType;
        //private string _vsForeignMethodName;
        ///private string _vsForeignMethodParameters;
        private Entities.Table _Table; //set on Inicialization
        private Entities.Table _ForeignTable;//used to set the table/collection asociated to the parameter.
        private bool _ForeignTableIsCollection; //used to keep the collection asociated to a parameter.
        private string _vsForeignMethodName;
        private string _vsForeignMethodParameters;
        #endregion

        #region Properties
        public string ForeignMethodName
        {
            get
            {
                return _vsForeignMethodName;
            }
            set
            {
                this._vsForeignMethodName = value;
            }
        }
        public string vsForeignMethodParameters
        {
            get
            {
                return _vsForeignMethodParameters;
            }
            set
            {
                this._vsForeignMethodParameters = value;
            }
        }
        
        public string dbName
        {
            get
            {
                return _dbName;
            }
            set
            {
                this._dbName = value.ToLowerInvariant();
                //this._vsName -> se carga al cargar la tabla
            }
        }
        public string vsName
        {
            get
            {
                return _vsName;
            }
            set
            {
                this._vsName = value;
                //this._vsName -> se carga al cargar la tabla
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
                this.vsType = Utilities.Conversion.convertToPropertyType(this.dbType);
            }
        }
        public string dbKey
        {
            get
            {
                return _dbKey;
            }
            set
            {
                this._dbKey = value;
            }
        }
        public string dbExtra
        {
            get
            {
                return _dbExtra;
            }
            set
            {
                this._dbExtra = value;
            }
        }
        public string vsType
        {
            get
            {
                return _vsType;
            }
            set
            {
                this._vsType = value;
            }
        }
        public Entities.Table Table //set on Inicialization
        {
            get
            {
                return _Table;
            }
            set
            {
                this._Table = value;
            }
        }
        public Entities.Table ForeignTable //used to set the table/collection asociated to the parameter.
        {
            get
            {
                return _ForeignTable;
            }
            set
            {
                this._ForeignTable = value;
            }
        }
        public bool ForeignTableIsCollection //used to keep the collection asociated to a parameter.
        {
            get
            {
                return _ForeignTableIsCollection;
            }
            set
            {
                this._ForeignTableIsCollection = value;
            }
        }
        public bool isPK
        {
            get
            {
                return (this.dbKey=="PRI")? true : false;
            }
        }
        #endregion

        #region Constructors
        public Row(Entities.Table oTable)
        {
            this.Inicializar(oTable);
        } 
        #endregion

        #region Metods
        public void Inicializar(Entities.Table oTable)
        {
            this.Table = oTable;
            this.ForeignTableIsCollection = false;
            //this.ForeignTable = null;
        }
        #endregion
    }
}
