%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\installutil.exe E:\Code\SFServer\SFServer\bin\Debug\SFServer.exe
Net Start SFServer
sc config SFServer start= auto
pause

\