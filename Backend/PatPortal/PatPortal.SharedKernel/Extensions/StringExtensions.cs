using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.SharedKernel.Extensions
{
    public static class StringExtensions
    {
        public static string? FirstToUpper(this string value)
        {
            if (value == null) return null;
            switch (value.Length)
            {
                case 0: return value;
                case 1: return value.ToUpper();
                default:
                    return char.ToUpper(value[0]) + value.Substring(1).ToLower();
            }
        }

        public static Guid ParseToGuidOrEmpty(this string value)
        {
            Guid guid;
            var ableToParse = Guid.TryParse(value, out guid);
            if (!ableToParse) return Guid.Empty;

            return guid;
        }

        public static bool ParsebleToDateTime(this string value)
        {
            DateTime DateTime;
            return DateTime.TryParse(value, out DateTime);
        }

        public static TEnum ParseToEnumOrThrow<TEnum, TException>(this string value)
            where TEnum : struct
            where TException : Exception, new()
        {
            TEnum result;
            var ableToParse = Enum.TryParse<TEnum>(value, true, out result);

            if (!ableToParse)
            {
                var exception = (TException)Activator.CreateInstance(typeof(TException), $"Unable to parse {value};.");
                throw exception;
            }

            return result;
        }

        public static DateTime ParseToDateTime(this string value)
        {
            DateTime result;
            var parsable = DateTime.TryParse(value, out result);

            if(parsable)
                return result;

            return DateTime.MinValue;
        }
    }
}
