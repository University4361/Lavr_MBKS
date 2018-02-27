using System.Collections.Generic;
using System.Linq;

namespace Lavrov_lab2
{
    public class AccessUser
    {
        private string _template;
        public Dictionary<char, byte> AccessDictionary;

        public AccessUser(string accessString, string template)
        {
            InitializeDictionary(accessString);
            _template = template;
        }

        private void InitializeDictionary(string accessString)
        {
            AccessDictionary = new Dictionary<char, byte>();

            for (int i = 0; i < _template.Count(); i++)
            {
                bool result = false;
                byte accessPoint = 0;

                if (i <= accessString.Length - 1)
                    result = byte.TryParse(accessString[i].ToString(), out accessPoint);

                AccessDictionary.Add(_template[i], accessPoint);

            }
        }
    }
}
