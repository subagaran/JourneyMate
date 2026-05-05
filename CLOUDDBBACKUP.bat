@echo off
setlocal

:: =====================================================
:: DATE TIME
:: =====================================================
for /f %%i in ('powershell -NoProfile -Command "Get-Date -Format yyyy-MM-dd_HH-mm-ss"') do set DATETIME=%%i

:: =====================================================
:: LOCAL ROOT BACKUP FOLDER
:: =====================================================
set ROOT=D:\SQLBackups

:: =====================================================
:: CLIENT 1 SETTINGS
:: =====================================================
set SERVER1=SUBAGARAN\SQL2022
set DATABASE1=HotelDB
set CLIENT1=Client1
set REMOTE1=Gdrive1:Client1

:: =====================================================
:: CLIENT 2 SETTINGS
:: =====================================================
set SERVER2=SUBAGARAN\SQL2019R1
set DATABASE2=HotelDBBR
set CLIENT2=Client2
set REMOTE2=Gdrive2:Client2

:: =====================================================
:: CREATE LOCAL FOLDERS
:: =====================================================
if not exist "%ROOT%\%CLIENT1%" mkdir "%ROOT%\%CLIENT1%"
if not exist "%ROOT%\%CLIENT2%" mkdir "%ROOT%\%CLIENT2%"

:: =====================================================
:: CLIENT 1 BACKUP
:: =====================================================
echo.
echo ==========================================
echo CLIENT 1 BACKUP STARTED
echo ==========================================

set BAK1=%ROOT%\%CLIENT1%\%DATABASE1%_%DATETIME%.bak
set ZIP1=%ROOT%\%CLIENT1%\%DATABASE1%_%DATETIME%.zip

sqlcmd -b -S "%SERVER1%" -E -Q "BACKUP DATABASE [%DATABASE1%] TO DISK='%BAK1%' WITH INIT"

if %ERRORLEVEL% neq 0 (
    echo CLIENT 1 BACKUP FAILED
    goto CLIENT2
)

if not exist "%BAK1%" (
    echo CLIENT 1 BAK FILE NOT FOUND
    goto CLIENT2
)

powershell -Command "Compress-Archive -Path '%BAK1%' -DestinationPath '%ZIP1%' -Force"

if %ERRORLEVEL% neq 0 (
    echo CLIENT 1 ZIP FAILED
    goto CLIENT2
)

del "%BAK1%"

echo CLIENT 1 UPLOADING TO GOOGLE DRIVE...

rclone copy "%ZIP1%" "%REMOTE1%" --progress

if %ERRORLEVEL% neq 0 (
    echo CLIENT 1 GOOGLE DRIVE UPLOAD FAILED
) else (
    echo CLIENT 1 GOOGLE DRIVE UPLOAD SUCCESS
)

:: =====================================================
:: CLIENT 2 BACKUP
:: =====================================================
:CLIENT2

echo.
echo ==========================================
echo CLIENT 2 BACKUP STARTED
echo ==========================================

set BAK2=%ROOT%\%CLIENT2%\%DATABASE2%_%DATETIME%.bak
set ZIP2=%ROOT%\%CLIENT2%\%DATABASE2%_%DATETIME%.zip

sqlcmd -b -S "%SERVER2%" -E -Q "BACKUP DATABASE [%DATABASE2%] TO DISK='%BAK2%' WITH INIT"

if %ERRORLEVEL% neq 0 (
    echo CLIENT 2 BACKUP FAILED
    goto END
)

if not exist "%BAK2%" (
    echo CLIENT 2 BAK FILE NOT FOUND
    goto END
)

powershell -Command "Compress-Archive -Path '%BAK2%' -DestinationPath '%ZIP2%' -Force"

if %ERRORLEVEL% neq 0 (
    echo CLIENT 2 ZIP FAILED
    goto END
)

del "%BAK2%"

echo CLIENT 2 UPLOADING TO GOOGLE DRIVE...

rclone copy "%ZIP2%" "%REMOTE2%" --progress

if %ERRORLEVEL% neq 0 (
    echo CLIENT 2 GOOGLE DRIVE UPLOAD FAILED
) else (
    echo CLIENT 2 GOOGLE DRIVE UPLOAD SUCCESS
)

:: =====================================================
:: FINISH
:: =====================================================
:END

echo.
echo ==========================================
echo ALL BACKUP PROCESSES COMPLETED
echo ==========================================

pause
endlocal