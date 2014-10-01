using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftMachine.Business.Entities
{
    public class Relation
    {
        #region Enum
        public enum Types
        {
            Entity,
            Collection
        }
        #endregion
        #region Properties
        public Table InitialTable {get; set;}
        public Table OtherTable {get; set;}
        public Types Type {get; set;}
        public Rows Parameters { get; set; }
        public Row MainParameter { get; set; }
        /// <summary>
        /// return OtherTable.vsName, but if is a collection adds an 's'.
        /// </summary>
        public string OtherEntitiyName
        {
            get
            {
                return OtherTable.vsName + (this.Type == Business.Entities.Relation.Types.Collection ? "s" : string.Empty);
            }
        }
        #endregion
        #region Constructor
        public Relation(Business.Entities.Table _InitialTable, Business.Entities.Table _OtherTable, Types _Type, Business.Entities.Rows _Parameters)
        {
            this.Inicialize(_InitialTable, _OtherTable, _Type, _Parameters);
        }
        public Relation()
        {
            this.Inicialize();
        }
        #endregion

        #region Methods
        public void Inicialize(Business.Entities.Table _InitialTable, Business.Entities.Table _OtherTable, Types _Type, Business.Entities.Rows _Parameters)
        {
            this.InitialTable = _InitialTable;
            this.OtherTable = _OtherTable;
            this.Type = _Type;
            this.Parameters = _Parameters;
        }
        public void Inicialize()
        {
            this.Parameters = new Rows();
        }
        #endregion
    }
}
