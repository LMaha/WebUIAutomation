using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedUIDataEntities;

namespace Utilities
{
    public class WorkingDayCalculator
    {
        private List<holiday> holidays;
        private List<international_holidays> CANholidays;
        private PLS20Entities DataContext = new PLS20Entities();

        public WorkingDayCalculator(int futureYear, int year)
        {
            holidays = (from hl in DataContext.holidays where (hl.holiday_year == futureYear.ToString() && hl.floating == false) || hl.holiday_year == year.ToString() select hl).ToList();
			CANholidays = (from hl in DataContext.international_holidays where (hl.holiday_year == futureYear && hl.country_code == "CAN") || hl.holiday_year == year select hl).ToList();
        }
		public int GetWorkingDay(int n)
        {
            n = HolidayMonth(n);
            if (DateTime.Today.AddDays(n).DayOfWeek == DayOfWeek.Saturday)
            {
                n = n + 2;
                n = HolidayMonth(n);
            }
            else if (DateTime.Today.AddDays(n).DayOfWeek == DayOfWeek.Sunday)
            {
                n = n + 1;
                n = HolidayMonth(n);
            }

            return n;
        }

		public int GetWorkingDay(DateTime dt, int n, bool isCAN = false)
        {
            n = HolidayMonth(dt, n, isCAN);
            if (dt.AddDays(n).DayOfWeek == DayOfWeek.Saturday)
            {
				n = n + 2;
                n = HolidayMonth(dt, n, isCAN);
            }
            else if (dt.AddDays(n).DayOfWeek == DayOfWeek.Sunday)
            {
				if (dt.DayOfWeek == DayOfWeek.Saturday)
				{
					n = n + 1;
				}
				else
				{
					n = n + 2;
				}
                n = HolidayMonth(dt, n, isCAN);
            }

            return n;
        }

        public int HolidayMonth(int n)
        {
            var monthfound = holidays.Find(d => d.holiday_date.Month == DateTime.Today.AddDays(n).Month);
            if (monthfound != null)
            {
                if (DateTime.Today.AddDays(n).Date.Day == monthfound.holiday_date.Date.Day)
                {
                    n++;
                }
            }
            return n;
        }

		public int HolidayMonth(DateTime dt, int n, bool isCAN = false)
        {
            if (isCAN == false)
			{
				var monthfound = holidays.Find(d => d.holiday_date.Month == dt.AddDays(n).Month);
				if (monthfound != null)
				{
					if (dt.AddDays(n).Date.Day == monthfound.holiday_date.Date.Day)
					{
						n++;
					}
				}
			}
			else
			{
				var monthfound = CANholidays.Find(d => d.holiday_date.Month == dt.AddDays(n).Month);
				if (monthfound != null)
				{
					if (dt.AddDays(n).Date.Day == monthfound.holiday_date.Date.Day)
					{
						n++;
					}
				}
			}
            return n;
        }

		public int GetWorkingDeliveryDay(DateTime pickupDay, int numbDaysAfterPickupDay, bool isCAN = false)
		{
			return GetWorkingDay(pickupDay, numbDaysAfterPickupDay, isCAN);
		}
    }
}
