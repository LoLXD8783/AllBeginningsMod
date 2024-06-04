@echo off

set OUT_ERR=err.log
for /r %%i in ("*.fx") do (
	for %%j in ("%%~pi.") do (
		powershell -Command "$content = Get-Content -Encoding UTF8 '%%i'; $Utf8NoBomEncoding = New-Object System.Text.UTF8Encoding $False; [System.IO.File]::WriteAllLines('"Compiled\%%~nxj\%%~ni.lol"', $content, $Utf8NoBomEncoding)"
		
		echo Compiling %%~nxj::%%~ni...
		fxc.exe /Gec /T "fx_2_0" /Fo "Compiled\%%~nxj\%%~ni.fxc" "Compiled\%%~nxj\%%~ni.lol" >nul 2>%OUT_ERR%
		del "Compiled\%%~nxj\%%~ni.lol"
		
		type %OUT_ERR%
    )
)

del %OUT_ERR%
pause