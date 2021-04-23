using System;
using System.Linq;
using System.Reflection;


namespace HUDElementsLib.Libraries.Helpers.TModLoader.Mods {
	/// <summary>
	/// Assorted static "helper" functions for alleviating tedious "boilerplate" code.
	/// </summary>
	public class ModBoilerplateHelpers {
		/// <summary>
		/// Allows using a class to bind its public static methods as `Mod.Call(...)` bindings (complete with parameter
		/// validations). Meant to be called within `Mod.Call(...)`.
		/// </summary>
		/// <param name="apiClassType">`Type` of class to use for defining API bindings.</param>
		/// <param name="args"></param>
		/// <returns>API binding call return result. Should be piped out from `Mod.Call(...)` in turn.</returns>
		public static object HandleModCall( Type apiClassType, params object[] args ) {
			if( args == null || args.Length == 0 ) { throw new Exception( "Undefined call." ); }

			string callType = args[0] as string;
			if( callType == null ) {
				HUDElementsLibMod.Instance.Logger.Warn( "Invalid call binding: " + args[0] );
				return null;
			}

			MethodInfo methodInfo = apiClassType.GetMethod( callType );
			if( methodInfo == null ) {
				var argsList = args.Select( a => a.GetType().Name + ": " + a == null ? "null" : a.ToString() );
				string argsListStr = string.Join( ", ", argsList );

				HUDElementsLibMod.Instance.Logger.Warn( apiClassType.Name+" has no Call binding for " + callType + " with args: "+argsListStr );
				return null;
			}

			var newArgs = new object[args.Length - 1];
			Array.Copy( args, 1, newArgs, 0, args.Length - 1 );

			try {
				return methodInfo.Invoke( null, newArgs );
			} catch( Exception e ) {
				throw new Exception( apiClassType.Name+" failed to execute Call binding " +callType, e );
			}
		}
	}
}
