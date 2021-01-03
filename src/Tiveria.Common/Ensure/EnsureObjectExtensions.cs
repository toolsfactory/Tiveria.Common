using System.Diagnostics;

namespace Tiveria.Common
{
    public static class EnsureObjectExtensions
    {
        [DebuggerStepThrough]
        public static Param<T> IsNotNull<T>(this Param<T> param) 
        {
            if (param.Value == null)
                throw ExceptionFactory.CreateForParamNullValidation(param, EnsureRes.Ensure_IsNotNull);

            return param;
        }
    }
}