Project Setup
=============

This project contains both a **React client app** and a **.NET Core server app**. The following instructions guide you on how to run both apps using the provided PowerShell script (run.ps1).

Prerequisites
-------------

Before running the script, ensure you have the following installed:

*   **.NET SDK** (for the server app)
    
*   **Node.js and npm** (for the React client app)
    

Make sure you also have **PowerShell** available on your machine.

How to Run the Script
---------------------

### 1\. Open PowerShell in the Root Folder

Navigate to the root folder of the project where run.ps1 is located. You can do this by opening PowerShell and changing the directory:

`   path/TMO Technical Task   `

### 2\. Run the run.ps1 Script

Once you’re in the root folder, run the run.ps1 script to build and start both apps:

`   ./run.ps1   `

### 3\. Allow Script Execution (If Required)

If you get an error related to script execution, you may need to enable script execution in PowerShell. To do this, run the following command to allow scripts to run temporarily for this session:

`   Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass   `

This will allow the script to execute without modifying your system's global script execution policy.

What Happens When You Run the Script
------------------------------------

*   **.NET Core Server App**:
    
    *   The script will build and run the ASP.NET Core server app.
        
    *   It will use the launch profile you have set up in the launchSettings.json file (default is "http").
        
*   **React Client App**:
    
    *   The script will build and start the React development server.
        
    *   If it's the first time running, it will install the necessary dependencies using npm install.
        

Stopping the Apps
-----------------

*   To stop the React development server, press Ctrl + C in the terminal where the React app is running.
    
*   To stop the .NET Core server, press Ctrl + C in the terminal where the .NET app is running.
    

Notes
-----

*   The run.ps1 script will start both apps in parallel, so both the React and .NET Core apps will be running simultaneously.