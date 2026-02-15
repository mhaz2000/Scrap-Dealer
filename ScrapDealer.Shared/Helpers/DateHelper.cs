using System.Globalization;

namespace ScrapDealer.Shared.Helpers
{
    public static class DateHelper
    {
        public static DateTime ToDate(this string date, bool isStart)
        {
            var pc = new PersianCalendar();
            var splitDate = date.Split("/");

            if (int.Parse(splitDate[0]) < 2000)
                return isStart ? new DateTime(int.Parse(splitDate[0]), int.Parse(splitDate[1]), int.Parse(splitDate[2]), 0, 0, 0, pc)
                : new DateTime(int.Parse(splitDate[0]), int.Parse(splitDate[1]), int.Parse(splitDate[2]), 23, 59, 59, pc);
            else
                return isStart ? new DateTime(int.Parse(splitDate[0]), int.Parse(splitDate[1]), int.Parse(splitDate[2]), 0, 0, 0)
                    : new DateTime(int.Parse(splitDate[0]), int.Parse(splitDate[1]), int.Parse(splitDate[2]), 23, 59, 59);
        }
    }
}
