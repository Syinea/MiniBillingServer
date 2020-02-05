using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniBillingServer.Model
{
    class DatabaseConfiguration : Configuration
    {
        public String Host
        {
            get;
            protected set;
        }

        public String Database
        {
            get;
            protected set;
        }

        public String Username
        {
            get;
            protected set;
        }

        public String Password
        {
            get;
            protected set;
        }

        public DatabaseConfiguration(string filename)
            : base(filename)
        {

        }

        protected override void LoadConfiguration()
        {
            Host = IniReadValue("DATABASE", "HOST_DB");
            Username = IniReadValue("DATABASE", "USER_DB");
            Password = IniReadValue("DATABASE", "PASS_DB");
            Database = IniReadValue("DATABASE", "ACC_DB");
        }

    }
}
