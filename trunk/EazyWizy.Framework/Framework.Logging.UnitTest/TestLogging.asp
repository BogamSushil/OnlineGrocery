<html>
<body>
<%
	Response.Write("Starting log...")
	
	Dim logWriterProxy

	Set logWriterProxy = CreateObject("EazyWizy.Framework.Logging.LogWriterProxy.1")
    logWriterProxy.Configure("UnitTest")

	logWriterProxy.WriteLog1 1, "Test message title", "Test message from VB"

    Dim logRecord

    Set logRecord = logWriterProxy.CreateLogRecord(1, "Test message title", "Another test message from VB")
    logWriterProxy.WriteLogRecord(logRecord)

    Set logRecord = Nothing
	Set logWriterProxy = Nothing
	
	Response.Write("Finished log.")
%>
</body>
</html>