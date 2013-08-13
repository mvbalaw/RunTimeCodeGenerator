//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using System;
using System.Collections.Generic;

namespace RunTimeCodeGenerator.Logging
{
	public class Logger : ILogger
	{
		private readonly List<IListener> _listeners;

		public Logger(string sourceName)
		{
			SourceName = sourceName;

			_listeners = new List<IListener>
			             {
				             new ConsoleListener()
			             };
		}

		public List<IListener> Listeners
		{
			get { return _listeners; }
		}

		public string SourceName { get; private set; }

		public void LogInformation(string format, params object[] args)
		{
			Log(SourceName, MessageType.Information, format, args);
		}

		public void LogWarning(string format, params object[] args)
		{
			Log(SourceName, MessageType.Warning, format, args);
		}

		public void LogError(string format, params object[] args)
		{
			Log(SourceName, MessageType.Error, format, args);
		}

		private void Log(string sourceName, MessageType messageType, string format, object[] args)
		{
			foreach (var listener in _listeners)
			{
				listener.Writeline(messageType, String.Format("[{0}] {1}", sourceName, String.Format(format, args)));
			}
		}
	}
}