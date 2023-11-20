cd ../
for /f %%f in ('dir /b "./*.fx"') do "Compiler/fxcompiler.exe" %%f
pause