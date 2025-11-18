Set objShell = CreateObject("WScript.Shell")
Set objFSO = CreateObject("Scripting.FileSystemObject")

' Get the directory where this VBS script is located
scriptDir = objFSO.GetParentFolderName(WScript.ScriptFullName)

' Build the full path to the batch file
batPath = objFSO.BuildPath(scriptDir, "Launch-CosmicGUI.bat")

' Run the batch file hidden
objShell.Run "cmd /c """ & batPath & """", 0, False

