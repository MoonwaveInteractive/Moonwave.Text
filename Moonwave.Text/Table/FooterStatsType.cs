// (c) 2019 Moonwave Interactive. All rights reserved under MIT License.

namespace Moonwave.Text
{
    /// <summary>
    /// Specifies constants that define the type of statistics to be displayed
    /// in the footer.
    /// </summary>
    public enum FooterStatsType
    {
        /// <summary>
        /// Disables the viewing of statistics in the footer.
        /// </summary>
        None = 0,

        /// <summary>
        /// Displays the amount of entries in the footer.
        /// </summary>
        Basic = 1,

        /// <summary>
        /// Displays the amount of entries, as well for how long the 
        /// overall pocess took to complete in milliseconds, in the footer.
        /// </summary>
        Advanced = 2
    }
}