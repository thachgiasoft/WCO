/*
 * Created by SharpDevelop.
 * User: spocke
 * Date: 2007-11-23
 * Time: 13:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace mojoPortal.Web.Editor
{
    public interface ISpellChecker
    {
        string[] CheckWords(string lang, string[] words);
        string[] GetSuggestions(string lang, string word);
    }
}
