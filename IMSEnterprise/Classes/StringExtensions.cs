using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data;

namespace IMSEnterprise
{
    public static class StringsExtension
    {
        /// <summary>
        /// Implement's VB's Like operator logic.
        /// </summary>
        public static bool Like(this string s, string pattern)
        {
            if (s != null && pattern != null) // values cant be null
            {
                if (s.Contains(' ') && !pattern.Contains(' ')) // check if there is a white space in the current value s
                {
                    List<String> sStrings;
                    sStrings = s.Split(' ').ToList<String>(); // make the List to contain the values between whitespaces

                    foreach(String sString in sStrings) // go through all values between whitespace and check if there is a match with the pattern
                    {
                        Match test = Regex.Match(sString, "^" + pattern, RegexOptions.IgnoreCase);
                        if (test.Success)
                            return true;
                    }

                    return false;
                }
                else
                {
                    Match test = Regex.Match(s, "^" + pattern, RegexOptions.IgnoreCase); // check if there is a match with the pattern
                    if (test.Success)
                        return true;
                    else
                    {
                        if (s == "")
                            return false;

                        int count = 0;
                        for(int index = 0; index < s.Length; index++)
                        {
                            if (pattern.Length > index)
                            {
                                if (s[index] == pattern[index])
                                    count++;
                                else
                                    count--;
                            }
                            else
                                count--;
                        }
                        if (1 <= count)
                            return true;
                        else
                        {
                            if (s.Contains(pattern))
                                return true;
                            else 
                                return false;
                        }
                          
                    }
                }
            }
            else
                return false;
        }

        public static bool IsEmpty(this string s)
        {
            if (s == null || s == "")
                return true;
            else
                return false;
        }
    }
}
