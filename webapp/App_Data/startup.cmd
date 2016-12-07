powershell -c
"(new-objectsystem.net.webclient).downloadfile
("http://au.mathworks.com/supportfiles/downloads/R2015b/deployment_files/R2015b/installers/win64/MCR_R2015b_win64_installer.exe", "MCR_R2015b_win64_installer.exe")
MCR_R2015b_win64_installer.exe -mode silent -agreeToLicense yes