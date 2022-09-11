namespace cvti.data.Input
{
    using System;

    /// <summary>
    /// Predstavuje predpis pre riadok zo vstupnych dat
    /// </summary>
    public abstract class InputDataRow : IInputDataRow
    {
        public abstract string Fk { get; }
        public abstract string Ek { get; }
        public abstract string Zk { get; }
        public abstract string Pk { get; }

        public abstract string Ico { get; set; }

        public abstract decimal Rozpp { get; set; }
        public abstract decimal Rozpu { get; set; }
        public abstract decimal Skut { get; set; }

        public virtual void Validate()
        {
            if (Fk.Length > 5) throw new ArgumentException("Funkčná klasifikácia nemôže mať viac ako 5 znakov " + Fk);
            if (Ek.Length > 6) throw new ArgumentException("Ekonomická klasifikácia nemôže mať viac ako 6 znakov " + Ek);
            if (Zk.Length > 4) throw new ArgumentException("Zdrojová klasifikácia nemôže mať viac ako 4 znakov " + Zk);
            if (Pk.Length > 7) throw new ArgumentException("Programová klasifikácia nemôže mať viac ako 7 znakov " + Pk);
        }

        public abstract string GetInsert(int rok);

        public override string ToString() => $"ICO: {Ico}, Ae: {Fk}{Ek}{Zk}{Pk}, Skut: {Skut}, Rozpp: {Rozpp}, Rozpu: {Rozpu}";
    }
}
