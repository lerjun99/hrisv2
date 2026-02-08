using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Common.Helpers
{
    public static class PhilippineTime
    {
        private static readonly TimeZoneInfo _phTimeZone =
            TimeZoneInfo.FindSystemTimeZoneById("Asia/Manila");

        /// <summary>
        /// Gets the current Philippine time
        /// </summary>
        public static DateTime Now =>
            TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _phTimeZone);

        /// <summary>
        /// Converts any UTC DateTime to Philippine time
        /// </summary>
        public static DateTime FromUtc(DateTime utcDateTime) =>
            TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, _phTimeZone);

        /// <summary>
        /// Converts Philippine time to UTC
        /// </summary>
        public static DateTime ToUtc(DateTime philippineDateTime) =>
            TimeZoneInfo.ConvertTimeToUtc(philippineDateTime, _phTimeZone);
    }

}
