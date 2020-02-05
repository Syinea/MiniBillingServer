using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace MiniBillingServer.Model
{
    abstract class Configuration
    {
        #region Imports
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key, string def, StringBuilder retVal,
            int size, string filePath);

        #endregion

        private string m_filename;

        /// <summary>
        /// Get or set the filename of the config file
        /// (Set will trigger a reload of all configuration values)
        /// </summary>
        public string Filename
        {
            get
            {
                return m_filename;
            }
            set
            {
                // Check if the config file exists
                if (!File.Exists(value))
                {
                    throw new FileNotFoundException(string.Format("Could not find {0}", value));
                }

                m_filename = value;

                this.LoadConfiguration();
            }
        }

        /// <summary>
        /// Load or reload the configuration from file
        /// </summary>
        /// <remarks>
        /// This method is called by the base (Model.Configuration). You don't need to
        /// call it in your code.
        /// </remarks>
        protected abstract void LoadConfiguration();

        public Configuration(string filename)
        {
            // Simply assign the filename, the setter of Filename will take care of everything.
            Filename = filename;
        }

        /// <summary>
        /// Write a value to an ini-file
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        protected void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.Filename);
        }

        /// <summary>
        /// Read a value from an ini file with a default value
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        protected string IniReadValue(string Section, string Key, string Default)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, Default, temp, 255, this.Filename);
            return temp.ToString();
        }

        /// <summary>
        /// Read a value from an ini file without a default value (defaults to "")
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        protected string IniReadValue(string Section, string Key)
        {
            return IniReadValue(Section, Key, "");
        }
    }
}
