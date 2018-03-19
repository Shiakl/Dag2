using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{

    public class Class1
    {
        /// <summary>
        /// Returns the length of the given string
        /// </summary>
        /// <param name="input">Accepts string input</param>
        /// <returns>Return character length value</returns>
        public int StringLength(string input)
        {
            int value = StringLength(input);
            return value;
        }

        /// <summary>
        /// Returns true if the given number is negative
        /// </summary>
        /// <param name="number">Accepts Integer value</param>
        /// <returns>Return True or false If negative value</returns>
        public bool IsNegative(int number)
        {
            if(number < 0)
            {
                bool state = true;
                return state;
            } else
            {
                bool state = false;
                return state;
            }
        }

        /// <summary>
        /// Returns "Welcome" + name
        /// </summary>
        /// <param name="name">Accepts characters in string value</param>
        /// <returns>returns "Welcome " + name </returns>
        public string Welcome(string name)
        {
            name = "Welcome " + name;
            return name;
        }
    }
}
