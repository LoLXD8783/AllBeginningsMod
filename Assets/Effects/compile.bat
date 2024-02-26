@echo off

for /r %%i in ("*.fx") do (
	for %%j in ("%%~pi.") do (
		powershell -Command "$content = Get-Content -Encoding UTF8 '%%i'; $Utf8NoBomEncoding = New-Object System.Text.UTF8Encoding $False; [System.IO.File]::WriteAllLines('"Compiled\%%~nxj\%%~ni.lol"', $content, $Utf8NoBomEncoding)"
		fxc.exe /Gec /T "fx_2_0" /Fo "Compiled\%%~nxj\%%~ni.fxc" "Compiled\%%~nxj\%%~ni.lol"
		del "Compiled\%%~nxj\%%~ni.lol"
    )
)

pause