/*
 * FCKeditor - The text editor for internet
 * Copyright (C) 2003-2005 Frederico Caldeira Knabben
 * 
 * Licensed under the terms of the GNU Lesser General Public License:
 * 		http://www.opensource.org/licenses/lgpl-license.php
 * 
 * For further information visit:
 * 		http://www.fckeditor.net/
 * 
 * "Support Open Source software. What about a donation today?"
 * 
 * File Name: FCKeditorConfigurations.cs
 * 	Class that holds all editor configurations.
 * 
 * File Authors:
 * 		Frederico Caldeira Knabben (fredck@fckeditor.net)
 */

using System ;
using System.Collections ;
using System.Runtime.InteropServices;
using System.Runtime.Serialization ;
using System.Security;

namespace mojoPortal.Web.Editor
{
	[ Serializable() ]
//#if !NET35
//    [SecuritySafeCritical]
//#endif
    //public class FCKeditorConfigurations : ISerializable
    public class FCKeditorConfigurations 
	{
		private Hashtable _Configs ;

		internal FCKeditorConfigurations()
		{
			_Configs = new Hashtable() ;
		}

		protected FCKeditorConfigurations( SerializationInfo info, StreamingContext context )
		{
			_Configs = (Hashtable)info.GetValue( "ConfigTable", typeof( Hashtable ) ) ;
		}

		public string this[ string configurationName ]
		{
			get
			{
				if ( _Configs.ContainsKey( configurationName ) )
					return (string)_Configs[ configurationName ] ;
				else
					return null ;
			}
			set
			{
				_Configs[ configurationName ] = value ;
			}
		}

		internal string GetHiddenFieldString()
		{
			System.Text.StringBuilder osParams = new System.Text.StringBuilder() ;

			foreach ( DictionaryEntry oEntry in _Configs )
			{
				if ( osParams.Length > 0 )
					osParams.Append( "&amp;" ) ;

				osParams.AppendFormat( "{0}={1}", EncodeConfig( oEntry.Key.ToString() ), EncodeConfig( oEntry.Value.ToString() ) ) ;
			}

			return osParams.ToString() ;
		}
		
		private string EncodeConfig( string valueToEncode )
		{
			string sEncoded = valueToEncode.Replace( "&", "%26" ) ;
			sEncoded = sEncoded.Replace( "=", "%3D" ) ;
			sEncoded = sEncoded.Replace( "\"", "%22" ) ;
			
			return sEncoded ;
		}

		#region ISerializable Members

        [SecurityCritical]
		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue( "ConfigTable", _Configs ) ;
		}

		#endregion
	}
}
