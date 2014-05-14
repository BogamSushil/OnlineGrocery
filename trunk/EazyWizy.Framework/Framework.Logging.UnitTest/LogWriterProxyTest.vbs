Dim logWriterProxy

MsgBox "Starting..."

WScript.Echo "Creating log writer proxy"

Set logWriterProxy = CreateObject("EazyWizy.Framework.Logging.LogWriterProxy.1")
logWriterProxy.Configure("UnitTest")

WScript.Echo "Created log writer proxy"

logWriterProxy.WriteLog1 1, "Test message title", "Test message from VB"

WScript.Echo "Wrote first log"

Dim logRecord

Set logRecord = logWriterProxy.CreateLogRecord(4, "Test message title", "Another test message from VB")
logWriterProxy.WriteLogRecord(logRecord)

WScript.Echo "Wrote second log"

logWriterProxy.Flush()

WScript.Echo "Flushed logs"

Set logRecord = Nothing
Set logWriterProxy = Nothing

WScript.Echo "Done"
