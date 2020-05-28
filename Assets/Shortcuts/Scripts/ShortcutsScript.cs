using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using rnd = UnityEngine.Random;

public class ShortcutsScript : MonoBehaviour
{

    public KMAudio Audio;
    public KMBombInfo Bomb;
    public KMBombModule Module;

    public KMSelectable[] Buttons;
    public TextMesh[] Screens;
    public TextMesh[] ButtonScreens;

    static int moduleIdCounter = 1;
    int moduleId;
    bool moduleSolved;
    int correctAnswer;
    int solvedStages = 0;
    int stages;

    struct Shortcut
    {
        public string Topic;
        public string Command;
        public string Answer;
    }

    List<Shortcut> answers = new List<Shortcut>();

    List<Shortcut> shortcuts = new List<Shortcut>()
    {
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "F1",
            Answer = "Get help on a\nselected command"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "Shift+F1",
            Answer = "Review text formatting"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "F2",
            Answer = "Move text or image"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "Shift+F2",
            Answer = "Copy text"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "F3",
            Answer = "Insert an autotext entry"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "Shift+F3",
            Answer = "Change the case\nof the selected text"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "F4",
            Answer = "Perform last action again"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "Shift+F4",
            Answer = "Perform a Find or\nGo To action again"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "F5",
            Answer = "Displays the Go To\ndialogue box"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "Shift+F5",
            Answer = "Move to a previous revision"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "F6",
            Answer = "Go to the next frame or pane"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "Shift+F6",
            Answer = "Go to the previous frame or pane"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "F7",
            Answer = "Launch the Spell Checker"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "Shift+F7",
            Answer = "Launch the Thesaurus"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "F8",
            Answer = "Extend the current selection"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "Shift+F8",
            Answer = "Shrink the current selection"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "F9",
            Answer = "Refresh"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "Shift+F9",
            Answer = "Switch between a field\ncode and its result"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "F10",
            Answer = "Show KeyTips"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "Shift+F10",
            Answer = "Display a Shortcut Menu"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "F11",
            Answer = "Go to the next field"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "Shift+F11",
            Answer = "Go to the previous field"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "F12",
            Answer = "Open Save As"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "Shift+F12",
            Answer = "Save document"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+A",
            Answer = "Selects all in the\ncurrent document"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+B",
            Answer = "Bold text"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+C",
            Answer = "Copies the item or text"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+D",
            Answer = "Displays the Font dialogue box"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+E",
            Answer = "Switch a paragraph between\ncenter and left alignment"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+F",
            Answer = "Displays the Find\ndialog box to search\nthe current document"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+G",
            Answer = "Displays the Go To dialog box\nto search for a specific location\nin the current document"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+H",
            Answer = "Displays the Replace dialog box"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+I",
            Answer = "Italicize text"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+J",
            Answer = "Switch a paragraph between\njustified and left alignment"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+K",
            Answer = "Create a hyperlink"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+L",
            Answer = "Left align a paragraph"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+M",
            Answer = "Indent a paragraph\nfrom the left"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+N",
            Answer = "Create a new document"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+O",
            Answer = "Opens a new document"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+P",
            Answer = "Prints a document"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+R",
            Answer = "Switch the alignment\nof a paragraph\nbetween left and right"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+S",
            Answer = "Saves a document"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+U",
            Answer = "Underlines text"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+V",
            Answer = "Pastes the copied item or text"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+X",
            Answer = "Cuts the selected item or text"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+Y",
            Answer = "Redo the last action"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+Z",
            Answer = "Undo the last action"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+ENTER",
            Answer = "Insert a page break"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+F2",
            Answer = "Select Print Preview command"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+F4",
            Answer = "Closes the active window"
        },
        new Shortcut
        {
            Topic = "Microsoft Word",
            Command = "CTRL+F6",
            Answer = "Opens the next window\nif multiple are open"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "Windows Key+R",
            Answer = "Opens the Run menu"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "Windows Key+E",
            Answer = "Opens Explorer"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "Alt+Tab",
            Answer = "Switch between open programs"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "Windows Key+Up Arrow",
            Answer = "Maximize current window"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "CTRL+Shift+Esc",
            Answer = "Open Task Manager"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "Windows Key+Break",
            Answer = "Open system properties"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "Windows Key+F",
            Answer = "Open search for\nfiles and folders"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "Windows Key+d",
            Answer = "Hide/display the desktop"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "alt+esc",
            Answer = "Switch between programs in\norder they were opened"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "alt+letter",
            Answer = "Select menu item\nby underlined letter"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "ctrl+esc",
            Answer = "Open Start menu"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "ctrl+f4",
            Answer = "Close active document"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "alt+f4",
            Answer = "Quit active application"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "alt+spacebar",
            Answer = "Open menu for active program"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "ctrl+left arrow",
            Answer = "Move cursor backward one word"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "ctrl+right arrow",
            Answer = "Move cursor forward one word"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "ctrl+up arrow",
            Answer = "Move cursor backward\none paragraph"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "ctrl+down arrow",
            Answer = "Move cursor forward\none paragraph"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "f1",
            Answer = "Open Help menu\nfor active application"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "Windows Key+m",
            Answer = "Minimize all windows"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "shift+Windows Key+m",
            Answer = "Restore windows\nthat were minimized"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "Windows Key+f1",
            Answer = "Open Windows Help\nand Support"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "Windows Key+tab",
            Answer = "Open Task view"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "Windows Key+break",
            Answer = "Open the System\nProperties dialog box"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "hold right shift for 8 seconds",
            Answer = "Switch FilterKeys on and off"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "left alt+left shift+print screen",
            Answer = "Switch High Contrast on and off"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "left alt+left shift+num lock",
            Answer = "Switch Mouse keys on and off"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "press shift five times",
            Answer = "Switch Sticky keys on and off"
        },
        new Shortcut
        {
            Topic = "Windows",
            Command = "hold num lock for 5 seconds",
            Answer = "Switch Toggle keys on and off"
        },
        new Shortcut
        {
            Topic = "Internet Explorer",
            Command = "Alt+left arrow",
            Answer = "Go back to the previous page"
        },
        new Shortcut
        {
            Topic = "Internet Explorer",
            Command = "Alt+Backspace",
            Answer = "Go back to the previous page"
        },
        new Shortcut
        {
            Topic = "Internet Explorer",
            Command = "alt+right arrow",
            Answer = "Go to next page"
        },
        new Shortcut
        {
            Topic = "Internet Explorer",
            Command = "F5",
            Answer = "Refresh page"
        },
        new Shortcut
        {
            Topic = "Internet Explorer",
            Command = "F11",
            Answer = "Toggle between full-screen\nand regular view"
        },
        new Shortcut
        {
            Topic = "Internet Explorer",
            Command = "esc",
            Answer = "Stop downloading a page"
        },
        new Shortcut
        {
            Topic = "Internet Explorer",
            Command = "ctrl++",
            Answer = "Zoom in to page by 10%"
        },
        new Shortcut
        {
            Topic = "Internet Explorer",
            Command = "ctrl+-",
            Answer = "Zoom out of page by 10%"
        },
        new Shortcut
        {
            Topic = "Internet Explorer",
            Command = "ctrl+enter",
            Answer = "Adds www. at the beginning\nand .com to the end\nin the Address bar"
        },
        new Shortcut
        {
            Topic = "Internet Explorer",
            Command = "ctrl+d",
            Answer = "Add the current\nsite to your favorites"
        },
        new Shortcut
        {
            Topic = "Internet Explorer",
            Command = "ctrl+i",
            Answer = "View your favorites"
        },
        new Shortcut
        {
            Topic = "Internet Explorer",
            Command = "ctrl+n",
            Answer = "Open a new window"
        },
        new Shortcut
        {
            Topic = "Internet Explorer",
            Command = "ctrl+p",
            Answer = "Print the current page"
        },
        new Shortcut
        {
            Topic = "Internet Explorer",
            Command = "ctrl+t",
            Answer = "Open a new tab"
        },
        new Shortcut
        {
            Topic = "Internet Explorer",
            Command = "ctrl+f4",
            Answer = "Closes tabs in\nthe background"
        },
        new Shortcut
        {
            Topic = "Internet Explorer",
            Command = "ctrl+tab",
            Answer = "Switch between tabs"
        },
        new Shortcut
        {
            Topic = "Internet Explorer",
            Command = "spacebar",
            Answer = "Click the notification bar"
        },
        new Shortcut
        {
            Topic = "Internet Explorer",
            Command = "shift+spacebar",
            Answer = "Move up one page"
        },
        new Shortcut
        {
            Topic = "Internet Explorer",
            Command = "alt+down arrow",
            Answer = "Move a selected item\ndown the favorites list"
        },
        new Shortcut
        {
            Topic = "File Explorer",
            Command = "end",
            Answer = "Display bottom\nof current window"
        },
        new Shortcut
        {
            Topic = "File Explorer",
            Command = "home",
            Answer = "Display top of current window"
        },
        new Shortcut
        {
            Topic = "File Explorer",
            Command = "left arrow",
            Answer = "Collapse the current selections\nor select a parent folder"
        },
        new Shortcut
        {
            Topic = "File Explorer",
            Command = "right arrow",
            Answer = "Display the current selection\nor select the first subfolder"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "devmgmt.msc",
            Answer = "device manager"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "msinfo32",
            Answer = "system information"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "cleanmgr",
            Answer = "Disk Cleanup"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "ntbackup",
            Answer = "Backup or restore Wizard"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "mmc",
            Answer = "Microsoft Management Console"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "excel",
            Answer = "Microsoft Excel"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "msaccess",
            Answer = "Microsoft Access"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "powerpnt",
            Answer = "Microsoft PowerPoint"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "winword",
            Answer = "Microsoft Word"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "frontpg",
            Answer = "Microsoft FrontPage"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "notepad",
            Answer = "Notepad"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "wordpad",
            Answer = "WordPad"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "calc",
            Answer = "Calculator"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "msmsgs",
            Answer = "Windows Messenger"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "mspaint",
            Answer = "Microsoft Paint"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "wmplayer",
            Answer = "Windows Media Player"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "rstrui",
            Answer = "System restore"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "control",
            Answer = "Opens the Control Panel"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "control printers",
            Answer = "Opens the printers dialog box"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "cmd",
            Answer = "Command Prompt"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "iexplore",
            Answer = "Internet Explorer"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "compmgmt.msc",
            Answer = "Computer Management"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "dhcpmgmt.msc",
            Answer = "DHCP Management"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "dnsmgmt.msc",
            Answer = "DNS Management"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "services.msc",
            Answer = "Services"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "eventvwr",
            Answer = "Event Viewer"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "dsa.msc",
            Answer = "Active Directory\nUsers and Computers"
        },
        new Shortcut
        {
            Topic = "Windows System",
            Command = "dssite.msc",
            Answer = "Active Directory\nSites and Services"
        },
        new Shortcut
        {
            Topic = "Firefox (Windows)",
            Command = "Alt+Left arrow",
            Answer = "Go back a page"
        },
        new Shortcut
        {
            Topic = "Firefox (Windows)",
            Command = "Alt+Right arrow",
            Answer = "Go forward a page"
        },
        new Shortcut
        {
            Topic = "Firefox (Windows)",
            Command = "F5",
            Answer = "Reload current page"
        },
        new Shortcut
        {
            Topic = "Firefox (Windows)",
            Command = "F11",
            Answer = "Toggle between full screen\nand regular screen"
        },
        new Shortcut
        {
            Topic = "Firefox (Windows)",
            Command = "esc",
            Answer = "Stop page from loading"
        },
        new Shortcut
        {
            Topic = "Firefox (Windows)",
            Command = "ctrl+enter",
            Answer = "Complete a .com address"
        },
        new Shortcut
        {
            Topic = "Firefox (Windows)",
            Command = "shift+enter",
            Answer = "Complete a .net address"
        },
        new Shortcut
        {
            Topic = "Firefox (Windows)",
            Command = "ctrl+shift+enter",
            Answer = "Complete a .org address"
        },
        new Shortcut
        {
            Topic = "Firefox (Windows)",
            Command = "ctrl+shift+delete",
            Answer = "Clear recent history"
        },
        new Shortcut
        {
            Topic = "Firefox (Windows)",
            Command = "ctrl+d",
            Answer = "Add a bookmark\nfor the current page"
        },
        new Shortcut
        {
            Topic = "Firefox (Windows)",
            Command = "ctrl+shift+b",
            Answer = "Display available bookmarks"
        },
        new Shortcut
        {
            Topic = "Firefox (Windows)",
            Command = "ctrl+j",
            Answer = "Display the download window"
        },
        new Shortcut
        {
            Topic = "Firefox (Windows)",
            Command = "ctrl+n",
            Answer = "Open a new browser window"
        },
        new Shortcut
        {
            Topic = "Firefox (Windows)",
            Command = "ctrl+p",
            Answer = "Print current page"
        },
        new Shortcut
        {
            Topic = "Firefox (Windows)",
            Command = "ctrl+t",
            Answer = "Opens a new tab"
        },
        new Shortcut
        {
            Topic = "Firefox (Windows)",
            Command = "ctrl+w",
            Answer = "Close the tab"
        },
        new Shortcut
        {
            Topic = "Firefox (Windows)",
            Command = "ctrl+shift+w",
            Answer = "Close window"
        },
        new Shortcut
        {
            Topic = "Firefox (Windows)",
            Command = "ctrl+shift+n",
            Answer = "Undo the close of a window"
        },
        new Shortcut
        {
            Topic = "Firefox (Windows)",
            Command = "ctrl+shift+t",
            Answer = "Undo the close of a tab"
        },
        new Shortcut
        {
            Topic = "Firefox (Windows)",
            Command = "ctrl+tab",
            Answer = "Moves through each\nof the open tabs"
        },
        new Shortcut
        {
            Topic = "Firefox (Windows)",
            Command = "end",
            Answer = "Go to bottom of page"
        },
        new Shortcut
        {
            Topic = "Firefox (Windows)",
            Command = "home",
            Answer = "Go to top of page"
        },
        new Shortcut
        {
            Topic = "Firefox (Mac OS)",
            Command = "Command+Left Arrow",
            Answer = "Go back a page"
        },
        new Shortcut
        {
            Topic = "Firefox (Mac OS)",
            Command = "Command+Right Arrow",
            Answer = "Go forward a page"
        },
        new Shortcut
        {
            Topic = "Firefox (Mac OS)",
            Command = "F5",
            Answer = "Reload current page"
        },
        new Shortcut
        {
            Topic = "Firefox (Mac OS)",
            Command = "command+shift+f",
            Answer = "Toggle between full screen\nand regular screen"
        },
        new Shortcut
        {
            Topic = "Firefox (Mac OS)",
            Command = "esc",
            Answer = "Stop page from loading"
        },
        new Shortcut
        {
            Topic = "Firefox (Mac OS)",
            Command = "command+return",
            Answer = "Complete a .com address"
        },
        new Shortcut
        {
            Topic = "Firefox (Mac OS)",
            Command = "shift+return",
            Answer = "Complete a .net address"
        },
        new Shortcut
        {
            Topic = "Firefox (Mac OS)",
            Command = "command+shift+return",
            Answer = "Complete a .org address"
        },
        new Shortcut
        {
            Topic = "Firefox (Mac OS)",
            Command = "command+shift+delete",
            Answer = "Clear recent history"
        },
        new Shortcut
        {
            Topic = "Firefox (Mac OS)",
            Command = "command+d",
            Answer = "Add a bookmark\nfor the current page"
        },
        new Shortcut
        {
            Topic = "Firefox (Mac OS)",
            Command = "command+shift+b",
            Answer = "Display available bookmarks"
        },
        new Shortcut
        {
            Topic = "Firefox (Mac OS)",
            Command = "command+j",
            Answer = "Display the download window"
        },
        new Shortcut
        {
            Topic = "Firefox (Mac OS)",
            Command = "command+n",
            Answer = "Open a new browser window"
        },
        new Shortcut
        {
            Topic = "Firefox (Mac OS)",
            Command = "command+p",
            Answer = "Print current page"
        },
        new Shortcut
        {
            Topic = "Firefox (Mac OS)",
            Command = "command+t",
            Answer = "Opens a new tab"
        },
        new Shortcut
        {
            Topic = "Firefox (Mac OS)",
            Command = "command+w",
            Answer = "Close the tab"
        },
        new Shortcut
        {
            Topic = "Firefox (Mac OS)",
            Command = "command+shift+w",
            Answer = "Close window"
        },
        new Shortcut
        {
            Topic = "Firefox (Mac OS)",
            Command = "command+shift+n",
            Answer = "Undo the close of a window"
        },
        new Shortcut
        {
            Topic = "Firefox (Mac OS)",
            Command = "command+shift+t",
            Answer = "Undo the close of a tab"
        },
        new Shortcut
        {
            Topic = "Firefox (Mac OS)",
            Command = "ctrl+tab",
            Answer = "Moves through each\nof the open tabs"
        },
        new Shortcut
        {
            Topic = "Firefox (Mac OS)",
            Command = "command+down arrow",
            Answer = "Go to bottom of page"
        },
        new Shortcut
        {
            Topic = "Firefox (Mac OS)",
            Command = "command+up arrow",
            Answer = "Go to top of page"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "f2",
            Answer = "Edit the active cell"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "f5",
            Answer = "Displays the Go To box"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "f7",
            Answer = "Open the Spelling dialogue\nbox to check a selected range"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "f11",
            Answer = "Create a chart of data\nin the current range\nin a separate sheet"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "alt+shift+f1",
            Answer = "Insert a new worksheet"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "shift+f3",
            Answer = "Opens Insert\nFunction dialogue box"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "shift+f5",
            Answer = "Opens the Find and\nReplace dialogue box"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+shift+:",
            Answer = "Enter the current time"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+;",
            Answer = "Enter the current date"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+a",
            Answer = "Select all content\nin the worksheet"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+b",
            Answer = "Bold highlighted selection"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+i",
            Answer = "Italicize highlighted selection"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+k",
            Answer = "Open the Insert\nhyperlink dialogue box"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+u",
            Answer = "Underline highlighted selection"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+5",
            Answer = "Apply strikethrough formatting"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+p",
            Answer = "Brings up the print dialog box"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+z",
            Answer = "Undo"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+f9",
            Answer = "Minimize a workbook\nwindow to an icon"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+f10",
            Answer = "Maximize a selected\nworkbook window"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+f6",
            Answer = "Switch to the next workbook\nwindow when multiple are open"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+page up",
            Answer = "Move to previous\nsheet in a workbook"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+page down",
            Answer = "Move to next\nsheet in a workbook"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+tab",
            Answer = "Switch to next\ntab in dialogue box"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+'",
            Answer = "Insert the value of the\nabove cell into the\ncell currently selected"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+shift+!",
            Answer = "Apply the Number format"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+shift+$",
            Answer = "Apply the Currency format"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+shift+#",
            Answer = "Apply the Date format"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+shift+%",
            Answer = "Apply the Percentage format"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+shift+^",
            Answer = "Apply the Exponential format"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+shift+@",
            Answer = "Apply the Time format"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+arrow key",
            Answer = "Move to the edge\nof the current data region\nin a worksheet"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "ctrl+space",
            Answer = "Select an entire column\nin a worksheet"
        },
        new Shortcut
        {
            Topic = "Microsoft Excel",
            Command = "shift+space",
            Answer = "Select an entire row\nin a worksheet"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "Command+x",
            Answer = "Cut selected text and copy it"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "Command+c",
            Answer = "Copy selected text"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "Command+v",
            Answer = "Paste copied text"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "Command+z",
            Answer = "Undo previous command"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "Command+a",
            Answer = "Select all items"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "Command+f",
            Answer = "Open Find window\nto search text"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "Command+h",
            Answer = "Hide windows of\nthe front app"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "Command+n",
            Answer = "Open a new document\nor window"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "Command+o",
            Answer = "Open a selected item"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "Command+p",
            Answer = "Print current document"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "Command+s",
            Answer = "Save current document"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "Command+w",
            Answer = "Close front window"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "Command+q",
            Answer = "Quit the app"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "Command+m",
            Answer = "Minimize the front\nwindow to the Dock"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "Command+space",
            Answer = "Open Spotlight search field"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "Command+tab",
            Answer = "Switch between open apps"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "Command+b",
            Answer = "Bold selected text"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "Command+i",
            Answer = "Italicize selected text"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "Command+u",
            Answer = "Underline selected text"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "Command+;",
            Answer = "Find misspelled\nwords in document"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "option+Command+esc",
            Answer = "Choose an app to force quit"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "shift+Command+~",
            Answer = "Switch between open windows"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "shift+Command+3",
            Answer = "Take a screenshot"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "fn+up arrow",
            Answer = "Scroll up one page"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "fn+down arrow",
            Answer = "Scroll down one page"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "fn+left arrow",
            Answer = "Scroll to beginning\nof document"
        },
        new Shortcut
        {
            Topic = "Mac OS",
            Command = "fn+right arrow",
            Answer = "Scroll to end of document"
        },
        new Shortcut
        {
            Topic = "Finder",
            Command = "shift+Command+f",
            Answer = "Open All My Files window"
        },
        new Shortcut
        {
            Topic = "Finder",
            Command = "shift+Command+k",
            Answer = "Open Network window"
        },
        new Shortcut
        {
            Topic = "Finder",
            Command = "option+Command+l",
            Answer = "Open Downloads folder"
        },
        new Shortcut
        {
            Topic = "Finder",
            Command = "shift+Command+o",
            Answer = "Open documents folder"
        },
        new Shortcut
        {
            Topic = "Finder",
            Command = "shift+Command+u",
            Answer = "Open Utilities folder"
        },
        new Shortcut
        {
            Topic = "Finder",
            Command = "option+Command+d",
            Answer = "Show or hide the Dock"
        },
        new Shortcut
        {
            Topic = "Finder",
            Command = "shift+Command+n",
            Answer = "Create a new folder"
        },
        new Shortcut
        {
            Topic = "Finder",
            Command = "Command+delete",
            Answer = "Move selected item\nto the Trash"
        },
        new Shortcut
        {
            Topic = "Finder",
            Command = "shift+Command+delete",
            Answer = "Empty Trash"
        },
        new Shortcut
        {
            Topic = "Chrome (Windows)",
            Command = "ctrl+n",
            Answer = "Open new window"
        },
        new Shortcut
        {
            Topic = "Chrome (Windows)",
            Command = "ctrl+t",
            Answer = "Open new tab"
        },
        new Shortcut
        {
            Topic = "Chrome (Windows)",
            Command = "ctrl+shift+t",
            Answer = "Reopen the last closed tab"
        },
        new Shortcut
        {
            Topic = "Chrome (Windows)",
            Command = "ctrl+tab",
            Answer = "Move to next tab"
        },
        new Shortcut
        {
            Topic = "Chrome (Windows)",
            Command = "ctrl+shift+tab",
            Answer = "Move to previous tab"
        },
        new Shortcut
        {
            Topic = "Chrome (Windows)",
            Command = "alt+left arrow",
            Answer = "Open previous page\nin browsing history"
        },
        new Shortcut
        {
            Topic = "Chrome (Windows)",
            Command = "alt+right arrow",
            Answer = "Open next page\nin browsing history"
        },
        new Shortcut
        {
            Topic = "Chrome (Windows)",
            Command = "ctrl+w",
            Answer = "Close current tab"
        },
        new Shortcut
        {
            Topic = "Chrome (Windows)",
            Command = "alt+f4",
            Answer = "Close current window"
        },
        new Shortcut
        {
            Topic = "Chrome (Windows)",
            Command = "ctrl+shift+o",
            Answer = "Open Bookmarks Manager"
        },
        new Shortcut
        {
            Topic = "Chrome (Windows)",
            Command = "ctrl+h",
            Answer = "Open History page"
        },
        new Shortcut
        {
            Topic = "Chrome (Windows)",
            Command = "ctrl+j",
            Answer = "Open Downloads page"
        },
        new Shortcut
        {
            Topic = "Chrome (Windows)",
            Command = "ctrl+f",
            Answer = "Open Find Bar"
        },
        new Shortcut
        {
            Topic = "Chrome (Windows)",
            Command = "ctrl+p",
            Answer = "Print current page"
        },
        new Shortcut
        {
            Topic = "Chrome (Windows)",
            Command = "ctrl+s",
            Answer = "Save current page"
        },
        new Shortcut
        {
            Topic = "Chrome (Windows)",
            Command = "f5",
            Answer = "Reload current page"
        },
        new Shortcut
        {
            Topic = "Chrome (Windows)",
            Command = "ctrl+d",
            Answer = "Save current page\nas a bookmark"
        },
        new Shortcut
        {
            Topic = "Chrome (Windows)",
            Command = "f11",
            Answer = "Toggle full-screen mode"
        },
        new Shortcut
        {
            Topic = "Chrome (Mac OS)",
            Command = "Command+n",
            Answer = "Open new window"
        },
        new Shortcut
        {
            Topic = "Chrome (Mac OS)",
            Command = "Command+t",
            Answer = "Open new tab"
        },
        new Shortcut
        {
            Topic = "Chrome (Mac OS)",
            Command = "Command+shift+t",
            Answer = "Reopen the last closed tab"
        },
        new Shortcut
        {
            Topic = "Chrome (Mac OS)",
            Command = "Command+option+right arrow",
            Answer = "Move to next tab"
        },
        new Shortcut
        {
            Topic = "Chrome (Mac OS)",
            Command = "Command+option+left arrow",
            Answer = "Move to previous tab"
        },
        new Shortcut
        {
            Topic = "Chrome (Mac OS)",
            Command = "Command+[",
            Answer = "Open previous page\nin browsing history"
        },
        new Shortcut
        {
            Topic = "Chrome (Mac OS)",
            Command = "Command+]",
            Answer = "Open next page\nin browsing history"
        },
        new Shortcut
        {
            Topic = "Chrome (Mac OS)",
            Command = "Command+w",
            Answer = "Close current tab"
        },
        new Shortcut
        {
            Topic = "Chrome (Mac OS)",
            Command = "Command+shift+w",
            Answer = "Close current window"
        },
        new Shortcut
        {
            Topic = "Chrome (Mac OS)",
            Command = "Command+option+b",
            Answer = "Open Bookmarks Manager"
        },
        new Shortcut
        {
            Topic = "Chrome (Mac OS)",
            Command = "Command+y",
            Answer = "Open History page"
        },
        new Shortcut
        {
            Topic = "Chrome (Mac OS)",
            Command = "Command+shift+j",
            Answer = "Open Downloads page"
        },
        new Shortcut
        {
            Topic = "Chrome (Mac OS)",
            Command = "Command+f",
            Answer = "Open Find Bar"
        },
        new Shortcut
        {
            Topic = "Chrome (Mac OS)",
            Command = "Command+p",
            Answer = "Print current page"
        },
        new Shortcut
        {
            Topic = "Chrome (Mac OS)",
            Command = "Command+s",
            Answer = "Save current page"
        },
        new Shortcut
        {
            Topic = "Chrome (Mac OS)",
            Command = "Command+r",
            Answer = "Reload current page"
        },
        new Shortcut
        {
            Topic = "Chrome (Mac OS)",
            Command = "Command+d",
            Answer = "Save current page\nas a bookmark"
        },
        new Shortcut
        {
            Topic = "Chrome (Mac OS)",
            Command = "Command+ctrl+f",
            Answer = "Toggle full-screen mode"
        }
    };

    KMSelectable.OnInteractHandler ButtonPress(int btn)
    {
        return delegate
        {
            Buttons[btn].AddInteractionPunch();
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, Buttons[btn].transform);
            if (moduleSolved)
                return false;
            if (!answers[correctAnswer].Answer.ToUpperInvariant().Equals(ButtonScreens[btn].text.ToUpperInvariant()))
            {
                Module.HandleStrike();
                return false;
            }
            solvedStages++;
            if (solvedStages == stages)
            {
                moduleSolved = true;
                Module.HandlePass();
                Screens[0].text = "GOOD JOB";
                Screens[0].color = new Color32(0, 255, 0, 255);
                Screens[1].text = "YOU DID IT :D";
                Screens[1].color = new Color32(0, 255, 0, 255);
                ButtonScreens[0].text = "WELL DONE";
                ButtonScreens[1].text = "TERRIFIC";
                ButtonScreens[2].text = "MARVELOUS";
                ButtonScreens[3].text = "WONDERFUL";
                return false;
            }
            Generate();
            return false;
        };
    }

    void Start()
    {
        moduleId = moduleIdCounter++;
        stages = rnd.Range(10, 16);
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].OnInteract += ButtonPress(i);
        }
        Generate();
    }

    void Generate()
    {
        tryAgain:
        answers.Clear();
        var ixs = Enumerable.Range(0, shortcuts.Count()).ToList();
        ixs.Shuffle();
        for (int i = 0; i < 4; i++)
        {
            answers.Add(shortcuts[ixs[i]]);
            if (i < 4)
            {
                if (answers.Any(sh => sh.Answer.Equals(shortcuts[ixs[i + 1]].Answer)))
                {
                    Debug.LogFormat(@"<Shortcuts #{0}> Found a duplicate answer, generating new.", moduleId);
                    goto tryAgain;
                }
            }
        }
        correctAnswer = rnd.Range(0, answers.Count());
        Screens[0].text = answers[correctAnswer].Topic.ToUpperInvariant();
        Screens[1].text = answers[correctAnswer].Command.ToUpperInvariant();
        for (int i = 0; i < ButtonScreens.Length; i++)
        {
            ButtonScreens[i].text = answers[i].Answer.ToUpperInvariant();
        }
        Debug.LogFormat(@"[Shortcuts #{0}] Stage: {1} - Topic: {2}, Shortcut: {3}, Answer: {4}.", moduleId, solvedStages + 1, answers[correctAnswer].Topic.ToUpperInvariant(), answers[correctAnswer].Command.ToUpperInvariant(), answers[correctAnswer].Answer.ToUpperInvariant());
    }

#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} 1234 [press the corresponding button, 1=TL, 2=TR, 3=BL, 4=BR]";
#pragma warning restore 414
    IEnumerator ProcessTwitchCommand(string command)
    {
        Match m;
        if (moduleSolved)
        {
            yield return "sendtochaterror The module is already solved.";
            yield break;
        }
        else if ((m = Regex.Match(command, @"^\s*[1234]\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)).Success)
        {
            yield return null;
            var btn = int.Parse(m.Groups[0].Value);
            Buttons[btn-1].OnInteract();
            yield break;
        }
        else
        {
            yield return "sendtochaterror Invalid Command.";
            yield break;
        }
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        while (solvedStages != stages)
        {
            Buttons[correctAnswer].OnInteract();
            yield return new WaitForSeconds(.1f);
        }
    }
}
