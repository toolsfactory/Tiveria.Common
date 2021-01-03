using System.Diagnostics;

namespace Tiveria.Common
{
    public static class EnsureByteExtensions
    {
        [DebuggerStepThrough]
        public static Param<byte> IsLowerThan(this Param<byte> param, byte limit)
        {
            if (param.Value >= limit)
                throw ExceptionFactory.CreateForParamValidation(param, EnsureRes.Ensure_IsNotLt.Inject(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<byte> IsLowerOrEqual(this Param<byte> param, byte limit)
        {
            if (!(param.Value <= limit))
                throw ExceptionFactory.CreateForParamValidation(param, EnsureRes.Ensure_IsNotLte.Inject(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<byte> IsGreaterThan(this Param<byte> param, byte limit)
        {
            if (param.Value <= limit)
                throw ExceptionFactory.CreateForParamValidation(param, EnsureRes.Ensure_IsNotGt.Inject(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<byte> IsGreaterOrEqual(this Param<byte> param, byte limit)
        {
            if (!(param.Value >= limit))
                throw ExceptionFactory.CreateForParamValidation(param, EnsureRes.Ensure_IsNotGte.Inject(param.Value, limit));

            return param;
        }

        [DebuggerStepThrough]
        public static Param<byte> IsInRange(this Param<byte> param, byte min, byte max)
        {
            if (param.Value < min)
                throw ExceptionFactory.CreateForParamValidation(param, EnsureRes.Ensure_IsNotInRange_ToLow.Inject(param.Value, min));

            if (param.Value > max)
                throw ExceptionFactory.CreateForParamValidation(param, EnsureRes.Ensure_IsNotInRange_ToHigh.Inject(param.Value, max));

            return param;
        }
    }
}