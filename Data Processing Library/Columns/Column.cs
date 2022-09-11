namespace cvti.data.Columns
{
    using cvti.data.Functions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Predstavuje jeden stlpec z tabulky
    /// </summary>
    /// <remarks>
    /// Pouzite pri vyberoch dat a pri vytvarani podmienok
    /// </remarks>
    public abstract class Column : ICloneable
    {
        #region Public Properties

        /// <summary>
        /// Vrati, nastavi hodnotu hovoriacu ci datovy typ pre stlpec je numericky 
        /// </summary>
        public bool IsNumeric { get; set; }

        /// <summary>
        /// Vrati, nastavi meno stlpca v tabulke
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Vratim nastavi alias stlpca
        /// </summary>
        public string ColumnAlias { get; set; }

        /// <summary>
        /// Vrati meno tabulky ktorej stlpec patri
        /// </summary>
        public abstract string TableName { get; }

        /// <summary>
        /// Vrati, nastavi hodnotu urcujucu ci je stlpec viditelny pri vytvarani vystupov
        /// </summary>
        public bool IsVisible { get; set; } = true;

        /// <summary>
        /// Vrati funckie aplikovane na stlpec
        /// </summary>
        /// <value>
        /// Funkcie aplikovane na stlpec ako <see cref="IList{T}"/> kde T je <see cref="Function"/>
        /// </value>
        public IList<Function> Functions { get; } 
            = new List<Function>();

        #endregion

        #region Public Methods
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public Column AddFunction(Function f)
        {
            if (((f.IsAggregate || f.IsNumeric) && !IsNumeric) || !f.IsNumeric && IsNumeric)
                throw new ArgumentException();

            Functions.Add(f);
            FunctionAdded?.Invoke(this, f);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="f"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public Column InsertFunction(Function f, int index)
        {
            if (((f.IsAggregate || f.IsNumeric) && !IsNumeric) || !f.IsNumeric && IsNumeric)
                throw new ArgumentException();

            Functions.Insert(index, f);
            FunctionAdded?.Invoke(this, f);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public Column RemoveFunction(Function f)
        {
            Functions.Remove(f);
            FunctionRemoved?.Invoke(this, f);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Column RemoveFunctionAt(int index)
        {
            var f = Functions[index];
            Functions.RemoveAt(index);
            FunctionRemoved?.Invoke(this, f);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ContainsAggregateFunction()
        {
            return Functions.Any(f => f.IsAggregate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var stlpec = ColumnName;
            foreach (var f in Functions)
                stlpec = f.Apply(stlpec);

            return stlpec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return CloneMe(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deep"></param>
        /// <returns></returns>
        public abstract Column CloneMe(bool deep = true);

        #endregion

        #region Public Events

        /// <summary>
        /// Event signalizujuci pridanie novej funkcie pre stlpec ako <see cref="Function"/>
        /// </summary>
        public event EventHandler<Function> FunctionAdded;

        /// <summary>
        /// Event signalizujuci odobranie funkcie pre stlpec ako <see cref="Function"/>
        /// </summary>
        public event EventHandler<Function> FunctionRemoved;

        #endregion
    }
}
