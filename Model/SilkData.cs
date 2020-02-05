using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniBillingServer.Model
{
    class SilkData
    {
        public int SilkOwn = 0;
        public int SilkGift = 0;
        public int Mileage = 0;

        public SilkData(int SilkOwn, int SilkGift, int Mileage)
        {
            this.SilkOwn = SilkOwn;
            this.SilkGift = SilkGift;
            this.Mileage = Mileage;
        }

    }
}
