%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe D:\AGV\Debug\SFServer.exe
Net Start SFServer
sc config SFServer start= auto
pause

\