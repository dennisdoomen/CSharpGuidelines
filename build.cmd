@ECHO OFF
powershell -ExecutionPolicy Bypass -NoProfile -File "%~dp0build.ps1" %*
