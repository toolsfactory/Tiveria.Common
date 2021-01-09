using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiveria.Common.Extensions
{
    public static partial class UnitConversionExtensions
    {

        /// <summary>
        /// Convert preassure in inch of mercury to millibar
        /// </summary>
        /// <param name="inHg">preassure in inch of mercury</param>
        /// <returns>preassure in millibar</returns>
        public static double InHg2mBar(this double inHg)
        {
            return (inHg * 33.864);
        }

        /// <summary>
        /// Convert preassure in millibar to inch of mercury
        /// </summary>
        /// <param name="mBar">preassure in millibar</param>
        /// <returns>preassure in inch of mercury</returns>
        public static double MBar2InHg(this double mBar)
        {
            return (mBar / 33.864);
        }

        /// <summary>
        /// Convert preassure in millibar to Hectopascal
        /// </summary>
        /// <param name="mBar">preassure in millibar</param>
        /// <returns>hectopascal</returns>
        public static double MBar2hPa(this double mBar)
        {
            return mBar;
        }

        /// <summary>
        /// Convert preassure in millibar to (technical) Atmosphere
        /// </summary>
        /// <param name="mBar">preassure in millibar</param>
        /// <returns>atmosphere</returns>
        public static double MBar2AT(this double mBar)
        {
            return mBar * 0.0010197162129779282;
        }

        /// <summary>
        /// Convert preassure in millibar to (standard) Atmosphere
        /// </summary>
        /// <param name="mBar">preassure in millibar</param>
        /// <returns>atmosphere</returns>
        public static double MBar2ATM(this double mBar)
        {
            return mBar * 0.00098692316931427;
        }
    }
}
