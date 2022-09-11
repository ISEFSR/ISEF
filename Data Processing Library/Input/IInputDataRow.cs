namespace cvti.data.Input
{
    /// <summary>
    /// zakladne info, ktore sa musi nachadzat v kazdom importnom subore
    /// </summary>
    /// <remarks>
    /// zakladne info, ktore sa musi nachadzat v kazdom importnom subore
    /// </remarks>
    public interface IInputDataRow
    {
        string GetInsert(int rok);

        string Fk { get;  }
        string Ek { get;  }
        string Zk { get;  }
        string Pk { get;  }

        /// <summary>
        /// Vrati ico ako unikatny identifikator organizacie
        /// </summary>
        string Ico { get; }

        /// <summary>
        /// Vrati schvaleny rozpocet
        /// </summary>
        decimal Rozpp { get; }
        /// <summary>
        /// Vrati upraveny rozpocet
        /// </summary>
        decimal Rozpu { get; }
        /// <summary>
        /// Vrati skutocnost
        /// </summary>
        decimal Skut { get; }
    }
}
