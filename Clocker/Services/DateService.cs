using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clocker.Interfaces;

namespace Clocker.Services
{
    class DateTimeService : IDateTimeService
    {
        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
