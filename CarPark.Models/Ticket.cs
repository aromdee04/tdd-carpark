using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPark.Models
{
    public class Ticket
    {
        public DateTime DateIn { get; set; }
        public DateTime? DateOut { get; set; }
        public decimal? ParkingFee {
            get
            {
                if (!DateOut.HasValue) return null;

                if (DateOut < DateIn) throw new Exception("Invalid DateOut < DateIn.");

                var TimeDiff = DateOut.Value.TimeOfDay - DateIn.TimeOfDay;

                decimal TotalMinutes = Convert.ToDecimal(TimeDiff.TotalMinutes);
                int NetHours = Convert.ToInt16(TotalMinutes / 60);

                if (TotalMinutes >= 195)
                {
                    return 50 + ((NetHours - 3) * 30) + ((((TotalMinutes - NetHours * 60) > 15) ? 1 : 0) * 30);
                }
                
                if (TotalMinutes > 15)
                    return 50m;
                else
                    return 0m;
            }
        }
        public string PlateNo { get; set; }
    }
}
