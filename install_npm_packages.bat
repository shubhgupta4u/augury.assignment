@echo off
:: Run npm install in multiple relative folders

:: List the directories where npm install should be run
set directories=Services\MachineInfoService\MachineInfoService Services\MachineRepairService\MachineRepairService Services\MachineTelemetryService\MachineTelemetryService


:: Loop through each directory and run npm install
for %%d in (%directories%) do (
    echo Installing npm packages in %%d...
    :: Change to the directory
    cd /d "%~dp0%%d"
    
    :: Run npm install
    npm install
    
    :: Return to the original directory
    cd /d "%~dp0"
)

:: End of script
echo All npm installs completed!
pause
