﻿using System.Collections.Generic;
using System.Linq;

namespace Lavrov_lab2
{
    public class AccessUser
    {
        private const string _template = "abcdefghijklmnopqrstuvwxyz";
        public Dictionary<char, byte> AccessDictionary;

        public AccessUser(string accessString)
        {
            InitializeDictionary(accessString);
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
