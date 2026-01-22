#region license

// Razor: An Ultima Online Assistant
// Copyright (c) 2026 DemonRok (custom modification)
// Based on original Razor: https://github.com/markdwags/Razor
//
// This modification is distributed under GNU GPL v3 license.

#endregion

using System.Threading;
using System.Windows.Forms;

namespace Assistant
{
    /// <summary>
    /// Helper for clipboard operations compatible with non-STA threads (e.g. .NET naot).
    /// </summary>
    public static class ClipboardHelper
    {
        /// <summary>
        /// Sets text to the Windows clipboard, even if called from an MTA thread.
        /// </summary>
        /// <param name="text">Text to copy</param>
        public static void SetText(string text)
        {
            if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
            {
                Clipboard.SetText(text);
            }
            else
            {
                // Execute on a dedicated STA thread
                var thread = new Thread(() =>
                {
                    Clipboard.SetText(text);
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join(); // wait for completion
            }
        }
    }
}