using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniBillingServer.Model
{
    class BindingConfiguration : Configuration
    {
        public string Address
        {
            get;
            protected set;
        }

        public string Port
        {
            get;
            protected set;
        }


        public BindingConfiguration(string filename)
            : base(filename)
        {

        }

        protected override void LoadConfiguration()
        {
            Address = this.IniReadValue("BINDING", "Listen_Address");
            Port = this.IniReadValue("BINDING", "Listen_Port");
        }

    }
}
