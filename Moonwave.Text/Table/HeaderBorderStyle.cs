// (c) 2019 Moonwave Interactive. All rights reserved under MIT License.

namespace Moonwave.Text
{
    /// <summary>
    /// Specifies constants that define the border style of the header.
    /// </summary>
    public enum HeaderBorderStyle
    {
        /// <summary>
        /// Disables viewing of the headers border.
        /// </summary>
        None = 0,

        /// <summary>
        /// Displays a single line divider under the header.
        /// </summary>
        Single = 1,

        /// <summary>
        /// Displays a single line divider for each columns title.
        /// </summary>
        ColumnTitle = 2,

        /// <summary>
        /// Displays a single line divider for each column.
        /// </summary>
        Column = 4,

        /// <summary>
        /// Displays a single line divider on the top and bottom of the header.
        /// </summary>
        Dual = 8
    }
}