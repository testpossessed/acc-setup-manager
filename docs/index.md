## ACC Setup Manager

Assetto Corsa Competitzione (ACC) Setup Manager allows you to view existing setups created with ACC in a desktop application.  It currently supports the following features

+ Automatic versioning of setups files
+ Side by side comparison of setup versions
+ Add and edit notes for setup files and versions
+ Select from multiple themes
+ Restore any version of a setup to ACC

The following features are in the works and will be released as soon as they are available

+ Highlight differences in the comparison setup panel (the one on the right when Compare To is enabled)
+ Editing setup files (this may take some time as it is not a trivial task)

The application is deployed using Microsoft ClickOnce technology, which means you only need to install it once.  Each time you start the application it checks for a newer version and prompts you to install the update.  You have the option to skip the new version for now, then you will be prompted again the next time the application starts.

You can download and install ACC Setup Manager from [here](https://raw.githubusercontent.com/testpossessed/acc-setup-manager/master/docs/installer/setup.exe)

### Automatic versioning

When it starts ACC Setup Manager scans the ACC Setups folder and copies each setup to it's own working folders as a Master.  It checks the timestamp on the original and if there is no matching version with the same timestamp assumes you have made changes and creates a new version.

While running ACC Setup Manager watches the ACC Setups folder for new files, changes and deletions for existing files.  When any of these events occurs the ACC Setup Manager replicates the change in it's Masters working folder and creates a version for that change.  Version files are never deleted so you can always restore an original from a version in the future.

Version files have the same name as the original with a timestamp suffix so you can match them to session files from your preferred telemetry application such as MoTec.

### Side by side comparison

By default ACC Setup Manager shows a single tabbed view of a setup file selected in the tree panel.  In the toolbar above the setup panel a list will be populated with all versions of all setups for the same vehicle and track.  To perform a side by side comparison of the selected setup with any of it's matches check the box labelled **Compare To**.  This will load the first version in the list for comparison in a similar tabbed view to the right of the main setup.  To compare a different version simply select it from the list in the toolbar.

### Add and edit notes

To help you track changes and their outcome each tabbed view provides a section at the bottom where you can capture up to four attributes.

+ A representative (probably fastest) lap time before the change was made
+ The reason for the change (understeer, oversteer etc.)
+ A representative lap time after the change was made
+ Any other comments about the change or setup.

Don't forget to click the Save button in the notes panel.

Notes are kept separate from the setup file so there is no chance they will affect the setup when applied in ACC.

Enjoy ACC Setup Manager and if you have any questions, issues, feature requests or just want to provide feedback you can do so via the [GitHub Issues Page](https://github.com/testpossessed/acc-setup-manager/issues)
