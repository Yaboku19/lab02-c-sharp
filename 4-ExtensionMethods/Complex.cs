namespace ExtensionMethods
{
    using System;

    /// <inheritdoc cref="IComplex"/>
    public class Complex : IComplex
    {
        private readonly double _re;
        private readonly double _im;

        /// <summary>
        /// Initializes a new instance of the <see cref="Complex"/> class.
        /// </summary>
        /// <param name="re">the real part.</param>
        /// <param name="im">the imaginary part.</param>
        public Complex(double re, double im)
        {
            this._re = re;
            this._im = im;
        }

        /// <inheritdoc cref="IComplex.Real"/>
        public double Real => _re;

        /// <inheritdoc cref="IComplex.Imaginary"/>
        public double Imaginary => _im;

        /// <inheritdoc cref="IComplex.Modulus"/>
        public double Modulus => Math.Sqrt(Real*Real + Imaginary*Imaginary);

        /// <inheritdoc cref="IComplex.Phase"/>
        public double Phase => Math.Atan2(_im, _re);

        /// <inheritdoc cref="IComplex.ToString"/>
        public override string ToString() => $"{Real} {(Imaginary >= 0 ? "+":"-")}{Math.Abs(Imaginary)}i";

        /// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
        public bool Equals(IComplex other)
        {
            return other.Real == this.Real && other.Imaginary == this.Imaginary;
        }

        /// <inheritdoc cref="object.Equals(object?)"/>
        public override bool Equals(object obj)
        {
            if (obj is Complex) return Equals(obj as Complex);
            return false;
        }

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Real, Imaginary);
        }
    }
}
