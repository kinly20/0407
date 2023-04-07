%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe WCFtestServer.exe
Net Start WCFtestServer
sc config WCFtestServer start= auto
pause