namespace Tiveria.Common.Extensions
{
    /// <summary>Provides extension methods for trigonometric, logarithmic, and other common mathematical functions.</summary>
    public static partial class UnitConversionExtensions
    {

        #region Unit Conversions
        public static double Fahrenheit2Celsius(this double fahrenheit)
        {
            return ((fahrenheit - 32) * 5) / 9;
        }

        public static double Celsius2Fahrenheit(this double celcius)
        {
            return (celcius * 9) / 5 + 32;
        }

        public static double Mile2KM(this double mile)
        {
            return (mile * 1.609344);
        }

        public static double KM2Mile(this double km)
        {
            return (km / 1.609344);
        }

        public static double Inch2MM(this double inch)
        {
            return (inch * 25.4);
        }

        public static double mm2Inch(this double mm)
        {
            return (mm / 25.4);
        }
        #endregion 
    }
}