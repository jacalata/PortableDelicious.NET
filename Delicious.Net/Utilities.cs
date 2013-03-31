#region Copyright (c) 2006-2008, Nate Zobrist
/*
 * Copyright (c) 2006-2008, Nate Zobrist
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 *
 * Redistributions of source code must retain the above copyright
 *     notice, this list of conditions and the following disclaimer.
 * Redistributions in binary form must reproduce the above
 *     copyright notice, this list of conditions and the following
 *     disclaimer in the documentation and/or other materials provided with
 *     the distribution.
 * Neither the name of "Nate Zobrist" nor the names of its
 *     contributors may be used to endorse or promote products derived from
 *     this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 * "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
 * A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
 * OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
 * LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion Copyright (c) 2006-2008, Nate Zobrist

using System;
using System.Globalization;
using System.Collections; //list
using System.Xml.Linq;

namespace Delicious
{
	internal static class Utilities
	{
        internal static string AddParameter (string baseUrl, string parameter, string value)
        {
            // this will get our url's properly formatted
            if (parameter == Constants.UrlParameter.Url)
            {
                try
                {
                    value = new Uri (value).ToString();
                }
                catch (System.FormatException e)  //UriFormatException is not supported in the portable libraries
                {
                    FormatException ufe = new FormatException("Delicious.Net was unable to parse the url \"" + value + "\".\n\n" + e);
                    throw (ufe);
                }
            }
            value = Uri.EscapeDataString(value); //HttpUtility.UrlEncode  is not supported in the portable libraries

            // insert the '?' if needed
            int qLocation = baseUrl.LastIndexOf ('?');
            if (qLocation < 0)
            {
                baseUrl += "?";
                qLocation = baseUrl.Length - 1;
            }

            if (baseUrl.Length > qLocation + 1)
                baseUrl += "&";

            baseUrl += parameter + "=" + value;
            return baseUrl;
        }


	    internal static DateTime ConvertFromDeliciousTime (string time)
		{
			return DateTime.Parse (time, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.AdjustToUniversal);
		}


		internal static string ConvertToDeliciousTime (DateTime time)
		{
			return time.ToUniversalTime().ToString();
		}


        // TODO don't really know what this is doing, should look at the sample xml
		internal static string ParseForResultCode (XDocument xmlDoc) //XElement xmlElement)
		{
            return xmlDoc == null? string.Empty : xmlDoc.ToString();
            /*
            XElement xmlElement = xmlDoc.FirstNode();
			if (xmlElement == null)
				return String.Empty;
            System.Collections.Generic.List<XAttribute> attributes = new System.Collections.Generic.List<XAttribute>(xmlElement.Attributes());
            if (attributes.Count > 0)
            {
				XAttribute xmlAttribute = attributes[0];
				if (xmlAttribute == null)
					return String.Empty;

				return xmlAttribute.Value;
			}
			else if (xmlElement.Value.Length > 0)
			{
				return xmlElement.Value;
			}

            return String.Empty;
             * */
		}
	}
}