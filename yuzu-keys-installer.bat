@echo off
title Yuzu Keys Installer
cls

set errcount=0
set uperr=0
set inserr=0
set keyerr=0
set saerr=0
:update
echo Checking for updates...
del  /q version.txt >nul 2>&1
powershell.exe (new-object System.Net.WebClient).DownloadFile('https://raw.githubusercontent.com/aaronliu0130/s/master/version.txt', 'version.txt')
rem If this is actually accepted in the pull request, change "aaronliu0130" to "jackscobey" of course.
for /f "tokens=* delims=" %%v in (version.txt) do set "version=%%v"
if "%version%" NEQ "v1.11.21.LC5112682" (
    echo An Update has been found. Will restart after update. Updating...
    powershell.exe (new-object System.Net.WebClient).DownloadFile('https://raw.githubusercontent.com/jackscobey/s/master/update.bat', 'update.bat')
    if %errorlevel% == 0 (
    	update.bat
	exit
    ) else (
    	echo Skipping due to error during updating, %errorlevel%
	set uperr=1
	set errcount=1
    )
)

:A
set /p menu="Do you want to install Yuzu? (Y/N/System Archives/Cancel): "
if %menu%==Y goto Yes
if %menu%==y goto Yes
if %menu%==N goto No
if %menu%==n goto No
if %menu%==C goto C
if %menu%==c goto C
if %menu%==System Archives goto SA
if %menu%==SA goto SA
if %menu%==system archives goto SA
if %menu%==sa goto SA
else (
	cls
	echo Invalid input. Please try again...
	goto :A
)
cls
set /p pause="Press any key to continue!... "

:Yes
cls
echo.
echo This will download the Yuzu installer, and run it. Allow it to install.
echo.
IF EXIST yuzu_installer.exe (
	echo Removing old version...
	del yuzu_installer.exe
)
powershell.exe (new-object System.Net.WebClient).DownloadFile('https://github.com/yuzu-emu/liftinstall/releases/download/1.6/yuzu_install.exe', 'yuzu_install.exe')
if %errorlevel% == 0 (
	echo We will now install yuzu, then delete the installer.
	yuzu_install.exe
) else (
	echo Error encountered during downloading, %errorlevel%
	set errcount=%errcount%+1
	set inserr=1
)
echo Cleaning up...
del yuzu_install.exe
del yuzu_install.log
echo Done.
pause

:No
cls
echo Okay, that means it's time to download the keys.
echo.
echo We will now download the keys.
cd %appdata%\yuzu
IF EXIST keys\prod.keys (
	echo Deleting old keys...
	rmdir /Q /S keys
)
IF EXIST keys\title.keys (
	echo Deleting old keys...
	rmdir /Q /S keys
)
mkdir keys
cd keys
echo Writing new keys to %appdata%\yuzu\keys
powershell.exe (new-object System.Net.WebClient).DownloadFile('https://raw.githubusercontent.com/jackscobey/s/master/prod.keys', 'prod.keys')
powershell.exe (new-object System.Net.WebClient).DownloadFile('https://raw.githubusercontent.com/jackscobey/s/master/title.keys', 'title.keys')
if %errorlevel% == 0 (
	echo Successfully downloaded title.keys, prod.keys
) else (
	echo Error during key writing, %errorlevel%
	set keyerr=1
	set errcount=%errcount%+1
)
pause

:SA
echo.
echo We will now download the System Archives. This may take a while, it will be blank, but let it run.
rem What does it will be blank mean? This is a comment, and it can be removed.
cd %appdata%\yuzu\nand\system
powershell.exe (new-object System.Net.WebClient).DownloadFile('https://www.dropbox.com/s/0gwmpgus9t4q1dm/System_Archives.zip?dl=1', 'System_Archives.zip')
if %errorlevel% == 0(
	echo unzipping System Archives.
	powershell.exe (new-object System.Net.WebClient).DownloadFile('https://www.dropbox.com/s/wcdhkat6oz0i3tm/unzip.exe?dl=1', 'unzip.exe')
	echo Writing System Archives to %appdata%\yuzu\keys\nand\system
	unzip.exe System_Archives.zip
) else(
	echo Error during System Archives downloading, %errorlevel%.
	set saerr=1
	set errcount=%errcount%+1
)
echo Cleaning up...
del System_Archives.zip
del unzip.exe
pause

:C
cls
echo The script is now finished.
echo.
echo Thanks to /u/yuzu_pirate, /u/Azurime, and /u/bbb651 for their contributions to /r/YuzuP I R A C Y.
echo.
echo This program made by /u/Hipeopeo.
echo.
echo Thanks to the yuzu devs for making Yuzu!
pause
